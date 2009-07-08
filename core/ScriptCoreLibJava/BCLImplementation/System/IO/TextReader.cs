using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
	[Script(Implements = typeof(global::System.IO.TextReader))]
	internal abstract class __TextReader : IDisposable
	{
		public virtual string ReadLine()
		{
			throw new NotImplementedException();
		}

		#region IDisposable Members

		public void Dispose()
		{
		}

		#endregion
	}
}
