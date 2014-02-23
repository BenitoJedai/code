using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using System.IO;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{
    [Script(Implements = typeof(global::System.Text.UTF8Encoding))]
    internal class __UTF8Encoding : __Encoding
    {
        public override string GetString(byte[] bytes)
        {
            // X:\jsc.svn\examples\javascript\Test\TestUTF8GetStringPerformance\TestUTF8GetStringPerformance\Application.cs
            // UTF8.GetString { Length = 1989731 }

            //            String.fromCharCode(40, 41)
            //"()"

            var w = new StringBuilder();

            // return String.fromCharCode(i);


            // { ElapsedMilliseconds = 13, Length = 1000 }
            // { ElapsedMilliseconds = 21, Length = 16384 }
            // { ElapsedMilliseconds = 33, Length = 65536 }
            // GetString { Length = 131072 }
            //bytes = new byte[bytes.Length];

            // etString { Length = 131072 }
            //bytes = new byte[131072];

            //var a = (IArray<byte>)(object)bytes;
            //a.
            //Console.WriteLine("GetString " + new { bytes.Length });

            // { ElapsedMilliseconds = 20, Length = 65536 }

            var r = new MemoryStream(bytes);
            // https://code.google.com/p/chromium/issues/detail?id=56588
            var chunk = new byte[0x10000];

            var ok = true;
            var s = "";

            while (ok)
            {


                var len = r.Read(chunk, 0, (int)chunk.Length);

                if (len > 0)
                {
                    var cm = new MemoryStream();
                    cm.Write(chunk, 0, len);

                    //Console.WriteLine("GetString chunk " + new { cm.Length });


                    var args = (object[])(object)cm.ToArray();


                    // http://jsperf.com/string-fromcharcode-apply-vs-string-fromcharcode-using-
                    //  message: "Maximum call stack size exceeded"
                    var f = (IFunction)new IFunction("return String.fromCharCode;").apply(null);

                    s += (string)f.apply(null, args);
                }
                else
                {
                    ok = false;
                }
            }

            //for (int i = 0; i < bytes.Length; i++)
            //{

            //    w.Append(__String.FromCharCode(bytes[i]));
            //}

            //var s = w.ToString();

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
