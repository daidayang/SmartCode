using System;

namespace TaHoGen
{
	public class AttributeNotFoundException : Exception
	{
		private string _name;
		public AttributeNotFoundException(string attributeName)
		{
			_name = attributeName;
		}
		public string AttributeName
		{
			get { return _name; }
		}
		public override string Message
		{
			get
			{
				return string.Format("Unable to find attribute '{0}'", AttributeName);
			}
		}

	}
	public class CompilerException : Exception
	{
		private string _message;
		public CompilerException(string message)
		{
			_message = message;
		}
		public override string Message
		{
			get
			{
				return _message;
			}
		}

	}
	public class ProviderNotFoundException : Exception
	{
		private string _language = string.Empty;
		public ProviderNotFoundException(string language)
		{
			_language = language;
		}
		public string Language
		{
			get { return _language; }
		}
		public override string Message
		{
			get
			{
				return string.Format("Unable to find a provider for Language '{0}'", Language);
			}
		}

	}
}
