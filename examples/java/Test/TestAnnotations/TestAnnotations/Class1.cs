using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestAnnotations
{
    [Script(Implements = typeof(global::System.Runtime.InteropServices._Attribute))]
    internal interface ___Attribute
    {
    }

    [Script(Implements = typeof(global::System.Attribute))]
    internal class __Attribute
    {

    }

    [Script]
    public class FooAttribute : Attribute
    {
        public string Text;
    }

    [Script]
    public class Bar : Attribute
    {
        public string Text;
    }



    [Script]
    [Foo(Text = "hello world"), Bar(Text = "hi")]
    public class Class1
    {
        
    }
}
