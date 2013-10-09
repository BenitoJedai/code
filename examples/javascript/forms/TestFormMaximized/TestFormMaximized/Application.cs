using TestFormMaximized;
using TestFormMaximized.Design;
using TestFormMaximized.HTML.Pages;
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

namespace TestFormMaximized
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // WindowState do InternalMaximizedForms { Location = { X = 600, Y = 219.5 }, ClientSize = { Width = 400, Height = 400 } }

            var f = new Form();

            f.Controls.Add(content);
            content.Dock = DockStyle.Fill;

            f.Show();

            // WindowState do InternalMaximizedForms { Location = { X = 592, Y = 0 }, ClientSize = { Width = 400, Height = 32 } }
            // WindowState do InternalMaximizedForms { Location = { X = 592, Y = 0 }, ClientSize = { Width = 392, Height = 0 } }

            //Native.window.requestAnimationFrame +=
            //    delegate
            //    {
            f.WindowState = FormWindowState.Maximized;
            //    };

        }

    }
}
