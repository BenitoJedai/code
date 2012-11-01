using FakeWindowsLoginExperiment.Design;
using FakeWindowsLoginExperiment.HTML.Pages;
using FakeWindowsLoginExperiment.Library;
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
    public sealed partial class Application
    {
        public readonly IApp page;



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
            : this()
        {
            this.page = page;

            new HTML.Images.FromAssets.wallpaper().ToBackground(Native.Document.body);

            new HTML.Audio.FromAssets.Windows_Logon_Sound().play();




            f.Show();

            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);
            @"Hello world".ToDocumentTitle();

        }

        private void idleTimer1_UserLostInterest()
        {
            new HTML.Audio.FromAssets.Windows_Hardware_Insert().play();

            shadowOverlay1.Show();
            //page.ShadowOverlay.Show();

        }

        private void idleTimer1_UserShowedInterest()
        {

            //new HTML.Audio.FromAssets.Windows_Hardware_Remove().play();
            shadowOverlay1.Hide();
            //page.ShadowOverlay.Hide();
        }

        private void TimerForLogout_UserLostInterest()
        {
            new HTML.Audio.FromAssets.Windows_Logoff_Sound().play();
            Native.Document.location.replace("/FakeLoginScreen");
        }

        private void form11_FormButtonClick()
        {
            MessageBox.Show("hey!!!");
        }

        private void f_FormClosed(object sender, FormClosedEventArgs e)
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
        }

    }
}
