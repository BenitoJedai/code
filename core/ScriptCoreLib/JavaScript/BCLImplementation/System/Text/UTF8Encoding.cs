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
    // https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/text/utf8encoding.cs


    [Script(Implements = typeof(global::System.Text.UTF8Encoding))]
    internal class __UTF8Encoding : __Encoding
    {
        public override string GetString(byte[] bytes)
        {
            // tested again?
            var s = __String.__fromCharCode(bytes);

            if (string.IsNullOrEmpty(s))
                return s;

            var ss = Native.escape(s);

            return Native.decodeURIComponent(ss);

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
