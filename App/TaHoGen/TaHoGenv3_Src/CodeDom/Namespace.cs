using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using TaHoGen.Collections;
namespace TaHoGen.CodeDom
{
	
	public sealed class NamespaceBuilder
	{
		private NamespaceBuilder() {}
		public static CodeNamespace Build(string namespaceName)
		{
			CodeNamespace newNamespace = new CodeNamespace(namespaceName);
			
			return newNamespace;
		}
		public static void Import(string namespaceName, CodeNamespace targetNamespace)
		{
			targetNamespace.Imports.Add(new CodeNamespaceImport(namespaceName));
		}
	}

}
