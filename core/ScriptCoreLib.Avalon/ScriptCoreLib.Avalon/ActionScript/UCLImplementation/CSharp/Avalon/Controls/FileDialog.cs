using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.net;

namespace ScriptCoreLib.ActionScript.UCLImplementation.CSharp.Avalon.Controls
{
	[Script(Implements = typeof(global::ScriptCoreLib.CSharp.Avalon.Controls.FileDialog))]
	internal class __FileDialog
	{
		// http://blog.everythingflex.com/2008/10/17/filereferencebrowse-bit-me-on-the-ass-today/
		// http://www.gotoandlearn.com/play?id=76

		public void Open(Action<MemoryStream> handler)
		{
			var f = new FileReference();

			

			f.select +=
				e =>
				{
					f.load();
				};

			f.complete +=
				e =>
				{
					var m = f.data.ToMemoryStream();

					handler(m);

				};

			f.browse(
				//new[]
				//        {
				//            new FileFilter("my file", "*.out")
				//        }
			);
		}

		public void Save(MemoryStream data, string FileName)
		{
			var a = data.ToByteArray();

			var f = new FileReference();

			f.save(a, FileName);
		}
	}
}
