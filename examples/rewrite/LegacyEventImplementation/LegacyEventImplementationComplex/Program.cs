using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegacyEventImplementationComplex
{
    class Program
    {
        class X<T>
        {
            public event Action<T> Action1;

            Action<T> _Action2;
            public event Action<T> Action2
            {
                add
                {
                    _Action2 += value;
                }
                remove
                {
                    _Action2 -= value;
                }
            }
        }
        static void Main(string[] args)
        {
            var x = new X<string>();
        }
    }
}
