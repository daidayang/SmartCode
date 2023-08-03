using System;

namespace TaHoGen.Tokens
{
	[Serializable]
	public class Comment : Token
	{
		public Comment(string comment) : base(comment)
		{
		}
	}
}
