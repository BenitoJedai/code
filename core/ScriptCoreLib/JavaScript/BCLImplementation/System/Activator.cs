using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://sourceforge.net/p/jsc/code/HEAD/tree/core/ScriptCoreLib/JavaScript/BCLImplementation/System/Activator.cs
    // http://referencesource.microsoft.com/#mscorlib/system/activator.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Activator.cs


    // X:\opensource\github\SaltarelleCompiler\Runtime\CoreLib\Activator.cs
    // X:\opensource\github\WootzJs\WootzJs.Runtime\Activator.cs

    [Script(Implements = typeof(global::System.Activator))]
    internal class __Activator
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Runtime\Serialization\FormatterServices.cs

        // use by?
        public static object CreateInstance(Type type, params object[] args)
        {
            if ((object)type == null)
                throw new NotSupportedException();


            //  var ctor$dRwABhEmij_awUY_aNs2ypYA = pcI2XBEmij_awUY_aNs2ypYA.ctor = $ctor$(null, 'dRwABhEmij_awUY_aNs2ypYA', type$pcI2XBEmij_awUY_aNs2ypYA);
            //  var ctor$rwAABqwhHjSCn60Jy_bMQpA = $ctor$(null, 'rwAABqwhHjSCn60Jy_bMQpA', type$fAZ65awhHjSCn60Jy_bMQpA);

            // X:\jsc.svn\examples\javascript\test\TestActivatorWithArgs\TestActivatorWithArgs\Application.cs

            // can we get the default ctor with args?
            var ctor = (IFunction)Expando.InternalGetMember(
                ((__Type)type).AsExpando().constructor, "ctor");

            // 0:31ms { type = <Namespace>.Foo, ctor = function (b)


            //Console.WriteLine(new { type, ctor });
            //[Script(OptimizedCode = @"return new f();")]

            // how to apply ctor args correctly?

            if (args.Length == 0)
                return new IFunction("f",
                    "return new f();").apply(null, ctor);

            if (args.Length == 1)
                return new IFunction("f", "a",
                    "return new f(a);").apply(null, ctor, args[0]);

            if (args.Length == 2)
                return new IFunction("f", "a", "b",
                    "return new f(a, b);").apply(null, ctor, args[0], args[1]);

            // manually add more or make it more elegant?
            throw new NotImplementedException();
            //return ctor.CreateType();
        }

        public static object CreateInstance(Type type)
        {
            if ((object)type == null)
                throw new NotSupportedException();

            // X:\jsc.svn\examples\javascript\forms\test\TestTypeActivatorRef\TestTypeActivatorRef\Class1.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140409
            // X:\jsc.svn\examples\javascript\test\TestTypeActivator\TestTypeActivator\Application.cs
            // No parameterless constructor defined for this object.

            // is jsc actually marking a consctructor ref for us?


            var ctor = (IFunction)Expando.InternalGetMember(
                ((__Type)type).AsExpando().constructor, "ctor");

            //var prototype = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(e.TypeHandle.Value);

            //var ctor = global::ScriptCoreLib.JavaScript.Runtime.Expando.Of(prototype.constructor).GetMember<IFunction>("ctor");

            //if (ctor == null)
            //    throw new NotSupportedException(e.Name);

            return ctor.CreateType();
        }
    }
}
