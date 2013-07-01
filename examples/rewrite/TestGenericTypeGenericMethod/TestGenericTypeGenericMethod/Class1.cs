using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenericTypeGenericMethod
{
    public class foo<TypeArgument>
    {
        public void bar<MethodArgument>(TypeArgument t, MethodArgument m)
        { 
        
        }
    }
    public class Class1
    {
        public void invoke(foo<string> a, foo<int> i)
        {
            a.bar("", 1);
            i.bar(1, "");
        }
    }
}
