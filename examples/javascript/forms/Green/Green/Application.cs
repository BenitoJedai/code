using Green;
using Green.Design;
using Green.HTML.Pages;
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
using ScriptCoreLib.JavaScript.Windows.Forms;
using Green.HTML.Images.FromAssets;

namespace Green
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary> 
    public sealed class Application : ApplicationWebService
    {

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //var e = content.GetHTMLTarget();
            var e = content.GetHTMLTargetContainer();

            e.AttachToDocument();
            e.className = "goo";

            e.style.margin = "auto";
            e.style.marginTop = "3em";
            e.style.backgroundColor = "rgba(255, 255, 255, 0.7)";
            //e.css.style.backgroundColor = "rgba(255, 255, 255, 0.5)";


            //IStyleSheet.all["button"].style.backgroundColor = "yellow";
            //IStyleSheet.all["button"].style.color = "green";


            this.GetFoo2().ContinueWithResult(
                data =>
                {
                    content.dataGridView1.DataSource = data;

                    foreach (DataGridViewRow item in content.dataGridView1.Rows)
                    {
                        DataGridViewCell c = item.Cells[0];

                        c.ReadOnly = true;

                        //ScriptCoreLib.Extensons.JavaScript.DataGridViewCellExtensions.AsHTMLElementContainer()

                        //DataGridViewCellExtensions
                        //c.as


                        //var i = new twitter_icon().AttachTo(
                        //    c.AsHTMLElementContainer()
                        //);

                        //i.style.width = "auto";
                        //i.style.height = "1em";
                        //i.style.Float = IStyle.FloatEnum.left;

                        var che = new CheckBox { Text = (string)c.Value, Checked = true };
                        c.Value = "";

                        //che.CheckedChanged +=
                        //    delegate
                        //    {
                        //        i.Show(che.Checked);
                        //    };

                        che.GetHTMLTarget().style.display = IStyle.DisplayEnum.inline_block;

                        che.GetHTMLTarget().AttachTo(
                            c.AsHTMLElementContainer()
                        );

                    }
                }
            );

        }

    }
}
