using System;

namespace TaHoGen.Tokens
{
#if REGIONS_ENABLED
	public class NonRegion : Token
	{
		public NonRegion(string text) : base(text)
		{
		}
	}
#endif
}
