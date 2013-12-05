using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using System.IO;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{
    [Script(Implements = typeof(global::System.Text.UTF8Encoding))]
    internal class __UTF8Encoding : __Encoding
    {
        public override string GetString(byte[] bytes)
        {
            var w = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                w.Append(__String.FromCharCode(bytes[i]));
            }

            var s = w.ToString();

            return Native.window.decodeURIComponent(
                Native.window.escape(s)
            );

        }

        public override byte[] GetBytes(string e)
        {
            // http://ecmanaut.blogspot.com/2006/07/encoding-decoding-utf8-in-javascript.html
            // http://msdn.microsoft.com/en-us/library/ie/aeh9cef7(v=vs.94).aspx
            // X:\jsc.svn\examples\javascript\Test\TestUTF8StringToService\TestUTF8StringToService\ApplicationWebService.cs

            var s = Native.window.unescape(
                Native.window.encodeURIComponent(e)
            );

            // unescape(encodeURIComponent("€")) === "\xE2\x82\xAC" and 
            //decodeURIComponent(escape("\xE2\x82\xAC")) === "€" both return true, as they they are supposed to.

            var a = new MemoryStream();

            for (int i = 0; i < s.Length; i++)
            {
                a.WriteByte((byte)s[i]);
            }

            return a.ToArray();
        }
    }
}
