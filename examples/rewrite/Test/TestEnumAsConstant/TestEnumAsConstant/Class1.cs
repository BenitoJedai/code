using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class TestEnumAsConstant
{
    [Flags]
    enum foo : ushort
    {
        bar = 0x2,
        zoo = 0x4,
    }

    public class Class1
    {
        const foo x = foo.bar | foo.zoo | (foo)0x1000;
    }
}
