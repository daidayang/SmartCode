using System;
using System.IO;
using TaHoGen.Tokens;
namespace TaHoGen
{
	public interface ITextGenerator
	{
		void Generate(TextWriter writer);
	}
	public interface IParser
	{
		Token[] Parse(string text);
	}
}
