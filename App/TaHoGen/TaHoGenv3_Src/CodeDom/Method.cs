using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using TaHoGen.Collections;
namespace TaHoGen.CodeDom
{
	public sealed class MethodBuilder
	{
		private MethodBuilder(){}
		public static CodeMemberMethod Build(string methodName, 
								 MemberAttributes attributes)
		{
			CodeMemberMethod method = new CodeMemberMethod();
			method.Name = methodName;
			method.Attributes = attributes;

			return method;
		}
	}
}
