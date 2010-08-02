using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "script")]
namespace TestAbstractBaseType
{
    abstract class Foo
    {
        abstract protected void Bar();
    }

    class Program : Foo
    {
        // http://stackoverflow.com/questions/2371490/how-to-declare-abstract-method-in-non-abstract-class-in-php

        static void Main(string[] args)
        {
        }

        protected override void Bar()
        {

        }
    }
}
