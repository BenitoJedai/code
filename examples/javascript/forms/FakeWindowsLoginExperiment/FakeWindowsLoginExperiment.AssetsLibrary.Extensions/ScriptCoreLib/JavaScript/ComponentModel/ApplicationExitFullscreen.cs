using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FakeWindowsLoginExperiment.Library
{
    [DefaultEvent("ExitFullscreen")]
    public class ApplicationExitFullscreen : Component
    {
        public ApplicationExitFullscreen()
        {

            InitializeComponent();

            if (Native.Window == null)
                return;


            Action frame = null;

            var IsFullscreen = false;

            frame = delegate
            {
                // http://stackoverflow.com/questions/504052/determining-position-of-the-browser-window-in-javascript

                dynamic window = Native.Window;


                int innerHeight = window.innerHeight;

                dynamic screen = window.screen;
                int height = screen.height;

                if (IsFullscreen)
                {
                    if (height != innerHeight)
                    {
                        IsFullscreen = false;

                        if (ExitFullscreen != null)
                            ExitFullscreen();
                    }
                }
                else
                {
                    if (height == innerHeight)
                    {
                        IsFullscreen = true;

                        if (EnterFullscreen != null)
                            EnterFullscreen();
                    }
                }

                Native.Window.requestAnimationFrame += frame;
            };



            Native.Window.requestAnimationFrame += frame;
        }

        private void InitializeComponent()
        {

        }

        public event Action ExitFullscreen;
        public event Action EnterFullscreen;
    }
}
