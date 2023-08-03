using System;
using System.CodeDom;
using System.CodeDom.Compiler;
namespace TaHoGen.CodeDom
{
	public sealed class LiteralTextBuilder
	{
		private LiteralTextBuilder() {}
		public static void Build(string writerName, string text, CodeMemberMethod targetMethod)
		{
			CodeExpression primitiveExpression = new CodePrimitiveExpression(text);
			
			CodeExpression writerObject = new CodeVariableReferenceExpression(writerName);
			CodeExpression invokeExpression = new CodeMethodInvokeExpression(writerObject, "Write", primitiveExpression);

			targetMethod.Statements.Add(new CodeExpressionStatement(invokeExpression));
		}
	}
}
