using System;
using System.Diagnostics;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace jsx
{
    [DebuggerDisplay("{Value.Name}")]
    public partial class InstructionAnalysis
    {
        public readonly PerformanceCounter MyPerformance = new PerformanceCounter();

        public readonly MethodBase Value;
        public readonly ReflectionCache Cache;



        public InstructionAnalysis(ReflectionCache c, DynamicMethod ne)
        {

        }

        public InstructionAnalysis(ReflectionCache c, Nonnullable<MethodBase> ne)
        {
            Value = ne;
            Cache = c;

            PrepareInstructions(
                out ByteArray,
                out Body,
                out Instructions
                );

            PrepareMetatokens(
                out ReferencedDelegateMethods,
                out ReferencedCallMethods,
                out ReferencedFields
                );

            PrepareStackChange();

            PrepareExceptionHandlingClause(
                out ExceptionHandlingClauseByOffset,
                out ExceptionHandlingClauseRangeByOffset
            );


            PrepareBranch(

                out BranchInByOffset,
                out BranchOutByOffset
                );


            PrepareStackUsage(
                out StackUsage
                );

            PrepareVariableIndex();

            PrepareVariableUsage(out Locals, out Parameters);

            PrepareBranchUsage(
                out BranchUsage
            );
        }


        public readonly Instruction[] Instructions;
        public readonly MethodBody Body;

        public readonly byte[] ByteArray;


        public readonly VariableUsage.LocalVariableList Locals;
        public readonly VariableUsage.ParameterList Parameters;

        public readonly StackUsage StackUsage;
        public readonly BranchUsage BranchUsage;


        public ExceptionHandlingClause[][] ExceptionHandlingClauseByOffset;
        public ExceptionHandlingClause[][] ExceptionHandlingClauseRangeByOffset;


        public Instruction[][] BranchInByOffset;
        public Instruction[][] BranchOutByOffset;


        public Instruction[] InstructionsByOffset;


        public readonly MethodBase[] ReferencedDelegateMethods;
        public readonly MethodBase[] ReferencedCallMethods;
        public readonly FieldInfo[] ReferencedFields;



        public static void MeasurePerformance(ReflectionCache r, params Func<Type[]>[] types)
        {
            foreach (var t in types)
                MeasurePerformance(r, t());
        }

        public static void MeasurePerformance(ReflectionCache r, params Type[] types)
        {
            Console.WriteLine("loading...");

            var s = new Stopwatch();

            s.Start();

            var u = (from v in types.SelectMany(type => type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                    where v.GetMethodBody() != null
                    let x = ReflectionCache.Default.InstructionAnalysis[v]
                    orderby x.MyPerformance.Total descending
                    select x).ToArray();


            s.Stop();

            Console.WriteLine("done...");
            Console.WriteLine("  elapsed: " + s.Elapsed);
            Console.WriteLine("    items: " + u.Count());

            Console.WriteLine("      avg: " + new TimeSpan(s.Elapsed.Ticks / u.Count()));

            var total = new TimeSpan((from v in u select v.MyPerformance.Total.Ticks).Sum());

            foreach (InstructionAnalysis v in u.Take(10))
            {

                using (new ConsoleColorText(v.Value.IsGenericMethod ? ConsoleColor.Green : ConsoleColor.DarkGreen))
                    Console.WriteLine("instructions: {0} name: 0x{1:x8} {2}", v.Instructions.Length, v.Value.MetadataToken, v.Value.Name);

                v.MyPerformance.ToConsole(total);
            }
        }


        public Instruction EntryPoint
        {
            get
            {
                return Instructions[0];
            }
        }

        

        public static OpCode[] opcodes;

        static InstructionAnalysis()
        {

            opcodes = new OpCode[0xffff + 1];


            foreach (var v in from i in typeof(OpCodes).GetFields() select (OpCode)i.GetValue(null))
            {
                try
                {
                    opcodes[v.Value & 0xFFff] = v;
                }
                catch
                {
                    throw;
                }
            }
        }

    }




}
