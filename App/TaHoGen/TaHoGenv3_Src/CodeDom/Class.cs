using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using TaHoGen.Collections;

namespace TaHoGen.CodeDom
{
	
	public sealed class ClassBuilder
	{
		private ClassBuilder() {}
		public static CodeTypeDeclaration Build(string className, CodeTypeReferenceCollection baseTypes, TypeAttributes attributes)
		{
			CodeTypeDeclaration newClass = new CodeTypeDeclaration(className);

			// Subclass from the parents
			newClass.BaseTypes.AddRange(baseTypes);
			newClass.IsClass = true;
			newClass.TypeAttributes = attributes;

			return newClass;
		}
	}
}
