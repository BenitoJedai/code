using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CSVAssetAsGridExperiment;
using CSVAssetAsGridExperiment.Design;
using CSVAssetAsGridExperiment.HTML.Pages;

namespace CSVAssetAsGridExperiment
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
            // X:\jsc.svn\examples\javascript\Test\TestCSVAsset\TestCSVAsset\ApplicationWebService.cs

            this.GetFoo().AttachDataGridViewToDocument().ContinueWithResult(
                async grid =>
                {
                    await Task.Delay(3000);

                    var MoreColumns = await this.GetFoo2();


                    grid.DataSource = MoreColumns;
                }
            );




        }

    }
}
