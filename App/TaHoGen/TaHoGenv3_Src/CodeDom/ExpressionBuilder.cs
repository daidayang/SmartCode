using System;
using System.CodeDom;
using System.CodeDom.Compiler;
namespace TaHoGen.CodeDom
{
	public sealed class ExpressionBuilder
	{
		private ExpressionBuilder() {}
		public static void Build(string writerName, string text, CodeMemberMethod method)
		{
			// This is equivalent to: Generate "Response.Write (<text here>);"
			CodeExpression writerObject = new CodeVariableReferenceExpression(writerName);
			CodeExpression invokeExpression = new CodeMethodInvokeExpression(writerObject, "Write", new CodeSnippetExpression(text));

			method.Statements.Add(new CodeExpressionStatement(invokeExpression));
		}
	}
}
