using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FakeWindowsLoginExperiment.Library
{
    [DefaultEvent("onfocus")]
    public class ApplicationGotFocus : Component
    {
        public ApplicationGotFocus()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {

        }

        public event Action<IEvent> onfocus
        {
            add
            {
                // design-mode
                if (Native.window == null)
                    return;

                Native.window.onfocus += value;
            }
            remove
            {

            }
        }
    }
}
