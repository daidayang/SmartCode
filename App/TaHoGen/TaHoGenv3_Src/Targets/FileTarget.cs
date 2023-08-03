using System;
using System.IO;
namespace TaHoGen.Targets
{
	public class FileTarget : OutputTarget
	{
		private string _fileName = string.Empty;
		private FileMode _mode;
		private StreamWriter _writer;

		public FileTarget(string fileName, FileMode mode) 
		{
			_fileName = fileName;
			_mode = mode;
			this.BeginWrite +=new OutputTargetEventHandler(OnBeginWrite);
			this.EndWrite +=new OutputTargetEventHandler(OnEndWrite);
		}
		public FileMode Mode
		{
			get { return _mode; }
			set { _mode = value; }
		}
		public string FileName
		{ 
			get { return _fileName;  }
			set { _fileName = value; }
		}
		private void OnBeginWrite(object sender, OutputTargetEventArgs args)
		{
			// Open the file before we output the text
			
		}

		protected override TextWriter GetTextWriter()
		{
			_writer = new StreamWriter(new FileStream(FileName, Mode));

			return _writer;
		}

		private void OnEndWrite(object sender, OutputTargetEventArgs args)
		{
			// Cleanup
			_writer.Close();
			_writer = null;
		}
		#region IDisposable Members

		

		#endregion
	}
}
