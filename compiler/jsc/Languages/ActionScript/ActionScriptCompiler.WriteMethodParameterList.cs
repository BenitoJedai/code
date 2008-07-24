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
        public override void WriteMethodParameterList(MethodBase m)
        {
            WriteMethodParameterList(m, null, null);
        }

        public void WriteMethodParameterList(MethodBase m, ILFlow.StackItem[] DefaultValues, Action<Action> AddDefaultVariableInitializer)
        {
            ParameterInfo[] mp = m.GetParameters();

            var ma = m.ToScriptAttribute();


            bool bStatic = (ma != null && ma.DefineAsStatic);

            if (bStatic)
            {
                if (m.IsStatic)
                {
                    Break("method is already static, but is marked to be declared out of band : " + m.DeclaringType.FullName + "." + m.Name);
                }


                DebugBreak(ma);

                // cannot use 'this' on arguments as it is a keyword
                WriteSelf();
                Write(":");

                if (m.DeclaringType.ToScriptAttributeOrDefault().Implements == typeof(object))
                {
                    Write(NativeTypes[typeof(object)]);
                }
                else
                {
                    WriteDecoratedTypeNameOrImplementationTypeName(m.DeclaringType, true, true, IsFullyQualifiedNamesRequired(m.DeclaringType, m.DeclaringType));
                }


            }

            DebugBreak(ma);

            for (int mpi = 0; mpi < mp.Length; mpi++)
            {
                if (mpi > 0 || bStatic)
                {
                    Write(",");
                    WriteSpace();
                }

                ParameterInfo p = mp[mpi];

                ScriptAttribute za = ScriptAttribute.Of(m.DeclaringType, true);


                var ParamIndex = mpi;

                // Nameless params is used by delegates and these parameters are not used
                WriteMethodParameter(ParamIndex, p);

                var ParameterType = p.ParameterType;

                // A NativeExtension class should never define a variable to its type rather the native type
                if (ParameterType == m.DeclaringType && m.DeclaringType.IsNativeTypeExtension())
                    ParameterType = za.Implements;

                Write(":");

                // fixme: byref supported?

                if (ParameterType.IsByRef)
                    Write("*");
                else
                    WriteDecoratedTypeNameOrImplementationTypeName(ParameterType, true, true, IsFullyQualifiedNamesRequired(m.DeclaringType, ParameterType));

                if (DefaultValues != null && mpi < DefaultValues.Length)
                {
                    WriteAssignment();

                    // if the value aint literal we cannot use it with
                    // the curent actionscript compiler

                    var DefaultValue = DefaultValues[mpi] == null ? null : DefaultValues[mpi].SingleStackInstruction;

                    if (DefaultValue == null)
                    {
                        // fixme!
                        Write("null");
                    }
                    else if (DefaultValue.IsLiteral)
                        EmitInstruction(null, DefaultValue);
                    else
                    {
                        WriteKeywordNull();

                        if (AddDefaultVariableInitializer == null)
                            throw new NullReferenceException("AddDefaultVariableInitializer");

                        AddDefaultVariableInitializer(
                            delegate
                            {
                                WriteIdent();
                                Write("if (");
                                WriteMethodParameter(ParamIndex, p);
                                Write(" == null) ");
                                WriteMethodParameter(ParamIndex, p);
                                WriteAssignment();
                                EmitInstruction(null, DefaultValue);
                                WriteLine(";");
                            }
                        );
                    }
                }
                /*
                if (za.Implements == null || m.DeclaringType.GUID != p.ParameterType.GUID)
                    WriteDecoratedTypeName(p.ParameterType);
                else
                    WriteDecoratedTypeName(za.Implements);
                */

            }
        }

        /// <summary>
        /// Some parameters can be nameless which are used by delegates and these parameters are not used
        /// </summary>
        /// <param name="mpi"></param>
        /// <param name="p"></param>
        private void WriteMethodParameter(int mpi, ParameterInfo p)
        {
            if (string.IsNullOrEmpty(p.Name))
                Write("_" + mpi);
            else
                WriteDecoratedMethodParameter(p);
        }

    }
}
