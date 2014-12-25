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

                if (Native.window == null)
                    return;

                if (this.InternalAudio == null)
                {
                    if (this.Audio != null)
                        this.InternalAudio = this.Audio();
                }

                if (this.InternalAudio == null)
                    return;

                this.InternalAudio.load();
            }
        }

        private void InitializeComponent()
        {

        }

        public void Play()
        {
            //this.ToTrace();

            if (this.InternalAudio == null)
            {
                if (this.Audio != null)
                    this.InternalAudio = this.Audio();
            }

            if (this.InternalAudio == null)
                return;

            this.InternalAudio.play();
            this.InternalAudio = this.Audio();
            this.InternalAudio.load();

        }
    }





}
