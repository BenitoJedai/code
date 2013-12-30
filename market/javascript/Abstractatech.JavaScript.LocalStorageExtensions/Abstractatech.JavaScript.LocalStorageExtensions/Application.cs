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
using Abstractatech.JavaScript.LocalStorageExtensions;
using Abstractatech.JavaScript.LocalStorageExtensions.Design;
using Abstractatech.JavaScript.LocalStorageExtensions.HTML.Pages;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace Abstractatech.JavaScript.LocalStorageExtensions
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

            Console.WriteLine("App load");
            var g = new DataGridView
            {
                BackgroundColor = Color.Transparent,

                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Name = "test",
                AllowUserToAddRows = false,
            };

            g.SetDataSourceFromLocalStorage();

            Action data = async delegate
            {
                var dt = await this.GetData();
                g.DataSource = dt;                
            };
            data();

            g.AttachControlToDocument();
        }
    }
}
namespace System.Windows.Forms
{
    public static class DataTableToLocalStorage
    {
        public static DataGridView SetDataSourceFromLocalStorage(this DataGridView g)
        {
            Console.WriteLine("SetDataSourceFromLocalStorage " + new { g.Name });
            if (Native.window.localStorage[g.Name] != null)
            {
                var dt = ScriptCoreLib.Library.StringConversionsForDataTable.ConvertFromString(Native.window.localStorage[g.Name]);
                g.DataSource = dt;
            }

            g.DataSourceChanged +=
                delegate
                {
                    var dt = ScriptCoreLib.Library.StringConversionsForDataTable.ConvertToString((DataTable)g.DataSource);
                    Native.window.localStorage[g.Name] = dt;
                };

            return g;
        }
    }
}
