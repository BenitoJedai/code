﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ScriptCoreLib.Extensions
{



    public static class LinqExtensions
    {
        // java does not support much of generics just yet

        [System.Diagnostics.DebuggerStepThrough]
        public static T With<T>(this T e, Action<T> h) where T : class
        {
            if (e != null)
                if (h != null)
                    h(e);

            return e;
        }

        public static object InvokeUnit<T1, T2>(this Action<T1, T2> h, T1 a1, T2 a2)
        {
            if (h != null)
                h(a1, a2);
            return new object();
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> source)
        {
            return new Queue<T>(source);
        }

        public static IEnumerable<Action> SelectAction<T>(this IEnumerable<T> source, Func<T, Action> selector)
        {
            return source.Select(selector);
        }


        public static int Times(this int e, Action y)
        {
            for (int i = 0; i < e; i++)
            {
                y();
            }

            return e;
        }

        /// <summary>
        /// Calls the selector delegate until the returned type is the same given to the delegate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        [System.Diagnostics.DebuggerStepThrough]
        public static T UntilSelected<T>(this T e, Func<T, T> selector) where T : class
        {
            var p = e;

            if (e != null)
                if (selector != null)
                {
                    var pp = e;
                    p = selector(pp);

                    while (p != pp)
                    {
                        var x = p;
                        p = selector(p);
                        pp = x;
                    }
                }

            return p;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static T With<T>(this T e, Func<T, bool> filter, Action<T> handler) where T : class
        {
            if (e != null)
                if (handler != null)
                    if (filter != null)
                        if (filter(e))
                            handler(e);

            return e;
        }

        [Obsolete]
        [System.Diagnostics.DebuggerStepThrough]
        public static T When<T>(this T e, Func<T, bool> h) where T : class
        {
            if (e != null)
                if (h != null)
                    if (h(e))
                        return e;

            return null;
        }

    

        [System.Diagnostics.DebuggerStepThrough]
        public static T Otherwise<T>(this T e, Action h) where T : class
        {
            if (e == null)
                if (h != null)
                    h();

            return e;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static IEnumerable<T> WithSingle<T>(this IEnumerable<T> collection, Action<T> h) where T : class
        {
            if (collection != null)
                if (h != null)
                    collection.SingleOrDefault().With(h);

            return collection;
        }


        [System.Diagnostics.DebuggerStepThrough]
        public static IEnumerable<T> WithEach<T>(this IEnumerable<T> collection, Action<T> h) where T : class
        {
            if (collection != null)
                if (h != null)
                    InternalWithEach<T>(collection, h);

            return collection;
        }

        //[System.Diagnostics.DebuggerStepThrough]
        [System.Diagnostics.DebuggerHidden]
        [System.Diagnostics.DebuggerNonUserCode]
        public static IEnumerable<T> WithEachIndex<T>(this IEnumerable<T> collection, Action<T, int> h) where T : class
        {
            if (collection != null)
                if (h != null)
                {
                    var i = -1;
                    foreach (var item in collection.AsEnumerable())
                    {
                        i++;
                        h(item, i);
                    }
                }

            return collection;
        }

        [System.Diagnostics.DebuggerStepThrough]
        private static void InternalWithEach<T>(IEnumerable<T> collection, Action<T> h) where T : class
        {
            // fixme: jsc shall convert Array to IEnumerable<>
            foreach (var item in collection.AsEnumerable())
            {
                item.With(h);
            }
        }


        public static IEnumerable<T> SelectWithSeparator<T>(this IEnumerable<T> source, T f)
        {
            return source.SelectWithSeparator((p, c) => f);
        }


        public static IEnumerable<T> SelectWithSeparator<T>(this IEnumerable<T> source, Func<T, T, T> f)
        {
            var i = -1;
            var x = default(T);

            return source.SelectMany(
                c =>
                {
                    var y = x;
                    x = c;
                    i++;

                    if (i > 0)
                        // FIXME: jsc shall convert Array to IEnumerable!
                        return new[] { f(y, c), c }.AsEnumerable();

                    // FIXME: jsc shall convert Array to IEnumerable!
                    return new[] { c }.AsEnumerable();
                }
            );
        }

        public static IEnumerable<Action> Invoke(this IEnumerable<Action> source)
        {
            foreach (var item in source.ToArray())
            {
                if (item != null)
                    item();
            }

            return source;
        }

        public static IEnumerable<Action<T>> Invoke<T>(this IEnumerable<Action<T>> source, T arg1)
        {
            foreach (var item in source.ToArray())
            {
                if (item != null)
                    item(arg1);
            }

            return source;
        }

        public class AnonymousContainer<T>
        {
            public T Default;
        }

        public static AnonymousContainer<T> ToAnonymousContainer<T>(this T t)
        {
            return new AnonymousContainer<T> { Default = t };
        }

        public static Func<T, A, A> ToFunc<T, A>(this AnonymousContainer<T> c, Func<T, A> h)
        {
            return
                (t, Default) =>
                {
                    if (t == null)
                        return Default;

                    return h(t);
                };
        }

        public static Func<F, A, A> FirstParameter<T, A, F>(this Func<T, A, A> s, Func<F, T> c)
        {
            return (f, a) => s(c(f), a);
        }

        public static Func<T> ToCachedFunc<T>(this Func<T> u)
        {
            var f = default(Func<T>);

            f = delegate
            {
                var r = u();

                f = () => r;

                return r;
            };

            return () => f();
        }


    }
}
