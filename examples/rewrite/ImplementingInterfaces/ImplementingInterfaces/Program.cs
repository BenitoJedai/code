using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "merge")]

namespace ImplementingInterfaces
{
    internal class Z : IMarker
    {
    }

    internal class ZZ : Z
    {
    }

    interface IMarker
    {

    }

    // what? jsc cannot rewrite private classes?
    public class X : Program
    {

    }

    public class Program : IMarker
    {
        static void Main(string[] args)
        {
            X x;
            ZZ z;
        }
    }
}
