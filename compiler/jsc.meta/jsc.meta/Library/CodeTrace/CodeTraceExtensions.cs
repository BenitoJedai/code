using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using ScriptCoreLib.Ultra.Library;
using jsc.Languages.IL;
using System.Diagnostics;
using ScriptCoreLib.Extensions;

namespace jsc.meta.Library.CodeTrace
{
    public static class CodeTraceExtensions
    {
        internal class InternalCodeTrace : Disposable
        {

            public TypeBuilder Program;

            public ILGenerator il;

            public readonly ILTranslationContext context = new ILTranslationContext();

            public readonly List<MethodBase> History = new List<MethodBase>();

            public readonly List<Type> DataTypes = new List<Type>();

            public void Invoke(Action e)
            {

                InternalInvoke(e.Target, e.Method);

                e();
            }

            public MethodBuilder CurrentContinuation;

            void InternalInvoke(object t, MethodBase m)
            {
                this.History.Add(m);

                var LazyInitialize = new Dictionary<FieldInfo, Action>();


                if (false && (CurrentContinuation == null || m.GetMethodBody().LocalVariables.Any()))
                {
                    if (CurrentContinuation != null)
                        CurrentContinuation.GetILGenerator().Emit(OpCodes.Ret);

                    CurrentContinuation = Program.DefineMethod("Continuation" + this.History.Count, MethodAttributes.Private | MethodAttributes.Static, CallingConventions.Standard);

                    this.il.Emit(OpCodes.Call, CurrentContinuation);
                }

                var il = CurrentContinuation == null ? this.il : CurrentContinuation.GetILGenerator();

                var st = new StackTrace(2, true);

                var Location = st.GetFrame(0).ToString().SkipUntilOrEmpty("file:line:column").Trim();
                var LocationFile = Location.TakeUntilLastOrEmpty(":").TakeUntilLastOrEmpty(":");
                var LocationColumn = int.Parse(Location.SkipUntilLastOrEmpty(":"));
                var LocationLine = int.Parse(Location.TakeUntilLastOrEmpty(":").SkipUntilLastOrEmpty(":"));

                // http://blogs.msdn.com/b/jmstall/archive/2005/02/03/366429.aspx

                // Tell Emit about the source file that we want to associate this with. 

                // Not a debug ModuleBuilder.
                var doc = (Program.Module as ModuleBuilder).DefineDocument(
                    LocationFile,
                    Guid.Empty,
                    Guid.Empty,
                    Guid.Empty
                );

                //il.EmitWriteLine(Location);
                il.MarkSequencePoint(doc, LocationLine, LocationColumn, LocationLine, LocationColumn + 1);

                m.EmitTo(il,
                     jsc.meta.Commands.Rewrite.RewriteToAssembly.CreateMethodBaseEmitToArguments(
                        m,

                        (MethodBase mm, ILTranslationExtensions.EmitToArguments a) =>
                        {


                            a.BeforeInstructions =
                                e =>
                                {
                                    var q = from f in m.DeclaringType.GetFields()
                                            where f.FieldType.Assembly == m.DeclaringType.Assembly
                                            let v = f.GetValue(t)
                                            from ff in f.FieldType.GetFields()
                                            where ff.FieldType.Assembly != m.DeclaringType.Assembly
                                            let vv = ff.GetValue(v)
                                            select new { ff, vv };

                                    q = q.Concat(
                                        from ff in m.DeclaringType.GetFields()
                                        where ff.FieldType.Assembly != m.DeclaringType.Assembly
                                        let vv = ff.GetValue(t)
                                        select new { ff, vv }
                                    );

                                    // load+store --- we do not need to load if we only read it once?
                                    // initialize
                                    foreach (var item in
                                        from k in q

                                        let CountStore = e.Context.Instructrions.Count(i => (i.OpCode == OpCodes.Stfld || i.OpCode == OpCodes.Stsfld) && i.TargetField == k.ff)
                                        let CountLoad = e.Context.Instructrions.Count(i => (i.OpCode == OpCodes.Ldfld || i.OpCode == OpCodes.Ldsfld) && i.TargetField == k.ff)

                                        let IsInt32Array = k.ff.FieldType == typeof(int[])
                                        let GetInt32Array = new Action(
                                            delegate
                                            {
                                                var value = (int[])k.vv;


                                                var aa = il.DeclareLocal(typeof(int[]));

                                                il.Emit(OpCodes.Ldc_I4, value.Length);
                                                il.Emit(OpCodes.Newarr, typeof(int));
                                                il.Emit(OpCodes.Stloc, aa);

                                                for (int i = 0; i < value.Length; i++)
                                                {
                                                    il.Emit(OpCodes.Ldloc, aa);
                                                    il.Emit(OpCodes.Ldc_I4, i);
                                                    // null ?
                                                    il.Emit(OpCodes.Ldc_I4, value[i]);
                                                    il.Emit(OpCodes.Stelem_I4);
                                                }

                                                il.Emit(OpCodes.Ldloc, aa);
                                            }
                                        )

                                        let IsStringArray = k.ff.FieldType == typeof(string[])
                                        let GetStringArray = new Action(
                                            delegate
                                            {
                                                var value = (string[])k.vv;

                                                var aa = il.DeclareLocal(typeof(string[]));

                                                il.Emit(OpCodes.Ldc_I4, value.Length);
                                                il.Emit(OpCodes.Newarr, typeof(string));
                                                il.Emit(OpCodes.Stloc, aa);

                                                for (int i = 0; i < value.Length; i++)
                                                {
                                                    il.Emit(OpCodes.Ldloc, aa);
                                                    il.Emit(OpCodes.Ldc_I4, i);
                                                    // null ?
                                                    il.Emit(OpCodes.Ldstr, value[i]);
                                                    il.Emit(OpCodes.Stelem_Ref);
                                                }

                                                il.Emit(OpCodes.Ldloc, aa);
                                            }
                                        )

                                        let IsString = k.ff.FieldType == typeof(string)
                                        let GetString = new Action(
                                            delegate
                                            {
                                                var value = (string)k.vv;

                                                if (value == null)
                                                    il.Emit(OpCodes.Ldnull);
                                                else
                                                    il.Emit(OpCodes.Ldstr, value);
                                            }
                                        )

                                        let IsInt32 = k.ff.FieldType == typeof(int) || k.ff.FieldType.IsEnum && Enum.GetUnderlyingType(k.ff.FieldType) == typeof(int)
                                        let GetInt32 = new Action(
                                            delegate
                                            {
                                                var value = (int)k.vv;

                                                il.Emit(OpCodes.Ldc_I4, (int)k.vv);
                                            }
                                        )

                                        let IsOpCode = k.ff.FieldType == typeof(OpCode)
                                        let GetOpCode = new Action(
                                            delegate
                                            {
                                                var value = (OpCode)k.vv;

                                                il.Emit(OpCodes.Ldsfld, typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static).Single(kk => (OpCode)kk.GetValue(null) == value));
                                            }
                                        )

                                        let GetValue = IsInt32Array ? GetInt32Array : IsStringArray ? GetStringArray : IsString ? GetString : IsInt32 ? GetInt32 : IsOpCode ? GetOpCode : null


                                        let Stsfld_ff = new Action(() => il.Emit(OpCodes.Stsfld, context.FieldCache[k.ff]))

                                        select new { k.ff, k.vv, Stsfld_ff, CountStore, CountLoad, GetValue })
                                    {
                                        if (item.GetValue != null)
                                        {
                                            if (item.CountLoad == 1 && item.CountStore == 0)
                                            {
                                                LazyInitialize[item.ff] = item.GetValue;
                                            }
                                            else if (item.CountLoad > 1)
                                            {
                                                item.GetValue();
                                                item.Stsfld_ff();
                                            }
                                        }
                                    }
                                };

                            a[OpCodes.Ldfld] =
                                e =>
                                {
                                    Action GetValue = () => e.il.Emit(OpCodes.Ldsfld, context.FieldCache[e.i.TargetField]);

                                    if (LazyInitialize.ContainsKey(e.i.TargetField))
                                        GetValue = LazyInitialize[e.i.TargetField];

                                    if (e.i.StackBeforeStrict[0].SingleStackInstruction.OpCode == OpCodes.Ldarg_0)
                                    {
                                        if (e.i.TargetField.FieldType.Assembly == m.DeclaringType.Assembly)
                                        {
                                        }
                                        else
                                        {
                                            GetValue();
                                        }
                                    }
                                    else
                                    {
                                        if (e.i.TargetField.DeclaringType.Assembly == m.DeclaringType.Assembly)
                                        {
                                            GetValue();
                                        }
                                        else
                                        {
                                            e.Default();
                                        }
                                    }
                                };

                            a[OpCodes.Stfld] =
                                e =>
                                {
                                    if (e.i.StackBeforeStrict[0].SingleStackInstruction.OpCode == OpCodes.Ldarg_0)
                                    {
                                        if (e.i.TargetField.FieldType.Assembly == m.DeclaringType.Assembly)
                                        {
                                        }
                                        else
                                        {
                                            e.il.Emit(OpCodes.Stsfld, context.FieldCache[e.i.TargetField]);
                                        }
                                    }
                                    else
                                    {
                                        if (e.i.TargetField.DeclaringType.Assembly == m.DeclaringType.Assembly)
                                        {
                                            e.il.Emit(OpCodes.Stsfld, context.FieldCache[e.i.TargetField]);
                                        }
                                        else
                                        {
                                            e.Default();
                                        }
                                    }
                                };

                            a[OpCodes.Ldarg_0] =
                              e =>
                              {
                              };

                            a[OpCodes.Ret] =
                                e =>
                                {
                                };
                        },

                        m.GetMethodBody().ExceptionHandlingClauses.ToArray(),
                        context
                     )
                );
            }
        }

