using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using NUnit.Core;
using NUnit.Framework;
using TaHoGen;
using TaHoGen.CodeDom;
using TaHoGen.Generators;
using TaHoGen.Targets;
using TaHoGen.Tokens;
#if REGIONS_ENABLED
using TaHoGen.Parsers;
#endif
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace Tests
{
	#region Utility Classes
	internal class Interop
	{
		[DllImport("kernel32.dll")]
		public extern static short QueryPerformanceCounter(ref long x);
		[DllImport("kernel32.dll")]
		public extern static short QueryPerformanceFrequency(ref long x);
		[DllImport("kernel32.dll")]
		public extern static long GetTickCount();
	}
	
	
	public class Timer
	{
		private string timerName = "";
		private long startTime = 0, endTime = 0;
		public Timer()
		{

		}
		public Timer(string sName)
		{
			timerName = sName;
		}
		public void Start()
		{
			Interop.QueryPerformanceCounter(ref startTime);
		}
		public void Stop()
		{
			Interop.QueryPerformanceCounter(ref endTime);
			long frequency = 0;

			Interop.QueryPerformanceFrequency(ref frequency);

			double dblElapsed = ((endTime - startTime) * 1.0 / frequency) * 1000;
			if (timerName.Length > 0)
			{
#if !SHOW_TIMING
				Console.Write("'" + timerName + "' Complete. ");
#endif
			}
	
			
			string sUnit = dblElapsed > 1000 ? " seconds" : "ms";

			dblElapsed = dblElapsed > 1000 ? dblElapsed / 1000 : dblElapsed;
			string sElapsed = dblElapsed.ToString("######.00");
#if !SHOW_TIMING
			Console.WriteLine("Time Elapsed: " + sElapsed + sUnit);
#endif
			//Console.WriteLine("Time Elapsed: " + sElapsed + sUnit);
		}
		public void Reset()
		{
			startTime = 0;
			endTime = 0;
		}
	}
	#endregion

	public class CompilerCallback : ICompilerCallback
	{
		#region ICompilerCallback Members

		public void EndCompile(CompilerArgs args)
		{
			foreach(CompilerError error in args.Errors)
			{
				Console.WriteLine(error);
			}
		}

		public void BeginCompile(CompilerArgs args)
		{
			Console.WriteLine(args.CompiledCode);
		}

		#endregion
	}
	[TestFixture]
	public class ParserTests
	{	
		Parser parser;
		[SetUp]
		public void Init()
		{
			parser = new Parser();
		}

		[TearDown]
		public void Term()
		{
			parser = null;
		}
		[Test]
		public void ReadTest()
		{
			StreamReader reader = new StreamReader(@"h:\templates\insteadoftriggers.cst");
			string text = reader.ReadToEnd();

			Token[] tokens = parser.Parse(text);
			Assert.IsNotNull(tokens);
			Assert.IsTrue(tokens.Length > 0);
			foreach(Token token in tokens)
			{
				Console.WriteLine(token.ToString());
			}
		}
		[Test]
		public void ClassGenerationWithParserTest()
		{
			
			SourceCodeGenerator sourceGenerator = new SourceCodeGenerator();

			StreamReader reader = new StreamReader(@"h:\templates\subtemplate.tgt");
			string text = reader.ReadToEnd();

			Token[] tokens = parser.Parse(text);
			Assert.IsNotNull(tokens);
			Assert.IsTrue(tokens.Length > 0);
			sourceGenerator.Generate(tokens);

			//CSharpCodeProvider provider = new CSharpCodeProvider();
			VBCodeProvider provider = new VBCodeProvider();
			ICodeGenerator generator = provider.CreateGenerator();

			CodeGeneratorOptions options = new CodeGeneratorOptions();
			options.BracingStyle = "C";
			StringWriter writer = new StringWriter();

			generator.GenerateCodeFromCompileUnit(sourceGenerator.GetCompileUnit(), writer, options);

			Console.WriteLine(writer.ToString());
		}
	}
	[TestFixture]
	public class CodeDomTests
	{	private CodeCompileUnit compileUnit = null;
		[SetUp]
		public void Init()
		{
			compileUnit = new CodeCompileUnit();
		}

		[TearDown]
		public void Term()
		{
			compileUnit = null;
		}
		[Test]
		public void SimpleClassGenerationTest()
		{
			CodeTypeDeclaration myClass = new CodeTypeDeclaration("MyClass");
			myClass.IsClass = true;

			CodeMemberProperty property = new CodeMemberProperty();
			property.Name = "Test";
			property.Type = new CodeTypeReference(typeof(string));
			
			myClass.Members.Add(property);
			CSharpCodeProvider provider = new CSharpCodeProvider();

			ICodeGenerator generator = provider.CreateGenerator();

			CodeGeneratorOptions options = new CodeGeneratorOptions();
			options.BracingStyle = "C";
			StringWriter writer = new StringWriter();

			generator.GenerateCodeFromType(myClass, writer, options);

			string result = writer.ToString();

			Assert.IsTrue(result.Length > 0);
			Console.WriteLine(result);
		}
		
	}
#if REGIONS_ENABLED
	[TestFixture]
	public class RegionParserTests
	{
	#region Setup / TearDown
		[SetUp]
		public void Init()
		{
			
		}

		[TearDown]
		public void Term()
		{

		}
	#endregion
        [Test]
		public void ParserTest()
		{
			StreamReader reader = new StreamReader(@"h:\regions.txt");
			string text = reader.ReadToEnd();
			reader.Close();

			RegionParser parser = new RegionParser();
			Token[] tokens = parser.Parse(text);
			Assert.IsNotNull(tokens);

			Assert.IsTrue(tokens.Length > 0);
		}
		[Test]
		public void RegionWriteTest()
		{
			StreamReader reader = new StreamReader(@"h:\regions.txt");
			//string text = reader.ReadToEnd();
			

			SimpleTextGenerator generator = new SimpleTextGenerator();
			generator.Text = "Test";

			StringWriter writer = new StringWriter();
			MergeTarget target = new MergeTarget("Region1", reader, writer, new RegionParser());

			target.Attach(generator);
			target.Write();
			reader.Close();

			Console.WriteLine(writer.ToString());


		}
	}
#endif
	[TestFixture]
	public class CompilerTests
	{
		[SetUp]
		public void Init()
		{
			
		}

		[TearDown]
		public void Term()
		{
			
		}
		[Test]
		public void LanguageDetectionTest()
		{
			StreamReader reader = new StreamReader(@"h:\templates\csvector.cst");
			string text = reader.ReadToEnd();

			Timer compileTimer = new Timer();
			compileTimer.Start();
			Assembly result = TemplateCompiler.Compile(text, false);
			compileTimer.Stop();
			Assert.IsNotNull(result);
			
		}
		[Test]
		public void ImportTest()
		{
			StreamReader reader = new StreamReader(@"h:\templates\collectiongen\test.tgt");
			string text = reader.ReadToEnd();
			reader.Close();

			Assembly result = TemplateCompiler.Compile(text, "", false, new CompilerCallback());
			Assert.IsNotNull(result);
		}
		[Test]
		public void SubTemplateCompilationTest()
		{
			StreamReader reader = new StreamReader(@"h:\templates\csvector.cst");
			string text = reader.ReadToEnd();

			Assembly result = TemplateCompiler.Compile(text, false);

			Type templateType = null;
			foreach(Type currentType in result.GetTypes())
			{
				if (!currentType.IsClass)
					continue;

				templateType = currentType;
				break;
				
			}

			File.Delete(@"h:\templates\test.txt");
			ITextGenerator generator = Activator.CreateInstance(templateType) as ITextGenerator;
			Assert.IsNotNull(generator);

			StringWriter writer = new StringWriter();

			generator.Generate(writer);

			Console.WriteLine(writer.ToString());
			Assert.IsTrue(writer.ToString().Length > 0);
			Assert.IsNotNull(result);

			FileTarget target = new FileTarget(@"h:\templates\test.txt", FileMode.Create);

			target.Attach(generator);

			target.Write();

			Assert.IsTrue(File.Exists(@"h:\templates\test.txt"));
		}
	}
}
