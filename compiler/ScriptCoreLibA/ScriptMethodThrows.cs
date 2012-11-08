using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScriptCoreLib
{
    [global::System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ScriptMethodThrows : Attribute
    {
        public Type ThrowType { get; set; }

        

        public ScriptMethodThrows(Type e)
        {
            this.ThrowType = e;
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

}
