using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using jsc.Languages.IL;
using jsc.Library;
using jsc.meta.Library;
using ScriptCoreLib.Extensions;

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
            ILTranslationContext context,

            RewriteToAssembly Command,

            Func<string, MethodAttributes, CallingConventions, MethodBuilder> AtCodeTraceDefineMethod,

            Action<MethodBuilder> AtCodeTraceDefineGenericParameters = null
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

            var MethodName = default(string);

            if (SourceMethod.GetMethodBody() == null || (SourceMethod.Attributes & MethodAttributes.Virtual) == MethodAttributes.Virtual)
            {
                MethodName = SourceMethod.Name;
            }
            else
            {
                var __MemberRenameCache = context.MemberRenameCache[SourceMethod];
                MethodName = __MemberRenameCache ?? SourceMethod.Name;

                var o = default(ObfuscationAttribute);

                if (SourceMethod.DeclaringType != null)
                    SourceMethod.DeclaringType.GetCustomAttributes<ObfuscationAttribute>().FirstOrDefault();

                if (o != null && o.Exclude)
                {
                }
                else
                {
                    MethodName = NameObfuscation[MethodName];
                }
            }



            var DeclaringMethod = default(MethodBuilder);

            var MethodAttributes__ = context.MethodAttributesCache[SourceMethod];

            if (SourceMethod.IsStatic)
                if ((MethodAttributes__ & MethodAttributes.Private) == MethodAttributes.Private)
                {
                    // <Module> methods should not be private unless used only by <Module> methods...
                    // we currently do not keep track for that

                    MethodAttributes__ = MethodAttributes__ & ~MethodAttributes.Private;
                    MethodAttributes__ = MethodAttributes__ | MethodAttributes.Assembly;
                }


            var DllImport__ = SourceMethod.GetCustomAttributes<DllImportAttribute>().SingleOrDefault();

            var ParametersTypes = SourceMethod.GetParameterTypes().Select(DelayedTypeCache).ToArray();

            if (ParametersTypes.Contains(null))
                throw new InvalidOperationException();

            var ReturnType = DelayedTypeCache(SourceMethod.ReturnType);

            var IsNonPublicStatic = DllImport__ == null && Command.obfuscate &&
                SourceMethod.IsStatic && !SourceMethod.IsPublic;

            var IsNonPublicStaticProperty = IsNonPublicStatic && Enumerable.Any(
                from _DeclaringType in new[] { SourceMethod.DeclaringType }
                where _DeclaringType != null
                from _Property in _DeclaringType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                from _Method in new[] { _Property.GetGetMethod(true), _Property.GetSetMethod(true) }
                where _Method != null
                where _Method == SourceMethod
                select _Property
            );

            var IsNonPublicStaticEvent = IsNonPublicStatic && Enumerable.Any(
                 from _DeclaringType in new[] { SourceMethod.DeclaringType }
                 where _DeclaringType != null
                 from _Event in _DeclaringType.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                 from _Method in new[] { _Event.GetAddMethod(true), _Event.GetRemoveMethod(true) }
                 where _Method != null
                 where _Method == SourceMethod
                 select _Event
            );

            var IsNonPublicStaticToGlobalMethodUpgrate =
                IsNonPublicStatic && !IsNonPublicStaticProperty && !IsNonPublicStaticEvent;


            if (DeclaringType == null || IsNonPublicStaticToGlobalMethodUpgrate)
            {


                #region DefineGlobalMethod
                DeclaringMethod = m.DefineGlobalMethod(
                    MethodName,
                    MethodAttributes__,
                    SourceMethod.CallingConvention,
                    null,
                    null
                );
                #endregion

            }
            else if (DllImport__ != null)
            {
                // http://msdn.microsoft.com/en-us/library/hb2et051.aspx

                #region DefinePInvokeMethod
                DeclaringMethod = DeclaringType.DefinePInvokeMethod(
                    MethodName,
                    DllImport__.Value,
                    MethodAttributes__,
                    SourceMethod.CallingConvention,
                    ReturnType,
                    ParametersTypes,
                    DllImport__.CallingConvention,
                    DllImport__.CharSet
                );
                #endregion
            }
            else
            {
                //if (Command != null)
                //    Command.WriteDiagnostics("DefineMethod " + MethodName);



                DeclaringMethod = DeclaringType.DefineMethod(
                    MethodName,
                    MethodAttributes__,
                     SourceMethod.CallingConvention,
                    null,
                    null
                );



            }

            context.MethodCache[SourceMethod] = DeclaringMethod;


            //Console.WriteLine("Method: " + km.Name);

            #region DefineGenericParameters
            if (SourceMethod.IsGenericMethodDefinition)
            {
                var ga = SourceMethod.GetGenericArguments();

                // Operation is not valid due to the current state of the object.

                // Calling the DefineGenericParameters method makes the current 
                // method generic. There is no way to undo this change. Calling 
                // this method a second time causes an InvalidOperationException.
                var GenericNames = ga.Select(k => NameObfuscation[k.Name]).ToArray();

                var gp = default(GenericTypeParameterBuilder[]);


                gp = DeclaringMethod.DefineGenericParameters(GenericNames);

                for (int i = 0; i < gp.Length; i++)
                {
                    context.TypeDefinitionCache[ga[i]] = gp[i];
                    context.TypeCache[ga[i]] = gp[i];

                    // http://msdn.microsoft.com/en-us/library/system.reflection.emit.generictypeparameterbuilder(v=VS.95).aspx

                    foreach (var item in ga[i].GetGenericParameterConstraints())
                    {
                        var GenericParameter = gp[i];

                        // any issues if circular referencing?
                        //var Constraint = DelayedTypeCache(item);
                        var Constraint = context.TypeDefinitionCache[item];

                        if (item.IsInterface)
                        {
                            GenericParameter.SetInterfaceConstraints(Constraint);
                        }
                        else
                            GenericParameter.SetBaseTypeConstraint(Constraint);
                    }
                }
            }
            #endregion

            // !! fixme
            // How to: Define a Generic Method with Reflection Emit
            // http://msdn.microsoft.com/en-us/library/ms228971.aspx


            // Calling this method replaces a return type established using the TypeBuilder.DefineMethod method.

            if (DllImport__ == null)
                DeclaringMethod.SetSignature(
                    ReturnType,
                    null, null,
                    ParametersTypes,
                    null, null
                );

            DelayedTypeCacheList.Invoke();


            // synchronized?
            DeclaringMethod.SetImplementationFlags(SourceMethod.GetMethodImplementationFlags());

            foreach (var SourceParameter in SourceMethod.GetParameters())
            {
                // http://msdn.microsoft.com/en-us/library/system.reflection.emit.methodbuilder.defineparameter.aspx

                // The position of the parameter in the parameter list. 
                // Parameters are indexed beginning with the number 1 for the first parameter; the number 0 represents the return value of the method. 

                var ParameterPosition = SourceParameter.Position + 1;

                var ParameterName =
                    Command != null && Command.obfuscate ? null :
                    SourceParameter.Name == null ? null :
                    NameObfuscation[SourceParameter.Name];

                var DeclaringParameter =
                    DeclaringMethod.DefineParameter(
                        ParameterPosition,
                        SourceParameter.Attributes,
                        ParameterName
                    );

                if ((SourceParameter.Attributes & ParameterAttributes.HasDefault) == ParameterAttributes.HasDefault)
                    DeclaringParameter.SetConstant(SourceParameter.RawDefaultValue);

                // should we copy attributes? should they be opt-out?
                foreach (var item in SourceParameter.GetCustomAttributes(false).Select(kk => kk.ToCustomAttributeBuilder()))
                {
                    DeclaringParameter.SetCustomAttribute(item(context));
                }
            }

            // should we copy attributes? should they be opt-out?
            foreach (var item in SourceMethod.GetCustomAttributes(false).Except(new[] { DllImport__ }).Select(kk => kk.ToCustomAttributeBuilder()))
            {
                DeclaringMethod.SetCustomAttribute(item(context));
            }

            var MethodBody = SourceMethod.GetMethodBody();

            if (MethodBody == null)
                return;

            var ExceptionHandlingClauses = MethodBody.ExceptionHandlingClauses.ToArray();



            MethodBase mb = SourceMethod;

            var kmil = DeclaringMethod.GetILGenerator();
            var kmil_Dirty = false;

            if (BeforeInstructions != null)
                BeforeInstructions(SourceMethod, DeclaringMethod,
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

            #region EntryPoint

            var DeclaringAssembly =
                SourceMethod.DeclaringType != null ?
                    SourceMethod.DeclaringType.Assembly :
                        SourceMethod.Module.Assembly;

            if (Command != null && Command.EntryPointAssembly != null
                && (
                    Command.EntryPointAssembly.TakeUntilLastIfAny(".exe").TakeUntilLastIfAny(".dll") == DeclaringAssembly.GetName().Name
                    && DeclaringAssembly.EntryPoint == SourceMethod)
                )
            {
                a.SetEntryPoint(DeclaringMethod);
            }
            else if (Command != null && Command.EntryPoint != null &&
                Command.EntryPoint ==
                    SourceMethod.DeclaringType.FullName + "." + SourceMethod.Name
                )
            {
                a.SetEntryPoint(DeclaringMethod);

            }
            else if (PrimarySourceAssembly != null)
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

                    a.SetEntryPoint(DeclaringMethod);
                }
            #endregion


            if (Command != null && Command.obfuscate)
            {
                //var skip = kmil.DefineLabel();

                //kmil.Emit(OpCodes.Br, skip);

                //// and the stack is now under water. good luck :)
                //kmil.Emit(OpCodes.Pop);
                //kmil.Emit(OpCodes.Ldnull);

                //kmil.MarkLabel(skip);

            }

            #region EnableSwitchRewrite
            if (Command != null && Command.EnableSwitchRewrite)
            {
                var xb = new ILBlock(SourceMethod);

                if (xb.Instructrions.Any(k => k.OpCode == OpCodes.Switch))
                {


                    WriteSwitchRewrite(
                       SourceMethod,
                       DeclaringType,
                       context,
                       ParametersTypes,
                       ReturnType,
                       ILOverride,
                       ExceptionHandlingClauses,
                       xb,
                       kmil
                   );



                    return;
                }
            }
            #endregion


            var x = CreateMethodBaseEmitToArguments(
                SourceMethod,
                ILOverride,
                ExceptionHandlingClauses,
                context
            );

            mb.EmitTo(kmil, x);


            // we need to emit the try/catch blocks too!

        }



        public static ILTranslationExtensions.EmitToArguments CreateMethodBaseEmitToArguments(
            MethodBase SourceMethod,
            //VirtualDictionary<string, string> NameObfuscation,
            Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
            ExceptionHandlingClause[] ExceptionHandlingClauses,
            ILTranslationContext context)
        {
            const string DebugPrefix = "~~debug: ";

            Action<ILGenerator, string> DebugWrite =
                (il, text) =>
                {
                    //il.Emit(OpCodes.Ldstr, DebugPrefix + text);
                    //il.Emit(OpCodes.Pop);
                };

            var x = new ILTranslationExtensions.EmitToArguments
            {
                #region BeforeInstruction
                BeforeInstruction =
                    e =>
                    {


                        #region ExceptionHandlingClauses
                        foreach (var ex in ExceptionHandlingClauses)
                        {
                            if (ex.TryOffset == e.i.Offset)
                            {
                                //Console.WriteLine(".try");
                                //DebugWrite(e.il, ".try " + e.i.Offset.ToString("x4"));

                                e.il.BeginExceptionBlock();

                            }

                            if (ex.HandlerOffset == e.i.Offset)
                            {
                                // http://blogs.msdn.com/clrteam/archive/2009/03/23/exceptions-out-of-fault-finally.aspx

                                if (ex.Flags == ExceptionHandlingClauseOptions.Finally)
                                {
                                    //Console.WriteLine(".finally");
                                    DebugWrite(e.il, ".finally (jump next or exit) " + e.i.Offset.ToString("x4"));

                                    // http://msdn.microsoft.com/en-us/library/system.reflection.emit.ilgenerator.beginfinallyblock.aspx
                                    // Label multiply defined ?
                                    e.il.BeginFinallyBlock();


                                }
                                else if (ex.Flags == ExceptionHandlingClauseOptions.Clause)
                                {

                                    DebugWrite(e.il, ".catch (jump next or exit) " + e.i.Offset.ToString("x4"));
                                    //Console.WriteLine(".catch");
                                    e.il.BeginCatchBlock(context.TypeCache[ex.CatchType]);


                                }
                                else if (ex.Flags == ExceptionHandlingClauseOptions.Fault)
                                {

                                    DebugWrite(e.il, ".fault " + e.i.Offset.ToString("x4"));
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
                        if (e.i.Next == null)
                            return;

                        #region ExceptionHandlingClauses
                        foreach (var ex in ExceptionHandlingClauses)
                        {


                            if ((ex.Flags & ExceptionHandlingClauseOptions.Finally) == ExceptionHandlingClauseOptions.Finally)
                            {

                                if ((ex.HandlerOffset + ex.HandlerLength) == e.i.Next.Offset)
                                {
                                    DebugWrite(e.il, ".endfinally " + e.i.Offset.ToString("x4"));


                                    //Console.WriteLine(".endfinally");
                                    e.il.EndExceptionBlock();
                                }
                            }
                            else
                            {

                                if ((ex.HandlerOffset + ex.HandlerLength) == e.i.Next.Offset)
                                {
                                    DebugWrite(e.il, ".endcatch (jump next or exit) " + e.i.Offset.ToString("x4"));

                                    //Console.WriteLine(".endcatch");
                                    e.il.EndExceptionBlock();


                                }
                            }
                        }
                        #endregion

                    },

                // we need to redirect any typerefs and methodrefs!



                TranslateTargetType = context.TypeCache,
                TranslateTargetField = context.FieldCache,
                TranslateTargetMethod = context.MethodCache,
                TranslateTargetConstructor = context.ConstructorCache,
                TranslateTargetLiteral = context.StringLiteralCache
            };

            x[OpCodes.Endfinally] =
                e =>
                {
                    //e.il.Emit(OpCodes.Nop);
                };

            x[OpCodes.Pop] =
              e =>
              {
                  if (e.i.Prev.OpCode == OpCodes.Ldstr)
                      if (e.i.Prev.TargetLiteral.StartsWith(DebugPrefix))
                          return;

                  e.Default();
              };

            x[OpCodes.Ldstr] =
                e =>
                {
                    if (e.i.TargetLiteral.StartsWith(DebugPrefix) && e.i.Next.OpCode == OpCodes.Pop)
                        return;

                    e.Default();
                };


            x[OpCodes.Leave, OpCodes.Leave_S] =
                e =>
                {
                    //see: http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/afc3b34b-1d42-427c-880f-1f6372ed81ca

                    // we can omit calls to current blocks finally or current blocks exit
                    // we can omit inter clause jumping...
                    // detecting such leaves is tricky.


                    // if next is try exit
                    //            catch exit
                    // and if target is join 
                    // then we can omit this instruction

                    var RegionByJoin = Enumerable.FirstOrDefault(
                        from ex in ExceptionHandlingClauses.AsEnumerable()

                        where ex.HandlerOffset + ex.HandlerLength == e.i.TargetInstruction.Offset
                        select ex
                    );

                    if (RegionByJoin != null)
                    {
                        var IsTryLeave = (e.i.Next.Offset == RegionByJoin.TryOffset + RegionByJoin.TryLength);
                        var IsCatchLeave = (e.i.Next.Offset == RegionByJoin.HandlerOffset + RegionByJoin.HandlerLength);

                        if (IsTryLeave)
                            return;

                        if (RegionByJoin.Flags == ExceptionHandlingClauseOptions.Clause)
                        {
                            if (IsCatchLeave)
                                return;
                        }
                    }

                    e.Default();
                };


            if (ILOverride != null)
                ILOverride(SourceMethod, x);

            return x;
        }



    }
}
