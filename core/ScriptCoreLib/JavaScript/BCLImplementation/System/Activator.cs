using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Activator))]
    internal class __Activator
    {

        public static object CreateInstance(Type e)
        {
            // X:\jsc.svn\examples\javascript\forms\test\TestTypeActivatorRef\TestTypeActivatorRef\Class1.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140409
            // X:\jsc.svn\examples\javascript\test\TestTypeActivator\TestTypeActivator\Application.cs
            // No parameterless constructor defined for this object.

            // is jsc actually marking a consctructor ref for us?


            var ctor = (IFunction)Expando.InternalGetMember(
                ((__Type)e).AsExpando().constructor, "ctor");

            //var prototype = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(e.TypeHandle.Value);

            //var ctor = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(prototype.constructor).GetMember<IFunction>("ctor");

            //if (ctor == null)
            //    throw new NotSupportedException(e.Name);

            return ctor.CreateType();
        }
    }
}
