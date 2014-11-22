using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Text
{
    // http://referencesource.microsoft.com/#mscorlib/system/text/stringbuilder.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Text/StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Text\StringBuilder.cs

    [Script(Implements = typeof(global::System.Text.StringBuilder))]
    internal class __StringBuilder
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\StringBuilder.cs

        global::java.lang.StringBuffer InternalBuffer;

        public int Capacity
        {
            // http://docs.oracle.com/javase/7/docs/api/java/lang/StringBuffer.html#setLength(int)
            // X:\jsc.svn\examples\java\JVMCLRBase64\JVMCLRBase64\Program.cs
            set
            {
                this.InternalBuffer.ensureCapacity(value);
            }
            get
            {
                return this.InternalBuffer.capacity();
            }
        }

        public __StringBuilder()
        {
            InternalBuffer = new global::java.lang.StringBuffer();
        }

        public __StringBuilder(string v)
            : this()
        {
            Append(v);
        }

        public __StringBuilder AppendLine(string e)
        {
            InternalBuffer.append(e + Environment.NewLine);

            return this;
        }

        public __StringBuilder AppendLine()
        {
            InternalBuffer.append(Environment.NewLine);

            return this;
        }

        public __StringBuilder Append(object e)
        {
            if (e != null)
                InternalBuffer.append(e.ToString());

            return this;
        }

        public __StringBuilder Append(string e)
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Web\HttpCookie.cs
            // X:\jsc.svn\examples\javascript\Test\TestServiceNullStringField\TestServiceNullStringField\ApplicationWebService.cs

            if (!string.IsNullOrEmpty(e))
                InternalBuffer.append(e);

            return this;
        }


        public __StringBuilder Append(long e)
        {
            InternalBuffer.append("" + e);

            return this;
        }

        public __StringBuilder Append(bool e)
        {
            InternalBuffer.append("" + e);

            return this;
        }

        public __StringBuilder Append(int e)
        {
            InternalBuffer.append("" + e);

            return this;
        }

        public __StringBuilder Append(short e)
        {
            InternalBuffer.append("" + e);

            return this;
        }

        public __StringBuilder Append(char e)
        {
            var x = new string(new[] { e });


            return this.Append(x);
        }

        public int Length
        {
            get
            {
                return InternalBuffer.length();
            }
        }

        public override string ToString()
        {
            return InternalBuffer.ToString();
        }

        public __StringBuilder Clear()
        {
            this.InternalBuffer = new java.lang.StringBuffer();

            return this;
        }
    }
}
