using System;
using System.IO;
namespace TaHoGen.Targets
{
	public class ConsoleTarget : OutputTarget
	{
		protected override TextWriter GetTextWriter()
		{
			return Console.Out;
		}

	}
}
