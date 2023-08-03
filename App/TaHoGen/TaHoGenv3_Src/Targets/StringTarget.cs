using System;
using System.IO;
namespace TaHoGen.Targets
{
	public class StringTarget : OutputTarget, IDisposable
	{
		private StringWriter _writer = null;
		private string _text = string.Empty;
		private bool _disposed = false;
		public StringTarget()
		{
			this.BeginWrite +=new OutputTargetEventHandler(OnBeginWrite);
			this.EndWrite +=new OutputTargetEventHandler(OnEndWrite);
		}
		protected override TextWriter GetTextWriter()
		{
			return _writer;
		}
		public override string ToString()
		{
			return _text;
		}

		protected virtual void OnBeginWrite(object sender, OutputTargetEventArgs args)
		{
			// Clear the results from the previous run
			_text = string.Empty;
			_writer = new StringWriter();
		}

		protected virtual void OnEndWrite(object sender, OutputTargetEventArgs args)
		{
			// Save the output
			_text = _writer.ToString();
			_writer = null;
		}
		#region IDisposable Members

		public void Dispose()
		{
			if (_disposed)
				return;

			if (_writer != null)
				_writer.Close();

			_disposed = true;
		}

		#endregion
	}
}
