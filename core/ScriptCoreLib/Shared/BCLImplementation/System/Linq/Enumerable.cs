using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

    [Script(Implements = typeof(global::System.Linq.Enumerable))]
    public static partial class __Enumerable
    {


        [Script]
        class __XGrouping<TKey, TElement> : IGrouping<TKey, TElement>
        {
            public List<TElement> InternalElements = new List<TElement>();


            public TKey Key
            {
                get;
                set;
            }

            public IEnumerator<TElement> GetEnumerator()
            {
                return this.InternalElements.ToArray().AsEnumerable().GetEnumerator();
            }

            global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
            {
                return this.InternalElements.ToArray().AsEnumerable().GetEnumerator();
            }
        }

        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            // X:\jsc.svn\examples\java\JVMCLRPrivateAddress\JVMCLRPrivateAddress\Program.cs

            //            Y:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable.java:299: incompatible types
            //found   : ScriptCoreLibJava.BCLImplementation.System.Collections.Generic.__List_1<ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable___XGrouping_2<TKey,TElement>>
            //required: ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic.__IEnumerable_1<ScriptCoreLib.Shared.BCLImplementation.System.Linq.__IGrouping_2<TKey,TElement>>


            var a = new List<IGrouping<TKey, TElement>>();

            foreach (var item in source)
            {
                var key = keySelector(item);

                var comparer = Comparer<TKey>.Default;

                // can we do ref equals?
                var g = a.FirstOrDefault(k => comparer.Compare(k.Key, key) == 0);
                if (g == null)
                {
                    g = new __XGrouping<TKey, TElement> { Key = key };

                    a.Add(g);

                }

                var value = elementSelector(item);


                var gg = (__XGrouping<TKey, TElement>)g;

                gg.InternalElements.Add(value);
            }

            return a;
        }

    }

}
