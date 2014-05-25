using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSwitchRewriterArguments
{
    struct LocalStructYetAByRefInWorkflowBeforeOtherByRefArgs
    {
        // Error	1	'TestSwitchRewriterArguments.LocalStructYetAByRefInWorkflowBeforeOtherByRefArgs.foo': 
        // cannot have instance field initializers in structs

        public string foo;
    }

    struct ArgStruct
    {

        public string foo;
    }


    class Program : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // X:\jsc.svn\examples\rewrite\Test\TestSwitchRewriterByRefCharArray\TestSwitchRewriterByRefCharArray\Program.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140525

        //public static void MoveNext(ref string[] args)
        //public static void MoveNext(string[] args, ref string[] z, ref ArgStruct a)
        public static void MoveNext(string[] args, ref string[] z, ArgStruct a)
        {
            // this will already create two locals!
            ////try
            ////{
            ////    args = z;
            ////}
            ////finally
            ////{
            ////}

            ////return;

            var newfoo = a.foo + " modified";
            //a.foo += " modified";

            var aa = new ArgStruct { foo = newfoo };

            //a.foo = newfoo;

            // this is special once swtich rewriter kicks in
            a = aa;

            //Console.WriteLine(args.Length);
            if (z.Length == 1)
            {
                var loc0 = new LocalStructYetAByRefInWorkflowBeforeOtherByRefArgs { foo = a.foo };

                // workflow entry #2

                Console.WriteLine(z[0]);

                // this would create an implicit local?
                var zargs = new[] { "y " + loc0.foo };
                //z = new[] { "y " + loc0.foo };

                // does this work too?
                args = z;
            }
        }

        static void Main(string[] args)
        {
            //new Program().MoveNext(new[] { "x" });
            var a = new ArgStruct { foo = "ArgStruct" };
            var x = new[] { "x" };
            //var z = new[] { "y" };

            //new Program().MoveNext(ref x);
            //MoveNext(ref x);
            //MoveNext(x, ref x, ref a);
            MoveNext(x, ref x, a);
            MoveNext(x, ref x, a);

            //x
            //y ArgStruct modified
        }

        void System.Runtime.CompilerServices.IAsyncStateMachine.MoveNext()
        {
            throw new NotImplementedException();
        }

        void System.Runtime.CompilerServices.IAsyncStateMachine.SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }
    }
}
