using System.Runtime.CompilerServices;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using ScriptCoreLib.CSharp.Extensions;

namespace ScriptCoreLib
{



    [global::System.AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptParameterByRefAttribute : Attribute
    {

    }



    [global::System.AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptParameterByValAttribute : Attribute
    {

    }








    /// <summary>
    /// allows the compiler to detect wether it is out of date. If this value is higher than the one from the compiler the compile proccess fill halt with an error.
    /// </summary>
    [Obsolete]
    [global::System.AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptVersionAttribute : Attribute
    {
        public int Value;

        public ScriptVersionAttribute(int e)
        {
            this.Value = e;
        }

    }
}
