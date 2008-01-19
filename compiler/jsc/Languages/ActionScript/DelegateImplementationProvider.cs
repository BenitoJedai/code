using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

namespace jsc.Languages.ActionScript
{
    static class DelegateImplementationProvider
    {
        public static void WriteDelegate(this ActionScriptCompiler w, Type z, ScriptAttribute za)
        {
            Type MulticastDelegate = w.MySession.ResolveImplementation(z.BaseType);
            Type Delegate = w.MySession.ResolveImplementation(z.BaseType.BaseType);

            FieldInfo FieldList = null;
            FieldInfo FieldTarget = null;
            FieldInfo FieldMethod = null;

            DelegateHint.Resolve(MulticastDelegate, Delegate,
                out FieldList,
                out FieldTarget,
                out FieldMethod
            );

            var Constructor = z.GetConstructors().Single();
            var Invoke = z.GetMethod("Invoke");

            w.WriteMethodSignature(Constructor, false);
            using (w.CreateScope())
            {
                w.WriteIdent();
                w.Write("super("
                    + Constructor.GetParameters()[0].Name + ", "
                    + Constructor.GetParameters()[1].Name + ");"
                );
                w.WriteLine();
            }
            w.WriteLine();

            w.WriteMethodSignature(Invoke, false);
            using (w.CreateScope())
            {
                // Invoke(args);
            }
            w.WriteLine();
        }
    }
}
