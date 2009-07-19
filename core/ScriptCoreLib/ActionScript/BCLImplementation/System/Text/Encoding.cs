using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Text
{
	[Script(Implements = typeof(global::System.Text.Encoding))]
	internal abstract class __Encoding
	{
		public virtual string GetString(byte[] bytes)
		{
			return default(string);
		}

		public virtual byte[] GetBytes(string s)
		{
			return default(byte[]);
		}

		public static Encoding ASCII
		{
			get
			{
				// cache?
				return new ASCIIEncoding();
			}
		}
	}
}
