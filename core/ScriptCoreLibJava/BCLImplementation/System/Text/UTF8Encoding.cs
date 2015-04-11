using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibJava.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Text
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
			var r = default(string);

			try
			{
                var _bytes = (sbyte[])(object)bytes;

                r = (string)(object)new java.lang.String(_bytes, "UTF-8");
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

				r = (byte[])(object)((java.lang.String)(object)s).getBytes("UTF-8");

			}
			catch
			{
                throw;
			}
			return r;
		}
	}
}
