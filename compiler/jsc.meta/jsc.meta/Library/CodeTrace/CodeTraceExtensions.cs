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
        internal class InternalCodeTrace : Disposable, ICodeTrace
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
                                    // initialize
                                    foreach (var item in from f in m.DeclaringType.GetFields()
                                                         where f.FieldType.Assembly == m.DeclaringType.Assembly
                                                         let v = f.GetValue(t)
                                                         from ff in f.FieldType.GetFields()
                                                         where ff.FieldType.Assembly != m.DeclaringType.Assembly
                                                         let vv = ff.GetValue(v)
                                                         select new { ff, vv })
                                    {
                                        if (item.ff.FieldType == typeof(string))
                                        {
                                            var value = (string)item.vv;

                                            il.Emit(OpCodes.Ldstr, value);
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

        public static ICodeTrace ToCodeTrace(this FileInfo target)
        {
            var a = AppDomain.CurrentDomain.DefineDynamicAssembly(
               new AssemblyName(Path.GetFileNameWithoutExtension(target.Name)), AssemblyBuilderAccess.RunAndSave, target.Directory.FullName
            );

            var m = a.DefineDynamicModule(Path.GetFileNameWithoutExtension(target.Name), target.Name);

            var Program = m.DefineType("Program", TypeAttributes.Abstract | TypeAttributes.Sealed);

            var Main = Program.DefineMethod("Main", MethodAttributes.Static);



            var ct = new InternalCodeTrace
            {
                Program = Program,

                il = Main.GetILGenerator(),

                AtDispose = delegate
                {
                    Main.GetILGenerator().Emit(OpCodes.Ret);

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
