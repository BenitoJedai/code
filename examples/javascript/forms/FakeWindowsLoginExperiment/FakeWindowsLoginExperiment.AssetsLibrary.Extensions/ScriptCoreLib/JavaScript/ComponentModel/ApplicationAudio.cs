using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;

namespace FakeWindowsLoginExperiment.Library
{
    public abstract class ApplicationAudio : Component
    {
        public ApplicationAudio()
        {
            InitializeComponent();
        }

        IHTMLAudio InternalAudio;

        protected Func<IHTMLAudio> Audio { get; set; }

        bool InternalAutoBuffer;
        public bool AutoBuffer
        {
            get { return InternalAutoBuffer; }
            set
            {
                InternalAutoBuffer = true;

                if (Native.Window == null)
                    return;

                Audio().With(
                    a =>
                    {
                        InternalAudio = a;
                        InternalAudio.load();
                    }
                );
            }
        }

        private void InitializeComponent()
        {

        }

        public void Play()
        {
            if (this.InternalAudio != null)
            {
                this.InternalAudio.play();
                return;
            }

            if (this.Audio != null)
            {
                this.Audio().With(
                    a => a.play()
                );
            }
        }
    }





}
