using System;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text;
using TaHoGen.CodeDom;
using TaHoGen.Generators;
using TaHoGen.Tokens;
using Microsoft.JScript;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using SmartCode.Template;
namespace TaHoGen
{
	public interface ICompilerCallback
	{
		void BeginCompile(CompilerArgs args);
		void EndCompile(CompilerArgs args);
	}
	public class CompilerArgs 
	{
		private string _source;
		private CompilerErrorCollection _errors = new CompilerErrorCollection();
		public CompilerArgs(string source, CompilerErrorCollection errors) 
		{
			_source = source;

			if (errors != null)
				_errors.AddRange(errors);
		}
		public string CompiledCode
		{
			get { return _source; }
		}
		public CompilerErrorCollection Errors
		{
			get { return _errors; }
		}
	}
	
	public sealed class TemplateCompiler
	{
		private TemplateCompiler() {}
		public static Assembly Compile(string text)
		{
			return Compile(text, false);
		}
		public static Assembly Compile(string text, bool addDebugSymbols)
		{
			return Compile(text, "", addDebugSymbols);
		}
		public static Assembly Compile(string text, string outputFileName, bool addDebugSymbols)
		{
			return Compile(text, outputFileName, addDebugSymbols, null);
		}
		public static Assembly Compile(string text, string outputFileName, bool addDebugSymbols, ICompilerCallback compilerCallback)
		{
            return Compile(text, outputFileName, addDebugSymbols, compilerCallback, null);
        }

        public static Assembly Compile(
            string text, 
            string outputFileName, 
            bool addDebugSymbols, 
            ICompilerCallback compilerCallback,
            params string[] referencedAssemblies)
        {
            // First, we need to extract the tokens from the text
            IParser parser = ParserFactory.CreateParser();
            Token[] tokens = parser.Parse(text);

            // Next, we're going to convert the tokens into a codedom graph
            SourceCodeGenerator sourceGen = new SourceCodeGenerator();
            sourceGen.Generate(tokens);

            // TODO: Group the tokens by language

            // Determine which .NET language was used in the markup text
            string language = LanguageHelper.GetLanguage(tokens);

            CodeDomProvider provider = Providers.GetProvider(language);

            StringCollection ReferencedAssemblies = new StringCollection();

            //string tahogenAssemblyLocation = typeof(TextGenerator).Assembly.Location;
            ReferencedAssemblies.Add(typeof(TextGenerator).Assembly.Location);
            ReferencedAssemblies.Add(typeof(SmartCode.Model.TableSchema).Assembly.Location);
            ReferencedAssemblies.Add(typeof(SmartCode.Template.TemplateBase).Assembly.Location);
            ReferencedAssemblies.Add("System.Data.dll");
            ReferencedAssemblies.Add("System.Design.dll");
            ReferencedAssemblies.Add("System.Drawing.dll");
            ReferencedAssemblies.Add("System.Xml.dll");
            ReferencedAssemblies.Add("System.Windows.Forms.dll");


            #region Referenced Assemblies
            if (referencedAssemblies != null)
            {
                foreach (string s in referencedAssemblies)
                {
                    if (! ReferencedAssemblies.Contains(s))
                        ReferencedAssemblies.Add(s);
                }
            }

            foreach (string s in sourceGen.GetCompileUnit().ReferencedAssemblies)
            {
                if (!ReferencedAssemblies.Contains(s))
                {
                    ReferencedAssemblies.Add(s);
                }
            }
            #endregion

            #region CodeBehind Assemblies

            // Precompile the codebehind assemblies if necessary
            string[] codeBehindFiles = CodeBehindHelper.GetCodeBehindFiles(tokens);
            foreach (string fileName in codeBehindFiles)
            {
                // Verify that the file does exist
                string fullFileName = SmartCode.Template.TemplateBase.TemplatesBaseDirectory + fileName;
                if (!File.Exists(fullFileName))
                    throw new FileNotFoundException("Unable to find codebehind file.", fullFileName);

                FileInfo fi = new FileInfo(fullFileName);
               
                // Read its contents
                StreamReader reader = new StreamReader(fullFileName);
                string source = reader.ReadToEnd();
                reader.Close();

                // Find the provider that can compile this particular type of file
                string extension = Path.GetExtension(fi.Name).Replace(".", "");
                
                CodeDomProvider codeBehindProvider = Providers.GetProviderByExtension(extension);
                Debug.Assert(codeBehindProvider != null);
                if (codeBehindProvider == null)
                    throw new CompilerException(string.Format("Unable to find a CodeDom provider for file extension '{0}'", extension));

                Assembly codeBehindAssembly =
                    AssemblyCompiler.Compile(source,  fi.Name  + ".dll", codeBehindProvider, ReferencedAssemblies, addDebugSymbols, compilerCallback);

                if (codeBehindAssembly == null)
                    throw new CompilerException(string.Format("Unable to compile '{0}'", fullFileName));

                string assemblyPath = codeBehindAssembly.Location;
                ReferencedAssemblies.Add(assemblyPath);
            }
            #endregion
            #region Additional @Compiled Templates
            string[] additionalAssemblies = IncludeHelper.GetAdditionalCompiledAssemblies(tokens, compilerCallback);
            foreach (string additionalAssembly in additionalAssemblies)
            {
                ReferencedAssemblies.Add(additionalAssembly);
            }
            #endregion
            return AssemblyCompiler.Compile(sourceGen.GetCompileUnit(), outputFileName, provider, ReferencedAssemblies, addDebugSymbols, compilerCallback);
        }

