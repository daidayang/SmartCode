using System;

namespace TaHoGen.Tokens
{
	[Serializable]
	public class Expression : Token
	{
		public Expression(string expression) : base(expression)
		{
		}
	}
}
