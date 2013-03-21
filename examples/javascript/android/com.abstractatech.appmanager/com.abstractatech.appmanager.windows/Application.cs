using com.abstractatech.appmanager.windows.Design;
using com.abstractatech.appmanager.windows.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
//using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace com.abstractatech.appmanager.windows
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //FormStyler.AtFormCreated = FormStylerLikeAero.LikeAero;

            var content = new ApplicationControl();


            var f = new Form();

            f.ClientSize = content.Size;

            f.Controls.Add(content);
            content.Dock = DockStyle.Fill;

            f.Show();


            f.PopupInsteadOfClosing();
        }

    }
}
