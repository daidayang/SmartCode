using System;

namespace TaHoGen.Tokens
{
	[Serializable]
	public class Include : Token
	{
		public Include(string file) : base(file)
		{

		}
	}
}
