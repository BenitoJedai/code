using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using java.util;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Array), IsArray = true)]
    internal class __Array
    {

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

            //            [javac] Compiling 479 source files to T:\bin\classes
            //[javac] T:\src\ScriptCoreLibJava\BCLImplementation\System\__Array.java:31: incompatible types
            //[javac] found   : java.lang.Object
            //[javac] required: java.lang.Object[]
            //[javac]         enumerator0.Target = that;
            //[javac]                              ^

            //public static  __IEnumerator GetEnumerator(Object that)
            //{
            //    __Array___Enumerator enumerator0;

            //    enumerator0 = new __Array___Enumerator();
            //    enumerator0.Target = that;
            //    return  enumerator0;
            //}


            //GetEnumerator() : IEnumerator
            //Analysis
            //Attributes
            //Signature Types
            //Declaring Module
            //Declaring Type
            //loc.0 <- 0x0001 newobj       [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array+__Enumerator..ctor()
            //loc.1 <- 0x0013 ldloc.0      loc.0 : __Enumerator
            //maxstack 3 (used 2)
            //IL Code (12)
            //0x0000 nop 
            //0x0001 . newobj         [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array+__Enumerator..ctor()
            //0x0006 stloc.0          loc.0 : __Enumerator
            //0x0007 . ldloc.0        loc.0 : __Enumerator
            //0x0008 . . ldarg.0      this [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array
            //0x0009 . . castclass    [mscorlib] System.Object[]
            //0x000e stfld            [ScriptCoreLibAndroid] ScriptCoreLibJava.BCLImplementation.System.__Array+__Enumerator.Target : object[]
            //0x0013 . ldloc.0        loc.0 : __Enumerator
            //0x0014 stloc.1          loc.1 : IEnumerator
            //0x0015 br.s 
            //0x0017 . ldloc.1        loc.1 : IEnumerator
            //0x0018 ret 




            return new __Enumerator { Target = (object[])(object)this };
        }

        public static void Sort(Array array)
        {
            java.util.Arrays.sort((object[])(object)array);
        }


        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
            Sort(array, comparer.Compare);
        }

        [Script]
        class __Comparator<T> : Comparator
        {
            public Comparison<T> comparison;

            public int compare(object o1, object o2)
            {
                return comparison((T)o1, (T)o2);
            }
        }

        public static void Sort<T>(T[] array, Comparison<T> comparison)
        {
            java.util.Arrays.sort((object[])(object)array,
                new __Comparator<T>
                {
                    comparison = comparison
                }
            );
        }

        public static void Copy(__Array sourceArray, __Array destinationArray, int length)
        {
            java.lang.JavaSystem.arraycopy(sourceArray, 0, destinationArray, 0, length);
        }

        public static void Copy(__Array sourceArray, int sourceIndex, __Array destinationArray, int destinationIndex, int length)
        {
            // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs

            java.lang.JavaSystem.arraycopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }

        public static Array CreateInstance(Type elementType, int length)
        {
            // X:\jsc.svn\examples\java\async\Test\JVMCLRAsync\JVMCLRAsync\Program.cs

            __Type t = elementType;
            var o = default(Array);

            try
            {
                o = (Array)java.lang.reflect.Array.newInstance(t.InternalTypeDescription, length);
            }
            catch
            {
                throw;
            }

            return o;
        }

        [Script(DefineAsStatic = true)]
        public void CopyTo(__Array dest, int soffset)
        {
            Copy(this, soffset, dest, 0, ((Array)(object)this).Length - soffset);
        }

        public int Length
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return java.lang.reflect.Array.getLength(this);
            }
        }

        [Script(DefineAsStatic = true)]
        public void SetValue(object value, int index)
        {
            java.lang.reflect.Array.set(this, index, value);

        }


        [Script(DefineAsStatic = true)]
        public object GetValue(int index)
        {
            return java.lang.reflect.Array.get(this, index);
        }
    }
}
