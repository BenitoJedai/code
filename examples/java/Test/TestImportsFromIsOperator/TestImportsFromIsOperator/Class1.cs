using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestImportsFromIsOperator
{
    public class Class1
    {
        public void Invoke(object e)
        {

            if (e is foo.Class2)
                Invoke(null);
        }
    }

    
}

namespace foo
{
    public class Class2
    {

    }
}
