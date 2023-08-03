using System;

namespace TaHoGen.Tokens
{
	[Serializable]
	public abstract class Token
	{
		private string _text;
		protected Token(string text)
		{
			_text = text;
		}
		public string Text
		{
			get { return _text;  }
			set { _text = value; }
		}
		public override string ToString()
		{
			return string.Format("Token Type: {0}\nText: {1}", GetType().ToString(), Text);
		}

	}
}
