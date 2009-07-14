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
			return (string)(object)new java.lang.String(__File.InternalByteArrayToSByteArray(bytes));
		}

		public override byte[] GetBytes(string s)
		{
			return (byte[])(object)((java.lang.String)(object)s).getBytes();
		}
	}
}
