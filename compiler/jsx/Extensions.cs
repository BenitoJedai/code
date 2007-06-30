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
    public delegate TReturn FuncParams<A0, TReturn>(params A0[] a0);
    public delegate TReturn FuncParams<A0, A1, TReturn>(A0 a0, params A1[] a1);

    public delegate void ActionParams<A0>(params A0[] a0);
    public delegate void ActionParams<A1, A0>(A1 a1, params A0[] a0);
    public delegate void ActionParams<A2, A1, A0>(A2 a2, A1 a1, params A0[] a0);

    public delegate void Action();
    public delegate void Action<A0, A1>(A0 a0, A1 a1);
    public delegate void Action<A0, A1, A2>(A0 a0, A1 a1, A2 a2);
    public delegate void Action<A0, A1, A2, A3>(A0 a0, A1 a1, A2 a2, A3 a3);
    public delegate void Action<A0, A1, A2, A3, A4>(A0 a0, A1 a1, A2 a2, A3 a3, A4 a4);

    public delegate void ActionOut<Arg1>(out Arg1 a);
    public delegate void ActionOut<Arg1, Arg2>(out Arg1 a1, out Arg2 a2);


    public static class Extenstions
    {
        public static void TimeCounterToConsole(this jsx.TimeCounter.IProvider e)
        {
            TimeCounter.ToConsole(e);
        }
    
        public static IEnumerable<InterfaceMapping> GetInterfaceMap(this Type e)
        {
            foreach (Type v in e.GetInterfaces())
            {
                yield return e.GetInterfaceMap(v);
            }
        }


        //public static IEnumerable<T> Where<T>(this IEnumerable<T> e, Func<T, bool> h)
        //{


        //    return e.Where(i => h(i));
        //}

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> e, Func<T, bool> h)
        {


            return e.Where(i => !h(i));
        }

        public static bool DoSingle<T>(this IEnumerable<T> e, Action<T> h)
        {
            var z = e.SingleOrDefault();

            if (z == null)
                return false;

            h(z);

            return true;
        }

        public static void RecursionCall(object obj, params object[] e)
        {
            new StackFrame(1).GetMethod().Invoke(obj, e);
        }

        public static bool IsExtensionMethod(this ICustomAttributeProvider e)
        {
            if (e == null)
                return false;

            return e.GetCustomAttributes(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false).Length > 0;
        }

        public static bool IsCompilerGenerated(this ICustomAttributeProvider e)
        {
            if (e == null)
                return false;

            return e.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), false).Length > 0;
        }

        public static void ToStruct<T>(this object e, out T o)
             where T : struct
        {
            o = ToStruct<T>(e);
        }

        public static T ToStruct<T>(this object e)
            where T: struct
        {
            var r = default(T);


            var rf = r.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var ef = e.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


            foreach (FieldInfo efi in ef)
            {
                FieldInfo rfi = rf.SingleOrDefault(i => "_" + i.Name == efi.Name);

                if (rfi == null)
                    continue;

                rfi.SetValue(r, efi.GetValue(e));
            }
            return r;
        }

        public static T[] CreateTypeArray<T>(int count)
            where T : new()
        {
            var n = new T[count];

            for (int i = 0; i < count; i++)
            {
                n[i] = new T();
            }


            return n;
        }

        public static string RemoveEnd(this string e, string v)
        {
            if (e.EndsWith(v))
                return e.Remove(e.Length - v.Length);

            return e;
        }

        public static Stack<V> Clone<V>(this Stack<V> e)
        {
            var a = e.ToArray().Reverse();

            var s = new Stack<V>(a);

            return s;
        }



        public static bool EqualsAny<T>(this T e, params T[] u)
        {
            foreach (var v in u)
            {
                if (e.Equals(v))
                    return true;
            }

            return false;
        }


        public static void Invoke(this IEnumerable<Action> e)
        {
            foreach (var v in e)
            {
                v();   
            }
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> e)
        {
            return new Queue<T>(e);
        }

        public static int? CountOrDefault<T>(this IEnumerable<T> e)
        {
            if (e == null)
                return null;

            return e.Count();
        }

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> e)
            where T : class
        {
            return e.Where(i => i != null).SelectMany(i => i);
        }

        public static int IndexOf<T>(this IEnumerable<T> e, Func<T, bool> v)
            where T : class
        {
            int i = 0;

            foreach (var z in e)
            {
                if (v(z))
                    return i;

                i++;
            }

            return -1;
        }

        public static int? IndexOf<T>(this IEnumerable<T> e, T v)
            where T : class
        {
            int i = 0;

            foreach (var z in e)
            {
                if (z == v)
                    return i;

                i++;
            }

            return null;
        }

        public static void ToConsole<T>(this IEnumerable<T> e)
        {
            ToConsole(e, "{0}");
        }

        public static void ToConsole<T>(this IEnumerable<T> e, string format)
        {
            foreach (var v in e)
            {
                Console.WriteLine(format, v);
            }
        }

        public static void ToConsole<T>(this IEnumerable<T> e, Func<T, object> args)
        {
            foreach (var v in e)
            {
                Console.WriteLine("{0}", args(v));
            }
        }

        public static void ToConsole<T>(this IEnumerable<T> e, string format, Func<T, object[]> args)
        {
            foreach (var v in e)
            {
                Console.WriteLine(format, args(v));
            }
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> e)
        {
            if (e != null)
            {

                foreach (var v in e)
                {
                    if (v != null)
                        yield return v;
                }
            }
        }

        public class IndexValuePair<T>
        {
            public int Index;
            public T Value;
        }

        public static IEnumerable<IndexValuePair<T>> NotNullWithIndex<T>(this IEnumerable<T> e)
        {
            if (e != null)
            {
                int Index = 0;

                foreach (var Value in e)
                {
                    if (Value != null)
                        yield return new IndexValuePair<T> { Index = Index, Value = Value };

                    Index++;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> e, Action<T> f)
        {
            if (e == null)
                return;

            foreach (var v in e)
            {
                f(v);
            }

        }

        public static void Pop32(this Stack<byte> e, uint[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = Pop32(e, 4);
            }
        }

        public static void PushAll<T>(this Stack<T> e, params T[] a)
        {
            if (a == null)
                return;

            foreach (var v in a)
            {
                e.Push(v);

            }
        }

        public static uint Pop32(this Stack<byte> e, int num)
        {
            uint v = 0;

            for (int i = 0; i < num; i++)
            {
                v += (uint)(e.Pop() << (i * 8));
            }

            return v;
        }

        public static IEnumerable<T> TakeRange<T>(this IEnumerable<T> e, T from, T to)
        {
            var z = e.SkipWhile(x => !Comparer.Equals(x, from));

            foreach (var v in z)
            {
                yield return v;

                if (Comparer.Equals(v, to))
                    break;
            }
        }


        public static int? GetOperandSize(this OpCode o)
        {

            switch (o.OperandType)
            {
                case OperandType.InlineBrTarget: return 4;
                case OperandType.InlineField: return 4;
                case OperandType.InlineI: return 4;
                case OperandType.InlineI8: return 8;
                case OperandType.InlineMethod: return 4;
                case OperandType.InlineNone: return 0;
                //case OperandType.InlinePhi: return 0;
                case OperandType.InlineR: return 8;
                case OperandType.InlineSig: return 4;
                case OperandType.InlineString: return 4;
                case OperandType.InlineSwitch: return 4;
                case OperandType.InlineTok: return 4;
                case OperandType.InlineType: return 4;
                case OperandType.InlineVar: return 2;
                case OperandType.ShortInlineBrTarget: return 1;
                case OperandType.ShortInlineI: return 1;
                case OperandType.ShortInlineR: return 4;
                case OperandType.ShortInlineVar: return 1;
            }

            return null;
        }

    }



    [DebuggerDisplay("{First} : {Second}")]
    public struct Tuple<A, B>
    {
        public A First;
        public B Second;

        public Tuple(A first, B second)
        {
            First = first;
            Second = second;
        }

        public static implicit operator Tuple<A, B>(A a)
        {
            return new Tuple<A, B>(a, default(B));
        }

        public static implicit operator Tuple<A, B>(B b)
        {
            return new Tuple<A, B>(default(A), b);
        }
    }

    public struct Range<T>
    {
        public T From;
        public T To;

        public Range(T f, T t)
        {
            From = f;
            To = t;
        }

        public override string ToString()
        {
            return From + " -> " + To;
        }

        public void Swap()
        {
            var x = From;

            From = To;

            To = x;
        }
    }


    [DebuggerStepThrough]
    public struct Nonnullable<T>
        where T : class
    {
        public readonly T Value;

        public static implicit operator Nonnullable<T>(T v)
        {
            return new Nonnullable<T>(v);
        }

        public static implicit operator T(Nonnullable<T> v)
        {
            return v.Value;
        }

        public Nonnullable(T v)
        {
            if (v == null)
                throw new NullReferenceException();

            this.Value = v;
        }

    }


    public class DisposableEvent : IDisposable
    {
        public Action Disposing;

        #region IDisposable Members

        public void Dispose()
        {
            Disposing();
        }

        #endregion

        public static implicit operator DisposableEvent(Action Disposing)
        {
            return new DisposableEvent { Disposing = Disposing };
        }
    }

    public class ConsoleColorText : DisposableEvent
    {
        static public ConsoleColorText Blue { get { return new ConsoleColorText(ConsoleColor.Blue); } }
        static public ConsoleColorText Cyan { get { return new ConsoleColorText(ConsoleColor.Cyan); } }
        static public ConsoleColorText Yellow { get { return new ConsoleColorText(ConsoleColor.Yellow); } }
        static public ConsoleColorText Red { get { return new ConsoleColorText(ConsoleColor.Red); } }
        static public ConsoleColorText Magenta { get { return new ConsoleColorText(ConsoleColor.Magenta); } }

        public ConsoleColorText(ConsoleColor c)
        {
            var x = Console.ForegroundColor;
            Console.ForegroundColor = c;
            Disposing = () => Console.ForegroundColor = x;
        }
    }





    [DebuggerDisplay("{Span}")]
    public class TimeCounter : DisposableEvent
    {




        public interface IProvider
        {
        }

        public long Value;

        public TimeSpan Span
        {
            get
            {
                return new TimeSpan(Value);
            }
        }

        TimeCounter Target;


        public TimeCounter()
        {
        }


        public TimeCounter(TimeCounter e)
        {
            var s = new Stopwatch();

            s.Start();

            Target = e;

            Disposing = delegate { s.Stop(); Target.Value += s.ElapsedTicks; };
        }

        public static TimeCounter operator ~(TimeCounter e)
        {
            return new TimeCounter(e);
        }

        public static TimeCounter operator +(TimeCounter e, TimeCounter x)
        {
            return new TimeCounter { Value = e.Value + x.Value };
        }

        public void ToConsole(string p)
        {
            Console.WriteLine(p.PadLeft(40) + " time: " + this.Span + " ticks: " + this.Value);
        }

        public void ToConsole(TimeCounter total, string p)
        {
            var t = (100 * this.Value / total.Value) + "%";

            Console.WriteLine(p.PadLeft(40) + " " + t.PadLeft(3) + " time: " + this.Span + " ticks: " + this.Value);
        }

        public static void ToConsole(IProvider e)
        {
            var t = e.GetType();

            var z = from i in t.GetFields()
                        where i.FieldType == typeof(TimeCounter)
                        let v = new { f = i, v = (TimeCounter)i.GetValue(e) }
                        orderby v.v.Value descending
                        select v;

            var total = new TimeCounter();

            total.Value = z.Select(i => i.v.Value).Sum();

            foreach (var v in z)
            {

                v.v.ToConsole(total, v.f.Name.RemoveEnd("Time"));
            }

            total.ToConsole("Total");
        }
    }


    public class RangeTable<T> : IEnumerable<T>
    {
        readonly T[] Values;

        public readonly int UpperBound;
        public readonly int LowerBound;

        public readonly int Count;


        public RangeTable(int lbound, int ubound)
        {
            UpperBound = ubound;
            LowerBound = lbound;

            Values = new T[ubound - lbound + 1];
        }



        public T this[int from, int to]
        {
            set
            {
                if (to < from) throw new InvalidOperationException();

                for (int i = from; i <= to; i++)
                {
                    this[i] = value;
                }
            }
        }

        public T this[int i]
        {
            get
            {
                return Values[i + LowerBound];
            }
            set
            {
                Values[i + LowerBound] = value;
            }
        }



        #region IEnumerable<T> Members

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var v in Values)
            {
                yield return v;
            }
        }

        #endregion

        #region IEnumerable Members


        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var v in Values)
            {
                yield return v;
            }
        }

        #endregion
    }



}
