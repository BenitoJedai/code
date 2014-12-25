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

            if (Native.window == null)
                return;


            Action frame = null;

            var IsFullscreen = false;

            frame = delegate
            {
                // http://stackoverflow.com/questions/504052/determining-position-of-the-browser-window-in-javascript

                dynamic window = Native.window;


                int innerHeight = window.innerHeight;
                int outerHeight = window.outerHeight;

                dynamic screen = window.screen;
                int height = screen.height;

                if (IsFullscreen)
                {
                    if (outerHeight != innerHeight)
                    {
                        IsFullscreen = false;

                        if (ExitFullscreen != null)
                            ExitFullscreen();
                    }
                }
                else
                {
                    if (outerHeight == innerHeight)
                    {
                        IsFullscreen = true;

                        if (EnterFullscreen != null)
                            EnterFullscreen();
                    }
                }

                Native.window.requestAnimationFrame += frame;
            };



            Native.window.requestAnimationFrame += frame;
        }

        private void InitializeComponent()
        {

        }

        public event Action ExitFullscreen;
        public event Action EnterFullscreen;
    }
}
