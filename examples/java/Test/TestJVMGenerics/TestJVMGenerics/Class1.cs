using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestJVMGenerics
{
    [Script]
    public class Class1
    {
        public Class1<int> __int;
        public Class1<string> __string;
        public Class1<bool> __bool;
        public Class1<ReferencedByGenericParameter> __bool;

        public Class1(Class1<bool> value)
        {
            __int = new Class1<int>();
            __string = new Class1<string>();
            __bool = value;

            var x = get(value);
        }

        public static T get<T>(Class1<T> e)
        {
            return e.get();
        }

        public event GenericAction<string, long> AtGenericAction;

        public static T Cast<T>(object e)
        {
            var u = e;

            var r = (T)u;

            return r;
        }
    }

    [Script]
    public delegate T GenericAction<T0, T>(T0 t0);

    [Script]
    public class ReferencedByGenericParameter
    {

    }

    [Script]
    public class Class1<T>
    {
        // see also: http://docs.oracle.com/javase/tutorial/java/generics/index.html
        // see also: http://en.wikipedia.org/wiki/Generics_in_Java
        // see also: http://docs.oracle.com/javase/tutorial/java/generics/gentypes.html

        // T stands for "Type"
        private T t;

        public void add(T t)
        {
            this.t = t;
        }

        public T get()
        {
            return t;
        }


    }

    interface IAssemblyReferenceToken : ScriptCoreLibJava.IAssemblyReferenceToken
    {
        
    }
}
