using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Text
{
    [Script(Implements = typeof(global::System.Text.StringBuilder))]
    internal class __StringBuilder
    {
        public __StringBuilder()
        {

        }

        public int Length
        {
            get
            {
                return this._Value.Length;
            }
        }


        string _Value = "";

        public __StringBuilder Append(bool e)
        {
            if (e)
                _Value += "true";
            else
                _Value += "false";

            return this;
        }

        public __StringBuilder Append(double e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(uint e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(int e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(long e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(char e)
        {
            _Value += Native.API.chr(e);

            return this;
        }

        public __StringBuilder Append(string e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(object value)
        {
            var ref0 = value;
            var ref1 = ScriptCoreLib.PHP.Runtime.Expando.Of(ref0);

            // does "is int" work?
            if (ref1.IsNumber)
            {
                _Value += (int)value;

                return this;
            }

            // does "is bool" work?
            if (ref1.IsBoolean)
            {

                return this.Append((bool)value);
            }

            if (value is string)
            {
                return this.Append((string)value);
            }

            if (value != null)
            {
                // php strings do not have the __ToString member
                // fixme: should use the is string operator instead



                _Value += value.ToString();
            }

            return this;
        }

        public __StringBuilder AppendLine()
        {
            return Append(Environment.NewLine);
        }

        public __StringBuilder AppendLine(string value)
        {
            return Append(value).AppendLine();
        }


        public override string ToString()
        {
            return _Value;
        }
    }
}
