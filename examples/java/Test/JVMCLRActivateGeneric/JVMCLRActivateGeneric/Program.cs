using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRActivateGeneric
{

    static class X
    {


        class XGrouping<TKey, T> : IGrouping<TKey, T>
        {
            public XGrouping()
            {
                Console.WriteLine("hello world");
            }

            public TKey Key
            {
                get { throw new NotImplementedException(); }
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        public static IGrouping<TKey, T> GroupBy<T, TKey>(IEnumerable<T> x, Func<T, TKey> f)
        {
            var a = new XGrouping<TKey, T>();


            return a;
        }





        static class __XCachedAnonymousMethodDelegate
        {


        }





        [Obsolete("what does it take to make javac happy? C# tends generates static generic fields which we cannot use.")]
        sealed class __XCachedAnonymousMethodDelegate<TOuter, TInner, TKey, TResult>
        {
            public IEnumerable<TOuter> outer;
            public IEnumerable<TInner> inner;
            public Func<TOuter, TKey> outerKeySelector;
            public Func<TInner, TKey> innerKeySelector;
            public Func<TOuter, IEnumerable<TInner>, TResult> resultSelector;



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

        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, IEnumerable<TInner>, TResult> resultSelector
            )
        {
            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRActivateGeneric\Program.java
            //Y:\staging\web\java\JVMCLRActivateGeneric\Program___c__DisplayClass5_4.java:68: error: ')' expected
            //        if ((Program___c__DisplayClass5_4<TOuter, TInner, TKey, TResult>.CS___9__CachedAnonymousMethodDelegate7 == null))
            //                                                                                                               ^

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140514
            // javac does not like this function?
            // X:\jsc.svn\examples\java\test\JVMCLRStringJoin\JVMCLRStringJoin\Program.cs

            //return null;


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

    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );


            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "V:\staging\web\java";release -d release java\JVMCLRActivateGeneric\Program.java
            //V:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable___c__DisplayClass5_4.java:47: error: ')' expected
            //        if ((__Enumerable___c__DisplayClass5_4<TOuter, TInner, TKey, TResult>.CS___9__CachedAnonymousMethodDelegate7 == null))
            //                                                                                                                    ^
            Func<string, string> f = x => x;


            //java\JVMCLRActivateGeneric\Program.java:52: error: method Of in class __SZArrayEnumerator_1<T#2> cannot be applied to given types;
            //        grouping_21 = Program.<Integer, Integer>GroupBy(__SZArrayEnumerator_1.<Integer>Of(numArray2), func_20);
            //                                                                             ^
            //  required: T#1[]
            //  found: int[]
            //  reason: actual argument int[] cannot be converted to Integer[] by method invocation conversion
            //  where T#1,T#2 are type-variables:
            //    T#1 extends Object declared in method <T#1>Of(T#1[])

            var z = X.GroupBy(new[] { "" }.AsEnumerable(), f);



            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );


            MessageBox.Show("click to close");

        }
    }


}
