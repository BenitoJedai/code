using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLibJava.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.CancelEventArgs))]
	internal class __CancelEventArgs : __EventArgs
	{
		public bool Cancel { get; set; }
	}
}
