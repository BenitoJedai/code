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

namespace TestAnnotations
{
    using OtherNamespace;

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
    [Foo(Text = "hello world"), ZooAttribute, Bar]
    public class Class1
    {
        
    }
}
