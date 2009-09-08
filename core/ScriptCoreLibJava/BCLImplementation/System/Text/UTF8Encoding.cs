using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Text
{
	[Script(Implements = typeof(global::System.Text.UTF8Encoding))]
	internal class __UTF8Encoding : __Encoding
	{
		public override string GetString(byte[] bytes)
		{
			var r = default(string);

			try
			{
				r = (string)(object)new java.lang.String(__File.InternalByteArrayToSByteArray(bytes), "UTF-8");
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

				r = (byte[])(object)((java.lang.String)(object)s).getBytes("UTF-8");

			}
			catch
			{
				throw new InvalidOperationException();
			}
			return r;
		}
	}
}
