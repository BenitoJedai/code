using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
    // http://referencesource.microsoft.com/#mscorlib/system/io/textreader.cs

    // 
	[Script(Implements = typeof(global::System.IO.TextReader))]
	public abstract class __TextReader : IDisposable
	{
        // used by
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\IO\StreamReader.cs

		public virtual string ReadLine()
		{
			throw new NotImplementedException();
		}

		public virtual string ReadToEnd()
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
