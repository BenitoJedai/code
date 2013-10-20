using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class FormExtensions
    {
        // WindowsFormsExtensions
        public static T AttachFormTo<T>(this T f, IHTMLElement c)
            where T : System.Windows.Forms.Form
        {

            f.GetHTMLTarget().AttachTo(c);

            f.Show();
            f.WindowState = FormWindowState.Maximized;

            return f;
        }

        public static T AttachControlToDocument<T>(this T that) where T : ScrollableControl
        {
            ScrollableControl content = that;
            // X:\jsc.svn\examples\javascript\forms\FormsProjectTemplateExperiment\FormsProjectTemplateExperiment\Application.cs

            // http://stackoverflow.com/questions/16649943/css-to-set-a4-paper-size
            // http://jsfiddle.net/2wk6Q/3/

            Native.document.documentElement.css.print.style.width = "21cm";
            Native.document.documentElement.css.print.style.height = "29.7cm";

            var f = new Form
            {
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,


                WindowState = FormWindowState.Maximized
            };

            //Error	79	'T' does not contain a definition for 'AutoScroll' and no extension method 'AutoScroll' accepting a first argument of type 'T' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\Extensions\FormExtensions.cs	44	21	ScriptCoreLib.Extensions

            content.AutoScroll = true;
            //content.AutoScrollMargin = new Size(8, 8);

            // needs a fix for print!
            content.Dock = DockStyle.Fill;


            f.TextChanged +=
                delegate
                {
                    Native.document.title = f.Text;
                };

            f.Controls.Add(content);
            f.Show();


            return that;
        }
    }
}
