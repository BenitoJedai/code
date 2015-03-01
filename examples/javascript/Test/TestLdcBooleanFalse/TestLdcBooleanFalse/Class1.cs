using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace TestLdcBooleanFalse
{
    public class Class1<T>
    {
        public Class1(T t)
        {

        }

    }

    public class Class1
    {
        //    a[0].field1 = false;
        //c = false;

        bool field1 = false;

        public Class1(bool e)
        {
            var loc1 = false;
        }

        // AQAABObWdDuZ9MiQovzKyw = new ctor$AgAABubWdDuZ9MiQovzKyw(0);
        static Class1 c = new Class1(false);
        static Class1<bool> cc = new Class1<bool>(false);

        //AgAABObWdDuZ9MiQovzKyw = new ctor$AwAABubWdDuZ9MiQovzKyw(0);
        //AwAABObWdDuZ9MiQovzKyw = new ctor$AQAABuDMETmuE4vORcbdWQ(0);
    }
}
