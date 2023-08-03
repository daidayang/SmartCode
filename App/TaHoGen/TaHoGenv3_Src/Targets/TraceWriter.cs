using System;
using System.Diagnostics;
namespace TaHoGen.Targets
{
	public class TraceWriter : System.IO.TextWriter
	{
		public override void Write(string Value)
		{
			Trace.Write(Value);
		}
		public override void WriteLine(string Value)
		{
			Trace.WriteLine(Value);
		}
		public void WriteLineIf(bool Condition, object obj, string category)
		{
			Trace.WriteLineIf(Condition, obj, category);
		}
		public void WriteIf(bool Condition, object obj, string category)
		{
			Trace.WriteIf(Condition, obj, category);
		}
		public override void Write(char Value)
		{
			Trace.Write(Value);
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
