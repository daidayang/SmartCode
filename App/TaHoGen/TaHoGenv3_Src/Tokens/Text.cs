using System;

namespace TaHoGen.Tokens
{
	[Serializable]
	public class TextToken : Token
	{
		public TextToken(string text) : base(text)
		{
		}
	}
}
