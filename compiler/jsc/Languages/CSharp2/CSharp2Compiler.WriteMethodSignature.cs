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
        public override void WriteMethodSignature(System.Reflection.MethodBase m, bool dStatic)
        {
            WriteMethodSignature(m, dStatic, WriteMethodSignatureMode.Delcaring);
        }

        protected enum WriteMethodSignatureMode
        {
            Delcaring,
            Implementing,
            Overriding,
            OverridingImplementing,
        }

        protected void WriteMethodSignature(System.Reflection.MethodBase m, bool dStatic, WriteMethodSignatureMode mode)
        {
            WriteMethodSignature(m, dStatic, mode, null, null, m);
        }

        protected void WriteMethodSignature(System.Reflection.MethodBase m, bool dStatic, WriteMethodSignatureMode mode, ILFlow.StackItem[] DefaultValues, Action<Action> AddDefaultVariableInitializer, System.Reflection.MethodBase _ParamSignature)
        {

            var DeclaringType = m.DeclaringType;
            var TypeScriptAttribute = DeclaringType.ToScriptAttribute();

            DebugBreak(m.ToScriptAttributeOrDefault());

            var IsNativeTarget = TypeScriptAttribute != null && TypeScriptAttribute.IsNative;

            WriteIdent();

            var prop = new PropertyDetector(m);
            var IsSet = //!DeclaringType.IsInterface &&
                    !dStatic &&
                    prop.SetProperty != null &&
                    prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1;
            var IsGet = //!DeclaringType.IsInterface &&
                    !dStatic &&
                    prop.GetProperty != null &&
                    prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0;

            if (IsSet || IsGet)
            {
                // only if its less visible than property's modifier
            }
            else
            {
                if (DeclaringType.IsInterface)
                {
                    if (mode == WriteMethodSignatureMode.Implementing)
                    {
                        WriteKeywordSpace(Keywords._public);
                    }
                }
                else
                {
                    if (m.IsPublic || m.IsConstructor)
                        WriteKeywordSpace(Keywords._public);
                    else
                        if (m.IsFamily)
                            WriteKeywordSpace(Keywords._protected);
                        else
                        {
                            WriteKeywordSpace(Keywords._internal);
                        }
                }
            }


            var IsOverride = false;

            if (mode == WriteMethodSignatureMode.Overriding)
            {
                WriteKeywordSpace(Keywords._override);
            }
            else if (mode == WriteMethodSignatureMode.OverridingImplementing)
            {
                WriteKeywordSpace(Keywords._public);
                WriteKeywordSpace(Keywords._override);
            }
            else
            {
                var z = m.DeclaringType;

                if (z.BaseType != null)
                {
                    var vm = z.BaseType.GetMethod(
                        m.Name,
                        BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public,
                        null,
                        m.GetParameters().Select(i => i.ParameterType).ToArray(),
                        null
                    );

                    if (vm != null)
                    {
                        var InterfaceMethodDeclaringType =
                             z.BaseType.IsGenericType ?
                             z.BaseType.GetGenericTypeDefinition() :
                             z.BaseType
                             ;


                        var InterfaceMethodImplementationSignature =
                            (MethodInfo)MySession.ResolveImplementation(InterfaceMethodDeclaringType, vm,
                            AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveMethodOnly
                            //AssamblyTypeInfo.ResolveImplementationDirectMode.ResolveBCLImplementation
                            ) ?? vm;

                        IsOverride = true;
                        WriteKeywordSpace(Keywords._override);
                    }
                }


            }

            // virtual?

            if (!m.DeclaringType.IsInterface && !m.DeclaringType.IsSealed)
                if (!IsSet && !IsGet)
                    if (m.IsVirtual && !IsOverride)
                        WriteKeywordSpace(Keywords._virtual);

            if (m.IsStatic || dStatic)
                WriteKeywordSpace(Keywords._static);




            if (IsSet || IsGet)
            {
                // property has already defied the type
            }
            else
            {
                #region ReturnType
                var mi = (_ParamSignature ?? m) as MethodInfo;

                if (mi != null)
                {
                    DebugBreak(mi.ToScriptAttributeOrDefault());

                    var ReturnType = mi.ReturnType;

                    WriteGenericTypeName(DeclaringType, ReturnType);
                    WriteSpace();
                }
                #endregion
            }

            var MethodHasParameterList = true;

            if (m.IsConstructor)
                Write(GetDecoratedTypeName(m.DeclaringType, false));
            else
            {
                if (IsSet)
                {
                    WriteKeyword(Keywords._set);

                    MethodHasParameterList = false;
                }
                else if (IsGet)
                {
                    WriteKeyword(Keywords._get);

                    MethodHasParameterList = false;
                }
                else if (IsNativeTarget)
                {
                    Write(m.Name);
                }
                else
                {

                    WriteDecoratedMethodName(m, false);
                    WriteGenericTypeParameters(m.DeclaringType, m);
                }
            }

            if (MethodHasParameterList)
            {
                Write("(");
                WriteMethodParameterList(_ParamSignature ?? m, DefaultValues, AddDefaultVariableInitializer);
                Write(")");
            }
            else
            {

            }



            if (mode == WriteMethodSignatureMode.Delcaring)
                if (DeclaringType.IsInterface)
                    Write(";");

            WriteLine();
        }

        private void WriteQualifiedTypeName(Type context, Type subject)
        {
            WriteDecoratedTypeNameOrImplementationTypeName(subject, true, true,
                IsFullyQualifiedNamesRequired(context, subject), WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);
        }



    }
}
