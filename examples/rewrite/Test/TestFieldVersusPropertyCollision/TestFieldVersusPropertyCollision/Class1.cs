using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[assembly: Obfuscation( Feature = "merge")]
namespace TestFieldVersusPropertyCollision
{
    public class Class1
    {
        public object OriginallyProperty { get; set; }
        public object OriginallyField;
    }
}
