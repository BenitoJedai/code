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

		public  void Save(MemoryStream data, string FileName)
		{
			var a = data.ToByteArray();

			var f = new FileReference();

			f.save(a, FileName);
		}
	}
}
