using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptCoreLib;

[assembly: Obfuscation(Feature = "script")]



namespace TestNestedAttributes
{
    [Script(Implements = typeof(global::System.Runtime.InteropServices._Attribute))]
    internal interface ___Attribute
    {
    }

    [Script(Implements = typeof(global::System.Attribute))]
    internal class __Attribute : ___Attribute
    {

    }

    [My, MyAttribute.FooMy]
    public class Class1
    {
       
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class MyAttribute : Attribute
    {
        [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
        public sealed class FooMyAttribute : Attribute
        {

        }
    }
}
