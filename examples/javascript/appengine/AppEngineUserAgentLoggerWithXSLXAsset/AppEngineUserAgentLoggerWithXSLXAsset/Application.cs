using AppEngineUserAgentLoggerWithXSLXAsset;
using AppEngineUserAgentLoggerWithXSLXAsset.Design;
using AppEngineUserAgentLoggerWithXSLXAsset.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AppEngineUserAgentLoggerWithXSLXAsset
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public Application(IApp page)
        {
            this.ScreenWidth = Native.window.screen.width;
            this.ScreenHeight = Native.window.screen.height;

            var now = DateTime.Now;
            this.ClientTime = now.ToString();

            this.Notfiy().ContinueWithResult(
                data =>
                {
                    var g = new DataGridView
                    {
                        DataSource = data,
                        ReadOnly = true,

                        SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    }.AttachControlToDocument();

                    g.CellDoubleClick +=
                        async (sender, args) =>
                        {

                            //As a service to the app, App Engine adds some headers:

                            //X-AppEngine-Country
                            //Country from which the request originated, as an ISO 3166-1 alpha-2 country code. App Engine determines this code from the client's IP address.
                            //X-AppEngine-Region
                            //Name of region from which the request originated. This value only makes sense in the context of the country in X-AppEngine-Country. For example, if the country is "US" and the region is "ca", that "ca" means "California", not Canada.
                            //X-AppEngine-City
                            //Name of the city from which the request originated. For example, a request from the city of Mountain View might have the header value mountain view.
                            //X-AppEngine-CityLatLong
                            //Latitude and longitude of the city from which the request originated. This string might look like "37.386051,-122.083851" for a request from Mountain View.

                            // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.get_Item(System.String, System.Int32)]
                            //var Key = g["Key", args.RowIndex];

                            // is it a string, long, or signed db enum?
                            var Key = (string)g.Rows[args.RowIndex].Cells["Key"].Value;



                            var headers = await this.GetVisitHeadersFor(
                                (Design.Book1Sheet1Key)Convert.ToInt64(
                                    Key
                                )
                            );

                            var gg = new DataGridView
                            {
                                DataSource = headers,
                                ReadOnly = true,
                                Dock = DockStyle.Fill,

                                SelectionMode = DataGridViewSelectionMode.FullRowSelect
                            };

                            var ff = new Form();
                            ff.Controls.Add(gg);
                            ff.Show();



                            //g.DataSource = headers;

                            //Native.window.alert(
                            //    new { args.RowIndex, Key }
                            //    );
                        };

                }
            );
        }

    }
}
