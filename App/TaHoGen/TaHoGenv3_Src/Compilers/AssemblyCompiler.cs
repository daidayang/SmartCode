using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

using Microsoft.VisualBasic;
using Microsoft.CSharp;
namespace TaHoGen
{
	[Serializable]
	internal struct CompilerCacheEntry
	{
		public CompilerCacheEntry(string hash, Assembly compiledAssembly, bool executable, DateTime Expiration)
		{
			Hash = hash;
			CompiledAssembly = compiledAssembly;
			ExpirationDate = Expiration;
			Executable = executable;
		}
		public bool Executable;
		public string Hash;
		public Assembly CompiledAssembly;
		public DateTime ExpirationDate;
	}
	// TODO: Replace this with a generic version when The .NET Framework v2.0 comes out
	internal class CompilerCache
	{
		private static Hashtable _entries;
		private static readonly string cacheFileName = AppDomain.CurrentDomain.BaseDirectory + "compilercache.dat";
		static CompilerCache()
		{
			LoadEntries();
		}
		~CompilerCache()
		{
			SaveEntries();
		}
		public bool Contains(string hash)
		{
#if CACHING_ENABLED
			return _entries.Contains(hash);
#else
			return false;
#endif
		}
		public void Store(CompilerCacheEntry entry)
		{
			_entries[entry.Hash] = entry;
		}
		public CompilerCacheEntry Retrieve(string hash)
		{
			return (CompilerCacheEntry) _entries[hash];
		}
		private static void LoadEntries()
		{
			FileStream cacheFile = new FileStream(cacheFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			
			BinaryFormatter formatter = new BinaryFormatter();
			try
			{
				_entries = (Hashtable) formatter.Deserialize(cacheFile);
			}
			catch
			{
				_entries = new Hashtable();
			}

			_entries = Hashtable.Synchronized(_entries);
			cacheFile.Close();
		}
		private static void SaveEntries()
		{
			// Remove any expired entries
			foreach(object key in _entries.Keys)
			{
				string hash = key as string;
				CompilerCacheEntry entry = (CompilerCacheEntry) _entries[key];

				
				if (entry.ExpirationDate < DateTime.Now)
				{
					// Remove the entry from the cache
					_entries.Remove(key);
				}

			}
			try
			{
				FileStream cacheFile = new FileStream(cacheFileName, FileMode.Create, FileAccess.ReadWrite);

				File.SetAttributes(cacheFileName, FileAttributes.Hidden);
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(cacheFile, _entries);
				cacheFile.Close();
			}
			catch(Exception)
			{
				
			}
			finally
			{
				
			}
		}
	}
	public sealed class AssemblyCompiler
	{
		private static readonly CompilerCache _cache = new CompilerCache();
		private AssemblyCompiler() {}

