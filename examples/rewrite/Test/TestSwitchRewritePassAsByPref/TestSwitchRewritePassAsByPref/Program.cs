using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSwitchRewritePassAsByPref
{
    class Program : IEnumerator
    {
        public void InternalMoveNext(ref string e)
        {
            e = "new value";
        }

        //string Field1;

        public void MoveNext(string e)
        {
            // PEVerify [IL]: Error: [X:\jsc.svn\examples\rewrite\test\TestSwitchRewritePassAsByPref\TestSwitchRewritePassAsByPref\bin\Debug\xTestSwitchRewritePassAsByPref.exe : TestSwitchRewritePassAsByPref.Program+<MoveNext>06000002::<0000> nop][offset 0x00000017][found ref 'System.String'][expected address of ref ] Unexpected type on the stack.

            //X:\jsc.svn\examples\rewrite\Test\TestSwitchRewritePassAsByPref\TestSwitchRewritePassAsByPref\bin\Debug>xTestSwitchRewritePassAsByPref.exe
            //TestSwitchRewritePassAsByPref.Program <0000> nop
            //new value

            InternalMoveNext(ref e);

            // X:\jsc.svn\examples\javascript\test\TestByRefField\TestByRefField\Application.cs
            // how would this work for js?
            //InternalMoveNext(ref this.Field1);

            Console.WriteLine(e);
        }

        static void Main(string[] args)
        {
            new Program().MoveNext("");
        }

        public object Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
