using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Text;

namespace jsx
{
    public enum VariableOperationEnum
    {
        None,
        Store,
        Load
    }

    public enum VariableNodeTypeEnum
    {
        Share,
        EntryPoint,
        Provider,
        BranchOut,
        BranchIn,
        BranchProtected,
        BranchProtectedHandler,
        BranchFixup

    }


    [DebuggerDisplay("index:{Index}, type:{NodeType},  in: {BranchIn.Count}, entry: {EntryPoint}, out: {BranchOut.Count}")]
    public class VariableUsage
    {
        [DebuggerDisplay("#{Index} {TargetProvider} => {Clients}")]
        public class ProviderInfo
        {
            public int Index;

            public VariableUsage TargetProvider;

            public readonly List<ClientInfo> Clients = new List<ClientInfo>();

            public ProviderInfo(int i, VariableUsage tp)
            {
                Index = i;
                TargetProvider = tp;
            }
        }

        [DebuggerDisplay("{Providers} => {TargetInstruction}")]
        public class ClientInfo
        {
            public Value Owner;

            public VariableUsage[] Providers;

            public VariableUsage CachedClient;

            public Instruction TargetInstruction;

            public readonly TimeCounter ResolveTime = new TimeCounter();

            static VariableUsage SingleProvider(VariableUsage e)
            {
                var p = e;

                while (true)
                {
                    if (p.NodeType == VariableNodeTypeEnum.Provider)
                        return p;

                    if (p.BranchIn.Count == 1)
                        p = p.BranchIn[0];
                    else
                        return null;
                }
            }

            public void Resolve()
            {
                using (~ResolveTime)
                {
                    foreach (var p in Providers = ResolveProviders())
                    {
                        //Console.WriteLine("provider #{0} 0x{1:x4} client 0x{2:x4}", p.Info.Index, p.EntryPoint.Offset, TargetInstruction.Offset);

                        p.Info.Clients.Add(this);
                    }
                }
            }

            VariableUsage[] ResolveToMultipleProviders()
            {
                var s = new Stack<VariableUsage>();
                var g = new byte[this.Owner.ByIndex.Length];
                var a = new List<VariableUsage>();

                s.PushAll(CachedClient.BranchIn.ToArray());

                while (s.Count > 0)
                {
                    var p = s.Pop();

                    if (g[p.Index]++ > 0)
                        continue;

                    // detect entry node if looking for arguments

                    if (p.NodeType == VariableNodeTypeEnum.Provider)
                    {
                        a.Add(p);
                    }
                    else
                    {
                        s.PushAll(p.BranchIn.ToArray());
                    }
                }

                return a.ToArray();
            }

            VariableUsage[] ResolveProviders()
            {
                var s = SingleProvider(CachedClient);

                if (s != null)
                    return new [] { s };


                return ResolveToMultipleProviders();
            }
        }

        public readonly List<Instruction> Instructions = new List<Instruction>();

        public readonly List<ClientInfo> Clients = new List<ClientInfo>();

        public readonly List<VariableUsage> BranchIn = new List<VariableUsage>();
        public readonly List<VariableUsage> BranchOut = new List<VariableUsage>();

        public ProviderInfo Info;


        public readonly int Index;
        public readonly VariableNodeTypeEnum NodeType;

        public Instruction EntryPoint
        {
            get
            {
                return Instructions.FirstOrDefault();
            }
        }

        public Instruction ExitPoint
        {
            get
            {
                return Instructions.LastOrDefault();
            }
        }

        public static VariableUsage operator +(VariableUsage from, VariableUsage to)
        {
            if (from != null)
            {
                from.BranchOut.Add(to);
                to.BranchIn.Add(from);
            }

            return to;
        }

        private VariableUsage(VariableNodeTypeEnum t, int i)
        {
            NodeType = t;
            Index = i;
        }



        // PrepareVariableUsage time: 00:00:00.0300432 ticks: 300432

        public class ParameterList : ValueList<Parameter, ParameterInfo>
        {
            public ParameterList(IEnumerable<ParameterInfo> e, InstructionAnalysis x)
                : base(e, x)
            {
            }
        }

        public class Parameter : Value<ParameterInfo>
        {
            public Parameter(ParameterInfo tt, InstructionAnalysis x)
                : base(tt, x,
                delegate(Instruction i)
                {

                    if (i.TargetParameterStoreIndex == tt.Position) return VariableOperationEnum.Store;
                    if (i.TargetParameterLoadIndex == tt.Position) return VariableOperationEnum.Load;

                    return VariableOperationEnum.None;
                }
                )
            {

            }

