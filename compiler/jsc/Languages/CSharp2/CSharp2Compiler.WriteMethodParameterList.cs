﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Reflection.Emit;
using ScriptCoreLib.CSharp.Extensions;

namespace jsc.Languages.CSharp2
{
    partial class CSharp2Compiler
    {
        public override void WriteMethodParameterList(MethodBase m)
        {
            WriteMethodParameterList(m, null, null);
        }

        public void WriteMethodParameterList(MethodBase m, ILFlowStackItem[] DefaultValues, Action<Action> AddDefaultVariableInitializer)
        {
            if (DefaultValues != null)
                throw new NotSupportedException();

            if (AddDefaultVariableInitializer != null)
                throw new NotSupportedException();

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

			WriteMethodParameterList(m.DeclaringType, mp, bStatic);
        }

		private void WriteMethodParameterList(Type m_DeclaringType, ParameterInfo[] mp, bool bStatic)
		{
			for (int mpi = 0; mpi < mp.Length; mpi++)
			{
				if (mpi > 0 || bStatic)
				{
					Write(",");
					WriteSpace();
				}

				ParameterInfo p = mp[mpi];

				ScriptAttribute za = ScriptAttribute.Of(m_DeclaringType, true);


				var ParamIndex = mpi;


				var ParameterType = p.ParameterType;

				// A NativeExtension class should never define a variable to its type rather the native type
				if (ParameterType == m_DeclaringType && m_DeclaringType.IsNativeTypeExtension())
					ParameterType = za.Implements;



				// fixme: byref supported?

				//if (ParameterType.IsByRef)
				//    Write("*");
				//else

				WriteGenericTypeName(m_DeclaringType, ParameterType);

				WriteSpace();

				// Nameless params is used by delegates and these parameters are not used
				WriteMethodParameter(ParamIndex, p);

				//if (DefaultValues != null && mpi < DefaultValues.Length)
				//{
				//    WriteAssignment();

				//    // if the value aint literal we cannot use it with
				//    // the curent actionscript compiler

				//    var DefaultValue = DefaultValues[mpi].SingleStackInstruction;

				//    if (DefaultValue.IsLiteral)
				//        EmitInstruction(null, DefaultValue);
				//    else
				//    {
				//        WriteKeywordNull();

				//        //if (AddDefaultVariableInitializer == null)
				//        //    throw new NullReferenceException("AddDefaultVariableInitializer");

				//        //AddDefaultVariableInitializer(
				//        //    delegate
				//        //    {
				//        //        WriteIdent();
				//        //        Write("if (");
				//        //        WriteMethodParameter(ParamIndex, p);
				//        //        Write(" == null) ");
				//        //        WriteMethodParameter(ParamIndex, p);
				//        //        WriteAssignment();
				//        //        EmitInstruction(null, DefaultValue);
				//        //        WriteLine(";");
				//        //    }
				//        //);
				//    }
				//}
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
