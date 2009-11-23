using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.TextWriter))]
	internal class __TextWriter : MarshalByRefObject, IDisposable
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
