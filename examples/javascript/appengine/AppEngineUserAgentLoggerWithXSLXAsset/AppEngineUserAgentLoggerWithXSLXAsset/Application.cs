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

            //Native.document.body.querySelectorAll("script[src='view-source']").WithEach(x => x.Orphanize());

            Console.WriteLine("before notify");
            this.Notfiy().ContinueWithResult(
                data =>
                {
                    Console.WriteLine("");
                    Console.WriteLine("at notify");
                    Console.WriteLine(new { data.visit, data.DataSource.Rows.Count });

                    //                    I/chromium( 2322): ", source: http://192.168.43.7:3678/view-source (29877)
                    //I/chromium( 2322): [INFO:CONSOLE(29877)] "{ visit = 4, Count = 0 }", source: http://192.168.43.7:3678/view-source (29877)

                    var g = new DataGridView
                    {
                        ReadOnly = true,

                        SelectionMode = DataGridViewSelectionMode.FullRowSelect,

                        DataSource = data.DataSource,
                    }.AttachControlToDocument();

                    var reset = new IHTMLButton { "reset" }.AttachToDocument();

                    reset.style.position = IStyle.PositionEnum.absolute;
                    reset.style.right = "1em";
                    reset.style.top = "1em";
                    reset.style.zIndex = 100;

                    reset.WhenClicked(
                        async delegate
                        {
                            await this.Reset();

                            Native.document.documentElement.Clear();

                            Native.window.alert("reload");

                            Native.document.location.reload();
                        }
                    );

                    Console.WriteLine("before CellDoubleClick");

                    #region CellDoubleClick
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





                            var gg = new DataGridView
                            {
                                ReadOnly = true,
                                Dock = DockStyle.Fill,

                                SelectionMode = DataGridViewSelectionMode.FullRowSelect
                            };

                            gg.CellFormatting +=
                                (ggsender, e) =>
                                {
                                    if (e.RowIndex < 0)
                                        return;
                                    if (e.ColumnIndex < 0)
                                        return;

                                    if (e.Value == null)
                                        return;

                                    if (e.Value is System.DBNull)
                                        return;



                                    string stringValue = (string)e.Value;

                                    var i = 0;

                                    if (int.TryParse(stringValue, out i))
                                    {
                                        //e.CellStyle.ForeColor = Color.Gray;
                                        //e.CellStyle.ForeColor = Color.FromArgb(0x808080);
                                        //e.CellStyle.BackColor = Color.FromArgb(0xa0, 0xa0, 0xa0);
                                        //e.CellStyle.BackColor = Color.FromArgb(0xf0, 0xf0, 0xf0);
                                        e.CellStyle.BackColor = SystemColors.ButtonFace;
                                    }
                                    else
                                    {
                                        e.CellStyle.BackColor = Color.White;
                                    }
                                };

                            var ff = new Form();
                            ff.Controls.Add(gg);
                            ff.Show();

                            var headers = await this.GetVisitHeadersFor(
                                 (Design.Book1BSheet1Key)Convert.ToInt64(
                                     Key
                                 )
                             );

                            gg.DataSource = headers;


                            //g.DataSource = headers;

                            //Native.window.alert(
                            //    new { args.RowIndex, Key }
                            //    );
                        };
                    #endregion


                }
            );
        }

    }
}