		public static Assembly Compile(CodeCompileUnit compileUnit, CodeDomProvider provider)
		{
			return Compile(compileUnit, provider, false);
		}
		public static Assembly Compile(CodeCompileUnit compileUnit, CodeDomProvider provider, bool addDebugSymbols)
		{
			return Compile(compileUnit, "", provider, addDebugSymbols);
		}
		public static Assembly Compile(CodeCompileUnit compileUnit, string outputFileName, CodeDomProvider provider, bool addDebugSymbols)
		{
			return Compile(compileUnit, outputFileName, provider, addDebugSymbols, null);
		}
		public static Assembly Compile(CodeCompileUnit compileUnit, string outputFileName, CodeDomProvider provider, bool addDebugSymbols, ICompilerCallback compilerCallback)
		{
			return Compile(compileUnit, outputFileName, provider, addDebugSymbols, compilerCallback);
		}
		public static Assembly Compile(CodeCompileUnit compileUnit, string outputFileName, CodeDomProvider provider, StringCollection ReferencedAssemblies, bool addDebugSymbols, ICompilerCallback compilerCallback)
		{
			StringWriter writer = new StringWriter();
			ICodeGenerator codeGenerator = provider.CreateGenerator();
			string sourceText = string.Empty;

			if (codeGenerator != null)
			{
				#region Code Generation Options
				CodeGeneratorOptions codeGenOptions = new CodeGeneratorOptions();

				// Change this option if you want the hanging 
				// curly brace style of output in the compiled
				// template source code (e.g. Java's
				// programming style)
				codeGenOptions.BracingStyle = "C";
				codeGenOptions.BlankLinesBetweenMembers = false;
				codeGenerator.GenerateCodeFromCompileUnit(compileUnit, writer, codeGenOptions);
				#endregion

                //foreach (string assemblyName in compileUnit.ReferencedAssemblies)
                //{
                //    ReferencedAssemblies.Add(assemblyName);
                //}

				sourceText = writer.ToString();
			}
			return Compile(sourceText, outputFileName, provider, ReferencedAssemblies, addDebugSymbols, compilerCallback); 
		}
		public static Assembly Compile(string text, CodeDomProvider provider)
		{
			return Compile(text, provider, false);
		}
		public static Assembly Compile(string text, CodeDomProvider provider, bool addDebugSymbols)
		{
			return Compile(text, "", provider, addDebugSymbols);
		}
		public static Assembly Compile(string text, string outputFileName, CodeDomProvider provider, bool addDebugSymbols)
		{
			return Compile(text, outputFileName, provider, addDebugSymbols, null);
		}
		public static Assembly Compile(string text, string outputFileName, CodeDomProvider provider, bool addDebugSymbols, ICompilerCallback compilerCallback)
		{
			return Compile(text, outputFileName, provider, null, addDebugSymbols, compilerCallback);
		}
		public static Assembly Compile(string text, string outputFileName, CodeDomProvider provider, StringCollection ReferencedAssemblies, bool addDebugSymbols, ICompilerCallback compilerCallback)
		{
			return Compile(text, outputFileName, provider, ReferencedAssemblies, addDebugSymbols, false, compilerCallback);
		}
		public static Assembly Compile(string text, string outputFileName, CodeDomProvider provider, StringCollection ReferencedAssemblies, bool addDebugSymbols, bool generateExecutable, ICompilerCallback compilerCallback)
		{
            string tmpDir = SmartCode.Template.TemplateBase.TemplatesBaseDirectory + "\\TMP";
            string tempFile = tmpDir + "\\" + Guid.NewGuid().ToString() + ".txt";
            DirectoryInfo dir = new DirectoryInfo(tmpDir);
            if (!dir.Exists)
            {
                dir.Create();
            }
            StreamWriter writer = new StreamWriter(tempFile);
            writer.Write(text);
            writer.Close();
            
			if (provider == null)
				return null;


            #region BeginCompile Callback Notification
            CompilerArgs args = new CompilerArgs(text, null);
            if (compilerCallback != null)
                compilerCallback.BeginCompile(args);
            #endregion

			// Reuse the results from a previous compilation if possible
			string sourceHash = Hash.Calculate(text);
			if (_cache.Contains(sourceHash))
			{
				CompilerCacheEntry entry = _cache.Retrieve(sourceHash);
                
                return entry.CompiledAssembly;

			}

			// Ensure that compiling is possible with this provider
			ICodeCompiler compiler = provider.CreateCompiler();
			if (compiler == null)
			{
				string msg = string.Format("This language isn't can't be compiled from CodeDom. (CodeDomProvider = {0})", provider.GetType().Name);
				throw new CompilerException(msg);
			}

			#region Compiler Options
			bool generateInMemory = outputFileName.Length == 0 ? true : false;

			CompilerParameters compilerParams = new CompilerParameters();
			compilerParams.GenerateExecutable = generateExecutable;
			compilerParams.GenerateInMemory = generateInMemory;			
			compilerParams.IncludeDebugInformation = addDebugSymbols;
			compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
			compilerParams.ReferencedAssemblies.Add("System.dll");
			
			// Add any additonal referenced assemblies
			if (ReferencedAssemblies != null)
			{
				foreach(string assembly in ReferencedAssemblies)
				{
					compilerParams.ReferencedAssemblies.Add(assembly);
				}
			}
			string options = string.Format("/target:{0}", (generateExecutable ? "exe" : " library"));
			compilerParams.CompilerOptions += options;

			if (generateInMemory == false)
				compilerParams.OutputAssembly = outputFileName;

			// Tweak the output; Note: The optimize+ switch only works for C# and VB compilers
			if (provider.GetType() == typeof(VBCodeProvider) || 
				provider.GetType() == typeof(CSharpCodeProvider))
				compilerParams.CompilerOptions = " /optimize+";
			#endregion



			CompilerResults results = compiler.CompileAssemblyFromSource(compilerParams, text);

			#region EndCompile Callback Notification
			if (compilerCallback != null)
			{
				args.Errors.AddRange(results.Errors);
				compilerCallback.EndCompile(args);
			}
			#endregion
            if (results.Errors.Count == 0)
            {
                // Cache the result
                CompilerCacheEntry newEntry = new CompilerCacheEntry(sourceHash, results.CompiledAssembly, generateExecutable, DateTime.Now.AddDays(14));
                _cache.Store(newEntry);
                return results.CompiledAssembly;
            }
            else
            {
                string error = "\n\n\n\n" + "LOOK THE SOURCE FILE CREATED ON " + tempFile + "\n\n\n\n";
                foreach (CompilerError errors in results.Errors)
                {
                    error += String.Format("{0} on line : {1} " + "\n", errors.ErrorText, errors.Line);
                }
                Debug.Write (error);
                throw new Exception(error);
            }

		}
		
	}
}
