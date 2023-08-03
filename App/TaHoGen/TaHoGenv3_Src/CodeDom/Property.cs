using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using TaHoGen.Collections;
namespace TaHoGen.CodeDom
{
	public sealed class PropertyBuilder
	{
		private PropertyBuilder() {}
		public static CodeMemberProperty Build(string propertyName, string propertyType, CodeTypeDeclaration targetClass)
		{
			// The fieldname will have the underscore character appended to it plus
			// the property name in camel case
			string fieldName = "_" + propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
			CodeMemberField newField = new CodeMemberField(propertyType, fieldName);
			
			// Construct the property itself
			CodeMemberProperty newProperty = new CodeMemberProperty();
			newProperty.Attributes = MemberAttributes.Public | MemberAttributes.Final;
			newProperty.Type = new CodeTypeReference(propertyType);
			newProperty.Name = propertyName;
			newProperty.HasGet = true;
			newProperty.HasSet = true;


			// Construct the get statement; It should look like this:
			// get { return this.< insert fieldname here>; }
			CodeExpression fieldExpression = new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName);
			newProperty.GetStatements.Add(new CodeMethodReturnStatement(fieldExpression));

			// Construct the set statement; It should look like this:
			// set { <field> = value; }
			newProperty.SetStatements.Add(new CodeAssignStatement(fieldExpression, new CodePropertySetValueReferenceExpression()));

			
			targetClass.Members.Add(newField);
			targetClass.Members.Add(newProperty);

			return newProperty;
		}
	}
}
