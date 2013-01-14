using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestNativePropertyAccess
{
    [Script(IsNative = true)]
    public interface IClass3
    {
        IClass3 invoke();

        IClass3 foo { get; set; }
    }

    [Script(IsNative = true)]
    public class Class3
    {
        public IClass3 foo { get; set; }
    }

    public class Class2
    {
        public IClass3 foo { get; set; }
    }

    public class Class1
    {
        public Class1(IClass3 a, Class3 b, Class2 c)
        {
            var aa = a.foo.invoke();
            var bb = b.foo.invoke();
            var cc = c.foo.invoke();
        }
    }
}
