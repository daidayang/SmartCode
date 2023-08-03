using System;
using System.CodeDom;
using System.CodeDom.Compiler;
namespace TaHoGen.CodeDom
{
	
	public sealed class LiteralCodeBuilder
	{
		private LiteralCodeBuilder(){}
		public static void Build(string writerName, string text, CodeMemberMethod method)
		{
			// Since it's literal code, pass the text to the output
			// stream unaltered
			CodeSnippetStatement literalCode = new CodeSnippetStatement(text);
			method.Statements.Add(literalCode);
		}
	}
}
