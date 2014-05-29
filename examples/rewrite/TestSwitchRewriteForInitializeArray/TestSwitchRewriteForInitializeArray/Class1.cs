using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSwitchRewriteForInitializeArray
{
    public class Program : IEnumerator
    {
        void MoveNext()
        {
            var value = new[] { 4, 8, 15, 16, 23, 42 };

            Console.WriteLine(value[0]);
            // 4
        }



        object IEnumerator.Current
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        // X:\jsc.svn\examples\javascript\forms\async\AsyncTaskYieldViaProgress\AsyncTaskYieldViaProgress\ApplicationControl.cs


        public static void Main(string[] args)
        {
            new Program().MoveNext();
        }

        bool IEnumerator.MoveNext()
        {
            throw new NotImplementedException();
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }
}
