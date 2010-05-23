﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jsc.meta.Library;
using System.Reflection;
using System.Reflection.Emit;
using jsc.Languages.IL;
using jsc.Library;
using ScriptCoreLib.Extensions;
using System.Runtime.InteropServices;

namespace jsc.meta.Commands.Rewrite
{
    public partial class RewriteToAssembly
    {
        internal static void CopyMethod(
            AssemblyBuilder a,
            ModuleBuilder m,
            MethodInfo SourceMethod,
            TypeBuilder DeclaringType,
            VirtualDictionary<string, string> NameObfuscation,
            Assembly PrimarySourceAssembly,
            Delegate codeinjecton,
            Func<Assembly, object[]> codeinjectonparams,


            Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,

            Action<MethodInfo, MethodBuilder, Func<ILGenerator>> BeforeInstructions,
            ILTranslationContext context
            )
        {
            // sanity check!

            if (context.MethodCache.BaseDictionary.ContainsKey(SourceMethod))
                return;

            context.MethodCache[SourceMethod] = null;

            var DelayedTypeCacheList = new List<Action>();
            Func<Type, Type> DelayedTypeCache = e =>
                {
                    DelayedTypeCacheList.Add(
                        delegate
                        {
                            var _ = context.TypeCache[e];
                        }
                    );
                    return context.TypeDefinitionCache[e];
                };


            // Unknown runtime implemented delegate method
            // Operation could destabilize the runtime.
            // http://www.fuzzydev.com/blogs/dotnet/archive/2006/06/10/Operation_could_destabilize_the_runtime.aspx


            // GenericArguments[0], 'T', on 'ScriptCoreLib.JavaScript.Concepts.SectionConcept`1[T]' violates the constraint of type parameter 'T'.

            var MethodName =
                //(source == this._assembly.EntryPoint) ||
                (SourceMethod.GetMethodBody() == null || (SourceMethod.Attributes & MethodAttributes.Virtual) == MethodAttributes.Virtual) ?
                SourceMethod.Name : NameObfuscation[context.MemberRenameCache[SourceMethod] ?? SourceMethod.Name];

            // !! fixme
            // How to: Define a Generic Method with Reflection Emit
            // http://msdn.microsoft.com/en-us/library/ms228971.aspx
            var Parameters =
                 SourceMethod.GetParameterTypes().Select(DelayedTypeCache).ToArray();

            if (Parameters.Contains(null))
                throw new InvalidOperationException();

            var km = default(MethodBuilder);

            var MethodAttributes__ = context.MethodAttributesCache[SourceMethod];
            var DllImport__ = SourceMethod.GetCustomAttributes<DllImportAttribute>().SingleOrDefault();

            if (DeclaringType == null)
            {
                km = m.DefineGlobalMethod(
                        MethodName, MethodAttributes__, SourceMethod.CallingConvention,
                    DelayedTypeCache(SourceMethod.ReturnType),
                    Parameters
                );
            }
            else
            {
                if (DllImport__ != null)
                {
                    // http://msdn.microsoft.com/en-us/library/hb2et051.aspx

                    km = DeclaringType.DefinePInvokeMethod(
                        MethodName,
                        DllImport__.Value,
                        MethodAttributes__,
                        SourceMethod.CallingConvention,
                        DelayedTypeCache(SourceMethod.ReturnType),
                        Parameters,
                        DllImport__.CallingConvention,
                        DllImport__.CharSet
                    );
                }
                else
                {
                    km = DeclaringType.DefineMethod(
                        MethodName,
                        MethodAttributes__,
                        SourceMethod.CallingConvention,
                        DelayedTypeCache(SourceMethod.ReturnType),
                        Parameters

                    );
                }
            }

            context.MethodCache[SourceMethod] = km;


            //Console.WriteLine("Method: " + km.Name);


            if (SourceMethod.IsGenericMethodDefinition)
            {
                var ga = SourceMethod.GetGenericArguments();

                // Operation is not valid due to the current state of the object.

                // Calling the DefineGenericParameters method makes the current 
                // method generic. There is no way to undo this change. Calling 
                // this method a second time causes an InvalidOperationException.
                var GenericNames = ga.Select(k => k.Name).ToArray();

                var gp = km.DefineGenericParameters(GenericNames);

                for (int i = 0; i < gp.Length; i++)
                {
                    // http://msdn.microsoft.com/en-us/library/system.reflection.emit.generictypeparameterbuilder(v=VS.95).aspx

                    foreach (var item in ga[i].GetGenericParameterConstraints())
                    {
                        // any issues if circular referencing?

                        if (item.IsInterface)
                            gp[i].SetInterfaceConstraints(context.TypeCache[item]);
                        else
                            gp[i].SetBaseTypeConstraint(context.TypeCache[item]);
                    }
                }
            }

            DelayedTypeCacheList.Invoke();


            // synchronized?
            km.SetImplementationFlags(SourceMethod.GetMethodImplementationFlags());

            SourceMethod.GetParameters().CopyTo(km);


            // should we copy attributes? should they be opt-out?
            foreach (var item in SourceMethod.GetCustomAttributes(false).Except(new [] {DllImport__}).Select(kk => kk.ToCustomAttributeBuilder()))
            {
                km.SetCustomAttribute(item(context));
            }

            var MethodBody = SourceMethod.GetMethodBody();

            if (MethodBody == null)
                return;

            var ExceptionHandlingClauses = MethodBody.ExceptionHandlingClauses.ToArray();



            MethodBase mb = SourceMethod;

            var kmil = km.GetILGenerator();
            var kmil_Dirty = false;

            if (BeforeInstructions != null)
                BeforeInstructions(SourceMethod, km,
                    delegate
                    {
                        kmil_Dirty = true;
                        return kmil;
                    }
                );

            // BeforeInstructions has replaced the IL
            // they may be calling a copy of us?
            if (kmil_Dirty)
                return;

            if (PrimarySourceAssembly != null)
                if (SourceMethod == PrimarySourceAssembly.EntryPoint)
                {
                    // we found the entrypoint
                    if (codeinjecton != null)
                    {
                        WriteEntryPointCodeInjection(
                            a, m, kmil, DeclaringType
                            , context.TypeCache,
                            context.ConstructorCache,
                            context.MethodCache,
                            PrimarySourceAssembly,
                            codeinjecton,
                            codeinjectonparams
                            );

                        // we have changed the IL offsets!
                    }

                    a.SetEntryPoint(km);
                }

            var x = CreateMethodBaseEmitToArguments(
                SourceMethod,

                NameObfuscation,
                ILOverride,
                ExceptionHandlingClauses,
                context
            );



            mb.EmitTo(kmil, x);


            // we need to emit the try/catch blocks too!

        }

