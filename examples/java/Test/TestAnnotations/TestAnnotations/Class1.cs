using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace OtherNamespace
{
    [Script]
    public class FooAttribute : Attribute
    {
        public string Text;
    }

}

namespace java.lang.annotation
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/annotation/Annotation.html
    [Script(IsNative = true)]
    public interface Annotation
    {

    }

    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/annotation/Documented.html
    [Script(IsNative = true)]
    public interface Documented : Annotation
    {

    }

    [Script]
    public class DocumentedAttribute : Attribute
    {
        // not implemented here? :)
    }
}

namespace java
{
    [Script]
    class ThisClassWillBeRedirectedTo_javax_Namespace
    {

    }
}

namespace TestAnnotations
{
    using OtherNamespace;
    using java.lang.annotation;

    [Script(Implements = typeof(global::System.Runtime.InteropServices._Attribute))]
    internal interface ___Attribute
    {
    }

    [Script(Implements = typeof(global::System.Attribute))]
    internal class __Attribute
    {

    }


    [Script]
    public class Bar : Attribute
    {
    }

    public class ZooAttribute : Attribute
    {
        // shall not be converted to java
    }




    [Script]
    public class Class1
    {
        [Foo(Text = "hello world"), ZooAttribute, Bar, Documented]
        public void FooMethod()
        {
        }

    }

    [Script]
    public class Class3
    {
        [Foo(Text = "hello world"), ZooAttribute, Bar, Documented]
        public string Foo;

    }

    [Script]
    [Foo(Text = "hello world"), ZooAttribute, Bar, Documented]
    public class Class2
    {
        [Foo(Text = "hello world"), ZooAttribute, Bar, Documented]
        public string Foo;


        [Foo(Text = "hello world"), ZooAttribute, Bar, Documented]
        public int Bar;

        [Foo(Text = "hello world"), ZooAttribute, Bar, Documented]
        public void FooMethod()
        {
        }
    }
}
