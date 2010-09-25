using System;
using System.Collections.Generic;
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
        // 1. generic constraints
        // 2. if (why does it not just work?)
        // 3. try catch
        // 4. string hash switching
        // 5. vb test
        // 6. optimize?
        // 7. document?
        // 8. PseudoExpression?
        // 9. Overloads?

        private static void WriteSwitchRewrite(
            MethodBase SourceMethodOrConstructor,
            TypeBuilder DeclaringType,
            ILTranslationContext context,
            Type[] ParametersTypes,
            Type ReturnType,

            Action<MethodBase, ILTranslationExtensions.EmitToArguments> ILOverride,
            ExceptionHandlingClause[] ExceptionHandlingClauses,
            ILBlock xb,

            ILGenerator kmil
            )
        {
            // do we suppport try?

            // ??
            //   Unhandled Exception: System.TypeLoadException: The signature is incorrect.




            var x = CreateMethodBaseEmitToArguments(
                    SourceMethodOrConstructor,
                    ILOverride,
                    ExceptionHandlingClauses,
                    context
                );


            // we need to capture the locals
            var ContextName = SourceMethodOrConstructor.Name + " <" + SourceMethodOrConstructor.MetadataToken + "> switch ";

            var Closure = DeclaringType.DefineNestedType(
                ContextName + "closure",
                TypeAttributes.Sealed | TypeAttributes.NestedAssembly
            );


            var Closure0 = (Type)Closure;
            var Closure_Generics = default(GenericTypeParameterBuilder[]);

            #region Closure0FieldCache
            var Closure0FieldCache = new VirtualDictionary<FieldBuilder, FieldInfo>();

            Closure0FieldCache.Resolve +=
                SourceField =>
                {
                    var fld0__ = default(FieldInfo);
                    fld0__ = SourceField;

                    if (SourceMethodOrConstructor.IsGenericMethodDefinition)
                    {
                        fld0__ = TypeBuilder.GetField(Closure0, SourceField);
                    }

                    Closure0FieldCache[SourceField] = fld0__;
                };
            #endregion


            var ClosureThis = default(FieldBuilder);

            (!SourceMethodOrConstructor.IsStatic).ThenDo(
                 delegate
                 {

                     ClosureThis = Closure.DefineField(" this", DeclaringType, FieldAttributes.Assembly);
                 }
             );

            var ClosureTypeCache = new VirtualDictionary<Type, Type>();

            ClosureTypeCache.Resolve +=
                SourceType =>
                {
                    ClosureTypeCache[SourceType] = context.TypeCache[SourceType];
                };


            if (SourceMethodOrConstructor.IsGenericMethodDefinition)
            {
                Closure_Generics = Closure.DefineGenericParameters(SourceMethodOrConstructor.GetGenericArgumentsNames());

                var i = -1;
                foreach (var item in SourceMethodOrConstructor.GetGenericArguments())
                {
                    i++;
                    ClosureTypeCache[item] = Closure_Generics[i];
                }

            }


            #region ClosureArguments
            var ClosureArguments = SourceMethodOrConstructor.GetParameters().Select(
                arg =>
                {


                    var DeclaringField = Closure.DefineField(
                        " arg" + arg.Position,
                        ClosureTypeCache[arg.ParameterType],
                        FieldAttributes.Assembly
                    );


                    #region init
                    Action<ILGenerator, LocalBuilder> init =
                        (il, closure_loc) =>
                        {
                            il.Emit(OpCodes.Nop);
                            il.Emit(OpCodes.Ldloc, closure_loc);

                            if (ClosureThis == null)
                                il.Emit(OpCodes.Ldarg, (short)arg.Position);
                            else
                                il.Emit(OpCodes.Ldarg, (short)(arg.Position + 1));

                            il.Emit(OpCodes.Stfld, Closure0FieldCache[DeclaringField]);
                            il.Emit(OpCodes.Nop);
                        };
                    #endregion

                    #region load
                    Action<ILTranslationExtensions.EmitToArguments.ILRewriteContext> load =
                       (e) =>
                       {
                           e.il.Emit(OpCodes.Ldarg_0);
                           e.il.Emit(OpCodes.Ldfld, Closure0FieldCache[DeclaringField]);
                       };
                    #endregion


                    return new
                    {
                        fld = DeclaringField,
                        init,
                        load
                    };
                }
            ).ToArray();
            #endregion


            #region ClosureLocals
            var ClosureLocals = SourceMethodOrConstructor.GetMethodBody().LocalVariables.Select(
                loc =>
                {
                    var fld = Closure.DefineField(
                        " loc" + loc.LocalIndex,
                        ClosureTypeCache[loc.LocalType],
                         FieldAttributes.Assembly
                    );

                    #region store
                    Action<ILTranslationExtensions.EmitToArguments.ILRewriteContext> store =
                        (e) =>
                        {
                            var value = e.il.DeclareLocal(context.TypeCache[loc.LocalType]);

                            e.il.Emit(OpCodes.Stloc, value);

                            e.il.Emit(OpCodes.Ldarg_0);
                            e.il.Emit(OpCodes.Ldloc, value);
                            e.il.Emit(OpCodes.Stfld, Closure0FieldCache[fld]);
                        };
                    #endregion

                    #region load
                    Action<ILTranslationExtensions.EmitToArguments.ILRewriteContext> load =
                        (e) =>
                        {
                            e.il.Emit(OpCodes.Ldarg_0);
                            e.il.Emit(OpCodes.Ldfld, Closure0FieldCache[fld]);
                        };
                    #endregion


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

            (SourceMethodOrConstructor as MethodInfo).With(
                Method =>
                {
                    if (ReturnType == typeof(void))
                        return;

                    SwitchClosureReturn = Closure.DefineField(" ret", ReturnType, FieldAttributes.Assembly);
                }
            );


            #region variable redirect
            var ArgumentOffset = (SourceMethodOrConstructor.IsStatic) ? 0 : 1;

            x[OpCodes.Ldarg_0] = e =>
            {
                if (SourceMethodOrConstructor.IsStatic)
                {
                    ClosureArguments[0].load(e);
                    return;
                }

                e.il.Emit(OpCodes.Ldarg_0);
                e.il.Emit(OpCodes.Ldfld, ClosureThis);

            };

            x[OpCodes.Ldarg_1] = e => ClosureArguments[1 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg_2] = e => ClosureArguments[2 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg_3] = e => ClosureArguments[3 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg_S] = e => ClosureArguments[e.i.OpParamAsInt8 - ArgumentOffset].load(e);
            x[OpCodes.Ldarg] = e => ClosureArguments[e.i.OpParamAsInt8 - ArgumentOffset].load(e);

            x[OpCodes.Ldloc_0] = e => ClosureLocals[0].load(e);
            x[OpCodes.Ldloc_1] = e => ClosureLocals[1].load(e);
            x[OpCodes.Ldloc_2] = e => ClosureLocals[2].load(e);
            x[OpCodes.Ldloc_3] = e => ClosureLocals[3].load(e);
            x[OpCodes.Ldloc_S] = e => ClosureLocals[e.i.OpParamAsInt8].load(e);
            x[OpCodes.Ldloc] = e => ClosureLocals[e.i.OpParamAsInt16].load(e);

            //x[OpCodes.Ldloca_S] = e => SwitchClosureFields[e.i.OpParamAsInt8].load(e);
            //x[OpCodes.Ldloca] = e => SwitchClosureFields[e.i.OpParamAsInt16].load(e);


            x[OpCodes.Stloc_0] = e => ClosureLocals[0].store(e);
            x[OpCodes.Stloc_1] = e => ClosureLocals[1].store(e);
            x[OpCodes.Stloc_2] = e => ClosureLocals[2].store(e);
            x[OpCodes.Stloc_3] = e => ClosureLocals[3].store(e);
            x[OpCodes.Stloc_S] = e => ClosureLocals[e.i.OpParamAsInt8].store(e);
            x[OpCodes.Stloc] = e => ClosureLocals[e.i.OpParamAsInt16].store(e);
            #endregion

            var ClosureConstructor = Closure.DefineDefaultConstructor(MethodAttributes.FamORAssem);




            var EntryBranchAttributes = MethodAttributes.Private;

            (SourceMethodOrConstructor.IsStatic).ThenDo(() => EntryBranchAttributes |= MethodAttributes.Static);






            var ForwardRef = DeclaringType.DefineMethod(
                ContextName + "forwardref",
                EntryBranchAttributes,
                SourceMethodOrConstructor.CallingConvention,
               context.TypeCache[ReturnType],
               context.TypeCache[SourceMethodOrConstructor.GetParameterTypes()]
            );
            


            var EntryBranch_Generics = default(GenericTypeParameterBuilder[]);


            var ClosureConstructor0 = (ConstructorInfo)ClosureConstructor;

            if (SourceMethodOrConstructor.IsGenericMethodDefinition)
            {
                EntryBranch_Generics = ForwardRef.DefineGenericParameters(SourceMethodOrConstructor.GetGenericArgumentsNames());


                Closure0 = Closure.MakeGenericType(EntryBranch_Generics.Select(k => (Type)k).ToArray());
                ClosureConstructor0 = TypeBuilder.GetConstructor(Closure0, ClosureConstructor);
            }

            Closure.DefineAttribute<CompilerGeneratedAttribute>(new object());
            ForwardRef.DefineAttribute<CompilerGeneratedAttribute>(new object());


            #region DefineWorkflowMethod
            Func<string, Type, MethodBuilder> DefineWorkflowMethod =
                (text, __ReturnType) =>
                {
                    var m = DeclaringType.DefineMethod(
                        ContextName + text,
                        MethodAttributes.Private | MethodAttributes.Static,
                        System.Reflection.CallingConventions.Standard,
                       __ReturnType,
                       new[] { Closure0 }
                    );



                    if (SourceMethodOrConstructor.IsGenericMethodDefinition)
                    {
                        var _ = m.DefineGenericParameters(SourceMethodOrConstructor.GetGenericArgumentsNames());

                    }

                    m.DefineAttribute<CompilerGeneratedAttribute>(new object());

                    return m;
                };

            #endregion

            var Workflow = DefineWorkflowMethod("workflow", typeof(void));


            #region EntryBranch
            {
                #region not DebugDisableMethodImplementation
                var il = ForwardRef.GetILGenerator();

                var closure_loc = il.DeclareInitializedLocal(Closure0, ClosureConstructor0);

                ClosureThis.With(
                  fld =>
                  {
                      il.Emit(OpCodes.Nop);

                      il.Emit(OpCodes.Ldloc, closure_loc);
                      il.Emit(OpCodes.Ldarg_0);
                      il.Emit(OpCodes.Stfld, fld);
                  }
                );

                ClosureArguments.WithEach(f => f.init(il, closure_loc));

                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldloc, closure_loc);
                il.Emit(OpCodes.Call, Workflow);

                SwitchClosureReturn.With(
                    fld =>
                    {
                        il.Emit(OpCodes.Ldloc, closure_loc);
                        il.Emit(OpCodes.Ldfld, Closure0FieldCache[fld]);
                    }
                );

                il.Emit(OpCodes.Ret);
                #endregion

            }
            #endregion

            #region Workflow
            {
                var il = default(ILGenerator);

                var offset_loc = default(LocalBuilder);
                var flag_loc = default(LocalBuilder);
                var loop_check = default(Label);
                var loop_continue = default(Label);



                il = Workflow.GetILGenerator();
                loop_check = il.DefineLabel();
                loop_continue = il.DefineLabel();

                #region not DebugDisableMethodImplementation
                // http://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.br.aspx
                offset_loc = il.DeclareLocal(typeof(int));
                flag_loc = il.DeclareLocal(typeof(bool));


                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldc_I4_0);
                il.Emit(OpCodes.Stloc, offset_loc);

                il.Emit(OpCodes.Br, loop_check);

                il.MarkLabel(loop_continue);

                il.Emit(OpCodes.Nop);

                #region FlowMethods
                var FlowMethods = new VirtualDictionary<ILFlow, MethodBuilder>();

                FlowMethods.Resolve +=
                    SourceFlow =>
                    {

                        #region FlowInstructions
                        var FlowInstructions = Enumerable.ToArray(
                            from i in xb.Instructrions
                            where i.Offset >= SourceFlow.Entry.Offset
                            where i.Offset < SourceFlow.Branch.Offset
                            select i
                        );

                        if (SourceMethodOrConstructor is ConstructorInfo)
                        {
                            // calling the base shall be done within .ctor.
                            // we have to exclude it from being rewritten to our new workflow

                            if (SourceFlow.Entry.Offset == 0)
                            {
                                Enumerable.FirstOrDefault(
                                    from k in FlowInstructions
                                    where k.OpCode == OpCodes.Call
                                    let TargetConstructor = k.TargetConstructor
                                    where TargetConstructor != null
                                    where TargetConstructor.DeclaringType == SourceMethodOrConstructor.DeclaringType.BaseType
                                    select k
                                ).With(
                                    BaseCall =>
                                    {
                                        FlowInstructions = Enumerable.ToArray(
                                            from i in FlowInstructions
                                            where i.Offset > BaseCall.Offset
                                            select i
                                        );
                                    }
                                );
                            }
                        }
                        #endregion


                        var FlowMethodName =
                            FlowInstructions.Length == 0
                            ?
                            "flow " + SourceFlow.Branch.Offset.ToString("x8") + " (" + SourceFlow.BranchFlow.Count + ")"
                            :
                            "flow " + FlowInstructions.First().Offset.ToString("x8") + " -> " + SourceFlow.Branch.Offset.ToString("x8") + " (" + SourceFlow.BranchFlow.Count + ")";

                        var FlowMethod = DefineWorkflowMethod(
                            FlowMethodName, typeof(int)
                        );

                        FlowMethods[SourceFlow] = FlowMethod;


                        foreach (var item in SourceFlow.BranchFlow)
                        {
                            var Branch = FlowMethods[item];
                        }


                        {

                            #region il
                            #region enter flow

                            il.Emit(OpCodes.Nop);


                            il.Emit(OpCodes.Ldloc, offset_loc);
                            il.Emit(OpCodes.Ldc_I4, SourceFlow.Entry.Offset);
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


                            //il.Emit(OpCodes.Br, loop_check);
                            il.MarkLabel(skip);
                            il.Emit(OpCodes.Nop);

                            #endregion


                            // http://msdn.microsoft.com/en-us/library/system.reflection.emit.ilgenerator.marklabel.aspx
                            var labels = Enumerable.ToDictionary(
                                from i in FlowInstructions
                                select new { i, label = il.DefineLabel() }
                            , k => k.i, k => k.label);


                            var flow_il = FlowMethod.GetILGenerator();

                            foreach (var i in FlowInstructions)
                            {
                                il.MarkLabel(labels[i]);

                                x.Configuration[i.OpCode](new ILTranslationExtensions.EmitToArguments.ILRewriteContext { SourceMethod = SourceMethodOrConstructor, i = i, il = flow_il, Complete = delegate { }, Labels = labels });
                            }

                            #region BranchTo
                            if (SourceFlow.BranchFlow.Count == 0)
                            {
                                SwitchClosureReturn.With(
                                    fld =>
                                    {
                                        var ret = flow_il.DeclareLocal(
                                            SwitchClosureReturn.FieldType
                                        );

                                        flow_il.Emit(OpCodes.Stloc, ret);

                                        flow_il.Emit(OpCodes.Ldarg_0);
                                        flow_il.Emit(OpCodes.Ldloc, ret);
                                        flow_il.Emit(OpCodes.Stfld, Closure0FieldCache[fld]);
                                    }
                                );

                                flow_il.Emit(OpCodes.Ldc_I4_M1);
                                flow_il.Emit(OpCodes.Ret);
                            }
                            else if (SourceFlow.BranchFlow.Count == 1)
                            {


                                flow_il.Emit(OpCodes.Ldc_I4, SourceFlow.BranchFlow[0].Entry.Offset);
                                flow_il.Emit(OpCodes.Ret);
                            }
                            else
                            {
                                #region WriteLookup
                                Action<Action<LocalBuilder>> WriteLookup =
                                    NotifyLookup =>
                                    {
                                        var m = new MemoryStream();

                                        var w = new BinaryWriter(m);

                                        foreach (var item1 in SourceFlow.BranchFlow)
                                        {
                                            w.Write(item1.Entry.Offset);
                                        }

                                        var lookup = Closure.DefineInitializedData(
                                            ContextName + FlowMethodName + " lookup",
                                            m.ToArray(),
                                            FieldAttributes.Static | FieldAttributes.Private
                                        );

                                        var lookup_loc = flow_il.DeclareLocal(typeof(int));

                                        flow_il.Emit(OpCodes.Ldc_I4, SourceFlow.BranchFlow.Count);
                                        flow_il.Emit(OpCodes.Newarr, typeof(int));
                                        flow_il.Emit(OpCodes.Dup);
                                        flow_il.Emit(OpCodes.Ldtoken, Closure0FieldCache[lookup]);
                                        flow_il.Emit(OpCodes.Call, ((Action<Array, RuntimeFieldHandle>)System.Runtime.CompilerServices.RuntimeHelpers.InitializeArray).Method);

                                        flow_il.Emit(OpCodes.Stloc, lookup_loc);

                                        if (NotifyLookup != null)
                                            NotifyLookup(lookup_loc);
                                    };
                                #endregion


                                //L_0001: ldc.i4.s 10
                                //L_0003: newarr int32
                                //L_0008: dup 
                                //L_0009: ldtoken valuetype $ArrayType$40 <PrivateImplementationDetails>{9D96CAA6-4058-426A-87F2-B3955165707E}::$$method0x6000004-1
                                //L_000e: call void [mscorlib]System.Runtime.CompilerServices.RuntimeHelpers::InitializeArray(class [mscorlib]System.Array, valuetype [mscorlib]System.RuntimeFieldHandle)
                                //L_0013: stloc.0 

                                if (SourceFlow.Branch.OpCode == OpCodes.Switch)
                                {
                                    #region Switch
                                    var index_loc = flow_il.DeclareLocal(typeof(int));

                                    // http://msdn.microsoft.com/en-us/library/system.reflection.emit.opcodes.switch.aspx
                                    // in our table -1 or "i > N" is actually at offset 0
                                    flow_il.Emit(OpCodes.Ldc_I4_1);
                                    flow_il.Emit(OpCodes.Add);
                                    flow_il.Emit(OpCodes.Stloc, index_loc);



                                    WriteLookup(
                                        lookup_loc =>
                                        {

                                            #region  if less than zero make it a zero

                                            var check_loc = flow_il.DeclareLocal(typeof(bool));


                                            flow_il.Emit(OpCodes.Ldloc, index_loc);
                                            flow_il.Emit(OpCodes.Ldc_I4_0);
                                            flow_il.Emit(OpCodes.Clt);
                                            flow_il.Emit(OpCodes.Stloc, check_loc);


                                            var br_skipmin = flow_il.DefineLabel();

                                            flow_il.Emit(OpCodes.Ldloc, check_loc);
                                            flow_il.Emit(OpCodes.Brfalse, br_skipmin);

                                            flow_il.Emit(OpCodes.Ldc_I4_0);
                                            flow_il.Emit(OpCodes.Stloc, index_loc);

                                            flow_il.MarkLabel(br_skipmin);
                                            flow_il.Emit(OpCodes.Nop);

                                            flow_il.Emit(OpCodes.Ldloc, index_loc);
                                            flow_il.Emit(OpCodes.Ldc_I4, SourceFlow.BranchFlow.Count - 1);
                                            flow_il.Emit(OpCodes.Cgt);
                                            flow_il.Emit(OpCodes.Stloc, check_loc);


                                            var br_skipmax = flow_il.DefineLabel();

                                            flow_il.Emit(OpCodes.Ldloc, check_loc);
                                            flow_il.Emit(OpCodes.Brfalse, br_skipmax);

                                            flow_il.Emit(OpCodes.Ldc_I4_0);
                                            flow_il.Emit(OpCodes.Stloc, index_loc);

                                            flow_il.MarkLabel(br_skipmax);
                                            flow_il.Emit(OpCodes.Nop);


                                            #endregion


                                            flow_il.Emit(OpCodes.Ldloc, lookup_loc);
                                            flow_il.Emit(OpCodes.Ldloc, index_loc);
                                            flow_il.Emit(OpCodes.Ldelem_I4);
                                            flow_il.Emit(OpCodes.Ret);
                                        }
                                    );
                                    #endregion

                                }
                                else
                                {
                                    //WriteLookup(null);

                                    var i = SourceFlow.Branch.StackBeforeStrict[0].SingleStackInstruction;

                                    x.Configuration[i.OpCode](new ILTranslationExtensions.EmitToArguments.ILRewriteContext { SourceMethod = SourceMethodOrConstructor, i = i, il = flow_il, Complete = delegate { }, Labels = labels });

                                    var BranchTarget = flow_il.DefineLabel();

                                    flow_il.Emit(SourceFlow.Branch.OpCode, BranchTarget);
                                    flow_il.Emit(OpCodes.Nop);
                                    flow_il.Emit(OpCodes.Ldc_I4, SourceFlow.BranchFlow[0].Entry.Offset);
                                    flow_il.Emit(OpCodes.Ret);

                                    flow_il.MarkLabel(BranchTarget);
                                    flow_il.Emit(OpCodes.Nop);
                                    flow_il.Emit(OpCodes.Ldc_I4, SourceFlow.BranchFlow[1].Entry.Offset);
                                    flow_il.Emit(OpCodes.Ret);
                                }
                            }
                            #endregion

                            #endregion


                        }
                    };
                #endregion


                var EntryFlowMethod = FlowMethods[xb.Flow];
                #endregion

                //il.Emit(OpCodes.Nop);
                //il.EmitCode(() => { throw new InvalidOperationException(); });


                #region not DebugDisableMethodImplementation
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
                #endregion

            }
            #endregion


            // step 1. create static version of the method and call that
            // step 2. create our closure and call with that

            Closure.CreateType();


            if (ForwardRef == null)
            {
                kmil.EmitCode(() => { throw new NotImplementedException(); });

            }
            else
            {
                if (SourceMethodOrConstructor is ConstructorInfo)
                {
                    // calling the base shall be done within .ctor.
                    // we have to exclude it from being rewritten to our new workflow

                    var FlowInstructions = Enumerable.ToArray(
                          from i in xb.Instructrions
                          where i.Offset < xb.Flow.Branch.Offset
                          select i
                      );

                    Enumerable.FirstOrDefault(
                        from k in FlowInstructions
                        where k.OpCode == OpCodes.Call
                        let TargetConstructor = k.TargetConstructor
                        where TargetConstructor != null
                        where TargetConstructor.DeclaringType == SourceMethodOrConstructor.DeclaringType.BaseType
                        select k
                    ).With(
                        BaseCall =>
                        {
                            FlowInstructions = Enumerable.ToArray(
                                from i in FlowInstructions
                                where i.Offset <= BaseCall.Offset
                                select i
                            );

                            var labels = Enumerable.ToDictionary(
                              from i in FlowInstructions
                              select new { i, label = kmil.DefineLabel() }
                               , k => k.i, k => k.label);


                            var kmil_x = CreateMethodBaseEmitToArguments(
                                SourceMethodOrConstructor,
                                ILOverride,
                                ExceptionHandlingClauses,
                                context
                            );


                            foreach (var i in FlowInstructions)
                            {
                                kmil.MarkLabel(labels[i]);

                                kmil_x.Configuration[i.OpCode](new ILTranslationExtensions.EmitToArguments.ILRewriteContext { SourceMethod = SourceMethodOrConstructor, i = i, il = kmil, Complete = delegate { }, Labels = labels });
                            }
                        }
                    );

                }

                kmil.Emit(OpCodes.Nop);
                kmil.Emit(OpCodes.Nop);
                kmil.Emit(OpCodes.Nop);
                kmil.Emit(OpCodes.Nop);

                if (!SourceMethodOrConstructor.IsStatic)
                    kmil.Emit(OpCodes.Ldarg_0);

                var ArgumentsOffset = SourceMethodOrConstructor.IsStatic ? 0 : 1;

                foreach (var item in SourceMethodOrConstructor.GetParameters())
                {
                    kmil.Emit(OpCodes.Ldarg, (short)(item.Position + ArgumentsOffset));
                }

                kmil.Emit(OpCodes.Call, ForwardRef);
                kmil.Emit(OpCodes.Ret);
            }

        }


    }
}
