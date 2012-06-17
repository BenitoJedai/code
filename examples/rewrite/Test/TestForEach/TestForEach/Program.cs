using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]

namespace TestForEach
{
    class Collection  
    {

        public object Current
        {
            get { return null; }
        }

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        {
        }

        public Collection GetEnumerator()
        {
            return this;
        }
    }



    class Program
    {
        public static void Method1()
        {
        }

        static void Main(string[] args)
        {
            Method1();

            foreach (var item in new Collection())
            {
                Method1();

                break;

                Method1();
            }

            Method1();
        }
    }
}
