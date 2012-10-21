using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestNuGetSupport.Foo
{
    public class Class1
    {
        // http://vkreynin.wordpress.com/2010/12/08/baby-steps-to-create-nuget-package/
        public const int FooValueDefault = 43;

        public int FooValue
        {
            get
            {
                
                return FooValueDefault;
            }
        }
    }
}
