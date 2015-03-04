using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{
    // http://referencesource.microsoft.com/#mscorlib/system/text/stringbuilder.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Text/StringBuilder.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Text/StringBuilder.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Text/StringBuilder.cs
    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Text/StringBuilder.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Text\StringBuilder.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Text\StringBuilder.cs

    // X:\opensource\github\WootzJs\WootzJs.Runtime\Text\StringBuilder.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System.Web.Extensions/System.Web.Script.Serialization/StringBuilderExtensions.cs


    [Script(Implements = typeof(global::System.Text.StringBuilder))]
    internal class __StringBuilder
    {
		// https://github.com/dotnet/coreclr/blob/master/src/classlibnative/bcltype/stringbuffer.cpp
		// https://github.com/dotnet/coreclr/blob/master/src/classlibnative/bcltype/stringbuffer.h

		// anonymous types no longer use it?

		public __StringBuilder()
        {

        }

        public virtual int Capacity
        {
            get
            {
                return _Value.Length;

            }
            set
            {

            }
        }


        string _Value = "";

        public int Length
        {
            get
            {
                return this._Value.Length;
            }
        }

        public __StringBuilder Append(bool e)
        {
            _Value += e;

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

        public __StringBuilder Append(byte e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(int e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(short e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(long e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(string e)
        {
            _Value += e;

            return this;
        }

        public __StringBuilder Append(char e)
        {
            _Value += new string(e, 1);

            return this;
        }

        public __StringBuilder Append(string value, int startIndex, int count)
        {
            return Append(value.Substring(startIndex, count));
        }


        public __StringBuilder Append(object value)
        {
            if (value != null)
            {
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

        public __StringBuilder AppendLine(string value, int startIndex, int count)
        {
            return Append(value.Substring(startIndex, count)).AppendLine();
        }

        public override string ToString()
        {
            return _Value;
        }
    }

}
