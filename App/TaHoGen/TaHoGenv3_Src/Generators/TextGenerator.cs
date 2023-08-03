using System;
using System.IO;
using System.Collections;

using TaHoGen.Targets;
namespace TaHoGen.Generators
{

	internal delegate void TextGeneratorHandler(TextWriter writer);
	public abstract class TextGenerator : ITextGenerator
	{
		private PropertyBag _properties;
		protected TextGenerator() {}
		#region ITextGenerator Members
		public static TextGenerator operator+(TextGenerator Left, string Right)
		{
			return Left + new SimpleTextGenerator(Right);
		}
		public static TextGenerator operator+(TextGenerator Left, TextGenerator Right)
		{
			return new ChainedGenerator(new TextGeneratorHandler(Left.Generate) + new TextGeneratorHandler(Right.Generate));
		}
		public void Generate(System.IO.TextWriter writer)
		{
			if (_properties != null)
				ReflectionHelper.LoadProperties(this, _properties);

			GenerateImpl(writer);
		}
		protected abstract void GenerateImpl(System.IO.TextWriter writer);
		

		#endregion
		/// <summary>
		/// Combines a list of text generators into a single text generator. (Note: You can use this method if your language doesn't
		/// support operator overloading)
		/// </summary>
		/// <param name="generators">The list of text generators to combine.</param>
		/// <returns>A new, combined text generator.</returns>
		public static TextGenerator Combine(params TextGenerator[] generators)
		{
			SimpleTextGenerator blankGenerator = new SimpleTextGenerator("");
			TextGenerator result = blankGenerator;
			foreach(TextGenerator generator in generators)
			{
				result += generator;
			}

			return blankGenerator;
		}
		public override string ToString()
		{
			StringTarget target = new StringTarget();
			target.Attach(this);
			target.Write();

			return target.ToString();
		}
		public void LoadProperties(PropertyBag propertyBag, bool lazyLoad)
		{
			_properties = propertyBag;

			if (!lazyLoad)
				ReflectionHelper.LoadProperties(this, propertyBag);
		}
		public void LoadProperties(PropertyBag propertyBag)
		{
			LoadProperties(propertyBag, true);	
		}
		protected string RunTemplate(ITextGenerator generator)
		{
			StringTarget target = new StringTarget();
			target.Attach(generator);

			target.Write();
			return target.ToString();
		}
	}
	internal class ChainedGenerator : TextGenerator
	{
		private TextGeneratorHandler _generate;
		public ChainedGenerator(TextGeneratorHandler handler)
		{
			_generate = handler;
		}
		protected override void GenerateImpl(TextWriter writer)
		{
			
			if (_generate != null)
				_generate(writer);
		}

	}
	public class SimpleTextGenerator : TextGenerator
	{
		private string _text = string.Empty;
		public SimpleTextGenerator()
		{
		}
		public SimpleTextGenerator(string text)
		{
			_text = text;
		}
		protected override void GenerateImpl(TextWriter writer)
		{
			writer.Write(Text);
		}
		public string Text
		{
			get { return _text;  }
			set { _text = value; }
		}

	}
}
