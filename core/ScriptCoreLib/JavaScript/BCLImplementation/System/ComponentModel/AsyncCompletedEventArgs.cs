using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel
{
	[Script(Implements = typeof(global::System.ComponentModel.AsyncCompletedEventArgs))]
	internal class __AsyncCompletedEventArgs : __EventArgs
	{
		public Exception Error { get; set;  }
	}
}
