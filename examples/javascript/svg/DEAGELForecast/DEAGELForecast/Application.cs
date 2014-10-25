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
            // internal nuclear war

            // https://www.youtube.com/watch?v=qgjlqzHbwhg
            // the day after disaster
            // countdown to zero
            // confusion/disonfirmation/ what is it?
            // BRICS vs NATO

            // https://web.archive.org/web/20090219070038/http://www.deagel.com/country/United-States-of-America_c0001.aspx

            new { }.With(
                async scope =>
                {
                    var rgb_red = new { r = 255, g = 0, b = 0 };
                    var rgb_yellow = new { r = 255, g = 255, b = 0 };
                    var rgb_green = new { r = 0, g = 255, b = 0 };

                    var rgb_red_to_yellow = Enumerable.Range(0, 100 + 1).Select(
                        i => new
                    {
                        // rgb likes ints instead of doubles
                        r = (int)(rgb_yellow.r * (i / 100.0) + rgb_red.r * ((100.0 - i) / 100.0)),
                        g = (int)(rgb_yellow.g * (i / 100.0) + rgb_red.g * ((100.0 - i) / 100.0)),
                        b = (int)(rgb_yellow.b * (i / 100.0) + rgb_red.b * ((100.0 - i) / 100.0))
                    }
                    ).ToArray();

                    var rgb_yellow_to_green = Enumerable.Range(0, 100 + 1).Select(
                           i => new
                    {
                        // rgb likes ints instead of doubles
                        r = (int)(rgb_green.r * (i / 100.0) + rgb_yellow.r * ((100.0 - i) / 100.0)),
                        g = (int)(rgb_green.g * (i / 100.0) + rgb_yellow.g * ((100.0 - i) / 100.0)),
                        b = (int)(rgb_green.b * (i / 100.0) + rgb_yellow.b * ((100.0 - i) / 100.0))
                    }
                       ).ToArray();

                    var rgb_200_red_to_yellow_to_green = rgb_red_to_yellow.Concat(rgb_yellow_to_green).ToArray();

                    var ds = Data.forecast.GetDataSet();

                    var forecast0 = (
                        from item in ds.Tables["population"].Rows.AsEnumerable()
                        let row = (Data.forecastpopulationRow)item
                        let diff = row.Forecast2025Population - row.Current2013Population
                        let delta = diff / row.Current2013Population

                        select new { row, diff, delta }
                    ).ToArray();

                    // whats the max delta?

                    var min = forecast0.Min(x => x.delta);
                    var max = forecast0.Max(x => x.delta);



                    new IHTMLPre {
                            new { min, max }
                        }.AttachToDocument();



                    var forecast = (
                        from item in forecast0

                        let indicator = (int)Math.Floor(200 * (item.delta + -min) / (-min + max))
                        //let rgb = rgb_red_to_yellow[indicator]
                        let rgb = rgb_200_red_to_yellow_to_green[indicator]

                        select new { item.row, item.diff, item.delta, indicator, rgb }
                    );

                    // {{ min = -0.7816455696202531, max = 0.16666666666666666 }}
                    // how do we get from min max to red yellow green?




                    foreach (var item in forecast)
                    {


                        var pre = new IHTMLPre {

                            new { item.row.Current2013Population,
                                    item.row.Name,
                                    item.row.Forecast2025Population,

                                    item.diff,

                                    item.delta,
                                    item.indicator
                                    //, rgb

                            }
                        }.AttachToDocument();

                        //pre.style.borderLeft = "2em solid yellow";
                        pre.style.borderLeft = "2em solid rgba(" + item.rgb.r + "," + item.rgb.g + "," + item.rgb.b + ", 1.0)";
                    }


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
                            landUS_style.Value = landUS.Value.TakeUntilIfAny("fill:").SkipUntilIfAny(";") + ";fill: red;";

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


                    // linq in async broken for now? need to rescope?
                    xml.Descendants().Where(x => x.Attribute("class") != null).With(
                         paths =>
                        {

                            (from item in forecast


                                 // http://msdn.microsoft.com/en-us/library/bb311040.aspx

                             join path in paths on item.row.Name equals path.Attribute("class").Value

                             select new { item, path }
                            ).WithEach(
                                x =>
                            {
                                // http://stackoverflow.com/questions/11257015/how-to-give-hsl-color-value-to-an-svg-element

                                var landUS_style = x.path.Attribute("style");
                                landUS_style.Value = x.path.Value.TakeUntilIfAny("fill:").SkipUntilIfAny(";")
                                + ";fill: "
                                    + "rgba(" + x.item.rgb.r + ", " + x.item.rgb.g + ", " + x.item.rgb.b + ", 1.0)";

                            }
                            );
                        }
                     );



                }
            );



        }

    }
}
