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
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DEAGELForecast;
using DEAGELForecast.Design;
using DEAGELForecast.HTML.Pages;
using ScriptCoreLib.JavaScript.DOM.SVG;

namespace DEAGELForecast
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
            // https://web.archive.org/web/20090219070038/http://www.deagel.com/country/United-States-of-America_c0001.aspx

            new { }.With(
                async scope =>
                {
                    var svg = new WebClient().DownloadStringTaskAsync(new HTML.Images.FromAssets.BlankMap_World6_Equirectangular().src);

                    var xml = XElement.Parse(await svg);

                    // jsc, will this work?
                    xml.AttachToDocument();

                    // script: error JSC1000: No implementation found for this native method, please implement [System.Xml.Linq.XContainer.Descendants(System.Xml.Linq.XName)]
                    //var landUS = xml.Descendants("path").FirstOrDefault(path => path.Attribute("class").Value == "land us");
                    xml.Descendants().Where(x => x.Attribute("class") != null).Where(path => path.Attribute("class").Value == "land us").WithEach(
                        landUS =>
                        {
                            // http://stackoverflow.com/questions/17616233/css-hover-sometimes-doesnt-work-on-svg-paths

                            var landUS_style = landUS.Attribute("style");
                            landUS_style.Value = landUS.Value.TakeUntilIfAny("fill:").SkipUntilIfAny(";") + ";fill: red; pointer-events:all;";

                            // http://www.deagel.com/country/United-States-of-America_c0001.aspx

                            // this wont work
                            //landUS.SetAttributeValue("title", "Forecast 2025: -78%");

                            //var path = (ISVGPathElement)landUS.AsHTMLElement();
                            //path.onmouseover +=
                            //    delegate
                            //{
                            //    landUS_style.Value = landUS.Value.TakeUntilIfAny("fill:").SkipUntilIfAny(";") + ";fill: yellow;";
                            //};
                        }
                    );


                }
            );



        }

    }
}
