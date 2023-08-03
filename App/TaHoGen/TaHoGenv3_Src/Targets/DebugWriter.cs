using System;
using System.Diagnostics;
using System.IO;
namespace TaHoGen.Targets
{
	public class DebugWriter : System.IO.TextWriter
	{
		public override void Write(string Value)
		{
			Debug.Write(Value);
		}
		public override void WriteLine(string Value)
		{
			Debug.WriteLine(Value);
		}
		public void WriteLineIf(bool Condition, object Obj, string Category)
		{
			Debug.WriteLineIf(Condition, Obj, Category);
		}
		public void WriteIf(bool Condition, object Obj, string Category)
		{
			Debug.WriteIf(Condition, Obj, Category);
		}
		public override void Write(char Value)
		{
			Debug.Write(Value);
		}
		public override System.Text.Encoding Encoding
		{
			get
			{
				return System.Text.Encoding.ASCII;
			}
		}

	}
}