        public static ILTranslationExtensions.EmitToArguments CreateMethodBaseEmitToArguments(
            MethodBase SourceMethod,
            VirtualDictionary<string, string> NameObfuscation,
            Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
            ExceptionHandlingClause[] ExceptionHandlingClauses,
            ILTranslationContext context)
        {
            var x = new ILTranslationExtensions.EmitToArguments
            {
                #region BeforeInstruction
                BeforeInstruction =
                    e =>
                    {
                        #region ExceptionHandlingClauses
                        foreach (var ex in ExceptionHandlingClauses)
                        {


                            if ((ex.Flags & ExceptionHandlingClauseOptions.Finally) == ExceptionHandlingClauseOptions.Finally)
                            {

                                if ((ex.HandlerOffset + ex.HandlerLength) == e.i.Offset)
                                {

                                    //Console.WriteLine(".endfinally");
                                    e.il.EndExceptionBlock();
                                }
                            }
                            else
                            {

                                if ((ex.HandlerOffset + ex.HandlerLength) == e.i.Offset)
                                {
                                    //Console.WriteLine(".endcatch");
                                    e.il.EndExceptionBlock();

                                }
                            }
                        }
                        #endregion

                        #region ExceptionHandlingClauses
                        foreach (var ex in ExceptionHandlingClauses)
                        {
                            if (ex.TryOffset == e.i.Offset)
                            {
                                //Console.WriteLine(".try");
                                e.il.BeginExceptionBlock();

                            }

                            if (ex.HandlerOffset == e.i.Offset)
                            {
                                // http://blogs.msdn.com/clrteam/archive/2009/03/23/exceptions-out-of-fault-finally.aspx

                                if (ex.Flags == ExceptionHandlingClauseOptions.Finally)
                                {
                                    //Console.WriteLine(".finally");

                                    // http://msdn.microsoft.com/en-us/library/system.reflection.emit.ilgenerator.beginfinallyblock.aspx
                                    // Label multiply defined ?
                                    e.il.BeginFinallyBlock();


                                }
                                else if (ex.Flags == ExceptionHandlingClauseOptions.Clause)
                                {

                                    //Console.WriteLine(".catch");
                                    e.il.BeginCatchBlock(context.TypeCache[ex.CatchType]);


                                }
                                else if (ex.Flags == ExceptionHandlingClauseOptions.Fault)
                                {

                                    //Console.WriteLine(".catch");
                                    e.il.BeginFaultBlock();


                                }
                                else
                                {
                                    throw new NotImplementedException();
                                }
                            }
                        }
                        #endregion

                    },
                #endregion


                AfterInstruction =
                    e =>
                    {


                    },

                // we need to redirect any typerefs and methodrefs!



                TranslateTargetType = context.TypeCache,
                TranslateTargetField = context.FieldCache,
                TranslateTargetMethod = context.MethodCache,
                TranslateTargetConstructor = context.ConstructorCache,
            };

            x[OpCodes.Endfinally] =
                e =>
                {
                };

            x[OpCodes.Leave_S] =
                e =>
                {
                    // see: http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/afc3b34b-1d42-427c-880f-1f6372ed81ca

                    // MethodBuilder.Emit is too nice and always writes .leave for us.
                    // As such we need not to write this twice
                };

            x[OpCodes.Leave] =
                e =>
                {
                    // see: http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/afc3b34b-1d42-427c-880f-1f6372ed81ca

                    // MethodBuilder.Emit is too nice and always writes .leave for us.
                    // As such we need not to write this twice
                };


            if (ILOverride != null)
                ILOverride(SourceMethod, x);
            return x;
        }



    }
}
