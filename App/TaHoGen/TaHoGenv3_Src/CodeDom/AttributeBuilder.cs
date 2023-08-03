using System;
using System.CodeDom;
using System.CodeDom.Compiler;
namespace TaHoGen.CodeDom
{
	public sealed class AttributeBuilder
	{
		private AttributeBuilder(){}

		public static CodeAttributeDeclaration CreateCustomAttribute(string attributeName, object attributeValue)
		{
			return CreateCustomAttribute(attributeName, new CodePrimitiveExpression(attributeValue));
		}
		public static CodeAttributeDeclaration CreateCustomAttribute(string attributeName, CodeExpression attributeValueExpression)
		{
			CodeAttributeArgument attributeValue = new CodeAttributeArgument(attributeValueExpression);
			CodeAttributeDeclaration newAttribute = new CodeAttributeDeclaration(attributeName, attributeValue);
			 
			return newAttribute;
		}
	}
}
