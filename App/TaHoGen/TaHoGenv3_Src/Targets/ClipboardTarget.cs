using System;
using System.IO;
using System.Windows.Forms;
namespace TaHoGen.Targets
{
	
	public class ClipboardTarget : StringTarget
	{
		public ClipboardTarget()
		{
			
		}
		protected override void OnEndWrite(object sender, OutputTargetEventArgs args)
		{
			base.OnEndWrite (sender, args);
			string textWritten = this.ToString();
			
			// Send the results to the clipboard
			Clipboard.SetDataObject(textWritten, true);
		}

	}
}
