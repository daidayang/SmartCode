using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using EnvDTE;

namespace TaHoGen.Targets
{
//	public class VSOutputPaneWriter : System.IO.TextWriter
//	{
//		private static readonly Hashtable _outputPanes = new Hashtable();
//		private string _paneName = string.Empty;
//		private _DTE _dte = null;
//		public VSOutputPaneWriter(string paneName, _DTE dte)
//		{
//			_paneName = paneName;
//			_dte = dte;
//		}
//		public void Clear()
//		{
//			OutputWindowPane currentPane = GetPane(_paneName, true);
//
//			if (currentPane == null)
//				return;
//
//			currentPane.Clear();
//		}
//		public override void Write(string Value)
//		{
//			WriteImpl(Value);
//		}
//		public override void WriteLine(string Value)
//		{
//			WriteImpl(Value + "\n");
//		}
//		public override void Write(char Value)
//		{
//			WriteImpl(Convert.ToString(Value));
//		}
//		private OutputWindowPane GetPane(string paneName, bool createPane)
//		{
//			Window window = _dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
//			OutputWindow outputWindow = window.Object as OutputWindow;
//			OutputWindowPane outputPane = null;
//
//
//			if (_outputPanes.Contains(paneName))
//				outputPane = _outputPanes[paneName] as OutputWindowPane;
//
//			if (createPane == false)
//				return null;
//
//			outputPane = outputWindow.OutputWindowPanes.Add(_paneName);
//			_outputPanes[paneName] = outputPane;
//
//			return outputPane;
//		}
//		private void WriteImpl(string Value)
//		{
//			
//			OutputWindowPane outputPane = null;
//			try
//			{
//				outputPane = GetPane(_paneName, true);
//			}
//			catch(Exception ex)
//			{
//				Debug.WriteLine(ex.ToString());
//				System.Diagnostics.Debugger.Break();
//			}
//			if (outputPane != null)
//				outputPane.OutputString(Value);
//		}
//		public override System.Text.Encoding Encoding
//		{
//			get
//			{
//				return System.Text.Encoding.ASCII;
//			}
//		}
//	}
}
