using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;

namespace FakeWindowsLoginExperiment.Library
{
    class ApplicationOverlay : Component
    {
        ScriptCoreLib.Shared.Drawing.IAssemblyReferenceToken ref0;

        public ApplicationOverlay()
        {
            this.OverlayColor = Color.Black;


            InitializeComponent();


        }

        private void InitializeComponent()
        {

        }

        public Color OverlayColor { get; set; }

        HTML.Pages.ShadowOverlay s;


        public ApplicationAudio ShowAudio { get; set; }

        public void Show()
        {
            this.ToTrace();

            if (s != null)
                s.Container.Orphanize();


            s = new HTML.Pages.ShadowOverlay();
            var backgroundColor = this.OverlayColor.ToString();
            s.Container.style.backgroundColor = backgroundColor;

            dynamic style = s.Container.style;
            style.cursor = "none";

            s.Container.AttachToDocument();
            s.Container.onclick +=
                delegate
                {
                    Native.Document.body.requestFullscreen();
                };

            if (ShowAudio != null)
                ShowAudio.Play();
        }

        public ApplicationAudio HideAudio { get; set; }
        public void Hide()
        {
            this.ToTrace();

            if (s != null)
                s.Container.Orphanize();

            if (HideAudio != null)
                HideAudio.Play();
        }
    }
}
