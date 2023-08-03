using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Drawing.Design;
using System.IO;
using System.Reflection;
using System.Windows.Forms.Design;

using TaHoGen.Generators;
using TaHoGen.Tokens;

namespace TaHoGen.CodeDom
{
	/// <summary>
	/// Generates the source code for a set of templates within an assembly.
	/// </summary>
	public sealed class SourceCodeGenerator : IVisitor
	{
		private CodeCompileUnit _compileUnit = new CodeCompileUnit();
		private ClassGenerator _activeGenerator = null;
		private CodeNamespace _defaultNamespace = null;
		private CodeNamespace _activeNamespace = null;
		private Hashtable _namespaces = new Hashtable();
		private int _templateCount = 0;

		public SourceCodeGenerator()
		{
			_defaultNamespace = CreateNamespace("TaHoGen.Templates");
			_namespaces["TaHoGen.Templates"] = _defaultNamespace;
		}
		public CodeCompileUnit GetCompileUnit()
		{
			return _compileUnit;
		}
		public void Generate(Token[] tokens)
		{
			foreach(Token token in tokens)
			{
				Visitable v = new Visitable(token);
				v.Accept(this);
			}

		}
		[VisitorMethod]
		private void Visit(Directive directive)
		{
			// Process only the codetemplate and import directives;
			// the rest will be handled by the other generators
			if (string.Compare(directive.Name, "CodeTemplate", true) == 0)
				OnCodeTemplateDirective(directive);

			if (string.Compare(directive.Name, "Import", true) == 0)
				OnImportDirective(directive);

            if (string.Compare(directive.Name, "Assembly", true) == 0)
            {
                OnAssemblyDirective(directive);
            }

			if (_activeGenerator != null)
				_activeGenerator.Visit(directive);
		}
		[VisitorMethod]
		private void Visit(Token token)
		{
			// Forward the rest to the other source generators
			Visitable v = new Visitable(token);
			v.Accept(_activeGenerator);
		}
		private void OnCodeTemplateDirective(Directive directive)
		{
			_activeNamespace = _defaultNamespace;
			if (directive.HasAttribute("Namespace"))
			{
				string namespaceName = directive["Namespace"];

				// Group the classes into their respective namespaces
				if (_namespaces.Contains(namespaceName))
				{
					_activeNamespace = _namespaces[namespaceName] as CodeNamespace;
				}
				else
				{
					_activeNamespace = CreateNamespace(namespaceName);
					_namespaces[namespaceName] = _activeNamespace;
				}
			}

			string className = "UnnamedTemplate" + (_templateCount++).ToString();

			if (directive.HasAttribute("ClassName"))
				className = directive["ClassName"];

			// Derive it from TextGenerator													  
			CodeTypeDeclaration targetClass = new CodeTypeDeclaration(className);
			targetClass.BaseTypes.Add(typeof(TextGenerator));
			_activeGenerator = new ClassGenerator(targetClass);

			_activeNamespace.Types.Add(targetClass);

			// Add the current namespace to the compile unit if it doesn't 
			// already exist
			if (_compileUnit.Namespaces.Contains(_activeNamespace))
				return;
				
			_compileUnit.Namespaces.Add(_activeNamespace);
		}

		private void OnImportDirective(Directive directive)
		{
			NamespaceBuilder.Import(directive["Namespace"], _activeNamespace);
		}
		private void OnAssemblyDirective(Directive directive)
		{
			if (!directive.HasAttribute("Name"))
				return;

			// Locate the assembly and
			// reference it if we find it
			string assemblyName = directive["Name"];
			
			string assemblyPath = ResolveAssemblyPath(assemblyName);

			if (assemblyPath.Length > 0)
				_compileUnit.ReferencedAssemblies.Add(assemblyPath);

		}
		private string ResolveAssemblyPath(string assemblyName)
		{
			string path = string.Empty;
			try
			{
				// Try to load the assembly into memory
				Assembly assembly = Assembly.LoadWithPartialName(assemblyName);

				// Save the path of the assembly and add it to the list of
				// assemblies to reference
				path = assembly.Location;
			}
			catch(Exception)
			{
				/*Do nothing*/
			}

			return path;
		}
		private CodeNamespace CreateNamespace(string namespaceName)
		{
			CodeNamespace newNamespace = new CodeNamespace(namespaceName);

			// Add the default namespace imports
			newNamespace.Imports.Add(new CodeNamespaceImport("System"));
			newNamespace.Imports.Add(new CodeNamespaceImport("System.ComponentModel"));
			newNamespace.Imports.Add(new CodeNamespaceImport("System.Data"));
			newNamespace.Imports.Add(new CodeNamespaceImport("System.Diagnostics"));
			//newNamespace.Imports.Add(new CodeNamespaceImport("System.Drawing.Design"));
			newNamespace.Imports.Add(new CodeNamespaceImport("System.IO"));
			//newNamespace.Imports.Add(new CodeNamespaceImport("TaHoGen.Design"));
			newNamespace.Imports.Add(new CodeNamespaceImport("TaHoGen.Generators"));
			newNamespace.Imports.Add(new CodeNamespaceImport("TaHoGen.Targets"));

			return newNamespace;
		}
	}
	/// <summary>
	/// Generates the source code for a single template class within a namespace.
	/// </summary>
	internal class ClassGenerator : IVisitor
	{
		private CodeTypeDeclaration _targetClass;
		private CodeMemberMethod _writerMethod;
		private CodeConstructor _defaultConstructor;
        private static readonly string writerName = "Response";
		public ClassGenerator(CodeTypeDeclaration targetClass)
		{
			_targetClass = targetClass;

			#region Default Constructor Definition
			_defaultConstructor = new CodeConstructor();
			_defaultConstructor.Attributes = MemberAttributes.Public;
			_targetClass.Members.Add(_defaultConstructor);
			#endregion

			#region Property Bag Constructor Definition

			// Add the property bag constructor
			CodeConstructor propertyBagConstructor = new CodeConstructor();
			propertyBagConstructor.Attributes = MemberAttributes.Public;
			propertyBagConstructor.Parameters.Add(ParameterBuilder.Build("propertyBag", typeof(PropertyBag)));

			// this.LoadProperties(bag);
			CodeExpression[] parameters = new CodeExpression[]{new CodeVariableReferenceExpression("propertyBag")};

			// Call the default constructor
			propertyBagConstructor.ChainedConstructorArgs.Add(new CodeSnippetExpression(""));
			propertyBagConstructor.Statements.Add(new CodeMethodInvokeExpression(new CodeThisReferenceExpression(),"LoadProperties", parameters));
			_targetClass.Members.Add(propertyBagConstructor);
			#endregion

			// Add the generate method by default
			_writerMethod = MethodBuilder.Build("GenerateImpl", MemberAttributes.Family | MemberAttributes.Override);
			_writerMethod.Parameters.Add(ParameterBuilder.Build(writerName, typeof(TextWriter)));

			_targetClass.Members.Add(_writerMethod);
		}

