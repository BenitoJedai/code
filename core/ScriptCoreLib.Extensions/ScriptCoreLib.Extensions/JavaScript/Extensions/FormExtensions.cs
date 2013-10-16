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
    }
}
