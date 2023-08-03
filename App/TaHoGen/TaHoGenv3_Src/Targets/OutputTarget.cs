using System;
using System.IO;
using TaHoGen.Generators;
namespace TaHoGen.Targets
{
	public class OutputTargetEventArgs : EventArgs
	{
		private OutputTarget _target;
		public OutputTargetEventArgs(OutputTarget target)
		{
			_target = target;
		}
		public OutputTarget Target
		{
			get { return _target; }
		}
	}
	public abstract class OutputTarget
	{
		private TextGeneratorHandler _handlers = null;
		public delegate void OutputTargetEventHandler(object sender, OutputTargetEventArgs e);
		public event OutputTargetEventHandler BeginWrite;
		public event OutputTargetEventHandler EndWrite;
		public static OutputTarget operator+(OutputTarget Left, ITextGenerator Right)
		{

			Left.Attach(Right);

			return Left;
		}
		
		protected OutputTarget()
		{
			
		}
		public void Attach(ITextGenerator generator)
		{
			_handlers += new TextGeneratorHandler(generator.Generate);
		}
		public void Write()
		{
			if (_handlers == null)
				return;

			

			if (BeginWrite != null)
				BeginWrite(this, new OutputTargetEventArgs(this));

			// Acquire the text writer that we'll be using
			// to output the text
			TextWriter writer = GetTextWriter();

			// Output the text, and we're done
			_handlers(writer);

			if (EndWrite != null)
				EndWrite(this, new OutputTargetEventArgs(this));
		}
		protected abstract TextWriter GetTextWriter();
	}
}
