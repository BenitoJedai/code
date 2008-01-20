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
            Type IntPtr = w.MySession.ResolveImplementation(typeof(IntPtr));

            FieldInfo FieldList = null;
            FieldInfo FieldTarget = null;
            FieldInfo FieldMethod = null;

            DelegateHint.Resolve(MulticastDelegate, Delegate,
                out FieldList,
                out FieldTarget,
                out FieldMethod
            );

            #region Constructor
            var Constructor = z.GetConstructors().Single();
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
            #endregion

            var _Operators = IntPtr.GetExplicitOperators(IntPtr, null);

            var _IntPtr_string = _Operators.Single(i => i.ReturnType == typeof(string));
            var _IntPtr_Function = _Operators.Single(i => i.ReturnType != typeof(string));


            #region Invoke
            var Invoke = z.GetMethod("Invoke");
            w.WriteMethodSignature(Invoke, false);
            using (w.CreateScope())
            {
                #region var target:Object
                w.WriteIdent();
                w.Write("var target");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(typeof(object), false, false);
                w.Write(";");
                w.WriteLine();
                #endregion

                #region var target:Object
                w.WriteIdent();
                w.Write("var method");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(IntPtr, false, false);
                w.Write(";");
                w.WriteLine();
                #endregion


                #region var target:Object
                w.WriteIdent();
                w.Write("var field");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(typeof(string), false, false);
                w.Write(";");
                w.WriteLine();
                #endregion

                if (Invoke.ReturnType != typeof(void))
                {
                    w.WriteIdent();

                    w.Write("var val");
                    w.Write(":");
                    w.WriteDecoratedTypeNameOrImplementationTypeName(Invoke.ReturnType, false, false);
                    w.Write(";");

                    w.WriteLine();
                }

                #region for each (var ptr:* in super.list)
                w.WriteIdent();

                w.Write("for each(");
                w.Write("var ptr");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(z, false, false);
                w.Write(" in ");
                w.Write(FieldList.Name);
                w.Write(")");

                w.WriteLine();
                #endregion

                using (w.CreateScope())
                {
                    #region target = ptr._Target;
                    w.WriteIdent();
                    w.Write("target");
                    w.WriteAssignment();
                    w.Write("ptr");
                    w.Write(".");
                    w.Write(FieldTarget.Name);
                    w.Write(";");
                    w.WriteLine();
                    #endregion

                    #region method = ptr._Method;
                    w.WriteIdent();
                    w.Write("method");
                    w.WriteAssignment();
                    w.Write("ptr");
                    w.Write(".");
                    w.Write(FieldMethod.Name);
                    w.Write(";");
                    w.WriteLine();
                    #endregion

                    #region field = ?;
                    w.WriteIdent();
                    w.Write("field");
                    w.WriteAssignment();
                    w.WriteDecoratedTypeNameOrImplementationTypeName(IntPtr, false, false);
                    w.Write(".");
                    w.WriteDecoratedMethodName(_IntPtr_string, false);
                    w.Write("(");
                    w.Write("method");
                    w.Write(")");
                    w.Write(";");
                    w.WriteLine();
                    #endregion

                    w.WriteIdent();
                    
                    if (Invoke.ReturnType != typeof(void))
                    {
                        w.Write("val");
                        w.WriteAssignment();
                    }

                    w.Write("(");
                    w.Write("field == null ? ");

                    
                    #region IntPtr -> Function
                    w.WriteDecoratedTypeNameOrImplementationTypeName(IntPtr, false, false);
                    w.Write(".");
                    w.WriteDecoratedMethodName(_IntPtr_Function, false);
                    w.Write("(");
                    w.Write("method");
                    w.Write(")");
                    #endregion

                    w.Write(":");

                    w.Write("target");
                    w.Write("[");
                    w.Write("field");
                    w.Write("]");

                    w.Write(")");
                    w.Write(".");
                    w.Write("apply(");
                    w.Write("target");
                    w.Write(",");
                    w.Write("arguments");
                    w.Write(")");
                    w.Write(";");
                    
                    w.WriteLine();
                     
                }

                if (Invoke.ReturnType != typeof(void))
                {
                    w.WriteIdent();
                    w.Write("return ");
                    w.Write("val");
                    w.Write(";");

                    w.WriteLine();
                }
            }
            w.WriteLine();
            #endregion
        }
    }
}
