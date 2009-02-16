using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.TextReader))]
	internal abstract class __TextReader : IDisposable
	{
		public virtual string ReadToEnd()
		{
			throw new NotImplementedException("");
		}

		public virtual string ReadLine()
		{
			throw new NotImplementedException("");
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
