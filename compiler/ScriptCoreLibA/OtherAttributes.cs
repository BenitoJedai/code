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

    [global::System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptDelegateDataHintAttribute : Attribute
    {
        // this class shall be omitted from the future versions of jsc
        // jsc needs to infer this information by itself

        public enum FieldType
        {
            List,
            Target,
            Method
        }

        public readonly FieldType Value;

        public ScriptDelegateDataHintAttribute(FieldType Value)
        {
            this.Value = Value;
        }
    }

    [global::System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptMethodThrows : Attribute
    {
        public Type ThrowType;

        public ScriptMethodThrows(Type e)
        {
            ThrowType = e;
        }

        public static ScriptMethodThrows[] ArrayOfProvider(ICustomAttributeProvider m)
        {
            try
            {
                ScriptMethodThrows[] s = m.GetCustomAttributes(typeof(ScriptMethodThrows), false) as ScriptMethodThrows[];

                return s;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
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
