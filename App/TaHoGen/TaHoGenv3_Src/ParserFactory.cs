using System;

namespace TaHoGen
{
	public sealed class ParserFactory
	{
		private ParserFactory() {}
		public static IParser CreateParser()
		{
			return new CachedParser(new Parser());
		}
	}
}
