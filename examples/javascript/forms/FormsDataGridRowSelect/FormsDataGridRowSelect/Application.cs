using FormsDataGridRowSelect;
using FormsDataGridRowSelect.Design;
using FormsDataGridRowSelect.HTML.Pages;
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

namespace FormsDataGridRowSelect
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131019

        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var f = new Form();
            f.FormBorderStyle = FormBorderStyle.None;
            f.TextChanged += (x, xx) => Native.document.title = f.Text;

            content.Dock = DockStyle.Fill;
            f.Controls.Add(content);
            f.Show();
            f.WindowState = FormWindowState.Maximized;
        }

    }
}