            public override int Index
            {
                get { return TargetVariable.Position; }
            }


            public override Type TargetType
            {
                get { return TargetVariable.ParameterType; }
            }
        }

        public class LocalVariableList : ValueList<LocalVariable, LocalVariableInfo>
        {
            public LocalVariableList(IEnumerable<LocalVariableInfo> e, InstructionAnalysis x)
                : base(e, x)
            {
            }


        }

        public class LocalVariable : Value<LocalVariableInfo>
        {

            public LocalVariable(LocalVariableInfo tt, InstructionAnalysis x)
                : base(tt, x,
                delegate(Instruction i)
                {
                    if (i.TargetVariableStoreIndex == tt.LocalIndex) return VariableOperationEnum.Store;
                    if (i.TargetVariableLoadIndex == tt.LocalIndex) return VariableOperationEnum.Load;

                    return VariableOperationEnum.None;
                })
            {
            }

            public override int Index
            {
                get { return TargetVariable.LocalIndex; }
            }

            public override Type TargetType
            {
                get { return TargetVariable.LocalType; }
            }
        }

        public class ValueList<T, U>
            where T : Value<U>
        {
            public readonly T[] Values;

            public readonly TimeCounter ConstructorTime = new TimeCounter();

            public ValueList(IEnumerable<U> e, InstructionAnalysis x)
            {
                using (~ConstructorTime)
                    Values = e.Select(i => (T)Activator.CreateInstance(typeof(T), i, x)).ToArray();
            }

           

            public void ToConsole()
            {
                Console.WriteLine();

                using (new ConsoleColorText(ConsoleColor.Cyan))
                    Console.WriteLine(typeof(U).Name);


                #region show p - c
                foreach (Value<U> loc in Values)
                {
                    // d: PrepareVariableUsage time: 00:00:00.0500720 ticks: 500720
                    // r: PrepareVariableUsage time: 00:00:00.0400576 ticks: 400576
                    if (loc == null)
                        continue;
                    
                    

                    using (new ConsoleColorText(ConsoleColor.Yellow))
                        Console.WriteLine("  value: " + loc.Index + " as " + loc.TargetType.Name);

                    foreach (var p in loc.Providers)
                    {

                        Console.WriteLine("    provider: 0x{0:x4} ({1})", p.TargetProvider.EntryPoint.Offset, p.Clients.Count);

                        foreach (var c in p.Clients)
                        {
                            Console.WriteLine("      client: 0x{0:x4}", c.TargetInstruction.Offset);

                        }
                    }
                }
                #endregion

                #region show c - p
                foreach (Value<U> loc in Values)
                {
                    if (loc == null)
                        continue;

                    // d: PrepareVariableUsage time: 00:00:00.0500720 ticks: 500720
                    // r: PrepareVariableUsage time: 00:00:00.0400576 ticks: 400576


                    using (new ConsoleColorText(ConsoleColor.Yellow))
                        Console.WriteLine("  value: " + loc.Index + " as " + loc.TargetType.Name);

                    foreach (var c in loc.Clients)
                    {

                        Console.WriteLine("    client: 0x{0:x4} ({1})", c.TargetInstruction.Offset, c.Providers.Length);

                        foreach (var p in c.Providers)
                        {
                            Console.WriteLine("      provider: 0x{0:x4}", p.EntryPoint.Offset);

                        }
                    }
                }
                #endregion
            }
        }

        public abstract class Value<T> : Value
        {
            public readonly T TargetVariable;

            public Value(T tt, InstructionAnalysis x, Func<Instruction, VariableOperationEnum> op)
                : base(x, op)
            {
                TargetVariable = tt;
            }

            public abstract Type TargetType { get; }

            public abstract int Index { get;  }
        }

        public class Value
        {
            public VariableUsage[] ByIndex;
            public VariableUsage[] ByOffset;

            public VariableUsage this[Instruction i]
            {
                get
                {
                    Debug.Assert(i != null);

                    return ByOffset[i.Offset];
                }
            }


