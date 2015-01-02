using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453For
{
    public class Class1
    {
        public static void join() { }
        public static void loopStart() { }
        //public static void Invoke(IEnumerable<object> e)
        public static void Invoke(object[] e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102/foreach


            for (int i = 0; i < e.Length; i++)
            {
                loopStart();

            }

            join();

            //for (c = 0; (c < (~~b.length)); c = (((d + 1))))
            //{
            //    AgAABs0E_bzKMnVv0a9VReQ();
            //    d = c;
            //}


        }
    }
}
