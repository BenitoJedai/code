using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestIEquatable
{
    [Script(Implements = typeof(IEquatable<>))]
    internal interface __IEquatable<T>
    {
        bool Equals(T other);
    }

    [Script]
    public class Class1 : IEquatable<Class1>
    {
        public bool Equals(Class1 other)
        {
            return false;
        }
    }
}
