using System;
using Interop.CodeSmithParser;
using TaHoGen.Collections;
namespace TaHoGen.Tokens
{
	[Serializable]
	public class Directive : Token
	{
		private StringTable _attributes = new StringTable();
		private string _name;
		public Directive(string text) : base(text)
		{
			CDirectiveParserClass parser = new CDirectiveParserClass();
			parser.Parse(text, new CallbackHandler(this));
		}
		public string Name
		{
			get { return _name;  }
			set { _name = value; }
		}
		public bool HasAttribute(string attributeName)
		{
			foreach(object key in _attributes.Keys)
			{
				string name = key as string;
				if (string.Compare(name, attributeName, true) == 0)
					return true;
			}
			return false;
		}
		public string this[string attributeName]
		{
			get { return GetAttribute(attributeName); }
			set { SetAttribute(attributeName, value); }
		}
		public void SetAttribute(string attributeName, string value)
		{
			_attributes[attributeName] = value;
		}
		public string GetAttribute(string attributeName)
		{
			foreach(object key in _attributes.Keys)
			{
				string name = key as string;
				if (string.Compare(name, attributeName, true) == 0)
					return _attributes[name];
			}
			throw new AttributeNotFoundException(attributeName);
		}
		private class CallbackHandler : IDirectiveCallback
		{
			private Directive _directive;
			public CallbackHandler(Directive directive)
			{
				_directive = directive;
			}
			#region IDirectiveCallback Members

			public void SetName(string Name)
			{
				_directive.Name = Name;
			}

			public void AddAttribute(string AttributeName, string Value)
			{
				_directive._attributes[AttributeName] = Value;
			}

			#endregion
		}
	}
}
