using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.DownloadStringCompletedEventHandler))]
	internal delegate void __DownloadStringCompletedEventHandler(object sender, DownloadStringCompletedEventArgs e);
}
