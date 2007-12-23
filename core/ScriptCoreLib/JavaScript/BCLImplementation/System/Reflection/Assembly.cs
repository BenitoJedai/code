using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Reflection
{
    [Script(Implements = typeof(global::System.Reflection.Assembly))]
    internal class __Assembly
    {
        public __AssemblyName[] GetReferencedAssemblies()
        {
            throw new NotImplementedException();
        }

        public virtual __Type[] GetTypes()
        {
            throw new NotImplementedException();
        }
    }
}
