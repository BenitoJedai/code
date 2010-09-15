using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
[assembly: Obfuscation(Feature = "merge")]

namespace NestedTypesPartialClose
{
    public class Class1
    {
        NestedTypesWithExtensions.KnownStockTypes.System.Windows.Forms.UserControl Foo;
    }
}
