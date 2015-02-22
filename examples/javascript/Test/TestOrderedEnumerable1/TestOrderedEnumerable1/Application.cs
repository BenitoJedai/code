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
using TestOrderedEnumerable1;
using TestOrderedEnumerable1.Design;
using TestOrderedEnumerable1.HTML.Pages;

namespace TestOrderedEnumerable1
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            var a = new[] { 3.4, 4.3, 5.1, 1.1 };

            var x = new __OrderedEnumerable<double>
            {
                source = a.AsEnumerable()
            };

            var r = x.GetEnumerator();

            while (r.MoveNext())
            {
                new IHTMLPre { new { r.Current } }.AttachToDocument();

            }
        }

    }

    class __OrderedEnumerable<TSource>
    {
        public __OrderedEnumerable<TSource> prev;
        public __OrderedEnumerable<TSource> next;

        public IEnumerable<TSource> source;

        public virtual int Compare(TSource a, TSource b)
        {

            return 0;
        }

        //{ SourceMethod = Int32<GetEnumerator> b__1(TSource, TSource) }
        //    script: error JSC1000: unknown opcode brtrue.s at<GetEnumerator>b__1 + 0x002f
        //script: error JSC1000: Method: <GetEnumerator>b__1, Type: TestOrderedEnumerable1.__OrderedEnumerable`1+<>c__DisplayClass0; emmiting failed : System.InvalidOperationException: unknown opcode brtrue.s at<GetEnumerator>b__1 + 0x002f

        public IEnumerator<TSource> GetEnumerator()
        {
            // X:\jsc.svn\examples\javascript\test\TestOrderedEnumerable1\TestOrderedEnumerable1\Application.cs

            var p = this;

            while (p.prev != null) p = p.prev;



            TSource[] array = p.source.ToArray();

            Array.Sort(array,
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

                        x = x.next;
                    }

                    return r;
                }
            );

            var enumerable = array.AsEnumerable();
            var enumerator = enumerable.GetEnumerator();
            return enumerator;
        }

    }
}
