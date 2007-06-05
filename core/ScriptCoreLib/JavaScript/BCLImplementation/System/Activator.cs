using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Activator))]
    internal class __Activator
    {
        public static object CreateInstance(Type e)
        {
            return Runtime.Expando.Of(e.TypeHandle.Value).constructor.CreateType();
        }
    }
}
