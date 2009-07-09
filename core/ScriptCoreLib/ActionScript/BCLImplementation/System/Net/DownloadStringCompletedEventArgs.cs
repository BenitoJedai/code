using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Net
{
	[Script(Implements = typeof(global::System.Net.DownloadStringCompletedEventArgs))]
	internal class __DownloadStringCompletedEventArgs : __AsyncCompletedEventArgs
	{
		public string Result { get; set; }
	}
}
