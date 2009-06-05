using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.CSharp.Avalon.Controls
{

	public class FileDialog
	{
		// http://msdn.microsoft.com/en-us/library/system.windows.controls.openfiledialog(VS.95).aspx
		// http://msdn.microsoft.com/en-us/library/microsoft.win32.openfiledialog.aspx
		// http://blog.everythingflex.com/2008/10/01/filereferencesave-in-flash-player-10/
		
		// at 2009.06.05 silverlight cannot use save file dialog

		public void Open()
		{

		}

		public void Save(MemoryStream data, string FileName)
		{
			// the actionscript API will show the dialog after the user event
			// has returned, so we must mimic it here

			1.AtDelay(
				delegate
				{
					var s = new Microsoft.Win32.SaveFileDialog();

					s.FileName = FileName;

					if (s.ShowDialog() ?? false)
					{
						using (var w = s.OpenFile())
						{
							var a = data.ToArray();
							w.Write(a, 0, a.Length);
						}
					}
				}
			);
		}
	}
}