            public Value(InstructionAnalysis x, Func<Instruction, VariableOperationEnum> GetOperation)
            {
                try
                {
                    var a = new List<VariableUsage>();
                    var o = new VariableUsage[x.InstructionsByOffset.Length];
                    var g = new OnlyOnce[x.InstructionsByOffset.Length];
                    var s = new Stack<Instruction>();

                    var c = new List<ClientInfo>();
                    var w = new List<ProviderInfo>();

                    #region DoCache
                    var DoCache = (Func<Instruction, VariableUsage, VariableNodeTypeEnum, VariableUsage>)
                        delegate(Instruction i, VariableUsage e, VariableNodeTypeEnum nt)
                        {
                            switch (nt)
                            {
                                case VariableNodeTypeEnum.EntryPoint:
                                case VariableNodeTypeEnum.Provider:
                                    break;
                                case VariableNodeTypeEnum.BranchFixup:
                                    if (o[i.Offset] != null)
                                    {
                                        var to = e + o[i.Offset];

                                        return e;
                                    }
                                    else
                                        throw new NotSupportedException();
                                case VariableNodeTypeEnum.BranchOut:
                                case VariableNodeTypeEnum.BranchIn:
                                case VariableNodeTypeEnum.BranchProtected:
                                    if (o[i.Offset] != null)
                                    {
                                        var to = e + o[i.Offset];

                                        return e;
                                    }
                                    else
                                        break;
                                default:
                                    if (o[i.Offset] != null)
                                        throw new NotSupportedException();
                                    else
                                        break;
                            }


                            var r = o[i.Offset] = (nt == VariableNodeTypeEnum.Share) ? e : (e + new VariableUsage(nt, a.Count));

                            r.Instructions.Add(i);

                            if (nt != VariableNodeTypeEnum.Share)
                                a.Add(r);

                            s.Push(i);

                            return r;
                        };
                    #endregion


                    DoCache(x.EntryPoint, null, VariableNodeTypeEnum.EntryPoint);

                    #region visitor
                    while (s.Count > 0)
                    {
                        var p = s.Pop();
                        var e = o[p.Offset];

                        if (e == null) throw new NotSupportedException();

                        if (g[p.Offset]++) continue;


                        switch (GetOperation(p))
                        {
                            case VariableOperationEnum.Store:
                                {
                                    e = DoCache(p, e, VariableNodeTypeEnum.Provider);

                                    w.Add(e.Info = new ProviderInfo(w.Count, e));

                                    var exc = x.ExceptionHandlingClauseRangeByOffset[p.Offset];

                                    if (exc == null)
                                        break;

                                    DoCache(p.BranchOut[0], e, VariableNodeTypeEnum.BranchProtected);


                                    foreach (var vexc in exc)
                                    {
                                        DoCache(x.InstructionsByOffset[vexc.HandlerOffset], e, VariableNodeTypeEnum.BranchFixup);

                                    }

                                    continue;
                                }
                            case VariableOperationEnum.Load:
                                {
                                    var n = new ClientInfo { CachedClient = e, TargetInstruction = p, Owner = this };

                                    e.Clients.Add(n);
                                    c.Add(n);

                                    break;
                                }
                        }

                        var uexc = x.ExceptionHandlingClauseByOffset[p.Offset];

                        if (uexc != null)
                        {
                            foreach (var vexc in uexc)
                            {
                                DoCache(x.InstructionsByOffset[vexc.HandlerOffset], e, VariableNodeTypeEnum.BranchProtectedHandler);
                            }

                            DoCache(p.BranchOut[0], e, VariableNodeTypeEnum.BranchProtected);

                            continue;
                        }

                        if (p.BranchOut != null)
                        {
                            if (p.BranchOut.Length == 1)
                            {
                                var v = p.BranchOut[0];

                                Debug.Assert(v.BranchIn != null);
                                Debug.Assert(v.BranchIn.Length > 0);

                                if (v.BranchIn.Length == 1)
                                {
                                    Debug.Assert(v.BranchIn[0] == p);

                                    DoCache(v, e, VariableNodeTypeEnum.Share);
                                }
                                else
                                {
                                    DoCache(v, e, VariableNodeTypeEnum.BranchIn);
                                }
                            }
                            else
                            {
                                foreach (var v in p.BranchOut)
                                {
                                    DoCache(v, e, VariableNodeTypeEnum.BranchOut);
                                }
                            }
                        }




                    }
                    #endregion

                    ByIndex = a.ToArray();
                    ByOffset = o;

                    foreach (var v in c)
                    {
                        v.Resolve();
                    }

                    Providers = w.ToArray();
                    Clients = c.ToArray();
                }
                catch
                {

                    throw;
                }
            }

            public readonly ProviderInfo[] Providers;
            public readonly ClientInfo[] Clients;
        }

    }

}
