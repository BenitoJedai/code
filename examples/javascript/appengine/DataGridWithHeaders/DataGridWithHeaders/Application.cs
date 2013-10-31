using DataGridWithHeaders;
using DataGridWithHeaders.Design;
using DataGridWithHeaders.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DataGridWithHeaders
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


            this.Headers.AttachDataGridViewToDocument();

            //new DataGridView().With(
            //    async grid =>
            //    {

            //        var data = await this.Headers;

            //        grid.DataSource = data;

            //        grid.AttachControlToDocument();

            //        Native.document.title = data.TableName;
            //    }
            //);
        }

    }
}
