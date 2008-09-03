using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
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


		public static bool operator !=(__Uri uri1, __Uri uri2)
		{
			object o1 = uri1;
			object o2 = uri2;

			if (o1 == null)
				return o1 != o2;

			if (o2 == null)
				return o1 != o2;

			return uri1.OriginalString == uri2.OriginalString;
		}

		public static bool operator ==(__Uri uri1, __Uri uri2)
		{
			object o1 = uri1;
			object o2 = uri2;

			if (o1 == null)
				return o1 == o2;

			if (o2 == null)
				return o1 == o2;

			return uri1.OriginalString == uri2.OriginalString;
		}
	}
}
