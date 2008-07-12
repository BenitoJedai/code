using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, System.Reflection.MethodBase m)
        {


            // remove the base call for now

            var TargetMethod = m;
            var MethodScriptAttribute = TargetMethod.ToScriptAttribute();

            if (ScriptAttribute.IsAnonymousType(TargetMethod.DeclaringType))
            {
                // nop
            }
            else
            {
                if (MethodScriptAttribute != null && MethodScriptAttribute.NotImplementedHere)
                {
                    // a native type has a method that is defined by scriptcorelib
                    // the implementation must be in some other type that is not native
                    // we need to find that type

                    TargetMethod = MySession.ResolveImplementation(m.DeclaringType, TargetMethod, AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveNativeImplementationExtension);
                    MethodScriptAttribute = TargetMethod.ToScriptAttribute();
                }
                else
                {
                    TargetMethod = ResolveMethod(m) ?? m;
                    MethodScriptAttribute = TargetMethod.ToScriptAttribute();
                }
            }

            var TypeScriptAttribute = TargetMethod.DeclaringType.ToScriptAttribute();





            var IsDefineAsStatic = MethodScriptAttribute != null && MethodScriptAttribute.DefineAsStatic;
            var HasMethodExternalTarget = MethodScriptAttribute != null && MethodScriptAttribute.ExternalTarget != null;

            Action WriteMethodName =
                delegate
                {
                    if (TypeScriptAttribute != null && TypeScriptAttribute.IsNative)
                        Write(TargetMethod.Name);
                    else
                        if (HasMethodExternalTarget)
                            Write(MethodScriptAttribute.ExternalTarget);
                        else
                            WriteDecoratedMethodName(TargetMethod, false);

                };

            var IsBaseConstructorCall = i.IsBaseConstructorCall(TargetMethod, ResolveMethod);

            var s = i.StackBeforeStrict;
            var offset = 1;

            if (TargetMethod.IsStatic || IsDefineAsStatic)
            {
                if (IsDefineAsStatic)
                    offset = 1;
                else
                    offset = 0;
            }

            #region WritePropertyAssignment
            Action<PropertyDetector> WritePropertyAssignment =
                prop =>
                {
                    WriteAssignment();

                    #region bool
                    if (prop.SetProperty.PropertyType == typeof(bool))
                    {
                        if (s[1].StackInstructions.Length == 1)
                        {
                            if (s[1].SingleStackInstruction.TargetInteger == 0)
                            {
                                Write("false");
                                return;
                            }

                            if (s[1].SingleStackInstruction.TargetInteger == 1)
                            {
                                Write("true");
                                return;
                            }
                        }
                    }
                    #endregion

                    Emit(p, s[1]);
                };
            #endregion


            if (TargetMethod.IsStatic || IsDefineAsStatic)
            {
                WriteDecoratedTypeName(i.OwnerMethod.DeclaringType);
                    //, TargetMethod.DeclaringType WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);
                Write(".");

                #region prop
                if (!IsDefineAsStatic)
                {


                    var prop = new PropertyDetector(TargetMethod);


                    if (prop.SetProperty != null && prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1)
                    {

                        WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.SetProperty.Name);
                        WritePropertyAssignment(prop);

                        return;
                    }

                    if (prop.GetProperty != null && prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0)
                    {
                        WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.GetProperty.Name);
                        return;
                    }
                }
                #endregion

                WriteMethodName();


            }
            else
            {
                if (IsBaseConstructorCall)
                {
                    DebugBreak(p.DeclaringMethod.ToScriptAttribute());

                    Write("super");
                }
                else
                {
                    if (!i.OwnerMethod.IsStatic && i.OwnerMethod.DeclaringType.IsSubclassOf(TargetMethod.DeclaringType) && s[0].SingleStackInstruction.OpCode == OpCodes.Ldarg_0)
                    {
                        Write("super");
                    }
                    else
                    {
                        Emit(p, s[0]);
                    }

                    if (TargetMethod.Name == "get_Item"
                        && TargetMethod.DeclaringType.ToScriptAttributeOrDefault().IsNative
                        && TargetMethod.GetParameters().Length == 1)
                    {
                        // call with and indexer... possibly an array or xml list
                        Write("[");

                        WriteParameters(p, TargetMethod, s, offset, TargetMethod.GetParameters(), false, ",");

                        Write("]");

                        return;
                    }


                    Write(".");


                    #region prop
                    if (!TargetMethod.DeclaringType.IsInterface)
                    {
                        var prop = new PropertyDetector(TargetMethod);

                        if (prop.SetProperty != null &&
                            (HasMethodExternalTarget ||
                                (prop.SetProperty.GetSetMethod(true) != null &&
                                 prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1
                                )
                            ))
                        {
                            WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.SetProperty.Name);
                            WritePropertyAssignment(prop);
                            return;
                        }

                        if (prop.GetProperty != null &&
                             (HasMethodExternalTarget ||
                                (prop.GetProperty.GetGetMethod(true) != null &&
                                 prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0
                                )
                            ))
                        {
                            WriteSafeLiteral(HasMethodExternalTarget ? MethodScriptAttribute.ExternalTarget : prop.GetProperty.Name);
                            return;
                        }
                    }
                    #endregion

                    WriteMethodName();
                }
            }

            WriteParameterInfoFromStack(TargetMethod, p, s, offset);
        }

    }
}
