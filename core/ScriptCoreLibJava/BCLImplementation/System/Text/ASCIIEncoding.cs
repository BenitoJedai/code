using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Text
{
	[Script(Implements = typeof(global::System.Text.ASCIIEncoding))]
	internal class __ASCIIEncoding : __Encoding
	{
		public override string GetString(byte[] bytes)
		{
			var r = default(string);

			try
			{
                var _bytes = (sbyte[])(object)bytes;

                r = (string)(object)new java.lang.String(_bytes, "ASCII");
			}
			catch
			{
				throw new InvalidOperationException();
			}
			return r;

		}

		public override byte[] GetBytes(string s)
		{
			var r = default(byte[]);

			try
			{

				r = (byte[])(object)((java.lang.String)(object)s).getBytes("ASCII");

			}
			catch
			{
				throw new InvalidOperationException();
			}
			return r;
		}
	}
}
