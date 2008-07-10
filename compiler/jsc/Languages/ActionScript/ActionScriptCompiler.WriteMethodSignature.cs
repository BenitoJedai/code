using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler
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

            if (DeclaringType.IsInterface)
            {
                if (mode == WriteMethodSignatureMode.Implementing)
                {
                    WriteKeywordSpace(Keywords._public);
                }
            }
            else
            {
                // as3: A constructor can only be declared public.
                if (m.IsPublic || m.IsConstructor)
                    WriteKeywordSpace(Keywords._public);
                else
                    if (m.IsFamily)
                        WriteKeywordSpace(Keywords._protected);
                    else
                    {
                        // cannot use private as it blocks off delegates
                        WriteKeywordSpace(Keywords._internal);
                    }
            }

            var prop = new PropertyDetector(m);
            var IsSet = !DeclaringType.IsInterface &&
                    !dStatic &&
                    prop.SetProperty != null &&
                    prop.SetProperty.GetSetMethod(true).GetParameters().Length == 1;
            var IsGet = !DeclaringType.IsInterface &&
                    !dStatic &&
                    prop.GetProperty != null &&
                    prop.GetProperty.GetGetMethod(true).GetParameters().Length == 0;


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
                // if we are a property and we are overriding... then write here
                if (IsGet || IsSet)
                {

                    var z = m.DeclaringType;
               
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

                        WriteKeywordSpace(Keywords._override);
                    }
                }
            }

            if (m.IsStatic || dStatic)
                WriteKeywordSpace(Keywords._static);


            WriteKeywordSpace(Keywords._function);

            if (m.IsConstructor)
                Write(GetDecoratedTypeName(m.DeclaringType, false));
            else
            {
                

                // actionscript 3 does not support indexers
                // Compiler error. Getters/setters are not allowed in interfaces.
                // http://livedocs.adobe.com/flash/9.0/main/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=00000830.html
                // -> implementing classes need to wrap this

                if (IsSet)
                {
                    WriteKeywordSpace(Keywords._set);
                    WriteSafeLiteral(prop.SetProperty.Name);
                }
                else if (IsGet)
                {
                    WriteKeywordSpace(Keywords._get);
                    WriteSafeLiteral(prop.GetProperty.Name);
                }
                else if (IsNativeTarget)
                {
                    Write(m.Name);
                }
                else
                {

                    WriteDecoratedMethodName(m, false);
                }
            }

            Write("(");
            WriteMethodParameterList(_ParamSignature ?? m, DefaultValues, AddDefaultVariableInitializer);
            Write(")");

            var cctor = m as ConstructorInfo;
            if (cctor != null && cctor.IsStatic)
            {
                Write(":");
                WriteDecoratedTypeName(typeof(void));
            }


            #region ReturnType
            var mi = (_ParamSignature ?? m) as MethodInfo;

            if (mi != null)
            {
                DebugBreak(mi.ToScriptAttributeOrDefault());

                Write(":");
                WriteDecoratedTypeNameOrImplementationTypeName(mi.ReturnType, true, true, 
                    IsFullyQualifiedNamesRequired(DeclaringType, mi.ReflectedType), WriteDecoratedTypeNameOrImplementationTypeNameMode.IgnoreImplementationType);

            }
            #endregion

            if (mode == WriteMethodSignatureMode.Delcaring)
                if (DeclaringType.IsInterface)
                    Write(";");

            WriteLine();
        }

    }
}