        /// <summary>
        /// Will build a strongly typed assembly which will replay the traced code. To be used for debugging and IL rewrite research.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="h"></param>
        public static void ToCodeTrace(this FileInfo target, Action<CodeTraceAction> h)
        {
            if (target == null)
            {
                h(y => y());

                return;
            }

            using (var ct = InternalToCodeTrace(target))
                try
                {
                    h(ct.Invoke);
                }
                catch (Exception ex)
                {
                    ct.il.EmitWriteLine("CodeTrace was aborted due to error: " + ex.ToString());

                    throw;
                }
        }

        internal static InternalCodeTrace InternalToCodeTrace(FileInfo target)
        {
            if (target.Exists)
                target.Delete();

            var a = AppDomain.CurrentDomain.DefineDynamicAssembly(
               new AssemblyName(Path.GetFileNameWithoutExtension(target.Name)), AssemblyBuilderAccess.RunAndSave, target.Directory.FullName
            );

            // Mark generated code as debuggable. 
            // See http://blogs.msdn.com/rmbyers/archive/2005/06/26/432922.aspx for explanation.        
            Type daType = typeof(DebuggableAttribute);
            ConstructorInfo daCtor = daType.GetConstructor(new Type[] { typeof(DebuggableAttribute.DebuggingModes) });
            CustomAttributeBuilder daBuilder = new CustomAttributeBuilder(daCtor, new object[] { 
            DebuggableAttribute.DebuggingModes.DisableOptimizations | 
            DebuggableAttribute.DebuggingModes.Default });
            a.SetCustomAttribute(daBuilder);


            // <-- pass 'true' to track debug info.
            var m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(target.Name), target.Name, true);

