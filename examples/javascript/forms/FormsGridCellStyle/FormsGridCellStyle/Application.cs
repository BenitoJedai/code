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
using FormsGridCellStyle;
using FormsGridCellStyle.Design;
using FormsGridCellStyle.HTML.Pages;
using System.Windows.Forms;
using System.Drawing;

namespace FormsGridCellStyle
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
            //var z = ScriptCoreLib.Shared.Drawing.Color.FromKnownName(
            //        "rgba(" + 255 + ", " + 0 + ", " + 0 + ", " + (127 / 255.0) + ")"
            //    );

            //var x = Color.FromArgb(0x10, 255, 255, 0);

            //Console.WriteLine(
            //    new { z, x }
            //    );

            //Native.document.body.style.backgroundColor = x.ToString();


            var data = Book1.GetDataSet();

            // X:\jsc.svn\examples\javascript\WebGL\WebGLGoldDropletTransactions\WebGLGoldDropletTransactions\Application.cs
            var g = new DataGridView
            {
                BackgroundColor = Color.Transparent,


                // does this work?
                DefaultCellStyle = new DataGridViewCellStyle
                {

                    SelectionBackColor = Color.Black,
                    SelectionForeColor = Color.Yellow,


                    //BackColor = Color.Transparent
                    BackColor = Color.FromArgb(0x05, 0, 0, 0)
                },

                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    //BackColor = Color.Yellow,
                    BackColor = Color.FromArgb(0x7f, 255, 255, 255),
                    ForeColor = Color.Black
                },

                RowHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    //BackColor = Color.Yellow,
                    BackColor = Color.FromArgb(0x7f, 255, 255, 255),
                    ForeColor = Color.Black
                },


                GridColor = Color.FromArgb(0x7f, 0, 0, 0),

                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,

                // do we have a test for this?
                AllowUserToAddRows = false,

                //AllowUserToDeleteRows = false,

                //RowHeadersVisible = false,


                // cannot hide column headers yet
                // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_ColumnHeadersVisible(System.Boolean)]
                //ColumnHeadersVisible = false,

                DataSource = data,
                DataMember = "Assets",
            };


            g.AttachControlToDocument();

            (g.Parent as Form).GetHTMLTarget().style.margin = "3em";

        }

    }
}
