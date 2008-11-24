using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.TextWriter))]
	internal abstract class __TextWriter : IDisposable
	{
		public virtual void WriteLine(string value)
		{

		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
