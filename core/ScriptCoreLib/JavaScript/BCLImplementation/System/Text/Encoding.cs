using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{
	// http://referencesource.microsoft.com/#mscorlib/system/text/encoding.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Text/Encoding.cs
	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Text/Encoding.cs

	[Script(Implements = typeof(global::System.Text.Encoding))]
    internal abstract class __Encoding
    {
		// https://github.com/dotnet/corefx/tree/master/src/System.Text.Encoding.CodePages/src/System/Text

		public virtual string GetString(byte[] bytes, int index, int count)
		{
			// X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerAsync\ChromeTCPServerAsync\Application.cs

			return default(string);
		}

		public virtual string GetString(byte[] bytes)
        {
            return default(string);
        }

        public virtual byte[] GetBytes(string s)
        {
            return default(byte[]);
        }

        [Obsolete("Not Supported By Portable Class Library")]
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
    }
}
