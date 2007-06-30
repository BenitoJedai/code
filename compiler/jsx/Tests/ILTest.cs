using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

namespace jsx.Tests
{
    [global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class MyAttribute : Attribute
    {
        public Action<InstructionAnalysis> Value;
    }

    public partial class ILTest
    {
        event Action MyEvent;

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        public event Action MyCustomEvent
        {
            add
            {
                MyEvent += value;
            }
            remove
            {
                MyEvent -= value;
            }
        }

        public static IEnumerable<int> GetValues()
        {
            foreach (var v in GetValue<int[]>())
            {
                yield return v;
            }
        }

        static bool GetBooleanForDoWhile()
        {
            return false;
        }

        static bool GetBoolean()
        {
            return false;
        }

        static int GetInteger()
        {
            return 6;
        }

        public static T GetValue<T>()
        {
            return default(T);
        }

        static void DoIfElseOuterA()
        {
        }

        static void DoIfElseOuterB()
        {
        }

        static void DoIfElseInnerA()
        {
        }

        static void DoIfElseInnerB()
        {
        }

        static void DoWhileInnerA()
        {
        }

        static void DoInnerDoWhile()
        {
        }

        static void DoNothing()
        {
        }

        static void DoAnything()
        {
        }

        static void DoSomething()
        {
        }

        public static T DoConvert<T, U>(U t)
        {
            return default(T);
        }

        public static void DoSomethingWith<T>(T t)
        {
        }

        public static void DoSomethingWith<T, U, W>(T t, U u)
        {
        }

        public static void DoSomethingWith<T, U, W>(T t, U u, W w)
        {
        }

        public static void DoSomethingWith<T, U, W, O>(T t, U u, W w, O o)
        {
        }

        public static void DoWithParams<T>(params T[] t)
        {

        }

        #region IfThen
        //              jsx.exe 0x0600006d jsx.Tests.ILTest IfThen
        //              0x0000 0 nop                 Next 0 0x0001
        //       0x0000 0x0001 0   call              Call 1 0x0006
        //       0x0001 0x0006 1 stloc.0             Next 0 0x0007 var+0 {0x0001}
        //       0x0006 0x0007 0   ldloc.0           Next 1 0x0008 var+0
        //       0x0007 0x0008 1     ldc.i4.0        Next 2 0x0009
        //       0x0008 0x0009 2   ceq               Next 1 0x000b {0x0007} {0x0008}
        //       0x0009 0x000b 1 stloc.1             Next 0 0x000c var+1 {0x0009}
        //       0x000b 0x000c 0   ldloc.1           Next 1 0x000d var+1
        //       0x000c 0x000d 1 brtrue.s     Cond_Branch 0 0x000f 0x0025 {0x000c}
        //       0x000d 0x000f 0 nop                 Next 0 0x0010
        //       0x000f 0x0010 0 call                Call 0 0x0015
        //       0x0010 0x0015 0 nop                 Next 0 0x0016
        //       0x0015 0x0016 0 call                Call 0 0x001b
        //       0x0016 0x001b 0 nop                 Next 0 0x001c
        //       0x001b 0x001c 0   call              Call 1 0x0021
        //       0x001c 0x0021 1 pop                 Next 0 0x0022 {0x001c}
        //       0x0021 0x0022 0 nop                 Next 0 0x0023
        //       0x0022 0x0023 0 br.s              Branch 0 0x0039
        //       0x000d 0x0025 0 nop                 Next 0 0x0026
        //       0x0025 0x0026 0   call              Call 1 0x002b
        //       0x0026 0x002b 1 pop                 Next 0 0x002c {0x0026}
        //       0x002b 0x002c 0 call                Call 0 0x0031
        //       0x002c 0x0031 0 nop                 Next 0 0x0032
        //       0x0031 0x0032 0 call                Call 0 0x0037
        //       0x0032 0x0037 0 nop                 Next 0 0x0038
        //       0x0037 0x0038 0 nop                 Next 0 0x0039
        //0x0023 0x0038 0x0039 0 call                Call 0 0x003e
        //       0x0039 0x003e 0 nop                 Next 0 0x003f
        //       0x003e 0x003f 0 ret               Return 0
        //                   PrepareStackVariables time: 00:00:00.0300432 ticks: 300432
        //                       PrepareMetatokens time: 00:00:00.0100144 ticks: 100144
        //                     PrepareInstructions time: 00:00:00.0100144 ticks: 100144
        //                     InstructionAnalysis time: 00:00:00 ticks: 0
        //                         PrepareVarIndex time: 00:00:00 ticks: 0
        //                           PrepareBranch time: 00:00:00 ticks: 0
        //                                   Total time: 00:00:00.0500720 ticks: 500720

        //              0x0000 0   call           Call 1 0x0005
        //       0x0000 0x0005 1 stloc.1          Next 0 0x0006 var+1 {0x0000}
        //       0x0005 0x0006 0   ldloc.1        Next 1 0x0007 var+1
        //       0x0006 0x0007 1 brfalse.s Cond_Branch 0 0x0009 0x001b {0x0006}
        //       0x0007 0x0009 0 call             Call 0 0x000e
        //       0x0009 0x000e 0 call             Call 0 0x0013
        //       0x000e 0x0013 0   call           Call 1 0x0018
        //       0x0013 0x0018 1 pop              Next 0 0x0019 {0x0013}
        //       0x0018 0x0019 0 br.s           Branch 0 0x002b
        //       0x0007 0x001b 0   call           Call 1 0x0020
        //       0x001b 0x0020 1 pop              Next 0 0x0021 {0x001b}
        //       0x0020 0x0021 0 call             Call 0 0x0026
        //       0x0021 0x0026 0 call             Call 0 0x002b
        //0x0019 0x0026 0x002b 0 call             Call 0 0x0030
        //       0x002b 0x0030 0 ret            Return 0
        public static void IfThen()
        {
            //var i = GetBoolean();
            //var a = GetBoolean();
            //var x = GetInteger();
            //var y = GetInteger();

            if (GetBoolean()
                || GetBoolean()
                || GetBoolean()
                )
            {
                DoIfElseOuterA();

                if (GetBoolean())
                {
                    DoIfElseInnerB();

                    //throw new Exception();
                    return;
                }


                if (GetBoolean())
                {
                    DoIfElseInnerB();

                    throw new Exception();
                }

                if (GetBoolean())
                {
                    return;
                }

                if (GetBoolean())
                {
                    throw new Exception();
                }

                //DoAnything();
                //GetBoolean();
            }

            DoNothing();
        }

        public static void IfThenElse()
        {
            if (GetBoolean()
                //|| GetBoolean()
                //|| GetBoolean()
                 )
            {
                DoIfElseOuterA();

                try
                {
                    if (GetBoolean())
                    {
                        DoIfElseInnerA();

                        return;
                    }
                }
                finally
                {



                    while (GetBoolean())
                    {
                        DoWhileInnerA();


                    }

                }

                do
                {
                    DoInnerDoWhile();


                }
                while (GetBooleanForDoWhile());


                for (int i = 0; i < GetInteger(); i++)
                {
                    DoWhileInnerA();

                }


                if (GetBoolean())
                {
                    DoIfElseInnerA();

                    //return;
                }
                else
                {
                    DoIfElseInnerB();

                }

                DoIfElseOuterA();

                //DoAnything();
                //GetBoolean();
                //throw new Exception();
            }
            else
            {
                DoIfElseOuterB();
            }

            DoNothing();
        }

        public static void IfThenElseIfThenElse()
        {
            var i = GetBoolean();
            var a = GetBoolean();
            var x = GetInteger();
            var y = GetInteger();
            var z = GetInteger();

            if (i || a && x > y)
            {
                DoNothing();
                DoAnything();
                GetBoolean();

                if (GetBoolean())
                    return;

            }
            else if (z + x < y)
            {
                GetBoolean();
                DoAnything();
                DoNothing();

                foreach (var v in GetValue<int[]>())
                {
                    DoSomethingWith(v);
                }

                DoNothing();

            }
            else
            {
                GetBoolean();
                DoAnything();
                DoNothing();
            }

            DoNothing();
        }
        #endregion

        #region WhileInteger
        //              jsx.exe 0x0600006f jsx.Tests.ILTest WhileInteger
        //              0x0000 0 nop                   Next 0 0x0001
        //       0x0000 0x0001 0   call                Call 1 0x0006
        //       0x0001 0x0006 1 stloc.0               Next 0 0x0007 var+0 {0x0001}
        //       0x0006 0x0007 0   ldc.i4.0            Next 1 0x0008
        //       0x0007 0x0008 1 stloc.1               Next 0 0x0009 var+1 {0x0007}
        //       0x0008 0x0009 0 br.s                Branch 0 0x0011
        //       0x001b 0x000b 0 call                  Call 0 0x0010
        //       0x000b 0x0010 0 nop                   Next 0 0x0011
        //0x0009 0x0010 0x0011 0   ldloc.0             Next 1 0x0012 var+0
        //       0x0011 0x0012 1     dup               Next 2 0x0013 {0x0011}
        //       0x0012 0x0013 2       ldc.i4.1        Next 3 0x0014
        //       0x0013 0x0014 3     add               Next 2 0x0015 {0x0012} {0x0013}
        //       0x0014 0x0015 2   stloc.0             Next 1 0x0016 var+0 {0x0014}
        //       0x0015 0x0016 1     ldloc.1           Next 2 0x0017 var+1
        //       0x0016 0x0017 2   clt                 Next 1 0x0019 {0x0012} {0x0016}
        //       0x0017 0x0019 1 stloc.2               Next 0 0x001a var+2 {0x0017}
        //       0x0019 0x001a 0   ldloc.2             Ne   t 1 0x001b var+2
        //       0x001a 0x001b 1 brtrue.s       Cond_Branch 0 0x000b 0x001d {0x001a}
        //       0x001b 0x001d 0 call                  Call 0 0x0022
        //       0x001d 0x0022 0 nop                   Next 0 0x0023
        //       0x0022 0x0023 0 ret                 Return 0

        //              jsx.exe 0x0600006f jsx.Tests.ILTest WhileInteger
        //              0x0000 0   call                Call 1 0x0005
        //       0x0000 0x0005 1 stloc.2               Next 0 0x0006 var+2 {0x0000}
        //       0x0005 0x0006 0   ldc.i4.0            Next 1 0x0007
        //       0x0006 0x0007 1 stloc.3               Next 0 0x0008 var+3 {0x0006}
        //       0x0007 0x0008 0 br.s                Branch 0 0x000f
        //       0x0015 0x000a 0 call                  Call 0 0x000f
        //0x0008 0x000a 0x000f 0   ldloc.2             Next 1 0x0010 var+2
        //       0x000f 0x0010 1     dup               Next 2 0x0011 {0x000f}
        //       0x0010 0x0011 2       ldc.i4.1        Next 3 0x0012
        //       0x0011 0x0012 3     add               Next 2 0x0013 {0x0010} {0x0011}
        //       0x0012 0x0013 2   stloc.2             Next 1 0x0014 var+2 {0x0012}
        //       0x0013 0x0014 1     ldloc.3           Next 2 0x0015 var+3
        //       0x0014 0x0015 2 blt.s          Cond_Branch 0 0x000a 0x0017 {0x0010} {0x0014}
        //       0x0015 0x0017 0 call                  Call 0 0x001c
        //       0x0017 0x001c 0 ret                 Return 0
        static public void WhileInteger()
        {
            var i = GetInteger();
            var x = 0;

            while (i++ < x)
                DoNothing();

            DoAnything();
        }

        static public void AnExpression()
        {

            var z = from i in GetValue<int[]>()
                    let xv = GetValue<int>() - i
                    where i > GetValue<int>() || GetBoolean() || GetBoolean()
                    select new { u = DoConvert<string, int>(i), x = xv };

            var x = new {
                z,
                a = GetValue<string>()
            };


        }

        #endregion



        public static int MyField;

        struct AX
        {
            public string z;
            public double PPp;


        }

        interface Ixx
        {
            void Do();
        }

        public class IX<T>
        {
            public T Lambada;

            public static implicit operator IX<T>(T Lambada)
            {
                return new IX<T> { Lambada = Lambada };
            }
        }

        // java listener support?
        // for j2me: infer target method signatur to a interface, 
        // set it to be implemented and tadaa!!

        public abstract class LambadaBase<T, U>
            where U : LambadaBase<T, U>, new()
        {
            public T[] Lambadas;



            public static implicit operator LambadaBase<T, U>(T e)
            {
                return (LambadaBase<T, U>)new [] { e };
            }

            public static implicit operator LambadaBase<T, U>(T[] e)
            {
                var n = new U();

                n.Lambadas = e;

                return n;
            }
        }

        public interface IDoSpecialCommand
        {
            void MyCommand();
        }

        public sealed class DoSpecialCommand : LambadaBase<Action, DoSpecialCommand>, IDoSpecialCommand
        {
            public void MyCommand()
            {
                foreach (var v in base.Lambadas) v();
            }
        }

        public static void Do()
        {
        }

        public static IX<Action> MyAction;
        public static IDoSpecialCommand MySpecialCommand;

        public static void DoCallback(IDoSpecialCommand e)
        {
            e.MyCommand();
        }

        public static void Do1()
        {
            Console.WriteLine(MethodInfo.GetCurrentMethod().Name);
        }

        public static void Do2()
        {
            Console.WriteLine(MethodInfo.GetCurrentMethod().Name);
        }

        public virtual void DoVirtual()
        {

        }

        public static void TestProjections()
        {
            Tuple<int, bool> t;

            t.First = 45;

            AX vax;

            vax.z = "yo";

            DoCallback((DoSpecialCommand)new ILTest().DoVirtual);

            var z = "sdfsdf";
            var PPp = 3.14 - t.First;

            if (PPp > 3)
                throw new NotSupportedException();

            var n = new { z, PPp, Do = (Action)delegate { Do1(); } };


            MyAction = (Action)Do;

            DoCallback((DoSpecialCommand)(() => z += ""));
            DoCallback((DoSpecialCommand)(new Action[] { Do1, Do2 }));
            //    () => z += "",  
            //    () => z += "" }
            //);


            AX r;

            n.ToStruct(out r);

            DoSomethingWith(n);
            DoSomethingWith(r);
        }

        public void DoVariableAnalysis(byte[] buffer,
            object a1,
                object a2,
                    object a3,
                        object a4,

            int abox, int a, ref int b, out int c, out int op, ref object e, IEnumerable<int> xu)
        {
            // pt is a managed variable, subject to g.c.

            if (abox == 5)
                throw null;

            // must use fixed to get address of cl.R

            var axx = xu.Min();

            var u = this;

            var p = a1 ?? a2 ?? a3 ?? a4 ?? e ?? GetValue<object>();


            a = b - 7;

            op = 0;
            c = 0;

            Action ax = () => a = 66;

            var z = new { b, u };

            DoSomethingWith(z.b);
            DoSomethingWith(z.u);
            DoSomethingWith(z);

            switch (a)
            {
                case 0: ax(); return;
                default:
                    MyField = a - 9;

                    ax();
                    break;
            }

            op = -1;

            c = b + GetInteger();

            try
            {
                b = a + GetInteger();

                var i = 7;

                if (GetBoolean())
                {
                    i++;
                }

                DoSomethingWith(i + c);
            }
            catch
            {

            }
        }

        static public void DoVariableAnalysis3()
        {
            var i = 0;

            try
            {
                i = 5;

                DoSomething();

                i = 6;
            }
            catch (Exception)
            {
                DoSomethingWith(i);

                i = 8;
            }
            catch
            {
                DoSomethingWith(i);

                i = 8;
            }
            finally
            {
                DoSomethingWith(i);

                i = 9;
            }

            DoSomethingWith(i);
        }

        static public void DoVariableAnalysis2()
        {
            var x = GetValue<Action>();
            var a = GetValue<object>();

            if (GetBoolean())
            {
                DoSomethingWith(a);

                a = null;
            }
            else
            {
                var i = GetInteger();

                var um = GetInteger();

                try
                {
                    um = 9;

                    DoSomethingWith(um++);

                    while (i++ < 4)
                    {
                        if (GetBoolean())
                            a = GetValue<object>();
                        else
                            a = null;
                    }
                }
                catch
                {
                    DoSomethingWith(um++);

                    try
                    {
                        DoSomethingWith(um++);
                    }
                    catch (Exception exc)
                    {
                        DoSomethingWith(um++);

                        throw;
                    }
                    catch
                    {
                        DoSomethingWith(um++);

                        throw;
                    }
                    finally
                    {
                        DoSomethingWith(um++);
                    }

                    DoNothing();

                    DoSomethingWith(a = GetValue<object>());
                }
                finally
                {
                    DoSomethingWith(um++);
                }
            }

            DoSomethingWith(a);
        }

        #region TryFinally
        static public void TryFinally()
        {
            DoAnything();

            try
            {
                DoAnything();

                var n = GetValue<bool>() ? 7 : 8;

                var i = GetValue<int>();

                while (GetInteger() < 0)
                {
                    try
                    {
                        DoAnything();
                    }
                    catch
                    {
                        i++;

                        DoSomething();
                    }
                    finally
                    {
                        DoNothing();
                    }
                }

                DoNothing();
            }
            catch
            {
                DoSomething();
            }
            finally
            {
                DoNothing();
            }

            DoNothing();
            if (GetBoolean())
            {
                try
                {
                    DoAnything();

                    try
                    {
                        DoAnything();
                    }
                    catch
                    {
                        DoSomething();
                    }
                    finally
                    {
                        DoNothing();
                    }
                }
                catch
                {
                    DoSomething();
                }
                finally
                {
                    DoNothing();
                }
            }

            DoNothing();
        }
        #endregion
    }

}
