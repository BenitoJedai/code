using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test453ForEach
{
    public class Class1
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102

        public void Invoke(IEnumerable<object> e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150102/foreach

            //02000002 Test453ForEach.Class1
            //script: error JSC1000: unknown while condition at Void Invoke(System.Collections.Generic.IEnumerable`1[System.Object]).Maybe you did not turn off c# compiler 'optimize code' feature?

            foreach (var item in e)
            {

            }
        }
    }
}
