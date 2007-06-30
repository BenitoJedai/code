using System;
using System.Reflection;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace jsx
{
    public class StackUsage
    {


        [DebuggerDisplay("Complex: {ComplexFlag} {Provider}")]
        public class Value
        {
            public bool ComplexFlag;

            public Instruction Provider;

            public readonly List<Instruction> ClientList = new List<Instruction>();

            public readonly List<Value> BranchIn = new List<Value>();
            public readonly List<Value> BranchOut = new List<Value>();

            public Type CatchType;

            public static Stack<Value> ToComplex(Stack<Value> e, Instruction p)
            {
                var u = new Stack<Value>(e.Count);

                foreach (var i in e.Reverse())
                {
                    var v = new Value();

                    v.ComplexFlag = true;
                    v.Provider = p;

                    v.BranchIn.Add(i);
                    i.BranchOut.Add(v);

                    u.Push(v);

                }

                return u;
            }


            public IEnumerable<Tuple<int, Instruction>> ToProviders(int depth)
            {
                if (!ComplexFlag)
                    yield return new Tuple<int, Instruction>(depth, Provider);
                else
                    foreach (var v in BranchIn)
                    {
                        foreach (var z in v.ToProviders(depth + 1))
                        {
                            yield return z;
                        }
                    }
            }

            public static implicit operator Instruction(Value v)
            {
                if (v.ComplexFlag)
                    throw new ArgumentException();

                if (v.Provider == null)
                    throw new ArgumentNullException();

                return v.Provider;
            }
        }


        public readonly Value[][] ByClient;
        public readonly Value[][] ByProvider;
        public readonly Stack<Value>[] ByOffset;

        public class ValueByIndex
        {
            public Value[] ByClient;
            public Value[] ByProvider;
            public Stack<Value> ByOffset;
        }

        public ValueByIndex this[int i]
        {
            get
            {
                return new ValueByIndex { 
                    ByClient = ByClient[i] ,
                    ByProvider = ByProvider[i] ,
                    ByOffset = ByOffset[i] 
                };
            }
        }

        public StackUsage(InstructionAnalysis x)
        {
            try
            {
                var maxstack = x.Body.MaxStackSize;


                var g = new OnlyOnce[x.InstructionsByOffset.Length];
                var q = new Stack<Instruction>();
                var o = ByOffset = new Stack<Value>[x.InstructionsByOffset.Length];
                var u = ByClient = new Value[x.InstructionsByOffset.Length][];
                var r = ByProvider = new Value[x.InstructionsByOffset.Length][];

                Action<Instruction, Stack<Value>> Prefetch = delegate(Instruction i, Stack<Value> v)
                {
                    o[i.Offset] = v;
                    q.Push(i);
                };

                Prefetch(x.EntryPoint, new Stack<Value>());

                #region visitor

                // add 1

                while (q.Count > 0)
                {
                    var p = q.Pop();
                    var poffset = p.Offset;

                    var e = o[poffset];

                    if (e == null) throw new ArgumentNullException();
                    if (g[poffset]++) continue;

                    var c = e.Clone();

                    p.StackSizeIn = c.Count;

                    {
                        #region stackchange
                        int pop, push;

                        var popx = u[poffset] = new Value[pop = -p.StackPopCount.Value];
                        for (int i = pop - 1; i >= 0; i--)
                        {
                            (popx[i] = c.Pop()).ClientList.Add(p);
                        }

                        var pushx = r[poffset] = new Value[push = p.StackPushCount.Value];
                        for (int i = push - 1; i >= 0; i--)
                        {
                            c.Push(pushx[i] = new Value { Provider = p });
                        }
                        #endregion
                    }

                    p.StackSizeOut = c.Count;

                    var exc = x.ExceptionHandlingClauseByOffset[poffset];

                    if (exc != null && exc.Length > 0)
                    {
                        foreach (var vexc in exc)
                        {
                            var i = x.InstructionsByOffset[vexc.HandlerOffset];
                            var f = o[i.Offset];

                            if (f != null)
                                throw new ArgumentException();

                            var fs =new Stack<Value>();

                            if (vexc.Flags == ExceptionHandlingClauseOptions.Clause)
                            {
                                fs.Push(new Value { CatchType = vexc.CatchType });
                            }

                            o[i.Offset] = fs;
                            q.Push(i);
                        }
                    }

                    if (p.BranchOut != null)
                    {
                        foreach (var i in p.BranchOut)
                        {
                            var f = o[i.Offset];

                            if (f == null)
                            {
                                o[i.Offset] = i.BranchIn.Length > 1 ? Value.ToComplex(c, i) : c;
                                q.Push(i);
                            }
                            else
                            {
                                foreach (var v in new PEnumerator<Value>(c, f))
                                {
                                    if (v[1].ComplexFlag)
                                    {
                                        v[1].BranchIn.Add(v[0]);
                                        v[0].BranchOut.Add(v[1]);
                                    }
                                    else
                                        throw new ArgumentException();
                                    // where is it already been used?
                                }
                            }
                        }
                    }
                }

                #endregion




            }
            catch
            {
                throw;
            }
            finally
            {

            }

            //ToConsole(x);

        }

        public void ToConsole(InstructionAnalysis x)
        {
            var u = this.ByClient;
            var r = this.ByProvider;

            Console.WriteLine();

            using (new ConsoleColorText(ConsoleColor.Cyan))
                Console.WriteLine("StackUsage");

            for (int i = 0; i < u.Length; i++)
            {
                var e = u[i];

                if (e != null && e.Length > 0)
                {
                    using (new ConsoleColorText(ConsoleColor.Yellow))
                        Console.WriteLine("  client: 0x{0:x4} {1} ({2})", i,x.InstructionsByOffset[i].Value.Name, e.Length);

                    ConsoleHelper(2, e, v => v.BranchIn,
                        delegate(int depth, Value v)
                        {
                            if (!v.ComplexFlag)
                            {
                                Console.Write("".PadLeft(depth));
                                using (new ConsoleColorText(ConsoleColor.Gray))
                                    if (v.CatchType == null)
                                        Console.WriteLine("  provider: 0x{0:x4} {1}", v.Provider.Offset, x.InstructionsByOffset[v.Provider.Offset].Value.Name);
                                    else
                                        Console.WriteLine("  provider execption of {0}", v.CatchType.Name);
                            }
                        });
                }
            }





            for (int i = 0; i < r.Length; i++)
            {
                var e = r[i];

                if (e != null && e.Length > 0)
                {
                    using (new ConsoleColorText(ConsoleColor.Yellow))
                        Console.WriteLine("  provider: 0x{0:x4} {1} ({2})", i, x.InstructionsByOffset[i].Value.Name, e.Length);


                    ConsoleHelper(4, e, v => v.BranchOut,
                        delegate(int depth, Value v)
                        {
                            foreach (var ex in v.ClientList)
                            {
                                using (new ConsoleColorText(ConsoleColor.Gray))
                                {
                                    Console.Write("".PadLeft(depth));
                                    Console.WriteLine("client: 0x{0:x4} {1}", ex.Offset, x.InstructionsByOffset[ex.Offset].Value.Name);
                                }

                            }
                        }
                  );



                }
            }
        }



        static void ConsoleHelper(int depth, IEnumerable<Value> a, Func<Value, IEnumerable<Value>> godeep, Action<int, Value> callback)
        {
            foreach (var ex in a)
            {
                if (callback != null)
                    callback(depth, ex);

                var ch = godeep(ex);

                if (ch.Count() > 0)
                {
                    using (new ConsoleColorText(ConsoleColor.Cyan))
                    {
                        Console.Write("".PadLeft(depth + 2));
                        Console.WriteLine("via 0x{0:x4}", ex.Provider.Offset);
                    }

                    if (depth < 2 * 5)
                        ConsoleHelper(depth + 2, godeep(ex), godeep, callback);
                    else
                    {
                        using (new ConsoleColorText(ConsoleColor.Red))
                            Console.WriteLine("*** too deep");
                    }
                }
            }


        }


    }


}
