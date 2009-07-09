
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;
using System.Threading;

using jsc.CodeModel;

using ScriptCoreLib;
using jsc.Script;

namespace jsc.Languages.Java
{

    partial class JavaCompiler
    {

        public override void WriteMethodCallVerified(ILBlock.Prestatement p, ILInstruction i, MethodBase m)
        {
            DebugBreak(ScriptAttribute.Of(i.OwnerMethod));

            ScriptAttribute ma = ScriptAttribute.Of(m);

            bool IsExternalDefined = ma != null && ma.ExternalTarget != null;
            bool IsDefineAsInstance = ma != null && ma.DefineAsInstance;
            bool IsBaseCall = false;
            bool IsDefineAsStatic = ma != null && ma.DefineAsStatic;

			if (m.IsInstanceConstructor())
            {
                // fixme: update the BCL resolving issue
                // the super ctor call gets lost otherwise

				if (m.DeclaringType == i.OwnerMethod.DeclaringType)
				{
					// ctor as this.ctor();
					IsBaseCall = true;
				}
				else if (i.IsBaseConstructorCall(m, k => ResolveImplementationMethod(k.DeclaringType, k), ResolveImplementation))
                {

                    IsBaseCall = true;
                }
                else
                    Break("If it was a native constructor, it should be remapped via InternalConstructor attribute.Cannot call constructor : " + m + " used at " + i.OwnerMethod.DeclaringType.FullName + "." + i.OwnerMethod.Name + ".");
            }



            ILFlow.StackItem[] s = i.StackBeforeStrict;

            int offset = 1;

            #region static call defined as instance call


            if (m.IsStatic && IsExternalDefined & IsDefineAsInstance)
            {
                // what?? string?

                Emit(p, s[0]);
                Write(".");
                WriteExternalMethod(ma.ExternalTarget, m);
                WriteParameterInfoFromStack(m, p, s, 1);

                return;
            }
            #endregion



            if ((m.IsStatic || IsDefineAsStatic) || IsBaseCall)
            {
                #region static
                //TODO: ???
                if (IsBaseCall)
                {
                    //WriteTypeBaseType();
                    //Write(".");

                }
                else
                {
                    //ScriptAttribute ta = ScriptAttribute.Of(m.DeclaringType);

                    if (IsExternalDefined)
                    {
                        //WriteBoxedComment("impl");



                        //WriteTypeOrExternalTargetTypeName(ta.Implements);

						this.Write(GetDecoratedTypeName(ScriptAttribute.Of(m.DeclaringType).ImplementationType, true, true, true, true));
                        
						Write(".");

                    }
                    else
                    {
                        //WriteBoxedComment("ext");

                        WriteTypeOrExternalTargetTypeName(m.DeclaringType);
                        Write(".");
                    }
                }
                #endregion

                offset = !m.IsStatic && (IsDefineAsStatic || IsBaseCall) ? 1 : 0;
            }
            else
            {

                // WriteBoxedComment("variable.call");

                // base. ?

                if (i.OpCode == OpCodes.Call &&
                    s[0].SingleStackInstruction == OpCodes.Ldarg_0 &&
                    i.OwnerMethod.DeclaringType.BaseType == m.DeclaringType)
                {
                    if (i.IsBaseConstructorCall())
                        WriteKeyword(Keywords._super);
                    else
                        WriteKeyword(Keywords._this);
                    
                }
                else
                {
                    Emit(p, s[0]);
                }

                Write(".");
            }



            if (IsExternalDefined)
            {
                WriteExternalMethod(ma.ExternalTarget, m);
            }
            else
            {
                if (IsBaseCall)
                {
                    if (i.IsBaseConstructorCall())
                        WriteKeyword(Keywords._super);
                    else
                        WriteKeyword(Keywords._this);
                    
                }
                else
                {
                    WriteDecoratedMethodName(m, false);
                }
            }

            WriteParameterInfoFromStack(m, p, s, offset);

        }


    }
}
