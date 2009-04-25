using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
    {
        private void WriteVirtualMethodOverrides(Type z)
        {
            // override: http://blogs.adobe.com/kiwi/2006/05/as3_language_101_for_cc_coders_1.html


            foreach (var tmethod in z.GetVirtualMethods())
            {
                if (tmethod.IsToString())
                    continue;



                var sa = tmethod.DeclaringType.ToScriptAttribute();

                if (sa == null)
                    continue;

                if (tmethod.ToScriptAttributeOrDefault().DefineAsStatic)
                    continue;

                DebugBreak(tmethod.ToScriptAttributeOrDefault());


                var iparams = tmethod.GetParameters();
                var iparamstypes = tmethod.GetParameters().Select(p => p.ParameterType).ToArray();

                var prop = new PropertyDetector(tmethod);
                var IsSet = iparams.Length == 1
                            && prop.SetProperty != null
                            && prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1;
                var IsGet = prop.GetProperty != null
                              && prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0;

                if (IsSet || IsGet)
                {
                    WriteIdent();
                    WriteCommentLine("override a virtual property by " + tmethod.Name);

                    continue;
                }

                var InterfaceMethodDeclaringType =
                    z.BaseType.IsGenericType ?
                    z.BaseType.GetGenericTypeDefinition() :
                    z.BaseType
                    ;

				Func<Type, MethodInfo> GetMethod =
					basetype =>
						basetype.GetMethod(
							tmethod.Name,
							BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public,
							null,
							iparamstypes,
							null
						);

				#region find method to overload
				var basetypep = z.BaseType;
			next_basetype:
				var vm = GetMethod(basetypep);
				if (vm == null && basetypep.BaseType != null)
				{
					basetypep = basetypep.BaseType;
					goto next_basetype;
				}
				#endregion


				var InterfaceMethodImplementationSignature = (MethodInfo)MySession.ResolveImplementation(InterfaceMethodDeclaringType, vm,
                    AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveMethodOnly
                    //AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveBCLImplementation
                    ) ?? vm;

                if (vm == null)
                {
                    throw new NotImplementedException("cannot find override for " + tmethod.ToString());
                }

                WriteIdent();
                WriteCommentLine("override a virtual member");

                WriteMethodSignature(InterfaceMethodImplementationSignature, false, WriteMethodSignatureMode.Overriding);

				// get correct names for params
				iparams = InterfaceMethodImplementationSignature.GetParameters();

                using (CreateScope())
                {
                    WriteIdent();

                    if (vm.ReturnType != typeof(void))
                    {
                        WriteKeywordReturn();
                        WriteSpace();
                    }

                    WriteThisReference();
                    Write(".");

                    // tmethod =
                    #region prop
                    {
                        if (IsSet)
                        {
                            Write(prop.SetProperty.Name);
                            WriteAssignment();
                            WriteSafeLiteral(iparams.Single().Name);
                        }
                        else if (IsGet)
                        {
                            Write(prop.GetProperty.Name);
                        }
                        else
                        {
                            WriteDecoratedMethodName(tmethod, false);
                            Write("(");
                            for (int i = 0; i < iparams.Length; i++)
                            {
                                if (i > 0)
                                {
                                    Write(",");
                                    WriteSpace();
                                }
                                WriteSafeLiteral(iparams[i].Name);
                            }
                            Write(")");
                        }
                    }
                    #endregion

                    Write(";");
                    WriteLine();
                }

            }

        }
    }
}
