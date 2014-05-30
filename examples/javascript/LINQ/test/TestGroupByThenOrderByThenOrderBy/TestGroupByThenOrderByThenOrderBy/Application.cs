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
using TestGroupByThenOrderByThenOrderBy;
using TestGroupByThenOrderByThenOrderBy.Design;
using TestGroupByThenOrderByThenOrderBy.HTML.Pages;
using ScriptCoreLib.JavaScript.Native;

namespace TestGroupByThenOrderByThenOrderBy
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
            // binding UI and WebService worlds
            // was the init value correctly received?
            // 0:21ms {{ Tag = null, XTag = i am just a row in a database. the roslyn base type PrimaryConstructor can be used to set fields? }}
            // jsc needs to also start syncing 1 level deep public fields. this impacts security.
            // should we allow it if only its a data class?
            // meaning it behaves like anonymous type and does not have any instance methods other than ToString and ctors.
            // X:\jsc.svn\examples\javascript\test\TestBaseFieldSync\TestBaseFieldSync\ApplicationWebService.cs

            Console.WriteLine(new { base.Tag, base.XTag });
            page.Tag.value = this.XTag;
            page.Tag.onchange += delegate { this.Tag = page.Tag.value; };




            page.body.onclick += async delegate
            {

                css.style.backgroundColor = "yellow";


                await this.Insert(new Data.PerformanceResourceTimingData2ApplicationResourcePerformanceRow //<TTag>
                                                                                                           // what if we were able to autmatically select UI from data? can we pass a constructor?
                {
                    path = "inserted by the client. " + new { Tag }
                }
                );

                css.style.backgroundColor = "transparent";

            };


        }

    }
}
