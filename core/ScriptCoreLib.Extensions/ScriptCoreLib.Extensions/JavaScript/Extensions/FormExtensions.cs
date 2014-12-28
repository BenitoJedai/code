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
            // tested by
            // X:\jsc.svn\examples\javascript\Test\TestSolutionBuilder\TestSolutionBuilderV1\Views\StudioView.cs

            // we need internal method to prevent a glitch. why is the glitch??
            InternalAttachFormTo(f, c);

            return f;
        }

        static void InternalAttachFormTo(this System.Windows.Forms.Form f, IHTMLElement c)
        {
            // X:\jsc.svn\examples\javascript\Test\TestServiceWorkerVisualizedScreens\TestServiceWorkerVisualizedScreens\Application.cs

            f.GetHTMLTarget().AttachTo(c);

            f.Show();
            f.WindowState = FormWindowState.Maximized;

        }


        public static T AttachControlToDocument<T>(this T content)
            //where T : ScrollableControl
            where T : Control
        {
            return AttachControlTo(content, null);
        }

        public static T AttachControlTo<T>(this T content, IHTMLElement c)
            //where T : ScrollableControl
            where T : Control
        {
            if (content is Form)
            {
                // Error	76	Cannot convert type 'T' to 'System.Windows.Forms.Form'	X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\Extensions\FormExtensions.cs	40	21	ScriptCoreLib.Extensions

                InternalAttachFormTo(
                   (Form)(Control)content,
                   c
               );

                return content;
            }
            // X:\jsc.svn\examples\javascript\forms\FormsProjectTemplateExperiment\FormsProjectTemplateExperiment\Application.cs

            // http://stackoverflow.com/questions/16649943/css-to-set-a4-paper-size
            // http://jsfiddle.net/2wk6Q/3/

            // http://stackoverflow.com/questions/16649943/css-to-set-a4-paper-size

            //Native.document.body.css.print.style.padding = "0px";
            //Native.document.body.css.print.style.margin = "0px";


            //Native.document.documentElement.css.print.style.padding = "0px";
            //Native.document.documentElement.css.print.style.margin = "0px";

            //Native.document.documentElement.css.print.style.width = "21cm";
            //Native.document.documentElement.css.print.style.height = "27.7cm";

            Native.css.print.style.width = "21cm";
            Native.css.print.style.height = "27.7cm";


            var f = new Form
            {
                Name = "ApplicationForm",

                // can we undo this on runtime?
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                WindowState = FormWindowState.Maximized,

                ControlBox = false,
                ShowIcon = false,

            };

            //Error	79	'T' does not contain a definition for 'AutoScroll' and no extension method 'AutoScroll' accepting a first argument of type 'T' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\JavaScript\Extensions\FormExtensions.cs	44	21	ScriptCoreLib.Extensions

            var asScrollableControl = content as ScrollableControl;
            if (asScrollableControl != null)
                asScrollableControl.AutoScroll = true;

            //content.AutoScrollMargin = new Size(8, 8);

            // needs a fix for print!
            content.Dock = DockStyle.Fill;


            f.TextChanged +=
                delegate
                {
                    Native.document.title = f.Text;
                };

            f.Controls.Add(content);



            if (c != null)
                f.GetHTMLTarget().AttachTo(c);

            f.Show();


            return content;
        }
    }
}
