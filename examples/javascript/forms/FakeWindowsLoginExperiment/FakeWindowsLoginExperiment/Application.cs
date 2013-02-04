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
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FakeWindowsLoginExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    /// 
    public sealed partial class Application
    {
        public readonly IApp page;



        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;

            InitializeComponent();

            this.page = page;

            new HTML.Images.FromAssets.wallpaper().ToBackground(Native.Document.body);

            new HTML.Audio.FromAssets.Windows_Logon_Sound().play();




            f.Show();

            taskManagerForm1.Show();

            //content.AttachControlTo(page.Content);
            //content.AutoSizeControlTo(page.ContentSize);
            @"Hello world".ToDocumentTitle();

        }

        private void idleTimer1_UserLostInterest()
        {
            this.ToTrace();
            shadowOverlay1.Show();

        }

        private void idleTimer1_UserShowedInterest()
        {
            this.ToTrace();

            shadowOverlay1.Hide();
        }

        private void TimerForLogout_UserLostInterest()
        {
            this.ToTrace();

            f.AskBeforeDisconnectionSession.Checked = false;
            // tell the server to wait
            new Cookie("FakeLoginScreen.Delay").IntegerValue = 1000;
            Native.Document.location.replace("/FakeLoginScreen");
        }

        private void form11_FormButtonClick()
        {
            MessageBox.Show("hey!!!");
        }

        private void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            page.Taskbar.Orphanize();

            // stop our screensaver
            idleTimer1.Enabled = false;


            if (f.RequestNoContent.Checked)
            {
                new Cookie("FakeLoginScreen.NoContent").BooleanValue = true;
            }

            if (!f.AskBeforeDisconnectionSession.Checked)
                new Cookie("FakeLoginScreen.Delay").IntegerValue = 1000;

            Native.Document.location.replace("/FakeLoginScreen");
        }

        private void applicationClosing1_onbeforeunload(IWindow.Confirmation obj)
        {
            if (f.AskBeforeDisconnectionSession.Checked)
                obj.Text = "arey you sure?";
        }

        private void applicationExitFullscreen1_ExitFullscreen()
        {
            this.ToTrace();

            shadowOverlay1.Show();
        }

        private void applicationExitFullscreen1_EnterFullscreen()
        {
            this.ToTrace();

            shadowOverlay1.Hide();
        }

        private void taskManagerForm1_Load(object sender, EventArgs e)
        {

        }

    }

}
