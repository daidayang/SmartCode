using System;
using System.IO;
using TaHoGen.Tokens;
namespace TaHoGen.Targets
{
#if REGIONS_ENABLED
	/// <summary>
	/// Summary description for MergeTarget.
	/// </summary>
	public class MergeTarget : OutputTarget
	{
		private TextReader _reader;
		private TextWriter _destination;
		private IParser _regionParser;
		private StringWriter _internalWriter = new StringWriter();
		private string _regionName;
		public MergeTarget(string regionName, TextReader source, TextWriter destination, IParser regionParser)
		{
			_reader = source;
			_destination = destination;
			_regionParser = regionParser;
			_regionName = regionName;

			this.BeginWrite +=new OutputTargetEventHandler(MergeTarget_BeginWrite);
			this.EndWrite +=new OutputTargetEventHandler(MergeTarget_EndWrite);
		}

		protected override TextWriter GetTextWriter()
		{
			return _internalWriter;
		}

		private void MergeTarget_BeginWrite(object sender, OutputTargetEventArgs e)
		{
			
		}

		private void MergeTarget_EndWrite(object sender, OutputTargetEventArgs e)
		{
			// Split the text into regions, and nonregions
			string sourceText = _reader.ReadToEnd();
			Token[] tokens = _regionParser.Parse(sourceText);

			// Copy the results of the write to the target
			MergeWriter writer = new MergeWriter(_regionName, _internalWriter.ToString(), _destination);
			foreach(Token token in tokens)
			{
				Visitable v = new Visitable(token);
				v.Accept(writer);
			}

			// Cleanup
			_internalWriter.Flush();
		}
		private class MergeWriter : IVisitor
		{
			private TextWriter _writer;
			private string _regionName;
			private string _output;
			public MergeWriter(string regionName, string outputText, TextWriter writer)
			{
				_writer = writer;
				_regionName = regionName;
				_output = outputText;
			}
			[VisitorMethod]
			public void Visit(Region v)
			{
				if (!StringHelper.AreEqual(v.Name, _regionName))
				{
					// If this isn't the region we're looking for,
					// then we need to preserve the contents of the existing
					// region
					_writer.WriteLine(v.Text);
					return;
				}
					
				_writer.WriteLine(_output);
			}

            [VisitorMethod]
			public void Visit(NonRegion v)
			{
				_writer.Write(v.Text);
			}
		}

	}
#endif
}
