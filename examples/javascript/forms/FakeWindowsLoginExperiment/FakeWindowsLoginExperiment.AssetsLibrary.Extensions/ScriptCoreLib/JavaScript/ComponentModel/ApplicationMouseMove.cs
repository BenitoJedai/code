using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FakeWindowsLoginExperiment.Library
{
    [DefaultEvent("onmousemove")]
    public class ApplicationMouseMove : Component
    {
        public ApplicationMouseMove()
        {

            InitializeComponent();
        }

        private void InitializeComponent()
        {

        }

        public event Action<IEvent> onmousemove
        {
            add
            {
                // design-mode
                if (Native.Document == null)
                    return;

                Native.Document.onmousemove += value;
            }
            remove
            {

            }
        }
    }
}
