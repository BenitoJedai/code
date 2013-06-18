using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConstEnum
{
    public enum foo
    {
        zero
    }

    public enum foo_ulong
    {
        zero
    }

    public class Class1
    {
        public const foo f = foo.zero;
        public const foo_ulong f_ulong = foo_ulong.zero;
    }
}
