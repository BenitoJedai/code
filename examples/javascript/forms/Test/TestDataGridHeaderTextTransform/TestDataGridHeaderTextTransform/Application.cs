using TestDataGridHeaderTextTransform;
using TestDataGridHeaderTextTransform.Design;
using TestDataGridHeaderTextTransform.HTML.Pages;
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
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace TestDataGridHeaderTextTransform
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        //public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //content.AttachControlToDocument();
            //@"Hello world".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //this.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);

            new DataGridView
            {
                RowHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Red
                },

                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Yellow
                },

                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Yellow,

                    // script: error JSC1000: No implementation found for this native method, please implement [static System.Drawing.Color.get_Gray()]
                    ForeColor = Color.FromArgb(0x7f, 0x7f, 0x7f)
                },

                // script: error JSC1000: No implementation found for this native method, please implement [System.Data.DataTableCollection.get_Item(System.Int32)]
                DataSource = Book1.GetDataSet().Tables.AsEnumerable().First()


            }.With(
                async g =>
                {
                    g.AttachControlToDocument();

                    __DataGridView gg = g;

                    Console.WriteLine(
                        new { gg.__ContentTable_css_alt_td }
                    );

                    // implicit

                    // X:\jsc.svn\examples\javascript\css\CSSHover\CSSHover\Application.cs
                    gg.__ColumnsTable_css_td.style.textTransform = IStyle.TextTransformEnum.uppercase;

                    gg.__ColumnsTable_css_td.style.transition = "background-color linear 700ms";
                    gg.__ContentTable_css_td.style.transition = "background-color linear 700ms";
                    gg.__ContentTable_css_alt_td.style.transition = "background-color linear 700ms";

                    //gg.__ColumnsTable_css_td.style.backgroundColor = "cyan";

                    while (true)
                    {
                        await Task.Delay(500);
                        g.ColumnHeadersDefaultCellStyle.BackColor = Color.Green;
                        g.RowHeadersDefaultCellStyle.BackColor = Color.Green;

                        g.AlternatingRowsDefaultCellStyle.BackColor = Color.Yellow;
                        g.DefaultCellStyle.BackColor = Color.White;

                        await Task.Delay(500);
                        g.ColumnHeadersDefaultCellStyle.BackColor = Color.Yellow;
                        g.RowHeadersDefaultCellStyle.BackColor = Color.Yellow;

                        g.AlternatingRowsDefaultCellStyle.BackColor = Color.Blue;
                        g.DefaultCellStyle.BackColor = Color.FromArgb(0xef, 0xef, 0xef);

                        await Task.Delay(500);
                        g.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
                        g.RowHeadersDefaultCellStyle.BackColor = Color.Blue;

                        g.AlternatingRowsDefaultCellStyle.BackColor = Color.Green;
                        g.DefaultCellStyle.BackColor = Color.FromArgb(0xdf, 0xdf, 0xdf);
                    }
                }
            );


        }

    }
}