		public static Assembly Compile(string[] fileList)
		{
			return Compile(fileList, "");
		}
		public static Assembly Compile(string[] fileList, string outputFileName)
		{
			return Compile(fileList, "", false);
		}
		public static Assembly Compile(string[] fileList, string outputFileName, bool addDebugSymbols)
		{
			return Compile(fileList, outputFileName, addDebugSymbols, null);
		}
		public static Assembly Compile(string[] fileList, string outputFileName, bool addDebugSymbols, ICompilerCallback compilerCallback)
		{
			// We're going to take all of the files, and clump
			// them together into one block of text
			StringBuilder builder = new StringBuilder();
			foreach(string file in fileList)
			{
				StreamReader reader = new StreamReader(file);
				reader.Close();

				builder.Append(reader.ReadToEnd());
			}

			// Compile everything as one big template file
			return Compile(builder.ToString(), outputFileName, addDebugSymbols, compilerCallback);
		}


        public static Assembly Compile(params StreamReader[] readers)
        {
            return Compile("", readers);
        }
        public static Assembly Compile(string outputFileName, params StreamReader[] readers)
        {
            return Compile("", false, readers);
        }
        public static Assembly Compile(string outputFileName, bool addDebugSymbols, params StreamReader[] readers)
        {
            return Compile(outputFileName, addDebugSymbols, null, readers);
        }
        public static Assembly Compile(string outputFileName, bool addDebugSymbols, ICompilerCallback compilerCallback, params StreamReader[] readers)
        {
            StringBuilder builder = new StringBuilder();
            foreach (StreamReader reader in readers)
            {
                builder.Append(reader.ReadToEnd());
                reader.Close();
            }

            // Compile everything as one big template file
            return Compile(builder.ToString(), outputFileName, addDebugSymbols, compilerCallback);
        }
	}
	public sealed class Providers
	{
		private static readonly Hashtable _providers = new Hashtable();
		private static readonly Hashtable _providerExtensionMap = new Hashtable();
		private Providers() {}
	
		static Providers()
		{
			_providers["C#"] = new CSharpCodeProvider();
			_providers["CSharp"] = new CSharpCodeProvider();
			_providers["CS"] = new CSharpCodeProvider();
			_providers["JScript"] = new JScriptCodeProvider();
			_providers["JS"] = new JScriptCodeProvider();
			_providers["VB.NET"] = new VBCodeProvider();
			_providers["VB"] = new VBCodeProvider();
			_providers["VisualBasic"] = new VBCodeProvider();

			RegisterExtension("cs", new CSharpCodeProvider());
			RegisterExtension("vb", new VBCodeProvider());
			RegisterExtension("js", new JScriptCodeProvider());
		}
		public static void RegisterExtension(string extension, CodeDomProvider provider)
		{
			if (_providerExtensionMap.Contains(extension.ToLower()))
				return;

			_providerExtensionMap[extension.ToLower()] = provider;
		}
		public static void Register(string language, CodeDomProvider provider)
		{
			if (_providers.Contains(language))
				return;

			_providers[language] = provider;
		}
		public static CodeDomProvider GetProviderByExtension(string extension)
		{
			if (_providerExtensionMap.Contains(extension.ToLower()))
			{
				return _providerExtensionMap[extension.ToLower()] as CodeDomProvider;
			}
			throw new CompilerException(string.Format("Unable to find a provider for filename extension '{0}'",extension));
		}
		public static CodeDomProvider GetProvider(string language)
		{
			// Find the provider that matches the given language.
			// Note: We have to manually do a string comparison
			// for each key because the hashtable does 
			// a case-sensitive comparison and this search
			// must be case-insensitive

			foreach(object key in _providers.Keys)
			{
				string currentLanguage = key as string; 
				if (StringHelper.AreEqual(currentLanguage, language))
					return _providers[key] as CodeDomProvider;
			}

			throw new ProviderNotFoundException(language);
		}
	}
	internal class LanguageHelper : IVisitor
	{
		private string _language;
		
