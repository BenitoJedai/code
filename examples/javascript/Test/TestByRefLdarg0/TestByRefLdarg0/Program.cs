using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestByRefLdarg0
{
    class Program : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        static void Main(string[] args)
        {
            var z = new foo { i = 3 };

            //            { i = 3 }
            //{ i = 5 }
            //{ i = 7 }


            z.invoke();
        }
    }

    struct foo
    {
        public int i;

        public void invoke()
        {
            //Console.WriteLine(new { this.i });

            //Error	1	Cannot convert lambda expression to type 'TestByRefLdarg0.foo' because it is not a delegate type	X:\jsc.svn\examples\javascript\Test\TestByRefLdarg0\TestByRefLdarg0\Program.cs	36	17	TestByRefLdarg0

            foo.invoke(ref this,
                this
                //,
                //new fyield(
                //    (ref foo z, ref foo copy) =>
                //    //delegate (ref foo z) 
                //    {
                //        //Error	2	A ref or out argument must be an assignable variable	X:\jsc.svn\examples\javascript\Test\TestByRefLdarg0\TestByRefLdarg0\Program.cs	33	21	TestByRefLdarg0

                //        //Error	1	Anonymous methods, lambda expressions, and query expressions inside structs cannot access instance members of 'this'. Consider copying 'this' to a local variable outside the anonymous method, lambda expression or query expression and using the local instead.	X:\jsc.svn\examples\javascript\Test\TestByRefLdarg0\TestByRefLdarg0\Program.cs	35	45	TestByRefLdarg0


                //        Console.WriteLine(new { z.i, copy = copy.i });

                //    }
                //)
            );

            //Console.WriteLine(new { this.i });
        }

        //public delegate void fyield(ref foo z, ref foo copy);

        public static void invoke(ref foo x, foo copy
            //, fyield yield = null
            
            )
        {
            //x.i = 5;

            //yield(ref x, ref copy);

            //var z = new foo { i = 7 };

            //x = z;

            //            // TestByRefLdarg0.foo.invoke
            //this.BAAABtvDSTeJ_aFd4zNwqpA = function (ref$b, c, d)
            //{
            //  var e = new zwksn9vDSTeJ_aFd4zNwqpA(), f = new zwksn9vDSTeJ_aFd4zNwqpA();

            //  ref$b[0].i = 5;
            //  d.BwAABgIE_bjmWsjXYOfeq4g(ref$b, c);
            //  f = new zwksn9vDSTeJ_aFd4zNwqpA();
            //  f.i = 7;
            //  e = f;
            //  ref$b[0]=e;
            //};

        }
    }
}
