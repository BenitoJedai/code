using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.AsyncCompletedEventArgs))]
	public class __AsyncCompletedEventArgs : __EventArgs
	{
		public Exception Error { get; set;  }
	}
}
