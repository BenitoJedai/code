using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Text
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

        public static Encoding UTF8
        {
            get
            {
                // cache?
                return new UTF8Encoding();
            }
        }

        public static Encoding GetEncoding(string name)
        {
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLR1252Encoding\TestJVMCLR1252Encoding\Program.cs
            return (Encoding)(object)new xEncoding
            {

                EncodingName = name
            };
        }
    }

    [Script]
    class xEncoding : __Encoding
    {
        public string EncodingName;

        public override string GetString(byte[] bytes)
        {
            var r = default(string);

            try
            {
                var _bytes = (sbyte[])(object)bytes;

                r = (string)(object)new java.lang.String(_bytes, EncodingName);
            }
            catch
            {
                throw;
            }
            return r;

        }

        public override byte[] GetBytes(string s)
        {
            var r = default(byte[]);

            try
            {

                r = (byte[])(object)((java.lang.String)(object)s).getBytes(EncodingName);

            }
            catch
            {
                throw;
            }
            return r;
        }
    }

}
