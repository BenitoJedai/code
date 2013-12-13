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
using CSSFormsBorderlessGrid;
using CSSFormsBorderlessGrid.Design;
using CSSFormsBorderlessGrid.HTML.Pages;
using System.Windows.Forms;
using System.Drawing;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms;

namespace CSSFormsBorderlessGrid
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
            new DataGridView
            {
                GridColor = Color.Transparent,

                BackgroundColor = Color.Transparent,

                // wont work for fill well?
                RowHeadersVisible = false,

                //DefaultCellStyle = new DataGridViewCellStyle
                //{
                //},

                RowHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    // hide the 4px 
                    BackColor = Color.Transparent
                },

                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(0x40, 0, 0x7f, 0x0)
                },

                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.Transparent,

                    // script: error JSC1000: No implementation found for this native method, please implement [static System.Drawing.Color.get_Gray()]
                    ForeColor = Color.FromArgb(0x7f, 0x7f, 0x7f)
                },

                //AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,

                DataSource = Book1.GetDataSet().Tables[0],

                ReadOnly = true

            }.With(
                async g =>
                {
                    Native.document.body.Clear();

                    g.DefaultCellStyle.SelectionBackColor = Color.Transparent;

                    g.AttachControlToDocument();

                    #region css
                    __DataGridView gg = g;

                    gg.__ColumnsTable_css_td.style.textTransform = IStyle.TextTransformEnum.uppercase;

                    gg.__ContentTable_css_td.hover.style.cursor = IStyle.CursorEnum.pointer;

                    gg.__ContentTable_css_td.hover.style.color = "blue";

                    gg.__ContentTable.css[IHTMLElement.HTMLElementEnum.tbody][IHTMLElement.HTMLElementEnum.tr].hover[IHTMLElement.HTMLElementEnum.td].style.backgroundColor = "yellow";

                    gg.__ContentTable_css_td.hover.style.textDecoration = "underline";

                    gg.__ContentTable_css_td.style.fontWeight = "bold";
                    gg.__ContentTable_css_td.style.paddingTop = "1em";
                    gg.__ContentTable_css_td.style.paddingBottom = "1em";
                    #endregion


                }
            );
        }

    }
}
