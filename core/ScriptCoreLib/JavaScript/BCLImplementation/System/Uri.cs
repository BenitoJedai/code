using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Uri))]
	internal class __Uri
	{
		string _OriginalString;

		public string OriginalString { get { return _OriginalString; } }

		public __Uri(string uri)
		{
			this._OriginalString = uri;
		}
	}
}