		[VisitorMethod]
		public void Visit(Expression expression)
		{
			ExpressionBuilder.Build(writerName, expression.Text, _writerMethod);
		}
		[VisitorMethod]
		public void Visit(TextToken text)
		{
			LiteralTextBuilder.Build(writerName, text.Text, _writerMethod);
		}
		[VisitorMethod]
		public void Visit(Code code)
		{
			LiteralCodeBuilder.Build(writerName, code.Text, _writerMethod);
		}
		[VisitorMethod]
		public void Visit(Directive directive)
		{
			DirectiveHandler handler = new DirectiveHandler(_targetClass, _defaultConstructor);
			handler.Visit(directive);
		}
		[VisitorMethod]
		public void Visit(ScriptBlock scriptBlock)
		{
			CodeSnippetTypeMember script = new CodeSnippetTypeMember(scriptBlock.Text);
			_targetClass.Members.Add(script);
		}
        [VisitorMethod]
        public void Visit(Include include)
        {
            StreamReader reader = new StreamReader(include.Text );
            while (!reader.EndOfStream)
            {
                CodeSnippetTypeMember mem = new CodeSnippetTypeMember("\t\t" + reader.ReadLine() + "\n");
                _targetClass.Members.Add(mem);
            }
            reader.Close();
        }
		private class DirectiveHandler : IVisitor
		{
			private CodeTypeDeclaration _targetClass;
			private CodeConstructor _defaultConstructor = null;

			public DirectiveHandler(CodeTypeDeclaration targetClass, CodeConstructor defaultConstructor)
			{
				_targetClass = targetClass;
				_defaultConstructor = defaultConstructor;
			}
			[VisitorMethod]
			public void Visit(Directive directive)
			{
				// We're only handling the property directive
				if (string.Compare(directive.Name,"Property",true) != 0)
					return;

				if (!directive.HasAttribute("Name") || !directive.HasAttribute("Type"))
					return;

				string propertyName = directive.GetAttribute("Name");
				string propertyType = directive.GetAttribute("Type");

				CodeMemberProperty newProperty = PropertyBuilder.Build(propertyName, propertyType, _targetClass);

				#region Design Attributes
				// Add the Category, Description, and Browsable custom attributes
				// (if they exist)
				ApplyAttribute("Category", directive, newProperty);
				ApplyAttribute("Description", directive, newProperty);
				ApplyAttribute("Browsable", directive, newProperty);

				#region Default Value Attribute
				// Set the default value (if there is one)
				if (directive.HasAttribute("Default"))
				{
				
					CodeExpression valueExpression = null;
				
					if (StringHelper.AreEqual(propertyType, "System.String") || 
						StringHelper.AreEqual(propertyType, "String"))
					{
						valueExpression = new CodePrimitiveExpression(directive["Default"]);
					}
					else
					{
						valueExpression = new CodeSnippetExpression(directive["Default"]);
					}
					CodePropertyReferenceExpression fieldReference = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), propertyName);
					CodeAssignStatement assignDefaultValue = new CodeAssignStatement(fieldReference, valueExpression);
                    _defaultConstructor.Statements.Add(assignDefaultValue);
                   
				}
				#endregion
				#endregion
				
			}
			private void ApplyAttribute(string attributeName, Directive directive, CodeMemberProperty newProperty)
			{
				if (directive.HasAttribute(attributeName))
					AddAttribute(attributeName, directive[attributeName], newProperty);
			}
			private void AddAttribute(string attributeName, object attributeValue, CodeMemberProperty newProperty)
			{
				newProperty.CustomAttributes.Add(AttributeBuilder.CreateCustomAttribute(attributeName, attributeValue));
			}
		}
	}
	

}
