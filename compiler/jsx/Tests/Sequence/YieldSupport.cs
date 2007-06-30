using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace jsx.Tests.Sequence
{
    class YieldSupportExtensions
    {
        public static IEnumerable<T> WhereIterator<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var e in source)
            {
                if (predicate(e)) yield return e;
            }
        }
        public static IEnumerable<S> SelectIterator<T, S>(IEnumerable<T> source, Func<T, S> selector)
        {
            foreach (T element in source)
            {
                yield return selector(element);
            }
        }
        public static IEnumerable<T> ConcatIterator<T>(IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach (var e in first) yield return e;
            foreach (var e in second) yield return e;
        }

    }

    class YieldSupport
    {
        static IEnumerable<S> SelectManyIterator<T, S>(IEnumerable<T> source, Func<T, IEnumerable<S>> selector)
        {
            foreach (T element in source)
            {
                foreach (S subElement in selector(element))
                {
                    yield return subElement;
                }
            }
        }

        public static void TestFuzzy()
        {
//( a )
//( b )
//( c )
//( d )
//( e )
//( f )
//( g )
//( h )
//( i )
//---
//[ d ]
//[ e ]
//[ f ]
//[ h ]
//---
//{ g }
//{ h }
//{ i }
//---
//< h >
//---

            foreach (var v in Fuzzy(
                0,
                new [] { "a", "b", "c" },
                new [] { "d", "e", "f" },
                new [] { "g", "h", "i" },
                () => Console.Write("( "),
                () => Console.WriteLine(" )"),
                () => Console.WriteLine("---")
                ))
            {
                Console.Write(v);
            }

            foreach (var v in Fuzzy(
                1,
                new [] { "a", "b", "c" },
                new [] { "d", "e", "f" },
                new [] { "g", "h", "i" },
                () => Console.Write("[ "),
                () => Console.WriteLine(" ]"),
                () => Console.WriteLine("---")
                ))
            {
                Console.Write(v);
            }

            foreach (var v in Fuzzy(
                2,
                new [] { "a", "b", "c" },
                new [] { "d", "e", "f" },
                new [] { "g", "h", "i" },
                () => Console.Write("{ "),
                () => Console.WriteLine(" }"),
                () => Console.WriteLine("---")
                ))
            {
                Console.Write(v);
            }

            foreach (var v in Fuzzy(
                3,
                new [] { "a", "b", "c" },
                new [] { "d", "e", "f" },
                new [] { "g", "h", "i" },
                () => Console.Write("< "),
                () => Console.WriteLine(" >"),
                () => Console.WriteLine("---")
                ))
            {
                Console.Write(v);
            }
        }

        public static void TestMixedIterator()
        {
            foreach (var v in MixedIterator(
                new [] { "a", "b", "c", "d", "e", "f" },
                new [] { "A", "B", "C" }
                ))
            {
                Console.WriteLine(v);
            }
        }

        public static IEnumerable<string> MixedIterator(IEnumerable<string> a, IEnumerable<string> b)
        {
            yield return "start";

            if (ILTest.GetValue<bool>())
                yield return "true";
            else
                yield return ILTest.GetValue<bool>() ? "ok" : "bad";

            if (ILTest.GetValue<bool>())
                foreach (var v in a)
                    yield return v;

            if (ILTest.GetValue<bool>())
                foreach (var v in b)
                    yield return v;

            var ac = a.Count();
            var bc = b.Count();
            var max = ac < bc ? ac : bc;

            var i = a.GetEnumerator();
            var j = b.GetEnumerator();

            while (max-- > 0)
            {
                if (!i.MoveNext()) throw null;
                if (!j.MoveNext()) throw null;

                yield return i.Current + " . " + j.Current;
            }


            yield return "end";
        }

        public static IEnumerable<T> StackedIterator<T>(
                IEnumerable<IEnumerable<IEnumerable<IEnumerable<IEnumerable<T>>>>> v0
            )

        {
            foreach (var v1 in v0)
                foreach (var v2 in v1)
                    foreach (var v3 in v2)
                        foreach (var v4 in v3)
                            foreach (var v5 in v4)
            {
                yield return v5;

            }
        }

        public static IEnumerable<T> Fuzzy<T>(
            int op,
            IEnumerable<T> a,
            IEnumerable<T> b,
            IEnumerable<T> c,
            Action before,
            Action after,
            Action end
            )
        {
            switch (op)
            {
                case 0:
                    foreach (var v in a)
                    {
                        before();
                        yield return v;
                        after();
                    }

                    goto case 1;
                case 1:
                    foreach (var v in b)
                    {
                        before();
                        yield return v;
                        after();
                    }

                    goto default;
                default:


                    int i = 0;

                    foreach (var v in c)
                    {
                        if (op % 2 != 0)
                            if (i++ % 2 == 0)
                                continue;

                        before();
                        yield return v;
                        after();
                    }

                    break;

            }

            end();
        }


        




        public static void ForEach(IEnumerable e, Action<object> action)
        {
            foreach (var v in e)
            {
                action(v);
            }
        }

        public static void TestSelectMany()
        {
            SelectManyIterator(
                new []
                {
                    new {b=new [] {1, 2, 3, 4, 5}},
                    new {b=new [] {6,7,8,9,0}},
                    new {b=new [] {11,22,33,44,55}}
                }, ( i) => i.b).ForEach(v => Console.WriteLine(v));
        }

        public static void TestConcat()
        {
            IEnumerable a = YieldSupportExtensions.ConcatIterator(
                new [] { 3, 5, 7 },
                new [] { 6, 8, 10 }
            );

            ForEach(a, v => Console.WriteLine(v));

        }

        public static void TestWhere()
        {
            int[] u = new [] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };


            IEnumerable a = YieldSupportExtensions.WhereIterator(u, i => i % 2 == 0);

            ForEach(a, v => Console.WriteLine(v));


            

 

            

        }
    }
}
