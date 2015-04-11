using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using System.IO;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{
	// http://referencesource.microsoft.com/#mscorlib/system/text/utf8encoding.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Text/UTF8Encoding.cs
	// https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/text/utf8encoding.cs

	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\UTF8Encoding.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Text\UTF8Encoding.cs

	[Script(Implements = typeof(global::System.Text.UTF8Encoding))]
	internal class __UTF8Encoding : __Encoding
	{
		public override string GetString(byte[] bytes, int index, int count)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs
			// X:\jsc.svn\examples\java\async\test\JVMCLRTCPServerAsync\JVMCLRTCPServerAsync\Program.cs

			var copy = new byte[count];

			Array.Copy(
				bytes,
				index,
				copy,
				0,
				count
			);

			return GetString(copy);
		}

		public override string GetString(byte[] bytes)
		{
			// bytes from FromBase64String ?
			// UTF8FromBase64StringOrDefault

			// X:\jsc.svn\examples\vr\VRTurbanPhotosphere\VRTurbanPhotosphere\ApplicationWebService.cs
			//Console.WriteLine("enter __UTF8Encoding.GetString before __fromCharCode " + new { bytes.Length });


			// tested again?
			var s = __String.__fromCharCode(bytes);

			if (string.IsNullOrEmpty(s))
				return s;

			//Console.WriteLine("__UTF8Encoding.GetString after __fromCharCode, before escape " + new { s });


			#region roslyn_
			//StringConversions.UTF8FromBase64StringOrDefault {{ Length = 84, e = PGgxPkpTQyAtIFRoZSAuTkVUIGNyb3NzY29tcGlsZXIgZm9yIHdlYiBwbGF0Zm9ybXMuIHJlYWR5LjwvaDE+ }}
			//StringConversions.UTF8FromBase64StringOrDefault before Convert.FromBase64String 
			//enter __Convert.FromBase64String {{ Length = 84 }}

			//exit __Convert.FromBase64String {{ Length = 63 }}

			//StringConversions.UTF8FromBase64StringOrDefault before Encoding.UTF8.GetString {{ Length = 63 }}

			//enter __UTF8Encoding.GetString before __fromCharCode {{ Length = 63 }}
			//enter __String.__fromCharCode {{ Length = 63 }}
			//exit __String.__fromCharCode {{ s = <h1>JSC - The .NET crosscompiler for web platforms. ready.</h1> }}
			//__UTF8Encoding.GetString after __fromCharCode, before escape {{ s = <h1>JSC - The .NET crosscompiler for web platforms. ready.</h1> }}
			#endregion

			#region preroslyn

			//enter StringConversions.UTF8FromBase64StringOrDefault {{ Length = 84, e = PGgxPkpTQyAtIFRoZSAuTkVUIGNyb3NzY29tcGlsZXIgZm9yIHdlYiBwbGF0Zm9ybXMuIHJlYWR5LjwvaDE+ }}
			// StringConversions.UTF8FromBase64StringOrDefault before Convert.FromBase64String 
			// enter __Convert.FromBase64String { Length = 84 }

			// exit __Convert.FromBase64String { Length = 31 }

			// StringConversions.UTF8FromBase64StringOrDefault before Encoding.UTF8.GetString {{ Length = 31 }}
			// enter __UTF8Encoding.GetString before __fromCharCode { Length = 31 }
			// enter __String.__fromCharCode { Length = 31 }
			// exit __String.__fromCharCode { s = ÊÑhJé-óÚÑ¬^	²X°I²^áå[ï }
			// __UTF8Encoding.GetString after __fromCharCode, before escape { s = ÊÑhJé-óÚÑ¬^	²X°I²^áå[ï }
			// __UTF8Encoding.GetString after escape, before decodeURIComponent { ss = %1B%19%13%CA%D1hJ%E9%14%1B-%F3%DA%D1%AC%5E%09%B2%1EX%B0%1BI%B2%5E%E1%E5%5B%98%EF%0F }

			#endregion

			var ss = Native.escape(s);

			//Console.WriteLine("__UTF8Encoding.GetString after escape, before decodeURIComponent " + new { ss });

			var value = Native.decodeURIComponent(ss);

			return value;

		}

		public override byte[] GetBytes(string e)
		{
			// http://ecmanaut.blogspot.com/2006/07/encoding-decoding-utf8-in-javascript.html
			// http://msdn.microsoft.com/en-us/library/ie/aeh9cef7(v=vs.94).aspx
			// X:\jsc.svn\examples\javascript\Test\TestUTF8StringToService\TestUTF8StringToService\ApplicationWebService.cs

			// X:\jsc.svn\examples\javascript\test\TestdecodeURIComponent\TestdecodeURIComponent\ApplicationWebService.cs

			var a = new MemoryStream();

			if (!string.IsNullOrEmpty(e))
			{
				var ss = Native.encodeURIComponent(e);
				var s = Native.unescape(ss);

				// unescape(encodeURIComponent("€")) === "\xE2\x82\xAC" and 
				//decodeURIComponent(escape("\xE2\x82\xAC")) === "€" both return true, as they they are supposed to.


				for (int i = 0; i < s.Length; i++)
				{
					a.WriteByte((byte)s[i]);
				}
			}

			return a.ToArray();
		}
	}
}
