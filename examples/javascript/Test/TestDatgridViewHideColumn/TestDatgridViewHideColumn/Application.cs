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
using System.Windows.Forms;
using System.Xml.Linq;
using TestDatgridViewHideColumn;
using TestDatgridViewHideColumn.Design;
using TestDatgridViewHideColumn.HTML.Pages;

namespace TestDatgridViewHideColumn
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
            Action getData = async delegate
            {
                var dt = await GetDataTable();
                var dg = new DataGridView ();
                dg.DataSourceChanged += delegate
                {
                    Console.WriteLine(new { DataSourceChangedEvent = "Inside Datagridsource changed" });
                    //foreach (var i in dg.Columns)
                    //{
                    //    Console.WriteLine(new { i });
                    //}
                    dg.Columns["Foo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                        Console.WriteLine(dg.Columns["Foo"].Width.ToString());
                        dg.Columns["Foo"].Width = 0;

                   
                };
                dg.AttachControlToDocument();
                dg.DataSource = dt;
                dg.DataMember = "Sheet1";
                
                
            };
            getData();
        }

    }
}
