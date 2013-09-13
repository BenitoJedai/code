using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ByRefLdarg0Experiment;
using ByRefLdarg0Experiment.Design;
using ByRefLdarg0Experiment.HTML.Pages;

namespace ByRefLdarg0Experiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {




//            { Location =
// assembly: V:\ByRefLdarg0Experiment.Application.exe
// type: ByRefLdarg0Experiment.foo, ByRefLdarg0Experiment.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x000c
//  method:Void invoke(ByRefLdarg0Experiment.foo ByRef, ByRefLdarg0Experiment.foo, fyield) }
//script: error JSC1000: Method: invoke, Type: ByRefLdarg0Experiment.foo; emmiting failed : System.NotImplementedException: { ParameterType = ByRefLdarg0Experiment.foo&, p = [0x000c] callvirt   +0 -3{[0x0008] ldarg.2    +1 -0} {[0x0009] ldarg.0    +1 -0} {[0x000a] ldarga.s   +1 -0} , m = Void Invoke(ByRefLdarg0Experiment.foo ByRef, ByRefLdarg0Experiment.foo ByRef) }
//   at jsc.IdentWriter.JavaScript_WriteParameters(Prestatement p, ILInstruction i, ILFlowStackItem[] s, Int32 offset, MethodBase m)
//   at jsc.IL2ScriptGenerator.OpCode_call_override(IdentWriter w, Prestatement p, ILInstruction i, ILFlowStackItem[] s, MethodBase m)



            //            { i = 3 }
            //{ i = 5 }
            //{ i = 7 }

//            { i = 3 } view-source:27522

// view-source:27522
//{ i = 5, copy = 5 } view-source:27522

// view-source:27522
//{ i = 7 } 

            var z = new foo { i = 3 };

            z.invoke();
        }

    }


    struct foo
    {
        public int i;

        public void invoke()
        {
            Console.WriteLine(new { this.i });

            //Error	1	Cannot convert lambda expression to type 'TestByRefLdarg0.foo' because it is not a delegate type	X:\jsc.svn\examples\javascript\Test\TestByRefLdarg0\TestByRefLdarg0\Program.cs	36	17	TestByRefLdarg0

            foo.invoke(ref this,
                this,
                new fyield(
                    (ref foo z, ref foo copy) =>
                    //delegate (ref foo z) 
                    {
                        //Error	2	A ref or out argument must be an assignable variable	X:\jsc.svn\examples\javascript\Test\TestByRefLdarg0\TestByRefLdarg0\Program.cs	33	21	TestByRefLdarg0

                        //Error	1	Anonymous methods, lambda expressions, and query expressions inside structs cannot access instance members of 'this'. Consider copying 'this' to a local variable outside the anonymous method, lambda expression or query expression and using the local instead.	X:\jsc.svn\examples\javascript\Test\TestByRefLdarg0\TestByRefLdarg0\Program.cs	35	45	TestByRefLdarg0


                        Console.WriteLine(new { z.i, copy = copy.i });

                    }
                )
            );

            Console.WriteLine(new { this.i });
        }

        public delegate void fyield(ref foo z, ref foo copy);

        public static void invoke(ref foo x, foo copy, fyield yield = null)
        {
            x.i = 5;

            yield(ref x, ref copy);

            var z = new foo { i = 7 };

            x = z;

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
