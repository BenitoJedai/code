using System;
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
using System.Runtime.CompilerServices;

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
            if (AtCodeTraceDefineMethod == null)
                AtCodeTraceDefineMethod = (__MethodName, __MethodAttributes__, CallingConvention) => DeclaringType.DefineMethod(
                                            __MethodName,
                                            __MethodAttributes__,
                                            CallingConvention,
                                            null,
                                            null
                                        );
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



            var DeclaringMethod = default(MethodBuilder);

            var MethodAttributes__ = context.MethodAttributesCache[SourceMethod];
            var DllImport__ = SourceMethod.GetCustomAttributes<DllImportAttribute>().SingleOrDefault();

            var ParametersTypes = SourceMethod.GetParameterTypes().Select(DelayedTypeCache).ToArray();

            if (ParametersTypes.Contains(null))
                throw new InvalidOperationException();

            var ReturnType = DelayedTypeCache(SourceMethod.ReturnType);

            if (DeclaringType == null)
            {
                #region DefineGlobalMethod
                DeclaringMethod = m.DefineGlobalMethod(
                        MethodName, MethodAttributes__, SourceMethod.CallingConvention,
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
                var GenericNames = ga.Select(k => k.Name).ToArray();

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


                var DeclaringParameter = DeclaringMethod.DefineParameter(SourceParameter.Position + 1, SourceParameter.Attributes, SourceParameter.Name);

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

                    a.SetEntryPoint(DeclaringMethod);
                }
            #endregion




            if (Command.EnableSwitchRewrite)
            {
                var xb = new ILBlock(SourceMethod);

                if (xb.Instructrions.Any(k => k.OpCode == OpCodes.Switch))
                {

                    WriteSwitchRewrite(SourceMethod, DeclaringType, context, ParametersTypes, ReturnType,
                        ILOverride,
                        ExceptionHandlingClauses,
                        xb
                    ).With(
                        ForwardRef =>
                        {
                            if (!SourceMethod.IsStatic)
                                kmil.Emit(OpCodes.Ldloc_0);

                            foreach (var item in SourceMethod.GetParameters())
                            {
                                kmil.Emit(OpCodes.Ldarg, (short)item.Position);
                            }

                            kmil.Emit(OpCodes.Call, ForwardRef);
                            kmil.Emit(OpCodes.Ret);
                        }
                    );

                    return;
                }
            }

            var x = CreateMethodBaseEmitToArguments(
                SourceMethod,
                ILOverride,
                ExceptionHandlingClauses,
                context
            );
            mb.EmitTo(kmil, x);


            // we need to emit the try/catch blocks too!

        }

        private static MethodBuilder WriteSwitchRewrite(
            MethodInfo SourceMethod,
            TypeBuilder DeclaringType,
            ILTranslationContext context,
            Type[] ParametersTypes,
            Type ReturnType,

            Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
            ExceptionHandlingClause[] ExceptionHandlingClauses,
            ILBlock xb
            )
        {
            // do we suppport try?

            var x = CreateMethodBaseEmitToArguments(
                    SourceMethod,
                    ILOverride,
                    ExceptionHandlingClauses,
                    context
                );


            // we need to capture the locals
            var ContextName = SourceMethod.Name + " <" + SourceMethod.MetadataToken + "> switch ";

            var SwitchClosure = DeclaringType.DefineNestedType(
                ContextName + "closure", TypeAttributes.Sealed | TypeAttributes.NestedAssembly);

            var SwitchClosureThis = default(FieldBuilder);

            (!SourceMethod.IsStatic).ThenDo(
                 delegate
                 {

                     SwitchClosureThis = SwitchClosure.DefineField(" this", DeclaringType, FieldAttributes.Assembly);
                 }
             );

            #region SwitchClosureArguments
            var SwitchClosureArguments = SourceMethod.GetParameters().Select(
                arg =>
                {
                    var fld = SwitchClosure.DefineField(" arg" + arg.Position, context.TypeCache[arg.ParameterType], FieldAttributes.Assembly);

                    Action<ILGenerator, LocalBuilder> init =
                        (il, closure_loc) =>
                        {
                            il.Emit(OpCodes.Nop);
                            il.Emit(OpCodes.Ldloc, closure_loc);

                            if (SwitchClosureThis == null)
                                il.Emit(OpCodes.Ldarg, (short)arg.Position);
                            else
                                il.Emit(OpCodes.Ldarg, (short)(arg.Position + 1));

                            il.Emit(OpCodes.Stfld, fld);
                            il.Emit(OpCodes.Nop);
                        };

                    Action<ILTranslationExtensions.EmitToArguments.ILRewriteContext> load =
                       (e) =>
                       {
                           e.il.Emit(OpCodes.Ldarg_0);
                           e.il.Emit(OpCodes.Ldfld, fld);
                       };

                    return new
                    {
                        fld,
                        init,
                        load
                    };
                }
            ).ToArray();
            #endregion


            #region SwitchClosureFields
            var SwitchClosureFields = SourceMethod.GetMethodBody().LocalVariables.Select(
                loc =>
                {
                    var fld = SwitchClosure.DefineField(" loc" + loc.LocalIndex, context.TypeCache[loc.LocalType], FieldAttributes.Assembly);

                    Action<ILTranslationExtensions.EmitToArguments.ILRewriteContext> store =
                        (e) =>
                        {
                            var value = e.il.DeclareLocal(context.TypeCache[loc.LocalType]);

                            e.il.Emit(OpCodes.Stloc, value);

                            e.il.Emit(OpCodes.Ldarg_0);
                            e.il.Emit(OpCodes.Ldloc, value);
                            e.il.Emit(OpCodes.Stfld, fld);
                        };

                    Action<ILTranslationExtensions.EmitToArguments.ILRewriteContext> load =
                        (e) =>
                        {
                            e.il.Emit(OpCodes.Ldarg_0);
                            e.il.Emit(OpCodes.Ldfld, fld);
                        };

                    return new
                    {
                        fld,
                        store,
                        load
                    };
                }
            ).ToArray();
            #endregion

            var SwitchClosureReturn = default(FieldBuilder);

            (SourceMethod as MethodInfo).With(
                Method =>
                {
                    if (ReturnType == typeof(void))
                        return;

                    SwitchClosureReturn = SwitchClosure.DefineField(" ret", ReturnType, FieldAttributes.Assembly);
                }
            );


            var ArgumentOffset = (SourceMethod.IsStatic) ? 0 : 1;

            x[OpCodes.Ldarg_0] = e => 
                {
                    if (SourceMethod.IsStatic )
                    {
                        SwitchClosureArguments[0].load(e);
                        return;
                    }

                    e.il.Emit(OpCodes.Ldarg_0);
                    e.il.Emit(OpCodes.Ldfld, SwitchClosureThis);

                 };

            x[OpCodes.Ldarg_1] = e => SwitchClosureArguments[1 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg_2] = e => SwitchClosureArguments[2 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg_3] = e => SwitchClosureArguments[3 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg_S] = e => SwitchClosureArguments[e.i.OpParamAsInt8 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg] = e => SwitchClosureArguments[e.i.OpParamAsInt8 - ArgumentOffset].load(e);

            x[OpCodes.Ldloc_0] = e => SwitchClosureFields[0].load(e);
            x[OpCodes.Ldloc_1] = e => SwitchClosureFields[1].load(e);
            x[OpCodes.Ldloc_2] = e => SwitchClosureFields[2].load(e);
            x[OpCodes.Ldloc_3] = e => SwitchClosureFields[3].load(e);
            x[OpCodes.Ldloc_S] = e => SwitchClosureFields[e.i.OpParamAsInt8].load(e);
            x[OpCodes.Ldloc] = e => SwitchClosureFields[e.i.OpParamAsInt16].load(e);

            //x[OpCodes.Ldloca_S] = e => SwitchClosureFields[e.i.OpParamAsInt8].load(e);
            //x[OpCodes.Ldloca] = e => SwitchClosureFields[e.i.OpParamAsInt16].load(e);


            x[OpCodes.Stloc_0] = e => SwitchClosureFields[0].store(e);
            x[OpCodes.Stloc_1] = e => SwitchClosureFields[1].store(e);
            x[OpCodes.Stloc_2] = e => SwitchClosureFields[2].store(e);
            x[OpCodes.Stloc_3] = e => SwitchClosureFields[3].store(e);
            x[OpCodes.Stloc_S] = e => SwitchClosureFields[e.i.OpParamAsInt8].store(e);
            x[OpCodes.Stloc] = e => SwitchClosureFields[e.i.OpParamAsInt16].store(e);






            var SwitchClosureConstructor = SwitchClosure.DefineDefaultConstructor(MethodAttributes.FamORAssem);

            SwitchClosure.CreateType();

            var EntryBranchAttributes = MethodAttributes.FamORAssem;

            (SourceMethod.IsStatic).ThenDo(() => EntryBranchAttributes |= MethodAttributes.Static);




            Func<string, Type, MethodBuilder> DefineWorkflowMethod =
                (text, __ReturnType) =>
                    DeclaringType.DefineMethod(
                        ContextName + text,
                        MethodAttributes.FamORAssem | MethodAttributes.Static,
                        SourceMethod.CallingConvention,
                       __ReturnType,
                       new[] { SwitchClosure }
                    ).With(m => m.DefineAttribute<CompilerGeneratedAttribute>(new object()));


            var EntryBranch = DeclaringType.DefineMethod(
                ContextName + "init",
                EntryBranchAttributes,
                SourceMethod.CallingConvention,
               ReturnType,
               ParametersTypes
           );

            var Workflow = DefineWorkflowMethod("workflow", null);


            #region EntryBranch
            {
                var il = EntryBranch.GetILGenerator();

                var closure_loc = il.DeclareLocal(SwitchClosure);

                il.Emit(OpCodes.Newobj, SwitchClosureConstructor);
                il.Emit(OpCodes.Stloc, closure_loc);

                SwitchClosureThis.With(
                  fld =>
                  {
                      il.Emit(OpCodes.Nop);

                      il.Emit(OpCodes.Ldloc, closure_loc);
                      il.Emit(OpCodes.Ldarg_0);
                      il.Emit(OpCodes.Stfld, fld);
                  }
                );

                SwitchClosureArguments.WithEach(f => f.init(il, closure_loc));

                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldloc, closure_loc);
                il.Emit(OpCodes.Call, Workflow);

                SwitchClosureReturn.With(
                    fld =>
                    {
                        il.Emit(OpCodes.Ldloc, closure_loc);
                        il.Emit(OpCodes.Ldfld, fld);
                    }
                );

                il.Emit(OpCodes.Ret);
            }
            #endregion

            {
                var il = Workflow.GetILGenerator();

                // http://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.br.aspx
                var offset_loc = il.DeclareLocal(typeof(int));
                var flag_loc = il.DeclareLocal(typeof(bool));

                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Stloc, offset_loc);

                var loop_check = il.DefineLabel();
                il.Emit(OpCodes.Br, loop_check);

                var loop_continue = il.DefineLabel();
                il.MarkLabel(loop_continue);

                il.Emit(OpCodes.Nop);

                #region FlowMethods
                var FlowMethods = new VirtualDictionary<ILFlow, MethodBuilder>();

                FlowMethods.Resolve +=
                    flow =>
                    {
                        var FlowMethodName = "flow " + flow.Entry.Offset.ToString("x8") + " -> " + flow.Branch.Offset.ToString("x8") + " (" + flow.BranchFlow.Count + ")";

                        var FlowMethod = DefineWorkflowMethod(
                            FlowMethodName, typeof(int));

                        FlowMethods[flow] = FlowMethod;


                        foreach (var item in flow.BranchFlow)
                        {
                            var Branch = FlowMethods[item];
                        }


                        #region enter flow
                        il.Emit(OpCodes.Nop);


                        il.Emit(OpCodes.Ldloc, offset_loc);
                        il.Emit(OpCodes.Ldc_I4, flow.Entry.Offset);
                        il.Emit(OpCodes.Ceq);
                        il.Emit(OpCodes.Ldc_I4_0);
                        il.Emit(OpCodes.Ceq);
                        il.Emit(OpCodes.Stloc, flag_loc);
                        il.Emit(OpCodes.Ldloc, flag_loc);

                        var skip = il.DefineLabel();
                        il.Emit(OpCodes.Brtrue, skip);

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Call, FlowMethod);
                        il.Emit(OpCodes.Stloc, offset_loc);



                        il.MarkLabel(skip);
                        il.Emit(OpCodes.Nop);
                        #endregion

                        var FlowInstructions = Enumerable.ToArray(
                            from i in xb.Instructrions
                            where i.Offset >= flow.Entry.Offset
                            where i.Offset < flow.Branch.Offset
                            select i
                        );

                        // http://msdn.microsoft.com/en-us/library/system.reflection.emit.ilgenerator.marklabel.aspx
                        var labels = Enumerable.ToDictionary(
                            from i in FlowInstructions
                            select new { i, label = il.DefineLabel() }
                        , k => k.i, k => k.label);


                        var flow_il = FlowMethod.GetILGenerator();

                        foreach (var i in FlowInstructions)
                        {
                            il.MarkLabel(labels[i]);

                            x.Configuration[i.OpCode](new ILTranslationExtensions.EmitToArguments.ILRewriteContext { SourceMethod = SourceMethod, i = i, il = flow_il, Complete = delegate { }, Labels = labels });
                        }

                        if (flow.BranchFlow.Count == 0)
                        {
                            SwitchClosureReturn.With(
                                fld =>
                                {
                                    var ret = flow_il.DeclareLocal(SwitchClosureReturn.FieldType);

                                    flow_il.Emit(OpCodes.Stloc, ret);

                                    flow_il.Emit(OpCodes.Ldarg_0);
                                    flow_il.Emit(OpCodes.Ldloc, ret);
                                    flow_il.Emit(OpCodes.Stfld, fld);
                                }
                            );

                            flow_il.Emit(OpCodes.Ldc_I4_M1);
                            flow_il.Emit(OpCodes.Ret);
                        }
                        else if (flow.BranchFlow.Count == 1)
                        {


                            flow_il.Emit(OpCodes.Ldc_I4, flow.BranchFlow[0].Entry.Offset);
                            flow_il.Emit(OpCodes.Ret);
                        }
                        else
                        {
                            Action<Action<LocalBuilder>> WriteLookup =
                                NotifyLookup =>
                                {
                                    var m = new MemoryStream();

                                    var w = new BinaryWriter(m);

                                    foreach (var item1 in flow.BranchFlow)
                                    {
                                        w.Write(item1.Entry.Offset);
                                    }

                                    var lookup = DeclaringType.DefineInitializedData(
                                        ContextName + FlowMethodName + " lookup", m.ToArray(), FieldAttributes.Static | FieldAttributes.Family
                                    );

                                    var lookup_loc = flow_il.DeclareLocal(typeof(int));

                                    flow_il.Emit(OpCodes.Ldc_I4, flow.BranchFlow.Count);
                                    flow_il.Emit(OpCodes.Newarr, typeof(int));
                                    flow_il.Emit(OpCodes.Dup);
                                    flow_il.Emit(OpCodes.Ldtoken, lookup);
                                    flow_il.Emit(OpCodes.Call, ((Action<Array, RuntimeFieldHandle>)System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray).Method);

                                    flow_il.Emit(OpCodes.Stloc, lookup_loc);

                                    if (NotifyLookup != null)
                                        NotifyLookup(lookup_loc);
                                };


                            //L_0001: ldc.i4.s 10
                            //L_0003: newarr int32
                            //L_0008: dup 
                            //L_0009: ldtoken valuetype $ArrayType$40 <PrivateImplementationDetails>{9D96CAA6-4058-426A-87F2-B3955165707E}::$$method0x6000004-1
                            //L_000e: call void [mscorlib]System.Runtime.CompilerServices.RuntimeHelpers::InitializeArray(class [mscorlib]System.Array, valuetype [mscorlib]System.RuntimeFieldHandle)
                            //L_0013: stloc.0 

                            if (flow.Branch.OpCode == OpCodes.Switch)
                            {
                                var index_loc = flow_il.DeclareLocal(typeof(int));

                                flow_il.Emit(OpCodes.Stloc, index_loc);

                                WriteLookup(
                                    lookup_loc =>
                                    {
                                        flow_il.Emit(OpCodes.Ldloc, lookup_loc);
                                        flow_il.Emit(OpCodes.Ldloc, index_loc);
                                        flow_il.Emit(OpCodes.Ldelem_I4);
                                        flow_il.Emit(OpCodes.Ret);
                                    }
                                );

                            }
                            else
                            {
                                WriteLookup(null);

                                FlowMethod.NotImplemented();
                            }
                        }
                    };
                #endregion

                var EntryFlowMethod = FlowMethods[xb.Flow];

                il.Emit(OpCodes.Nop);

                il.MarkLabel(loop_check);

                il.Emit(OpCodes.Nop);

                il.Emit(OpCodes.Ldloc, offset_loc);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Clt);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Ceq);
                il.Emit(OpCodes.Stloc, flag_loc);

                il.Emit(OpCodes.Ldloc, flag_loc);
                il.Emit(OpCodes.Brtrue, loop_continue);

                il.Emit(OpCodes.Ret);
            }


            // step 1. create static version of the method and call that
            // step 2. create our closure and call with that

            return EntryBranch;
        }

        public static ILTranslationExtensions.EmitToArguments CreateMethodBaseEmitToArguments(
            MethodBase SourceMethod,
            //VirtualDictionary<string, string> NameObfuscation,
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

            //x[OpCodes.Leave_S] =
            //    e =>
            //    {
            //        // see: http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/afc3b34b-1d42-427c-880f-1f6372ed81ca

            //        // MethodBuilder.Emit is too nice and always writes .leave for us.
            //        // As such we need not to write this twice
            //    };

            x[OpCodes.Leave, OpCodes.Leave_S] =
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
