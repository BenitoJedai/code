using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestGenericParameterArray
{
    public class Class1<T> : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        // ref_Array_CreateInstance

        public Class1()
        {
            // is this a good idea?
            // tArray0 = (T[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(Object.class)), 1);
            var a = new T[1];

            //TestGenericArray.Class1_1<Class1>[] class1_1Array0;

            //class1_1Array0 = (TestGenericArray.Class1_1<Class1>[])Object(TestGenericArray.Class1_1<Class1>, 1);
            // class1_1Array0 = (TestGenericArray.Class1_1<Class1>[])__Array.CreateInstance(__Type.GetTypeFromHandle(__RuntimeTypeHandle.op_Explicit(TestGenericArray.Class1_1.class)), 1);

        }
    }
}
