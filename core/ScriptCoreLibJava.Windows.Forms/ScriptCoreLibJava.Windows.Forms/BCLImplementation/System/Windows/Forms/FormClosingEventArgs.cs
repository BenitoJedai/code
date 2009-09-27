using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.FormClosingEventArgs))]
	internal class __FormClosingEventArgs : __CancelEventArgs
	{
		public __FormClosingEventArgs(CloseReason closeReason, bool cancel)
		{
			this.CloseReason = closeReason;
			this.Cancel = cancel;
		}

		public CloseReason CloseReason { get; set; }
	}
}
