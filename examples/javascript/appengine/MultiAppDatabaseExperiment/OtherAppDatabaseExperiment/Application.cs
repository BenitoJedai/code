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
using OtherAppDatabaseExperiment;
using OtherAppDatabaseExperiment.Design;
using OtherAppDatabaseExperiment.HTML.Pages;
using System.Windows.Forms;
using System.Drawing;

namespace OtherAppDatabaseExperiment
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

            var content = new MultiAppDatabaseExperiment.UserControl1
            {
                _ClientsTable_Insert = base._ClientsTable_Insert
            };

            content.AttachControlToDocument();

            //var g = new DataGridView
            //{
            //    BackgroundColor = Color.Transparent,
            //    DefaultCellStyle = new DataGridViewCellStyle
            //    {
            //        SelectionBackColor = Color.Black,
            //        SelectionForeColor = Color.Yellow,
            //        BackColor = Color.FromArgb(0x3f, 255, 255, 255)
            //    },

            //    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            //    {

            //        BackColor = Color.FromArgb(0x8f, 255, 255, 255)
            //    },

            //    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            //    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            //    AllowUserToAddRows = false,
            //    RowHeadersVisible = false,
            //}.With(async gg => { var temp = await base._ClientsTable_SelectAll(); gg.DataSource = temp; });
            //g.GetHTMLTargetContainer().With(
            //       div =>
            //       {
            //           div.style.overflow = IStyle.OverflowEnum.hidden;
            //           (div.style as dynamic).zIndex = "";

            //           div.style.position = IStyle.PositionEnum.relative;
            //           div.style.left = "";
            //           div.style.top = "";
            //           div.style.right = "";
            //           div.style.bottom = "";

            //           div.AttachTo(page.output);
            //       }
            //   );
        }

    }
}
