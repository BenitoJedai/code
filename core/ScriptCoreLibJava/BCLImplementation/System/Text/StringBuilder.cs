using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Text
{
    [Script(Implements = typeof(global::System.Text.StringBuilder))]
    internal class __StringBuilder
    {
        global::java.lang.StringBuffer InternalBuffer;

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

        public __StringBuilder Append(int e)
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
