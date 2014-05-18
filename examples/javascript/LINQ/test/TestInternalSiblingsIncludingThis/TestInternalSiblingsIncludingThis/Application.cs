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
using TestInternalSiblingsIncludingThis;
using TestInternalSiblingsIncludingThis.Design;
using TestInternalSiblingsIncludingThis.HTML.Pages;

namespace TestInternalSiblingsIncludingThis
{
    //using __Control = Application;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //public __Control Parent;
        //public List<__Control> Controls;

        //public IEnumerable<__Control> InternalSiblingsIncludingThis
        public IEnumerable<int> InternalSiblingsIncludingThis
        {
            get
            {
                // X:\jsc.svn\examples\rewrite\TestRoslynSelectMany\TestRoslynSelectMany\Class1.cs
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140518

                //            script: error JSC1000: ***stack is empty, invalid pop?
                //script: error JSC1000: error at TestInternalSiblingsIncludingThis.Application.get_InternalSiblingsIncludingThis,
                // assembly: V:\TestInternalSiblingsIncludingThis.Application.exe
                // type: TestInternalSiblingsIncludingThis.Application, TestInternalSiblingsIncludingThis.Application, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null


                // broken on roslyn. why?
                return from p in Enumerable.Range(0, 1)
                       from i in Enumerable.Range(0, 1)
                       select i;

            }
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

        }

    }
}
