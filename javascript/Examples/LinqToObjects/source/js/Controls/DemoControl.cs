using ScriptCoreLib;

using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Serialized;
using ScriptCoreLib.JavaScript.DOM.HTML;
//using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.DOM;

using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Drawing;

using global::System.Collections.Generic;
using System;
using System.Linq;

namespace LinqToObjects.source.js.MyLinq
{
    [Script]
    class DefaultComparer<T> : IComparer<T>
    {
        public int Compare(T ka, T kb)
        {
            var r = -2;

            if (Expando.Of(ka).IsString)
                r = Expando.Compare(ka, kb);

            if (Expando.Of(ka).IsNumber)
                r = Expando.Compare(ka, kb);

            if (Expando.Of(ka).IsBoolean)
                r = Expando.Compare(ka, kb);


            if (r == -2)
                throw new NotSupportedException();

            return r;
        }
    }

    [Script]
    class VirtualComparer<T> : IComparer<T>
    {
        readonly Func<T, T, int> VirtualCompare;

        public VirtualComparer(Func<T, T, int> e)
        {
            this.VirtualCompare = e;
        }

        public int Compare(T ka, T kb)
        {
            return VirtualCompare(ka, kb);
        }
    }


    [Script]
    public class VirtualEnumerable<TSource> : IEnumerable<TSource>
    {
        public Func<IEnumerator<TSource>> VirtualGetEnumerator;

        #region IEnumerable<TSource> Members

        public IEnumerator<TSource> GetEnumerator()
        {
            return VirtualGetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


    }

    [Script]
    public class ComparerVirtualEnumerable<TSource, TKey2> : VirtualEnumerable<TSource>, IOrderedEnumerable<TSource>
    {
        // immutable

        IComparer<TSource> NextComparer;

        ComparerVirtualEnumerable()
        {

        }

        public ComparerVirtualEnumerable(IEnumerable<TSource> source, Func<TSource, TKey2> keySelector, IComparer<TKey2> comparer, bool descending)
        {
            // original source
            // comparer 1
            // comparer 

            var y = new VirtualComparer<TSource>
            (
                (a, b) =>
                {
                    var r = comparer.Compare(keySelector(a), keySelector(b));

                    if (r == 0)
                    {
                        if (this.NextComparer != null)
                            r = this.NextComparer.Compare(a, b);
                    }

                    return r;
                }
            );

            this.VirtualGetEnumerator = () => MyLinq.MyEnumerable.Sort(source, y).GetEnumerator();
        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {

            var v = new ComparerVirtualEnumerable<TSource, TKey>
            {
                // get parent
                VirtualGetEnumerator = base.VirtualGetEnumerator
            };

            this.NextComparer =
                new VirtualComparer<TSource>(
                   (a, b) =>
                   {
                       var r = comparer.Compare(keySelector(a), keySelector(b));

                       if (r == 0)
                       {
                           if (v.NextComparer != null)
                               r = v.NextComparer.Compare(a, b);
                       }


                       return r;
                   }
            );

            return v;
        }
    }

    [Script]
    public class OrderedEnumerable<TSource, TKey> : OrderedEnumerable<TSource>
    {
        public Func<TSource, TKey> keySelector;
        public IComparer<TKey> comparer;
        public bool descending;

        internal OrderedEnumerable()
        {

        }
    }

    [Script]
    public class OrderedEnumerable<TSource> : IEnumerable<TSource>, IOrderedEnumerable<TSource>
    {
        // immutable 

        readonly OrderedEnumerable<TSource> prev;
        readonly OrderedEnumerable<TSource> next;

        IEnumerable<TSource> source;

        #region IOrderedEnumerable<TSource> Members

        public static IOrderedEnumerable<TSource> Create<TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedEnumerable<TSource, TKey>
            {
                keySelector = keySelector,
                comparer = comparer,
                descending = descending,
                source = source
            };
        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            // deep clone current and set as parent

            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable<TSource> Members

