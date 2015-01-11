using ScriptCoreLib.JavaScript.BCLImplementation.System.Threading.Tasks;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: Obfuscation(Feature = "script")]
namespace Test453NamedParameter
{
    public class Class1 : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150107

        //public static CSSStyleRuleMonkier __problematic_operator1(CSSStyleRuleMonkier left, int millisecondsDelay)
        public static void __problematic_operator1(CSSStyleRuleMonkier left, int millisecondsDelay)
        {
            // X:\jsc.svn\examples\javascript\Test\Test453NamedParameter\Test453NamedParameter\Class1.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150107

            // x:\jsc.svn\examples\javascript\test\test453history\test453history\application.cs

            // X:\jsc.svn\examples\javascript\css\Test\CSSDelayedStyle\CSSDelayedStyle\Application.cs
            // X:\jsc.svn\examples\javascript\css\Test\CSSDelayedTimeConditional\CSSDelayedTimeConditional\Application.cs

            //if (left.__task != null)
            {
                var t = new TaskCompletionSource<object>();
                //Console.WriteLine("conditional after?");
                left.__task.ContinueWith(
                    x =>
                    {

                        //Console.WriteLine("conditional after? onclick complete");
                        __Task.Delay(millisecondsDelay: millisecondsDelay).ContinueWith(
                            xx =>
                            {
                                //Console.WriteLine("conditional after? delay complete");
                                t.SetResult(null);
                            }
                        );

                    }
                );

                //InternalTaskNameLookup[t.Task] = "conditional after " + millisecondsDelay + "ms";
                //return (left + t.Task);
            }

            //var task = (Task)__Task.Delay(millisecondsDelay: millisecondsDelay);

            ////InternalTaskNameLookup[task] = "after " + millisecondsDelay + "ms";
            //return (left + task);
        }

    }

}
