using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test453WhileBreak
{
    class Program
    {
        private Program next;


        //while (f!=null)
        //{
        //  e = f.AgAABiwiQTmQStujX5drSQ(b, c);
        //  g = (e > 0);

        //  if (g)
        //  {
        //    break;
        //  }

        //  f = f.next;
        //}

        int Foo(Program a, Program b)
        {
            var p = this;
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/redux
                // X:\jsc.svn\examples\javascript\Test\Test453WhileBreak\Test453WhileBreak\Program.cs

                int r = 0;
                var x = p;

                // i {[0x0008] br.s       +0 -0}
                // i {[0x002f] brtrue.s   +0 -1{[0x002d] ldloc.s    +1 -0} }
                while (x != null)
                {
                    r = x.Compare(a, b);

                    if (r != 0)
                        break;

                    x = x.next;
                }
                // i.InlineLoopConstruct.Join = {[0x0031] ldloc.1    +1 -0}
                return r;
            }
        }

        private int Compare(Program a, Program b)
        {
            return 0;
        }

        static void Main(string[] args)
        {
        }
    }
}
