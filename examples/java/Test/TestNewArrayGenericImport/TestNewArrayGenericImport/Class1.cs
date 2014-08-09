using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib;

[assembly: Obfuscation(Feature = "script")]



namespace TestNewArrayGenericImport
{
    public class Class1
    {
        // 
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRTypeOfTupleArray\TestJVMCLRTypeOfTupleArray\Program.cs
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRNewTupleArray\TestJVMCLRNewTupleArray\Program.cs
        public Class1()
        {
            var typeof1a = typeof(xTuple<IntPtr>[]);

            //  type0 = __Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(TestNewArrayGenericImport.xTuple_1.class));
            var typeof1 = typeof(xTuple<IntPtr>);

            // type1 = __Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(TestNewArrayGenericImport.xTuple_1<__IntPtr>[].class));
            var typeof1aa = typeof(xTuple<IntPtr>[][]);

            //  tuple_1Array0 = new TestNewArrayGenericImport.xTuple_1<xMemberInfo>[] {};

            //  tuple_1Array0 = (TestNewArrayGenericImport.xTuple_1<xMemberInfo>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(TestNewArrayGenericImport.xTuple_1.class)), 0);

            // it seems to resolve ok.

            var z = new xTuple<IntPtr>[] {
                // Tuple.Create(item.m, index)
            };
        }
    }

    public class xTuple<T>
    {
    }

    public class xMemberInfo
    {

    }


    [Script(Implements = typeof(global::System.Array), IsArray = true)]
    internal class __Array
    {
        public static Array CreateInstance(Type elementType, int length)
        {
            return null;
        }
    }

    [Script(Implements = typeof(global::System.Type))]
    internal class __Type //: __MemberInfo
    {

        public static global::System.Type GetTypeFromHandle(RuntimeTypeHandle TypeHandle)
        {
            return null;

        }
    }

    [Script(Implements = typeof(global::System.IntPtr))]
    internal class __IntPtr
    {
    }

    [Script(Implements = typeof(global::System.RuntimeTypeHandle))]
    internal class __RuntimeTypeHandle
    {
        // http://bugs.adobe.com/jira/browse/ASC-2677
        public IntPtr Value { get; /* private */ set; }

        //public static explicit operator __RuntimeTypeHandle(global::java.lang.Class _ptr)
        public static explicit operator __RuntimeTypeHandle(Class1 _ptr)
        {
            return null;
            //return new __RuntimeTypeHandle { Value = (IntPtr)(object)new __IntPtr { ClassToken = _ptr } };
        }
    }
}
