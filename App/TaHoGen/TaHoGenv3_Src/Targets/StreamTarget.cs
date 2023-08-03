using System;
using System.IO;
namespace TaHoGen.Targets
{
	/// <summary>
	/// This class sends the output of a text generator to a stream.
	/// </summary>
	public class StreamTarget : OutputTarget, IDisposable
	{
		private Stream _targetStream;
		private bool _disposed = false;
		public StreamTarget(Stream targetStream)
		{
			_targetStream = targetStream;
		}
		public Stream TargetStream
		{
			get { return _targetStream;  }
			set { _targetStream = value; }
		}

		protected override TextWriter GetTextWriter()
		{
			return new StreamWriter(_targetStream);
		}
		#region IDisposable Members

		public virtual void Dispose()
		{
			if (_disposed)
				return;

			if (_targetStream != null)
				_targetStream.Close();

			_disposed = true;
		}

		#endregion
	}
}
