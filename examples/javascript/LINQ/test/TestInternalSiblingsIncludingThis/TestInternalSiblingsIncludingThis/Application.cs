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
    using __Control = Application;

    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public __Control Parent;
        public List<__Control> Controls;

        public IEnumerable<__Control> InternalSiblingsIncludingThis
        {
            get
            {
                //            script: error JSC1000: ***stack is empty, invalid pop?
                //script: error JSC1000: error at TestInternalSiblingsIncludingThis.Application.get_InternalSiblingsIncludingThis,
                // assembly: V:\TestInternalSiblingsIncludingThis.Application.exe
                // type: TestInternalSiblingsIncludingThis.Application, TestInternalSiblingsIncludingThis.Application, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null
                // offset:
                //                0x0067
                //  method: System.Collections.Generic.IEnumerable`1[TestInternalSiblingsIncludingThis.Application] get_InternalSiblingsIncludingThis()

                return from p in new[] { this.Parent }
                       where p != null
                       from i in Enumerable.Range(0, p.Controls.Count)
                       let cc = (__Control)this.Parent.Controls[i]
                       select cc;

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
