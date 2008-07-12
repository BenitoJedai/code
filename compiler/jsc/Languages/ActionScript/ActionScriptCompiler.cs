using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using ScriptCoreLib;
using System.Reflection.Emit;
using jsc.Script;
using System.Runtime.InteropServices;

namespace jsc.Languages.ActionScript
{
    partial class ActionScriptCompiler : Script.CompilerCLike
    {
        public static string FileExtension = "as";

        public readonly AssamblyTypeInfo MySession;

        public ActionScriptCompiler(TextWriter xw, AssamblyTypeInfo xs)
            : base(xw)
        {
            MySession = xs;

            CreateInstructionHandlers();
        }


  


 

        Type CachedArrayEnumeratorType;

        public Type GetArrayEnumeratorType()
        {
            return CachedArrayEnumeratorType ?? (CachedArrayEnumeratorType = (from i in MySession.ImplementationTypes
                                                                              let a = i.ToScriptAttribute()
                                                                              where a != null
                                                                              where a.IsArrayEnumerator
                                                                              select i).SingleOrDefault());
        }

        public override void WriteArrayToCustomArrayEnumeratorCast(Type Enumerable, Type ElementType, ILBlock.Prestatement p, ILFlow.StackItem s)
        {
            var x = GetArrayEnumeratorType();
            if (x == null)
                throw new Exception("SZArrayEnumerator is missing");

            var ArrayToEnumerator = x.GetImplicitOperators(null, null).Single();

            WriteDecoratedTypeNameOrImplementationTypeName(x, false, false, IsFullyQualifiedNamesRequired(p.DeclaringMethod.DeclaringType, x));
            Write(".");
            WriteDecoratedMethodName(ArrayToEnumerator, false);
            Write("(");

            Emit(p, s);

            Write(")");

        }

        public override string GetDecoratedMethodParameter(ParameterInfo p)
        {
            return GetSafeLiteral(p.Name);
        }




        public static uint[] StructAsUInt32Array(object data)
        {
            // http://www.vsj.co.uk/articles/display.asp?id=501

            var size = Marshal.SizeOf(data);
            var buf = Marshal.AllocHGlobal(size);


            Marshal.StructureToPtr(data, buf, false);

            var a = new uint[size / sizeof(int)];

            unsafe
            {
                var p = (uint*)buf;
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = *p;
                    p++;
                }
            }

            Marshal.FreeHGlobal(buf);

            return a;
        }

        public static int[] StructAsInt32Array(object data)
        {
            // http://www.vsj.co.uk/articles/display.asp?id=501

            var size = Marshal.SizeOf(data);
            var buf = Marshal.AllocHGlobal(size);


            Marshal.StructureToPtr(data, buf, false);

            var a = new int[size / sizeof(int)];

            unsafe
            {
                int* p = (int*)buf;
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] = *p;
                    p++;
                }
            }

            Marshal.FreeHGlobal(buf);

            return a;
        }


        protected override void WriteTypeInstanceConstructors(Type z)
        {
            var zci = GetAllInstanceConstructors(z);

            if (zci.Length > 1)
            {
                // visual basic can have optional parameters on its own, its c# that needs some help
                // as3 does not support method overloading but does support default parameters
                // we need to figure out which ctor is real and which are just sattelites

                // default values should be extended to allow instance values
                // a workaround could be:
                // if (param == null) { param = UIComponent; } 

                var query =
                    from c in zci
                    let b = new ILBlock(c).Prestatements.PrestatementCommands.Where(p => !p.Instruction.IsAnyOpCodeOf(OpCodes.Ret, OpCodes.Nop)).ToArray()
                    where (b.Length == 1 && b[0].Instruction == OpCodes.Call)
                    let i = b[0].Instruction
                    let t = i.TargetConstructor
                    where t != null && zci.Contains(t)
                    // skip the ldarg0/this op
                    select new { ctor = c, target = t, args = i.StackBeforeStrict.Skip(1).ToArray() };

                var cache = query.ToArray();
                var targets = zci.Except(cache.Select(i => i.ctor)).ToArray();


                if (targets.Length != 1)
                    throw new NotSupportedException("Unable to transform overloaded constructors to a single constructor via optional parameters for " + z.FullName);

                var target = targets.Single();


                // step 1
                var ctor = cache.Single(i => i.target == target);
                var args = ctor.args;

                while (true)
                {
                    ctor = cache.SingleOrDefault(i => i.target == ctor.ctor);

                    if (ctor == null)
                        break;

                    args = args.Select((s, i) => s.SingleStackInstruction.TargetParameter == null ? s : ctor.args[i]).ToArray();
                }

                // now we should have one ctor and others that point to them

                Action CustomVariableInitialization = delegate { };

                WriteMethodSignature(target, false, WriteMethodSignatureMode.Delcaring, args, i => CustomVariableInitialization += i, null);
                WriteMethodBody(target, this.MethodBodyFilter, CustomVariableInitialization);


            }
            else
            {
                foreach (var zc in zci)
                {
                    WriteMethodSignature(zc, false);
                    WriteMethodBody(zc);

                }
            }

            WriteLine();
        }

        public override void WriteAbstractMethodBody(MethodBase m)
        {
            WriteIdent();
            WriteLine("{ throw new Error(\"Abstract method not implemented\"); }");
        }
    }


}
