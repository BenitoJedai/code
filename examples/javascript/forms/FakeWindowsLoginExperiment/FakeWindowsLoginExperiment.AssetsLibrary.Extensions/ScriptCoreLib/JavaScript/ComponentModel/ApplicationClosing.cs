using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;

namespace FakeWindowsLoginExperiment.Library
{
    [DefaultEvent("onbeforeunload")]
    public class ApplicationClosing : Component
    {
        public ApplicationClosing()
        {

            InitializeComponent();

            this.SoundChanged +=
                delegate
                {
                    this.Sound.AutoBuffer = true;
                };

            if (Native.window == null)
                return;

            Native.window.onbeforeunload +=
                x =>
                {
                    if (Sound != null)
                        Sound.Play();

                    if (onbeforeunload != null)
                        onbeforeunload(x);

                };


        }

        private void InitializeComponent()
        {

        }

        public event Action SoundChanged;

        ApplicationAudio InternalSound;
        public ApplicationAudio Sound
        {
            get
            { return InternalSound; }
            set
            {
                InternalSound = value;
                if (SoundChanged != null)
                    SoundChanged();
            }
        }

        public event Action<IWindow.Confirmation> onbeforeunload;

    }
}
