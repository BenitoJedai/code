using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FakeWindowsLoginExperiment
{
    public sealed partial class Application : Component
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.idleTimer1 = new FakeWindowsLoginExperiment.Library.ApplicationIdleTimer();
            this.windows_Hardware_Remove1 = new FakeWindowsLoginExperiment.Audio.Windows_Hardware_Remove();
            this.windows_Hardware_Insert1 = new FakeWindowsLoginExperiment.Audio.Windows_Hardware_Insert();
            this.shadowOverlay1 = new FakeWindowsLoginExperiment.Library.ApplicationOverlay();
            this.TimerForLogout = new FakeWindowsLoginExperiment.Library.ApplicationIdleTimer();
            this.f = new FakeWindowsLoginExperiment.Library.Form1();
            this.applicationClosing1 = new FakeWindowsLoginExperiment.Library.ApplicationClosing();
            this.windows_Logoff_Sound1 = new FakeWindowsLoginExperiment.Audio.Windows_Logoff_Sound();
            this.taskManagerForm1 = new FakeWindowsLoginExperiment.Library.TaskManagerForm();
            this.applicationWebService1 = new FakeWindowsLoginExperiment.ApplicationWebService();
            this.rootViewSampleComponent1 = new FakeWindowsLoginExperiment.Library.Designers.Samples.RootViewSampleComponent();
            this.exampleComponent1 = new FakeWindowsLoginExperiment.Library.Designers.Samples.ExampleComponent();
            // 
            // idleTimer1
            // 
            this.idleTimer1.Interval = 5000;
            this.idleTimer1.UserLostInterestSound = null;
            this.idleTimer1.UserShowedInterestSound = null;
            this.idleTimer1.UserLostInterest += new System.Action(this.idleTimer1_UserLostInterest);
            this.idleTimer1.UserShowedInterest += new System.Action(this.idleTimer1_UserShowedInterest);
            // 
            // windows_Hardware_Remove1
            // 
            this.windows_Hardware_Remove1.AutoBuffer = true;
            // 
            // windows_Hardware_Insert1
            // 
            this.windows_Hardware_Insert1.AutoBuffer = true;
            // 
            // shadowOverlay1
            // 
            this.shadowOverlay1.HideAudio = this.windows_Hardware_Insert1;
            this.shadowOverlay1.OverlayColor = System.Drawing.Color.Black;
            this.shadowOverlay1.ShowAudio = this.windows_Hardware_Remove1;
            // 
            // TimerForLogout
            // 
            this.TimerForLogout.Interval = 10000;
            this.TimerForLogout.UserLostInterestSound = null;
            this.TimerForLogout.UserShowedInterestSound = null;
            this.TimerForLogout.UserLostInterest += new System.Action(this.TimerForLogout_UserLostInterest);
            // 
            // f
            // 
            this.f.ClientSize = new System.Drawing.Size(410, 422);
            this.f.Foo = null;
            this.f.Location = new System.Drawing.Point(175, 175);
            this.f.Name = "f";
            this.f.Text = "Form1";
            this.f.Visible = false;
            this.f.FormButtonClick += new System.Action(this.form11_FormButtonClick);
            this.f.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.f_FormClosed);
            // 
            // applicationClosing1
            // 
            this.applicationClosing1.Sound = this.windows_Logoff_Sound1;
            this.applicationClosing1.onbeforeunload += new System.Action<ScriptCoreLib.JavaScript.DOM.IWindow.Confirmation>(this.applicationClosing1_onbeforeunload);
            // 
            // windows_Logoff_Sound1
            // 
            this.windows_Logoff_Sound1.AutoBuffer = true;
            // 
            // taskManagerForm1
            // 
            this.taskManagerForm1.ClientSize = new System.Drawing.Size(528, 529);
            this.taskManagerForm1.Location = new System.Drawing.Point(200, 200);
            this.taskManagerForm1.Name = "taskManagerForm1";
            this.taskManagerForm1.Text = "Windows Task Manager";
            this.taskManagerForm1.Visible = false;
            this.taskManagerForm1.Load += new System.EventHandler(this.taskManagerForm1_Load);

        }

        public Application()
        {
            this.InitializeComponent();
        }

        private Library.ApplicationIdleTimer idleTimer1;
        private Library.ApplicationOverlay shadowOverlay1;
        private Library.ApplicationIdleTimer TimerForLogout;
        private Library.Form1 f;
        private Audio.Windows_Hardware_Remove windows_Hardware_Remove1;
        private Audio.Windows_Hardware_Insert windows_Hardware_Insert1;
        private Library.ApplicationClosing applicationClosing1;
        private Audio.Windows_Logoff_Sound windows_Logoff_Sound1;
        private Library.TaskManagerForm taskManagerForm1;
        private ApplicationWebService applicationWebService1;
        private FakeWindowsLoginExperiment.Library.Designers.Samples.RootViewSampleComponent rootViewSampleComponent1;
        private Library.Designers.Samples.ExampleComponent exampleComponent1;

    }
}
