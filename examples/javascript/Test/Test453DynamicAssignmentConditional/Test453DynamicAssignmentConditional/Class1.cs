using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]

namespace Test453DynamicAssignmentConditional
{


    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // X:\jsc.svn\examples\rewrite\Test\Test453If\Test453If\Program.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/test435coreasdynamic

        public static void Invoke(object self)
        {
            // X:\jsc.svn\examples\javascript\Test\Test435CoreDynamic\Test435CoreDynamic\Class1.cs
            // X:\jsc.svn\examples\javascript\Test\Test435CoreAsDynamic\Test435CoreAsDynamic\Class1.cs
            // X:\jsc.svn\examples\javascript\Test\Test453DynamicAssignment\Test453DynamicAssignment\Class1.cs
            // X:\jsc.svn\examples\javascript\Test\Test453DynamicAssignmentConditional\Test453DynamicAssignmentConditional\Class1.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150103/dynamic

            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ContinueWith.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\Task\Task.ctor.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Tasks\TaskFactory.ContinueWhenAll.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Type.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\CanvasRenderingContext2D.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IDocument.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\IStyle.cs
            // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\Worker.cs

            // roslyn seems to emit dynamic sites a bit differently.
            // while stack rewriter can help in merge mode
            // scriptcorelib would also lie to make use of it, with out the merge mode, only with script mode.

            //dynamic self = Native.self;
            //object value = self[item];


            // we should test all use cases for dynamic for roslyn js.

            //new Array(1)[0] = twgABr0mMjmREJxf62g10A(0, null);
            //AQAABKl6eD2kr62vxldjAQ = hQkABkmHWjqHBHzPjs4Qsg(PQkABiO2_aTySKN41_aKL3ew(0, 'bar', _4R8ABtC6ljmbrk8x5kK6iA(new ctor$bR8ABhfpfj6IFLf_a4gLSZg(type$UxpR0RNEiTmJ760665b1dw)), new Array(1)));

            dynamic x = self;



            if (ScriptCoreLib.JavaScript.Runtime.Expando.Of(self).Contains("Uint8Array"))
            {

                Console.WriteLine("Uint8ClampedArray not available. while Uint8Array seems to be.");

                x.Uint8ClampedArray = x.Uint8Array;
                return;
            }


            // what about other operators?
            x.Uint8ClampedArray = x.Array;


            //          // Test453DynamicAssignmentConditional.Class1.Invoke
            //          this.AQAABvhdzz63eE177xi3RA = function(b)
            //{
            //              var c, d;

            //              c = b;
            //              d = Rw4ABtVg6j6tf7_aLlpqs7A(Mw4ABtVg6j6tf7_aLlpqs7A(b), 'Uint8Array');

            //              if (d)
            //              {
            //                  _0h4ABo302jK_arjcePTx37w('Uint8ClampedArray not available. while Uint8Array seems to be.');

            //                  if (AgAABIMaLDOZpFTc83zBpQ)
            //                  {
            //                  }
            //                  else
            //                  {
            //                      var __0040 = new Array(2);
            //                      __0040[0] = twgABr0mMjmREJxf62g10A(0, null);
            //                      __0040[1] = twgABr0mMjmREJxf62g10A(0, null);
            //                      AgAABIMaLDOZpFTc83zBpQ = hQkABkmHWjqHBHzPjs4Qsg(OwkABiO2_aTySKN41_aKL3ew(0, 'Uint8ClampedArray', _4R8ABtC6ljmbrk8x5kK6iA(new ctor$bR8ABhfpfj6IFLf_a4gLSZg(type$S4zLdfhdzz63eE177xi3RA)), __0040));
            //                  }


            //                  if (AQAABIMaLDOZpFTc83zBpQ)
            //                  {
            //                  }
            //                  else
            //                  {
            //                      var __0092 = new Array(1);
            //                      __0092[0] = twgABr0mMjmREJxf62g10A(0, null);
            //                      AQAABIMaLDOZpFTc83zBpQ = hQkABkmHWjqHBHzPjs4Qsg(PQkABiO2_aTySKN41_aKL3ew(0, 'Uint8Array', _4R8ABtC6ljmbrk8x5kK6iA(new ctor$bR8ABhfpfj6IFLf_a4gLSZg(type$S4zLdfhdzz63eE177xi3RA)), __0092));
            //                  }

            //                  AgAABIMaLDOZpFTc83zBpQ.Target.GREABtXRBzSbUYeYQmTv4A(AgAABIMaLDOZpFTc83zBpQ, c, AQAABIMaLDOZpFTc83zBpQ.Target.FREABqSnJzGqSLFy_aB0XMg(AQAABIMaLDOZpFTc83zBpQ, c));
            //                  return;
            //              }


            //              if (BAAABIMaLDOZpFTc83zBpQ)
            //              {
            //              }
            //              else
            //              {
            //                  var __00ea = new Array(2);
            //                  __00ea[0] = twgABr0mMjmREJxf62g10A(0, null);
            //                  __00ea[1] = twgABr0mMjmREJxf62g10A(0, null);
            //                  BAAABIMaLDOZpFTc83zBpQ = hQkABkmHWjqHBHzPjs4Qsg(OwkABiO2_aTySKN41_aKL3ew(0, 'Uint8ClampedArray', _4R8ABtC6ljmbrk8x5kK6iA(new ctor$bR8ABhfpfj6IFLf_a4gLSZg(type$S4zLdfhdzz63eE177xi3RA)), __00ea));
            //              }


            //              if (AwAABIMaLDOZpFTc83zBpQ)
            //              {
            //              }
            //              else
            //              {
            //                  var __013c = new Array(1);
            //                  __013c[0] = twgABr0mMjmREJxf62g10A(0, null);
            //                  AwAABIMaLDOZpFTc83zBpQ = hQkABkmHWjqHBHzPjs4Qsg(PQkABiO2_aTySKN41_aKL3ew(0, 'Array', _4R8ABtC6ljmbrk8x5kK6iA(new ctor$bR8ABhfpfj6IFLf_a4gLSZg(type$S4zLdfhdzz63eE177xi3RA)), __013c));
            //              }

            //              BAAABIMaLDOZpFTc83zBpQ.Target.GREABtXRBzSbUYeYQmTv4A(BAAABIMaLDOZpFTc83zBpQ, c, AwAABIMaLDOZpFTc83zBpQ.Target.FREABqSnJzGqSLFy_aB0XMg(AwAABIMaLDOZpFTc83zBpQ, c));
            //          };

        }
    }

}
