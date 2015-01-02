using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/int16.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Int16.cs

    [Script(Implements = typeof(global::System.Int16))]
    internal class __Int16
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Int16.cs

        [Script(OptimizedCode = "return parseInt(e);")]
        static public __Int16 Parse(string e)
        {
            return default(__Int16);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(__Int16 e)
        {
            return Expando.Compare(this, e);

        }

        [Script(DefineAsStatic = true)]
        public string ToString(string format)
        {
            var value = (short)(object)this;


            var w = new StringBuilder();


            w.Append(value);

            return w.ToString();
        }


    }
}