            var Program = m.DefineType("Program", TypeAttributes.Abstract | TypeAttributes.Sealed);

            var Main = Program.DefineMethod("Main", MethodAttributes.Static);

            var il = Main.GetILGenerator();

            il.EmitWriteLine("CodeTrace was used to record the steps to build a specific assembly.");
            il.EmitWriteLine("CodeTrace started at " + DateTime.Now.ToString());

            var ct = new InternalCodeTrace
            {
                Program = Program,

                il = il
            };

            ct.AtDispose = delegate
                {
                    if (ct.CurrentContinuation != null)
                        ct.CurrentContinuation.GetILGenerator().Emit(OpCodes.Ret);

                    il.EmitWriteLine("CodeTrace ended at " + DateTime.Now.ToString());

                    il.Emit(OpCodes.Ret);

                    Program.CreateType();

                    a.SetEntryPoint(Main, PEFileKinds.ConsoleApplication);
                    a.Save(target.Name);


                };

            ct.context.MethodCache.Resolve += e => ct.context.MethodCache[e] = e;
            ct.context.ConstructorCache.Resolve += e => ct.context.ConstructorCache[e] = e;


            ct.context.TypeDefinitionCache.Resolve +=
                e =>
                {
                    if (ct.History.Any(k => k.DeclaringType.Assembly == e.Assembly))
                    {
                        ct.context.TypeDefinitionCache[e] = Program;

                        foreach (var item in from f in e.GetFields()
                                             where !ct.History.Any(k => k.DeclaringType.Assembly == f.DeclaringType.Assembly)
                                             select f)
                        {
                            var _ = ct.context.FieldCache[item];
                        }

                        ct.DataTypes.Add(e);

                        return;
                    }

                    ct.context.TypeDefinitionCache[e] = e;
                };

            ct.context.TypeCache.Resolve +=
                e =>
                {

                    ct.context.TypeCache[e] = ct.context.TypeDefinitionCache[e];
                };

            var FieldNames = new List<string>();

            ct.context.FieldCache.Resolve +=
                e =>
                {

                    if (ct.History.Any(k => k.DeclaringType.Assembly == e.DeclaringType.Assembly))
                    {

                        var Name = e.Name;
                        var i = -1;

                        while (FieldNames.Contains(Name))
                        {
                            i++;
                            Name = e.Name + i;
                        }

                        FieldNames.Add(e.Name);

                        var f = Program.DefineField(Name, ct.context.TypeCache[e.FieldType], e.Attributes | FieldAttributes.Static);

                        ct.context.FieldCache[e] = f;

                        return;
                    }

                    ct.context.FieldCache[e] = e;
                };

            return ct;
        }
    }
}
