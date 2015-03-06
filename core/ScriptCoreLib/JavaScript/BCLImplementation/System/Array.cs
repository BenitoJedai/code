using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/array.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Array.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Array.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Array.cs

    [Script(Implements = typeof(global::System.Array))]
    internal class __Array
    {
		// https://github.com/dotnet/coreclr/blob/master/src/classlibnative/bcltype/arraynative.h
		// https://github.com/dotnet/coreclr/blob/master/src/classlibnative/bcltype/arraynative.cpp


		// X:\jsc.svn\examples\javascript\LINQ\ClickCounter\ClickCounter\Application.cs
		public void SetValue(object value, int index)
        {
            ScriptCoreLib.JavaScript.Runtime.Expando.InternalSetMember(this, index, value);
        }

        [Script]
        class __Enumerator : IEnumerator
        {
            public object[] Target;

            object InternalCurrent;
            int InternalIndex = -1;

            #region __IEnumerator Members

            public object Current
            {
                get { return InternalCurrent; }
            }

            public bool MoveNext()
            {
                InternalIndex++;

                if (InternalIndex < Target.Length)
                {
                    InternalCurrent = Target[InternalIndex];
                    return true;
                }

                InternalCurrent = null;
                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        [Script(DefineAsStatic = true)]
        public IEnumerator GetEnumerator()
        {
            return new __Enumerator { Target = (object[])(object)this };
        }

        public static int IndexOf<T>(T[] array, T value)
        {
            return ((IArray<T>)(object)(array)).indexOf(value);
        }

        [Script(OptimizedCode = "d[i] = s[i];")]
        internal static void InternalCopyElement(global::System.Array s, global::System.Array d, int i)
        {
        }

        [Script(OptimizedCode = "d[di] = s[si];")]
        internal static void InternalCopyElement(global::System.Array s, int si, global::System.Array d, int di)
        {
        }


        public static void Copy(global::System.Array sourceArray, global::System.Array destinationArray, int length)
        {
            for (int i = 0; i < length; i++)
            {
                InternalCopyElement(sourceArray, destinationArray, i);
            }
        }

        public static void Copy(global::System.Array sourceArray, int sourceOffset, global::System.Array destinationArray, int destinationOffset, int length)
        {
			// http://stackoverflow.com/questions/7110666/il-instructions-not-exposed-by-c-sharp
			// http://referencesource.microsoft.com/#mscorlib/system/buffer.cs,570e88af5685d024
			// can Uint8ClampedArray copy be made any faster?

			for (int i = 0; i < length; i++)
            {
				// x:\jsc.svn\market\synergy\javascript\chrome\chrome\bclimplementation\system\net\sockets\tcplistener.cs

				InternalCopyElement(sourceArray, i + sourceOffset, destinationArray, i + destinationOffset);
            }
        }


        /*
        public static IEnumerable<TSource> Sort<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, int> c)
        {
 
        }*/
        /*
        public static void Sort(Array array, IComparer comparer)
        {
        }*/


        public static void Sort<T>(T[] array, Comparison<T> c)
        {
            // tested by?
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\OrderedEnumerable.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/redux

            ((IArray<T>)(object)(array)).sort((a, b) => c(a, b));
        }

        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
            Sort(array, comparer.Compare);
        }

        public static global::System.Array CreateInstance(Type elementType, int length)
        {
            // .MakeArray with GetElementType
            var a = new object[length];

            return (global::System.Array)a;
        }
    }
}
