using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace FakeWindowsLoginExperiment.Library
{
    [DefaultEvent("UserLostInterest")]
    public class ApplicationIdleTimer : Timer
    {
        private ApplicationMouseMove documentMouse1;

        public ApplicationIdleTimer()
        {
            InitializeComponent();


        }

        private void InitializeComponent()
        {
            this.documentMouse1 = new FakeWindowsLoginExperiment.Library.ApplicationMouseMove();
            this.applicationGotFocus1 = new FakeWindowsLoginExperiment.Library.ApplicationGotFocus();
            this.applicationLostFocus1 = new FakeWindowsLoginExperiment.Library.ApplicationLostFocus();
            // 
            // documentMouse1
            // 
            this.documentMouse1.onmousemove += new System.Action<ScriptCoreLib.JavaScript.DOM.IEvent>(this.documentMouse1_onmousemove);
            // 
            // applicationGotFocus1
            // 
            this.applicationGotFocus1.onfocus += new System.Action<ScriptCoreLib.JavaScript.DOM.IEvent>(this.applicationGotFocus1_onfocus);
            // 
            // applicationLostFocus1
            // 
            this.applicationLostFocus1.onblur += new System.Action<ScriptCoreLib.JavaScript.DOM.IEvent>(this.applicationLostFocus1_onblur);
            // 
            // ApplicationIdleTimer
            // 
            this.Interval = 5000;
            this.Tick += new System.EventHandler(this.IdleTimer_Tick);

        }

        private ApplicationGotFocus applicationGotFocus1;
        private ApplicationLostFocus applicationLostFocus1;

        bool hasInterest = false;

        // assume focus on first load?
        bool hasFocus = true;
        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            this.Stop();
            hasInterest = false;

            if (!this.Enabled)
                return;

            if (UserLostInterestSound != null)
                UserLostInterestSound.Play();

            if (UserLostInterest != null)
                UserLostInterest();
        }

        public ApplicationAudio UserShowedInterestSound { get; set; }
        public ApplicationAudio UserLostInterestSound { get; set; }

        public event Action UserLostInterest;
        public event Action UserShowedInterest;

        private void documentMouse1_onmousemove(ScriptCoreLib.JavaScript.DOM.IEvent obj)
        {
            if (!this.Enabled)
                return;

            InterestIntent();
        }

        private void InterestIntent()
        {
            if (!hasInterest)
            {
                if (UserShowedInterestSound != null)
                    UserShowedInterestSound.Play();

                if (UserShowedInterest != null)
                    UserShowedInterest();
            }

            hasInterest = true;
            if (hasFocus)
                this.Stop();
            else
                this.Restart();
        }

        private void applicationGotFocus1_onfocus(ScriptCoreLib.JavaScript.DOM.IEvent obj)
        {
            if (!this.Enabled)
                return;

            this.ToTrace();

            hasFocus = true;
            InterestIntent();
        }

        private void applicationLostFocus1_onblur(ScriptCoreLib.JavaScript.DOM.IEvent obj)
        {
            if (!this.Enabled)
                return;

            this.ToTrace();

            hasFocus = false;
            this.Restart();
        }
    }
}
