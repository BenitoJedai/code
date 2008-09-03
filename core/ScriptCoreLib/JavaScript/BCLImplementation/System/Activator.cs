using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    
    [Script(Implements = typeof(global::System.Activator))]
    internal class __Activator
    {

        public static object CreateInstance(Type e)
        {
            var prototype = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(e.TypeHandle.Value);

			var ctor = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(prototype.constructor).GetMember<IFunction>("ctor");

            if (ctor == null)
                throw new NotSupportedException(e.Name);

            return ctor.CreateType();
        }
    }
}
