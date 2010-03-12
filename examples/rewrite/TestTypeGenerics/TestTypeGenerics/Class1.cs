//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace TestTypeGenerics
//{
//    public class Class1<T>
//    {
//        public static T TField;

//        public class Nested1<N>
//        {
//            public void Method<M>(T t, N n, M m)
//            {

//            }

//            public void Method2(T t, N n, int m)
//            {

//            }
//        }


//    }

//    public class Class2
//    {
//        public void Method<M>(M m)
//        {

//        }
//    }
//    class Test2X
//    {
//        static Class1<object>.Nested1<object> n;

//        public void Test1()
//        {
//            var x = new Class2<object>(null);

//        }

//        public void Test2()
//        {
//            n.Method(null, null, 0);
//            n.Method2(null, null, 0);

//            Class1<int>.TField = 5;
//        }

//        public void Test3()
//        {
//            Class2 c = null;

//            c.Method<string>(null);
//        }
//    }


//    public class Class2<T>
//    {
//        public Class2(T t)
//        {

//        }
//    }

//    class A
//    {
//    }
//    public class MyAssets //: HTML.Pages.FromAssets.Assets
//    {
//        public MyAssets()
//        {
//            var jsc = new ScriptCoreLib.JavaScript.DOM.HTML.IHTMLButton();

//            jsc.onclick +=
//                delegate
//                {
//                    ScriptCoreLib.JavaScript.Native.Window.alert("hello");
//                };


//        }

//        static void Mix1()
//        {
//            var a = new ScriptCoreLib.Shared.EventHandler<A, int>(Mix2);
//        }

//        static void Mix2(A a, int i)
//        {
//        }
//    }

//    static partial class LambdaExtensions
//    {
//        public sealed class ConcatStream<T> : IEnumerable<T>
//        {
//            public IEnumerable<T> Source;

//            readonly Queue<T> Queue = new Queue<T>();

//            public void Add(T e)
//            {
//                Queue.Enqueue(e);
//            }

//            #region IEnumerable<T> Members

//            public IEnumerator<T> GetEnumerator()
//            {
//                return new Enumerator(Source.GetEnumerator(), Queue);
//            }

//            #endregion

//            #region IEnumerable Members

//            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//            {
//                return GetEnumerator();
//            }

//            #endregion


//            internal class Enumerator : IEnumerator<T>
//            {
//                readonly IEnumerator<T> Source;
//                readonly Queue<T> Queue;

//                public Enumerator(IEnumerator<T> Source, Queue<T> Queue)
//                {
//                    this.Source = Source;
//                    this.Queue = Queue;
//                }

//                #region IEnumerator<T> Members

//                T InternalCurrent;

//                public T Current
//                {
//                    get { return InternalCurrent; }
//                }

//                #endregion

//                #region IDisposable Members

//                public void Dispose()
//                {

//                }

//                #endregion

//                #region IEnumerator Members

//                object System.Collections.IEnumerator.Current
//                {
//                    get { return this.Current; }
//                }


//                public bool MoveNext()
//                {
//                    InternalCurrent = default(T);

//                    if (this.Source.MoveNext())
//                    {
//                        InternalCurrent = Source.Current;
//                        return true;
//                    }

//                    if (this.Queue.Count > 0)
//                    {
//                        InternalCurrent = Queue.Dequeue();
//                        return true;
//                    }

//                    return false;
//                }

//                public void Reset()
//                {
//                    throw new NotImplementedException();
//                }

//                #endregion
//            }
//        }


//        public static ConcatStream<T> ToConcatStream<T>(this IEnumerable<T> source)
//        {
//            return new ConcatStream<T> { Source = source };
//        }
//    }
//}