        public IEnumerator<TSource> GetEnumerator()
        {
            // get the lowest level and start sorting

            throw new NotSupportedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    [Script]
    public static class MyEnumerable
    {
        #region ok
        static IArray<T> ToIArray<T>(T[] e)
        {
            return (IArray<T>)(object)e;
        }


        public static IEnumerable<TSource> Sort<TSource>(IEnumerable<TSource> source, IComparer<TSource> c)
        {
            var s = ToIArray(System.Linq.Enumerable.ToArray(source));


            s.sort((a, b) => c.Compare(a, b));

            return System.Linq.Enumerable.AsEnumerable(s.ToArray());
        }
        #endregion
        #region ok


        public static IOrderedEnumerable<TSource> OrderByX<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return OrderByX(source, keySelector, new DefaultComparer<TKey>());
        }

        public static IOrderedEnumerable<TSource> OrderByY<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return OrderByY(source, keySelector, new DefaultComparer<TKey>());
        }

        public static IOrderedEnumerable<TSource> OrderByX<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return new ComparerVirtualEnumerable<TSource, TKey>(source, keySelector, comparer, false);
        }

        public static IOrderedEnumerable<TSource> OrderByY<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return OrderedEnumerable<TSource>.Create<TKey>(source, keySelector, comparer, false);
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ThenBy(source, keySelector, new DefaultComparer<TKey>());
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new NullReferenceException("source");
            }

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }
        #endregion






    }

}

namespace LinqToObjects.source.js.Controls
{
    using MyLinq;
    using System.Linq;
    using System;



    [Script, ScriptApplicationEntryPoint]
    public class DemoControl //: SpawnControlBase
    {
        //public const string Alias = "fx.DemoControl";

        IHTMLDiv Control = new IHTMLDiv();

        public IStyle Style { get { return Control.style; } }








        public DemoControl(IHTMLElement e)
        //    : base(e)
        {
            e.insertNextSibling(Control);


            var users = new IHTMLTextArea("mike, mac, ken, neo, zen, jay, morpheous, trinity, Agent Smith");

            users.rows = 10;

            var filter = new IHTMLInput(HTMLInputTypeEnum.text, "");
            var result = new IHTMLDiv();
            var result2 = new IHTMLDiv();

            result2.style.color = Color.Blue;

            Action Update =
                delegate
                {
                    var user_filter = filter.value.Trim().ToLower();

                    result.removeChildren();
                    result2.removeChildren();

                    var __users = users.value.Split(',');

                    var query = from i in __users
                                where i.ToLower().IndexOf(user_filter) > -1
                                let name = i.Trim()
                                orderby name.Length, name
                                select new { length = name.Length, name };

                    foreach (var v in /*OrderBy(*/query/*, i => i.name)*/)
                    {

                        result.appendChild(new IHTMLDiv("match: " + v));
                    }

                    var sorted_query =
                        MyLinq.MyEnumerable.ThenBy(
                            MyLinq.MyEnumerable.OrderByX(query, x => x.length)
                        , x => x.name);

                    foreach (var v in sorted_query)
                    {

                        result2.appendChild(new IHTMLDiv("match: " + v));
                    }
                };

            users.onchange += delegate { Update(); };
            users.onkeyup += delegate { Update(); };

            filter.onchange += delegate { Update(); };
            filter.onkeyup += delegate { Update(); };

            Func<IHTMLBreak> br = () => new IHTMLBreak();

            Control.appendChild(
                new IHTMLLabel("Enter a list of names separated by commas", users),
                br(),
                users,
                br(),
                new IHTMLLabel("Enter a partial name to be found from the list above.", filter),
                br(),
                filter,
                br(),
                new IHTMLLabel("Found matches:", result),
                br(),
                result,
                br(),
                result2
            );

            Update();

        }


        static DemoControl()
        {
            typeof(DemoControl).SpawnTo(i => new DemoControl(i));
        }
    }


}
