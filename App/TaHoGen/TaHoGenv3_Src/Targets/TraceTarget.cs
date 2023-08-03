using System;
using System.Diagnostics;

namespace TaHoGen.Targets
{
	public class TraceTarget : OutputTarget
	{
		protected override System.IO.TextWriter GetTextWriter()
		{
			return new TraceWriter();
		}
	}
}
