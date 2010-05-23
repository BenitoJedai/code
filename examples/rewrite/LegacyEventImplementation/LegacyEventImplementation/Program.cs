using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegacyEventImplementation
{
    class Program
    {
        public event Action Action1;

        Action _Action2;
        public event Action Action2
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

        static void Main(string[] args)
        {
        }
    }
}