		[VisitorMethod]
		public void Visit(Directive directive)
		{
			// We're only looking for a CodeTemplate directive with the language
			// attribute on it
			if (!StringHelper.AreEqual(directive.Name, "CodeTemplate"))
				return;

			if (!directive.HasAttribute("Language"))
				return;

			_language = directive["Language"];
		}
		private string Language
		{
			get { return _language; }
		}
		public static string GetLanguage(Token[] tokens)
		{

			LanguageHelper helper = new LanguageHelper();
			Visitable.Accept(helper, tokens);

			return helper.Language;
		}
	}
	internal class IncludeHelper : IVisitor
	{
		private StringCollection _additionalTemplateFiles = new StringCollection();
		private StringCollection _compiledAssemblies = new StringCollection();
		private ICompilerCallback _callback;
		private IncludeHelper(ICompilerCallback callback)
		{
			_callback = callback;
		}
		[VisitorMethod]
		public void Visit(Directive directive)
		{
			// We're looking for the @compile template="..." outputfilename="" directive
			if (!StringHelper.AreEqual(directive.Name, "Compile"))
				return;

			if (!directive.HasAttribute("template") || !directive.HasAttribute("outputfilename"))
				return;

			string filename = directive["Template"];
			string outputFileName = directive["outputfilename"];

			StreamReader reader = new StreamReader(TemplateBase.TemplatesBaseDirectory + filename);
			string text = reader.ReadToEnd();
			reader.Close();

			Assembly compiledAssembly = TemplateCompiler.Compile(text, outputFileName, true, _callback);
			if (compiledAssembly == null)
				return;
			
			// If it compiles ok then we need to keep track of where the file is
			// located so that we can reference it
			string location = compiledAssembly.Location;
			Debug.Assert(File.Exists(location));
			
			if (!File.Exists(location))
				throw new FileNotFoundException(string.Format("Unable to find the compiled assembly for {0}: '{1}'", filename, location),location);

			_compiledAssemblies.Add(location);

		}
		public static string[] GetAdditionalCompiledAssemblies(Token[] tokens, ICompilerCallback callback)
		{
			IncludeHelper helper = new IncludeHelper(callback);
			foreach(Token token in tokens)
			{
				Visitable v = new Visitable(token);
				v.Accept(helper);
			}

			string[] result = new string[helper._compiledAssemblies.Count];
			helper._compiledAssemblies.CopyTo(result, 0);

			return result;
		}
	}


	internal class CodeBehindHelper : IVisitor
	{
		private StringCollection _codeBehindFiles = new StringCollection();
		private CodeBehindHelper() {}

        [VisitorMethod]
		private void Visit(Directive directive)
		{
			// We're looking for the @Assembly Src="..." directive
            if (StringHelper.AreEqual(directive.Name, "Assembly") && directive.HasAttribute("Src"))
            {
                this._codeBehindFiles.Add(directive["Src"]);
            }
		}
		public string[] CodeBehindFiles
		{
			get 
			{
				string[] fileList = new string[_codeBehindFiles.Count];

				if (fileList.Length > 0)
					_codeBehindFiles.CopyTo(fileList, 0);

				return fileList;
			}
		}
		public static string[] GetCodeBehindFiles(Token[] tokens)
		{
			CodeBehindHelper helper = new CodeBehindHelper();
			Visitable.Accept(helper, tokens);

			return helper.CodeBehindFiles;
		}
	}
}
