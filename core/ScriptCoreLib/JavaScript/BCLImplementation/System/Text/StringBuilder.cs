using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{
    using ScriptCoreLib.JavaScript.DOM;

    [Script(Implements = typeof(global::System.Text.StringBuilder))]
    internal class __StringBuilder
    {
        readonly IArray<string> data = new IArray<string>();

        public __StringBuilder()
        {

        }

        public __StringBuilder Append(string e)
        {
            if (e != null)
                data.push(e);

            return this;
        }

        public __StringBuilder AppendLine(string e)
        {
            if (e != null)
                data.push(e);

            AppendLine();

            return this;
        }

        public __StringBuilder AppendLine()
        {
            data.push("\r\n");

            return this;
        }


        public __StringBuilder Append(object value)
        {
            if (value == null)
            {
                return this;
            }
            return this.Append(value.ToString());
        }


        public override string ToString()
        {
            return data.join("");
        }



    }

}
