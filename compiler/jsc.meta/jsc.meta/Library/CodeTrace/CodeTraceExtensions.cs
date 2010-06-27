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

namespace jsc.meta.Library.CodeTrace
{
    public static class CodeTraceExtensions
    {
        internal class InternalCodeTrace : Disposable
        {

            public ILGenerator il;

            public readonly ILTranslationContext context = new ILTranslationContext();

            public readonly List<MethodBase> History = new List<MethodBase>();

            public readonly List<Type> DataTypes = new List<Type>();

            public void Invoke(Action e)
            {
                this.il.EmitWriteLine("CodeTrace #" + History.Count);

                InternalInvoke(e.Target, e.Method);

                e();
            }

            void InternalInvoke(object t, MethodBase m)
            {
                this.History.Add(m);

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
                                    foreach (var item in from k in q
                                                         where e.Context.Instructrions.Any(i => (i.OpCode == OpCodes.Ldfld || i.OpCode == OpCodes.Ldsfld) && i.TargetField == k.ff)
                                                         select k)
                                    {
                                        if (item.ff.FieldType == typeof(string))
                                        {
                                            var value = (string)item.vv;

                                            if (value == null)
                                                il.Emit(OpCodes.Ldnull);
                                            else
                                                il.Emit(OpCodes.Ldstr, value);
                                            il.Emit(OpCodes.Stsfld, context.FieldCache[item.ff]);

                                        }
                                        else if (item.ff.FieldType == typeof(int) || item.ff.FieldType.IsEnum && Enum.GetUnderlyingType(item.ff.FieldType) == typeof(int))
                                        {
                                            var value = (int)item.vv;

                                            il.Emit(OpCodes.Ldc_I4, value);
                                            il.Emit(OpCodes.Stsfld, context.FieldCache[item.ff]);

                                        }
                                        if (item.ff.FieldType == typeof(OpCode))
                                        {
                                            var value = (OpCode)item.vv;

                                            il.Emit(OpCodes.Ldsfld, typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static).Single(k => (OpCode)k.GetValue(null) == value));
                                            il.Emit(OpCodes.Stsfld, context.FieldCache[item.ff]);
                                        }
                                    }
                                };

                            a[OpCodes.Ldfld] =
                                e =>
                                {
                                    if (e.i.StackBeforeStrict[0].SingleStackInstruction.OpCode == OpCodes.Ldarg_0)
                                    {
                                        if (e.i.TargetField.FieldType.Assembly == m.DeclaringType.Assembly)
                                        {
                                        }
                                        else
                                        {
                                            e.il.Emit(OpCodes.Ldsfld, context.FieldCache[e.i.TargetField]);
                                        }
                                    }
                                    else
                                    {
                                        if (e.i.TargetField.DeclaringType.Assembly == m.DeclaringType.Assembly)
                                        {
                                            e.il.Emit(OpCodes.Ldsfld, context.FieldCache[e.i.TargetField]);
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

            var m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(target.Name), target.Name);

            var Program = m.DefineType("Program", TypeAttributes.Abstract | TypeAttributes.Sealed);

            var Main = Program.DefineMethod("Main", MethodAttributes.Static);

            var il = Main.GetILGenerator();

            il.EmitWriteLine("CodeTrace was used to record the steps to build a specific assembly.");
            il.EmitWriteLine("CodeTrace started at " + DateTime.Now.ToString());

            var ct = new InternalCodeTrace
            {

                il = il,

                AtDispose = delegate
                {
                    il.EmitWriteLine("CodeTrace ended at " + DateTime.Now.ToString());

                    il.Emit(OpCodes.Ret);

                    Program.CreateType();

                    a.SetEntryPoint(Main, PEFileKinds.ConsoleApplication);
                    a.Save(target.Name);


                }
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
