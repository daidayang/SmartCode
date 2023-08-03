using System;
using System.CodeDom;
using System.CodeDom.Compiler;
namespace TaHoGen.CodeDom
{
	public sealed class ParameterBuilder
	{			
		private ParameterBuilder() {}
		public static CodeParameterDeclarationExpression Build(string parameterName, System.Type parameterType)
		{
			System.CodeDom.CodeParameterDeclarationExpression parameter = new CodeParameterDeclarationExpression(parameterType, parameterName);
			parameter.Direction = FieldDirection.In;

			return parameter;
		}
	}
}
