using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test453DelegateArgument
{
    class Program
    {
        public virtual void InvokeCallback(Func<string,string> lookup)
        {
        }

        void Complete()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102

            this.InvokeCallback(
                p =>
                {

                    return p;
                }
            );
        }

        static void Main(string[] args) => new Program { }.Complete();
        //{
        //    // new XElement("\{xxx}")
        //    Console.WriteLine("done");
        //}
    }
}
