using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.NotSupportedException))]
	internal class __NotSupportedException : __Exception
	{
		public __NotSupportedException(string message) : base(message) { }



	}
}
