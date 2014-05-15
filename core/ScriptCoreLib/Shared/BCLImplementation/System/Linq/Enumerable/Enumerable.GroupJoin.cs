using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    [Obsolete("what does it take to make javac happy? C# tends generates static generic fields which we cannot use.")]
    [Script]
    sealed class __XCachedAnonymousMethodDelegate<TOuter, TInner, TKey, TResult>
    {
        public IEnumerable<TOuter> outer;
        public IEnumerable<TInner> inner;
        public Func<TOuter, TKey> outerKeySelector;
        public Func<TInner, TKey> innerKeySelector;
        public Func<TOuter, IEnumerable<TInner>, TResult> resultSelector;



        [Script]
        class __Invoke
        {
            //0001 02000014 JVMCLRActivateGeneric__i__d.jvm::<module>.SHA1cefaa9390246c05b17201928356345079bca657c@639399487$000000a8
            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRActivateGeneric\Program.java
            //Y:\staging\web\java\JVMCLRActivateGeneric\X___XCachedAnonymousMethodDelegate_4___Invoke0.java:18: error: cannot find symbol
            //    public __Tuple_2<TOuter, TKey>[] o;

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRActivateGeneric\Program.java
            //Y:\staging\web\java\JVMCLRActivateGeneric\X___XCachedAnonymousMethodDelegateInvoke_4.java:18: error: cannot find symbol
            //    public __Tuple_2<TOuter, TKey>[] o;
            //           ^


            public Tuple<TOuter, TKey> ref0;

            [Obsolete("why cant jsc java import gen see ElementType?")]
            public Tuple<TOuter, TKey>[] o;
            public Tuple<TInner, TKey>[] i;

            public Comparer<TKey> c;


            public __XCachedAnonymousMethodDelegate<TOuter, TInner, TKey, TResult> context;

            public TResult selector(TOuter jo)
            {
                var ko = context.outerKeySelector(jo);


                var e = context.inner.Where(
                    ji => c.Compare(ko, context.innerKeySelector(ji)) == 0
                );

                var r = context.resultSelector(jo, e);

                return r;
            }
        }



        public IEnumerable<TResult> Invoke()
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Enumerable\Enumerable.GroupJoin.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140515/new

            Console.WriteLine("Invoke");


            //var o = outer.ToArray();
            //var i = inner.ToArray();

            // java does not support generic type field variants.
            // so. until jsc figures out and starts to do 
            // generic type bakeing we cannot write such complex code can we.


            var z = new __Invoke
            {
                context = this,
                c = Comparer<TKey>.Default,
                o = outer.Select(jo => Tuple.Create(jo, outerKeySelector(jo))).ToArray(),
                i = inner.Select(ji => Tuple.Create(ji, innerKeySelector(ji))).ToArray()
            };

            return outer.Select(z.selector);
        }


    }


    static partial class __Enumerable
    {
        // X:\jsc.svn\examples\javascript\test\TestGroupJoin\TestGroupJoin\Application.cs
        // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.GroupJoin(



        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector
            )
        {
            // X:\jsc.svn\examples\javascript\Test\TestGroupJoin\TestGroupJoin\ApplicationWebService.cs

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRActivateGeneric\Program.java
            //Y:\staging\web\java\JVMCLRActivateGeneric\Program___c__DisplayClass5_4.java:68: error: ')' expected
            //        if ((Program___c__DisplayClass5_4<TOuter, TInner, TKey, TResult>.CS___9__CachedAnonymousMethodDelegate7 == null))
            //                                                                                                               ^

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140514
            // javac does not like this function?
            // X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs

            //return null;



            //return
            //    from jo in outer
            //    let ko = outerKeySelector(jo)
            //    let e = from ji in inner
            //            let ki = innerKeySelector(ji)
            //            where c.Compare(ko, ki) == 0
            //            select ji
            //    let r = resultSelector(jo, e)
            //    select r;

            //V:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable___c__DisplayClass5_4.java:21: error: non-static type variable TInner cannot be referenced from a static context
            //    public static __Func_2<__AnonymousTypes__ScriptCoreLib.__f__AnonymousType_1750_1a_2<TInner, TKey>, TInner> CS___9__CachedAnonymousMethodDelegate7;
            //                                                                                        ^


            //return
            //    from jo in o
            //    select resultSelector(jo.jo,
            //        from ji in i
            //        where c.Compare(jo.ko, ji.ki) == 0
            //        select ji.ji
            //    );


            var x = new __XCachedAnonymousMethodDelegate<TOuter, TInner, TKey, TResult>
            {
                outer = outer,
                inner = inner,
                outerKeySelector = outerKeySelector,
                innerKeySelector = innerKeySelector,
                resultSelector = resultSelector
            };


            return x.Invoke();
        }




    }
}
