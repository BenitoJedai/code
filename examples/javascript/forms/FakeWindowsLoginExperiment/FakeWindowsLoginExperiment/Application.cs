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

        //02000030 ScriptCoreLib.Library.StringConversions
        //02000032 FakeWindowsLoginExperiment.Library.Designers.Samples.RootDesignedComponent
        //02000031 FakeWindowsLoginExperiment.Library.Designers.Samples.RootViewSampleComponent
        //02000033 FakeWindowsLoginExperiment.Library.Designers.SampleRootDesigner
        //02000034 FakeWindowsLoginExperiment.Library.ScriptResourceWriter
        //no implementation for System.Reflection.Emit.AssemblyBuilder 0814be2a-48e5-3d61-90f3-ef3d05df9d5e
        //script: error JSC1000: No implementation found for this native method, please implement [System.Reflection.Emit.AssemblyBuilder.SetCustomAttribute(System.Reflection.Emit.CustomAttributeBuilder)]


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
            FormStyler.AtFormCreated = x =>
                {
                    FormStylerLikeAero.LikeAero(x);


                };

            InitializeComponent();

            this.page = page;

            new HTML.Images.FromAssets.wallpaper().ToBackground(Native.Document.body);

            new HTML.Audio.FromAssets.Windows_Logon_Sound().play();




            f.Show();

            //Error	2	The call is ambiguous between the following methods or properties: 'ScriptCoreLib.JavaScript.Windows.Forms.FormAsPopupExtensions.PopupInsteadOfClosing<System.Windows.Forms.Form>(System.Windows.Forms.Form)' and 'ScriptCoreLib.Extensions.FormAsPopupExtensionsForConsoleFormPackage.PopupInsteadOfClosing<System.Windows.Forms.Form>(System.Windows.Forms.Form)'	X:\jsc.svn\examples\javascript\forms\FakeWindowsLoginExperiment\FakeWindowsLoginExperiment\Application.cs	47	21	FakeWindowsLoginExperiment

            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensionsForConsoleFormPackage.PopupInsteadOfClosing(
            //ScriptCoreLib.Extensions.FormAsPopupExtensionsForConsoleFormPackage.PopupInsteadOfClosing(
                taskManagerForm1
                );

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
