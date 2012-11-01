using FakeWindowsLoginExperiment.Design;
using FakeWindowsLoginExperiment.HTML.Pages;
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FakeWindowsLoginExperiment
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
            new HTML.Images.FromAssets.wallpaper().ToBackground(Native.Document.body);

            new HTML.Audio.FromAssets.Windows_Logon_Sound().play();

            var f = new Form();
            f.Height = content.Height + 32;
            f.Width = content.Width + 8;
            f.Controls.Add(content);

            f.FormClosed +=
                delegate
                {

                    new HTML.Audio.FromAssets.Windows_Logoff_Sound().play();
                    page.Taskbar.Orphanize();

                    // we need a ceneric timer regardless of target platform
                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                       delegate
                       {


                           Native.Document.location.replace("/FakeLoginScreen");

                       }
                     ).StartTimeout(800);

                };

            f.Show();

            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
