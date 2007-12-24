using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script]
    internal class __AssemblyValue
    {
        internal string FullName;

        internal object[] Types;

        internal __AssemblyValue[] References;
        internal __AssemblyNameValue Name;
    }

    [Script(Implements = typeof(global::System.Reflection.Assembly))]
    internal class __Assembly
    {
        public virtual AssemblyName GetName()
        {
            return (AssemblyName)(object)new __AssemblyName { __NameValue = __Value.Name };
        }

        internal __AssemblyValue __Value;

        public __AssemblyName[] GetReferencedAssemblies()
        {
            var z = __Value.References;
            var x = new __AssemblyName[z.Length];

            for (int i = 0; i < z.Length; i++)
            {
                x[i] = new __AssemblyName { __Value = z[i] };
            }

            return x;
        }

        public static __Assembly Load(AssemblyName assemblyRef)
        {
            var x = (__AssemblyName)(object)assemblyRef;

            if (x.__Value == null)
                throw new Exception("Cannot load this assembly");

            return new __Assembly { __Value = x.__Value };
        }

        public virtual __Type[] GetTypes()
        {
            var t = this.__Value.Types;
            var x = new __Type[t.Length];

            for (int i = 0; i < t.Length; i++)
            {
                var constructor = Runtime.Expando.Of(t[i]);

                var p = new __RuntimeTypeHandle
                {
                    Value = (IntPtr)(object)constructor.prototype
                };

                x[i] = new __Type
                {
                    TypeHandle = p
                };
            }

            return x;
        }

        public virtual string FullName { get { return this.GetName().FullName; } }
    }
}
