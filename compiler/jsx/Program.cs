using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace jsx
{
    using jsx.Visual;

    
    class Program
    {

        static MethodInfo Fetch(string t, string m)
        {
            var u = Assembly.GetExecutingAssembly().GetType(t);

            return Fetch(u, m);
        }

        static MethodInfo Fetch(Type u, string m)
        {
            var z = u.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(i => i.Name == m).ToArray();

            return z.FirstOrDefault();
        }

        static IEnumerable<MethodBase> GetMoveNextMethodsFromIterators(params Type[] e)
        {
            foreach (Type v in e)
            {
                
                foreach (var z in v.GetInterfaces())
                {
                    if (z == typeof(IEnumerator))
                    {
                        var map = v.GetInterfaceMap(typeof(IEnumerator));


                        yield return map.TargetMethods[map.InterfaceMethods.IndexOf(i => i.Name == "MoveNext")];

                    }
                }
            }
        }

        static public void Main(string[] args)
        {
            {


            }

            var u = new List<string> { "asx", "ppo", "ppx" };

            var tv = new Visual.TypeViewer();



            tv.Preview +=
                delegate(object o)
                {
                    if (o is ReflectionCache)
                    {
                        var z = (ReflectionCache)o;

                        z.InstructionAnalysis.PrefetchTime.ToConsole(string.Format("{0} methods cache", z.InstructionAnalysis.Count));
                    }
                    else if (o is Action)
                    {
                        var z = (Action)o;

                        z();
                    }
                    else if (o is Assembly)
                    {
                        var z = (Assembly)o;

                        if (z.EntryPoint != null)
                        {
                            var zil = tv.Cache.InstructionAnalysis[z.EntryPoint];

                            if (zil != null)
                            {
                                Console.Clear();

                                zil.ToConsole(tv.Options);

                                return;
                            }
                        }
                    }
                    else if (o is MethodBase)
                    {
                        var z = (MethodBase)o;

                        var zil = tv.Cache.InstructionAnalysis[z];

                        if (zil != null)
                        {
                            Console.Clear();

                            zil.ToConsole(tv.Options);

                            return;
                        }
                    }
                    else if (o is InstructionAnalysisNode)
                    {
                        var z = (InstructionAnalysis)(InstructionAnalysisNode)o;

                        if (z != null)
                        {
                            Console.Clear();

                            z.ToConsole(tv.Options);

                            return;
                        }
                    }

                    //Console.WriteLine("preview: " + o);
                };


            Func<Type, CustomNode> GetEnumerators =
                delegate(Type te)
                {
                    return new CustomNode {
                        Text = "Enumerators : " + te.FullName,
                        ImageKeys = new [] { "references" },
                        Update = delegate(TreeNode tn, ActionParams<object> ap)
                        {

                            ap(GetMoveNextMethodsFromIterators(
                                te.GetNestedTypes(BindingFlags.NonPublic)
                                ).ToArray());

                        }
                    };
                };

            tv.ShowDialogThreaded(
                GetEnumerators(typeof(jsx.Tests.Sequence.YieldSupportExtensions)),

                (Action)Tests.Sequence.YieldSupport.TestMixedIterator,
                (Action)Tests.Sequence.YieldSupport.TestSelectMany,
                jsx.Tests.ILTest.bu_Test.Method,
                typeof(object),
                typeof(jsx.Tests.Sequence.YieldSupport),
                typeof(jsx.Tests.ILTest),
                Assembly.GetExecutingAssembly(),

                //Assembly.LoadFile(@"X:\c#\jsc\svn\templates\OrcasVisualBasicScriptApplication\bin\Release\OrcasVisualBasicScriptApplication.dll"),
                Assembly.LoadFile(@"C:\util\reflector\reflector.exe"),
                GetEnumerators(typeof(jsx.Tests.Sequence.YieldSupport)),
                GetEnumerators(typeof(System.Linq.Enumerable))

                ).Join();


            return;

            jsx.Tests.Sequence.YieldSupport.TestFuzzy();



            FuncParams<string, Type, MethodBase> FetchConstructor = ( t, xargs) => Assembly.GetExecutingAssembly().GetType(t).GetConstructor(xargs);

            //elapsed: 00:00:38.8833756
            //  items: 1521
            //    avg: 00:00:00.0255643

            //          elapsed: 00:00:38.4798224
            //items: 1521
            //  avg: 00:00:00.0252990

            //elapsed: 00:00:36.7755016
            //  items: 1521
            //    avg: 00:00:00.0241785

            //elapsed: 00:00:38.1082754
            //  items: 1521
            //    avg: 00:00:00.0250547
            //InstructionAnalysis.MeasurePerformance(
            //    ReflectionCache.Default,
            //    () => Assembly.LoadFile(@"X:\util\reflector\reflector.exe").GetTypes()
            ////    () => Assembly.GetExecutingAssembly().GetTypes()
            ////    //() => Assembly.LoadFile(@"X:\c#\jsc\jsc\bin\Debug\ScriptCoreLibJava.dll").GetTypes(),
            ////    //() => Assembly.LoadFile(@"X:\c#\jsc\jsc\bin\Debug\ScriptCoreLibA.dll").GetTypes(),
            ////    //() => Assembly.LoadFile(@"X:\c#\jsc\jsc\bin\Debug\ScriptCoreLib.dll").GetTypes()

            ////    //Assembly.GetExecutingAssembly().GetTypes()
            ////    // typeof(Tests.ILTest), typeof(InstructionAnalysis)
            //    );

            // 0x060006fa

            //new InstructionAnalysis(ReflectionCache.Default, ((Expression<Func<int, bool>>) n => !((n * 3) < 5)).Compile().Method).ToConsole();

            var x = ReflectionCache.Default.InstructionAnalysis[
                //Assembly.LoadFile(@"X:\util\reflector\reflector.exe").ManifestModule.ResolveMethod(0x0600073b)
                //    //Assembly.LoadFile(@"X:\util\reflector\reflector.exe").ManifestModule.ResolveMethod(0x06000001)
                //    //Assembly.LoadFile(@"X:\util\reflector\reflector.exe").ManifestModule.ResolveMethod(0x060006a8)
                //    Assembly.LoadFile(@"X:\util\reflector\reflector.exe").EntryPoint

                 //Assembly.GetExecutingAssembly().EntryPoint
                //Assembly.GetExecutingAssembly().ManifestModule.ResolveMethod(0x060000f8)
                //typeof(System.Query.Sequence).Module.ResolveMethod(0x0600025a)
                //Fetch(typeof(System.Query.Sequence).Module.ResolveType(0x0200005a), "System.IDisposable.Dispose")
                //Fetch(typeof(System.Query.Sequence).Module.ResolveType(0x0200005a), "MoveNext")
                //Fetch(typeof(System.Query.Sequence), "Where")
                //    //Fetch("jsx.Tests.ILTest", "DoVariableAnalysis")
                //    //FetchConstructor("jsx.StackUsage", typeof(InstructionAnalysis))

                //ReflectionCache.Default.InstructionAnalysis[
                //    (Action)jsx.Tests.ILTest.AnExpression].ReferencedDelegateMethods[1]

                //(Action)jsx.Tests.ILTest.IfThen
                //jsx.Tests.ILTest.bu_Test
                (Action)jsx.Tests.Sequence.YieldSupport.TestConcat
                //(Action)jsx.Tests.ILTest.IfThenElseIfThenElse
                //        //Fetch(typeof(jsx.Tests.ILTest), "IfThen")
                ];

            //var sz = x.InstructionsByOffset[0x00ae].TargetString;
            //var op = 11;
            //var ux = Assembly.LoadFile(@"X:\util\reflector\reflector.exe").ManifestModule.ResolveMethod(0x06000001).Invoke(null, new object[] { sz, op });


            x.ToConsole();


            Console.ReadLine();
        }

    }
}
