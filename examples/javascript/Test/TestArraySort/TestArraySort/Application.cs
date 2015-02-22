using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestArraySort;
using TestArraySort.Design;
using TestArraySort.HTML.Pages;

namespace TestArraySort
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //{ SourceMethod = Int32<.ctor> b__1(Double, Double) }
        //    script: error JSC1000: unknown opcode brtrue.s at<.ctor>b__1 + 0x0028
        //script: error JSC1000: Method: <.ctor>b__1, Type: TestArraySort.Application+<>c__DisplayClass0; emmiting failed : System.InvalidOperationException: unknown opcode brtrue.s at<.ctor>b__1 + 0x0028

        public Application(IApp page)
        {
            Invoke();
        }

        static void Invoke()
        {
            // X:\jsc.svn\examples\javascript\Test\Test453WhileBreak\Test453WhileBreak\Program.cs
            //var p = default(IComparable<double>);
            var p = default(IComparer<double>);


            var array = new[] { 3.4, 4.3, 5.1, 1.1 };

            // X:\jsc.svn\examples\javascript\test\TestOrderedEnumerable1\TestOrderedEnumerable1\Application.cs

            Array_Sort(
                array,

                (a, b) =>
                {
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201502/20150222
                    // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/redux
                    // X:\jsc.svn\examples\javascript\Test\Test453WhileBreak\Test453WhileBreak\Program.cs

                    int r = 0;
                    var x = p;

                    while (x != null)
                    {
                        r = x.Compare(a, b);

                        if (r != 0)
                            break;

                        //x = x.next;
                    }

                    return r;
                }
            );
        }

        static void Array_Sort(double[] array, Comparison<double> p)
        {
            Array.Sort(array, p);

        }
    }
}
