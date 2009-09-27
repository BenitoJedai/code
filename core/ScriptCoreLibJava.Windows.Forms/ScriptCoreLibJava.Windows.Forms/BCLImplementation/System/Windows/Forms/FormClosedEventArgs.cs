using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.FormClosedEventArgs))]
	internal class __FormClosedEventArgs
	{
		public __FormClosedEventArgs(CloseReason closeReason)
		{
			this.CloseReason = closeReason;
		}

		public CloseReason CloseReason { get; set; }
	}
}
