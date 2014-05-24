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
using TestRoslynYieldReturn;
using TestRoslynYieldReturn.Design;
using TestRoslynYieldReturn.HTML.Pages;
using ScriptCoreLib.JavaScript.Native;

namespace TestRoslynYieldReturn
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        // X:\jsc.svn\examples\javascript\forms\BSONExperiment\BSONExperiment\ApplicationControl.cs

        public static IEnumerable<Type> GetAllInterfaces(Type target)
        {
            foreach (var i in target.GetInterfaces())
            {
                yield return i;
                foreach (var ci in i.GetInterfaces())
                {
                    yield return ci;
                }
            }
        }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // X:\jsc.svn\examples\javascript\forms\BSONExperiment\BSONExperiment\ApplicationControl.cs

            document.body += new IHTMLPre { "GetAllInterfaces" };

            GetAllInterfaces(typeof(Application)).WithEach(
                SourceType =>
                {
                    // Error	5	Operator '+=' cannot be applied to operands of type 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLBody' and 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLPre'	X:\jsc.svn\examples\javascript\test\TestRoslynYieldReturn\TestRoslynYieldReturn\Application.cs	52	21	TestRoslynYieldReturn
                    document.body += new IHTMLPre { new { SourceType } };

                    //new IHTMLPre { new { SourceType } }.AttachToDocument();
                }
            );
        }

    }
}
