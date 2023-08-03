using System;

namespace TaHoGen.Targets
{
	
	public class DebugTarget : OutputTarget
	{

		protected override System.IO.TextWriter GetTextWriter()
		{
			return new DebugWriter();
		}
	}
}
