using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FakeWindowsLoginExperiment.Library
{
    [DefaultEvent("onblur")]
    public class ApplicationLostFocus : Component
    {
        public ApplicationLostFocus()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {

        }

        public event Action<IEvent> onblur
        {
            add
            {
                // design-mode
                if (Native.window == null)
                    return;

                Native.window.onblur += value;
            }
            remove
            {

            }
        }
    }
}
