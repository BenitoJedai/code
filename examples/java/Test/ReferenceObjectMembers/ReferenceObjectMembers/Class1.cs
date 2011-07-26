using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace ReferenceObjectMembers
{
    [Script]
    public class Class1 : ReferenceObjectMembers.IClass1
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    [Script]
    class MyClass
    {
        public static void UsingClass1(Class1 c)
        {
            var e = c.Equals(c);
            var h = c.GetHashCode();
            var s = c.ToString();
        }
    }
}


namespace ScriptCoreLibJava.BCLImplementation.System
{

    [Script(

        Implements = typeof(global::System.Object),
        ImplementationType = typeof(global::java.lang.Object)

        //Implements = typeof(global::System.Object),
        //ImplementationType = typeof(object)

        )]
    internal class __Object
    {
        //[Script(ExternalTarget = "toString")]
        //public new string ToString()
        //{
        //    return default(string);
        //}

        //[Script(DefineAsStatic = true)]
        //new public Type GetType()
        //{
        //    return __Type.GetTypeFromValue(this);
        //}


    }


}


namespace java.lang
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/lang/Object.html
    [Script(IsNative = true)]
    public class Object
    {

        /// <summary>
        /// Indicates whether some other object is "equal to" this one.
        /// </summary>
        public override bool Equals(object @obj)
        {
            return default(bool);
        }



        /// <summary>
        /// Returns a hash code value for the object.
        /// </summary>
        public override int GetHashCode()
        {
            return default(int);
        }

      

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        public override string ToString()
        {
            return default(string);
        }


    }
}
