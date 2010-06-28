using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhyIncorrectFormat
{
    class C : IDisposable
    {
        CC cc;

        public void Dispose()
        {
        }
    }



    class B
    {
        public string Property { get; set; }
    }

    class L<T>
    {

    }

    class LX<T> : List<T>
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            var B = new B { Property = "Why? An attempt was made to load a program with an incorrect format. (Exception from HRESULT: 0x8007000B)" };

            using (new C())
            {
                var l = new L<object>();
                var lx = new LX<object>();
            }
        }
    }
}
