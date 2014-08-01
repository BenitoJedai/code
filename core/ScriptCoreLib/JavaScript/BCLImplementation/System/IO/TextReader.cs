﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.IO
{
    // http://referencesource.microsoft.com/#mscorlib/system/io/textreader.cs

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
