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
		const string IsExtensionMethod = "IsExtensionMethod";
		public const string AsExtensionMethod = "AsExtensionMethod";

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




			const string Local_a = "_arguments";


            #region Invoke
            var Invoke = z.GetMethod("Invoke");
            w.WriteMethodSignature(Invoke, false);
            using (w.CreateScope())
            {
                #region var target:Object
                w.WriteIdent();
                w.Write("var target");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(typeof(object), false, false, w.IsFullyQualifiedNamesRequired(z, typeof(object)));
                w.Write(";");
                w.WriteLine();
                #endregion

				#region var method:Object
				w.WriteIdent();
                w.Write("var method");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(IntPtr, false, false, w.IsFullyQualifiedNamesRequired(z, IntPtr));
                w.Write(";");
                w.WriteLine();
                #endregion


				#region var field:Object
				w.WriteIdent();
                w.Write("var field");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(typeof(string), false, false, w.IsFullyQualifiedNamesRequired(z, typeof(string)));
                w.Write(";");
                w.WriteLine();
                #endregion

				#region ReturnType
				if (Invoke.ReturnType != typeof(void))
                {
                    w.WriteIdent();

					w.WriteKeywordSpace(ActionScriptCompiler.Keywords._var);
                    w.Write("val");
                    w.Write(":");
                    w.WriteDecoratedTypeNameOrImplementationTypeName(Invoke.ReturnType, false, false, w.IsFullyQualifiedNamesRequired(z, Invoke.ReturnType));
                    w.Write(";");

                    w.WriteLine();
				}
				#endregion


				#region for each (var ptr:* in super.list)
				w.WriteIdent();

                w.Write("for each(");
                w.Write("var ptr");
                w.Write(":");
                w.WriteDecoratedTypeNameOrImplementationTypeName(z, false, false, w.IsFullyQualifiedNamesRequired(z, z));
                w.Write(" in ");
                w.Write(FieldList.Name);
                w.Write(")");

                w.WriteLine();
                #endregion

                using (w.CreateScope())
				{
					#region target = IsExtensionMethod ? null :  ptr._Target;
					w.WriteIdent();
                    w.Write("target");
                    w.WriteAssignment();

					w.WriteKeyword(ActionScriptCompiler.Keywords._this);
					w.Write(".");
					w.Write(IsExtensionMethod);

					w.WriteSpace();

					w.Write("?");
					w.WriteSpace();

					w.Write("null");
					w.WriteSpace();

					w.Write(":");
					w.WriteSpace();

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
                    w.WriteDecoratedTypeNameOrImplementationTypeName(IntPtr, false, false, w.IsFullyQualifiedNamesRequired(z, IntPtr));
                    w.Write(".");
                    w.WriteDecoratedMethodName(_IntPtr_string, false);
                    w.Write("(");
                    w.Write("method");
                    w.Write(")");
                    w.Write(";");
                    w.WriteLine();
                    #endregion


					#region var Local_a
					w.WriteIdent();
					w.WriteKeywordSpace(ActionScriptCompiler.Keywords._var);
					w.Write(Local_a);
					w.Write(":");
					w.WriteDecoratedTypeName(typeof(object[]));
					w.WriteAssignment();
					w.WriteKeyword(ActionScriptCompiler.Keywords._arguments);
					w.Write(".");
					w.Write("slice(0)");
					w.Write(";");
					w.WriteLine();
					#endregion

					w.WriteIdent();
					w.WriteKeywordSpace(ActionScriptCompiler.Keywords._if);

					w.Write("(");
					w.WriteKeyword(ActionScriptCompiler.Keywords._this);
					w.Write(".");
					w.Write(IsExtensionMethod);
					w.Write(")");
					w.WriteSpace();
					w.Write(Local_a);
					w.Write(".");
					w.Write("splice(0, 0, ");
					w.Write("ptr");
					w.Write(".");
					w.Write(FieldTarget.Name);
					w.Write(")");
					
					w.Write(";");
					w.WriteLine();

					w.WriteIdent();
                    
                    if (Invoke.ReturnType != typeof(void))
                    {
                        w.Write("val");
                        w.WriteAssignment();
                    }

                    w.Write("(");
                    w.Write("field == null ? ");

                    
                    #region IntPtr -> Function
                    w.WriteDecoratedTypeNameOrImplementationTypeName(IntPtr, false, false, w.IsFullyQualifiedNamesRequired(z, IntPtr));
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
                    w.Write(Local_a);
                    w.Write(")");
                    w.Write(";");
                    
                    w.WriteLine();
                     
                }

                if (Invoke.ReturnType != typeof(void))
                {
                    w.WriteIdent();
                    w.WriteKeywordSpace(ActionScriptCompiler.Keywords._return);
                    w.Write("val");
                    w.Write(";");

                    w.WriteLine();
                }
            }
            w.WriteLine();
            #endregion

			#region IsExtensionMethod
			w.WriteIdent();
			w.WriteKeywordSpace(ActionScriptCompiler.Keywords._private);
			w.WriteKeywordSpace(ActionScriptCompiler.Keywords._var);
			w.Write(IsExtensionMethod);
			w.Write(":");
			w.WriteDecoratedTypeName(typeof(bool));
			w.Write(";");
			w.WriteLine();
			#endregion

			#region AsExtensionMethod
			w.WriteIdent();
			w.WriteKeywordSpace(ActionScriptCompiler.Keywords._public);
			w.WriteKeywordSpace(ActionScriptCompiler.Keywords._function);
			w.Write(AsExtensionMethod);
			w.Write("()");
			w.Write(":");
			w.WriteDecoratedTypeName(z);
			w.WriteLine();
			using (w.CreateScope())
			{
				w.WriteIdent();
				w.WriteKeyword(ActionScriptCompiler.Keywords._this);
				w.Write(".");
				w.Write(IsExtensionMethod);
				w.WriteAssignment();
				w.WriteKeyword(ActionScriptCompiler.Keywords._true);
				w.Write(";");
				w.WriteLine();

				w.WriteIdent();
				w.WriteKeywordSpace(ActionScriptCompiler.Keywords._return);
				w.WriteKeyword(ActionScriptCompiler.Keywords._this);
				w.Write(";");
				w.WriteLine();
			}
			#endregion

		}
    }
}
