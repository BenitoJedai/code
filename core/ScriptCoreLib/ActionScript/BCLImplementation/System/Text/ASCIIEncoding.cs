using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using System.IO;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Text
{
	[Script(Implements = typeof(global::System.Text.ASCIIEncoding))]
	internal class __ASCIIEncoding : __Encoding
	{
		public override string GetString(byte[] bytes)
		{
			var w = new StringBuilder();

			for (int i = 0; i < bytes.Length; i++)
			{
				w.Append(__String.FromCharCode(bytes[i]));
			}

			return w.ToString();
		}

		public override byte[] GetBytes(string s)
		{
			var a = new MemoryStream();

			for (int i = 0; i < s.Length; i++)
			{
				a.WriteByte((byte)s[i]);
			}

			return a.ToArray();
		}
	}
}
