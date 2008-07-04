using ScriptCoreLib;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ScriptCoreLib.Shared.Lambda
{
    [Script]
    public static partial class LambdaExtensions
    {
        public static T AtOrDefault<T>(this IEnumerable<T> a, int i, T value)
        {
            var j = 0;
            var r = value;

            foreach (var v in a)
            {
                if (j == i)
                {
                    r = v;
                    break;
                }

                j++;
            }

            return r;
        }

        public static Func<bool> And(this Func<bool> a, Func<bool> b)
        {
            return () => a() && b();
        }

        public static Func<bool> Or(this Func<bool> a, Func<bool> b)
        {
            return () => a() || b();
        }

        public static T Previous<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var prev = source.Last();
            var r = default(T);

            foreach (var v in source.AsEnumerable())
            {
                if (predicate(v))
                {
                    r = prev;
                    break;
                }

                prev = v;
            }

            return r;
        }

        public static T Next<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var last = source.Last();
            var r = default(T);

            foreach (var v in source.AsEnumerable())
            {
                if (predicate(last))
                {
                    r = v;
                    break;
                }

                last = v;
            }

            return r;
        }


        #region min max

        public static double Max(this double e, double x)
        {

            if (e > x)
                return e;

            return x;
        }

        public static double Min(this double e, double x)
        {
            if (e < x)
                return e;

            return x;
        }

        public static int Min(this int e, int x)
        {
            if (e < x)
                return e;

            return x;
        }

        public static int Max(this int e, int x)
        {
            if (e > x)
                return e;

            return x;
        }
        #endregion

        public static Action AsCyclic(this global::System.Action<Action> f)
        {
            var done = default(Action);

            done = () => f(done);

            return done;
        }

        public static ParamsAction<B> FixParam<A, B>(this ParamsAction<A, B> f, A a)
        {
            return b => f(a, b);
        }

        public static ParamsAction<B> FixParam<A, B>(this  A a, ParamsAction<A, B> f)
        {
            return FixParam(f, a);
        }

        public static Action FixParam<A>(this A a, Action<A> f)
        {
            return FixParam(f, a);
        }

        public static Action FixParam<A>(this global::System.Action<A> f, A a)
        {
            return () => f(a);
        }

        public static global::System.Func<T> FixParam<A, T>(this global::System.Func<A, T> f, A a)
        {
            return () => f(a);
        }

        public static global::System.Func<A, T> FixParam<A, B, T>(this global::System.Func<A, B, T> f, B b)
        {
            return (a) => f(a, b);
        }

        #region FixFirstParam

        public static global::System.Func<B, T> FixFirstParam<A, B, T>(this global::System.Func<A, B, T> f, A a)
        {
            return (b) => f(a, b);
        }

        public static Action<B> FixFirstParam<A, B>(this Action<A, B> f, A a)
        {
            return (b) => f(a, b);
        }

        #endregion

        #region FixFirstParam

        public static global::System.Func<A, T> FixLastParam<A, B, T>(this global::System.Func<A, B, T> f, B b)
        {
            return (a) => f(a, b);
        }

        public static Action<A> FixLastParam<A, B>(this Action<A, B> f, B b)
        {
            return (a) => f(a, b);
        }

        public static global::System.Func<A, B, T> FixLastParam<A, B, C, T>(this global::System.Func<A, B, C, T> f, C c)
        {
            return (a, b) => f(a, b, c);
        }

        public static Action<A, B> FixLastParam<A, B, C>(this Action<A, B, C> f, C c)
        {
            return (a, b) => f(a, b, c);
        }

        #endregion

        public static ParamsAction<A> AsParamsAction<A>(this global::System.Action<A> f)
        {
            return 
                a =>
                {
                    foreach (var i in a)
                    {
                        f(i);
                    }
                };
        }

        public static Action<A> AsAction<A, T>(this global::System.Func<A, T> f)
        {
            return (a) => f(a);
        }

        public static Action<int, int> WithOffset(this Action<int, int> f, int x, int y)
        {
            return (ix, iy) => f(ix + x, iy + y);
        }

        public static Func<int, int, T> WithOffset<T>(this Func<int, int, T> f, int x, int y)
        {
            return (ix, iy) => f(ix + x, iy + y);
        }
    }

    [Script]
    public delegate global::System.Func<A, T> YFunc<A, T>(global::System.Func<A, T> e);

    [Script]
    public delegate global::System.Func<A, B, T> YFunc<A, B, T>(global::System.Func<A, B, T> e);



    partial class LambdaExtensions
    {
        public static global::System.Func<A, T> Y<A, T>(this YFunc<A, T> le)
        {
            var me = default(global::System.Func<A, T>); return me = (a) => le(me)(a);
        }

        public static global::System.Func<A, B, T> Y<A, B, T>(this YFunc<A, B, T> le)
        {
            var me = default(global::System.Func<A, B, T>); return me = (a, b) => le(me)(a, b);
        }


    }

    [Script]
    public delegate void ParamsAction<A, B>(A a, params B[] b);

    [Script]
    public delegate void ParamsAction<A>(params A[] a);


    [Script]
    public delegate System.Action<A> YAction<A>(System.Action<A> e);

    [Script]
    public delegate System.Action<A, B> YAction<A, B>(System.Action<A, B> e);



    partial class LambdaExtensions
    {
        public static Action<A> Y<A>(this YAction<A> le)
        {
            var me = default(Action<A>); return me = (a) => le(me)(a);
        }

        public static Action<A, B> Y<A, B>(this YAction<A, B> le)
        {
            var me = default(Action<A, B>); return me = (a, b) => le(me)(a, b);
        }
    }

    partial class LambdaExtensions
    {
        public static TSeed Aggregate<TSeed>(this TSeed e, Action<TSeed> a)
        {
            a(e);

            return e;
        }

        public static IEnumerable<TSource> Randomize<TSource>(this IEnumerable<TSource> u)
        {
            var x = u.ToList();
            var y = new List<TSource>();
            var r = new System.Random();

            while (x.Count > 0)
            {
                var i = r.Next(x.Count - 1);

                y.Add(x[i]);
                x.RemoveAt(i);
            }

            return y;
        }

        public static T Random<T>(this IEnumerable<T> e)
        {
            return e.Randomize().First();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T, int> action)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }
            if (action == null)
            {
                throw new System.ArgumentNullException("action");
            }

            var i = 0;
            foreach (var v in array.AsEnumerable())
            {
                action(v, i);
                i++;
            }

            return array;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> action)
        {
            if (array == null)
            {
                throw new System.ArgumentNullException("array");
            }
            if (action == null)
            {
                throw new System.ArgumentNullException("action");
            }

            foreach (var v in array.AsEnumerable())
            {
                action(v);
            }

            return array;
        }


        public static IEnumerable<T> ForEachReversed<T>(this IEnumerable<T> array, Action<T> action)
        {
            var a = array.ToArray();

            for (int i = a.Length - 1; i >= 0; i--)
            {
                action(a[i]);
            }


            return array;
        }
    }
}
