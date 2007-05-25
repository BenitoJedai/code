using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FormsExample.js
{
    [ScriptCoreLib.Script]
    public partial class TweenComponent : Component
    {
        private double _Value;

        public double Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private TimeSpan _Duration;

        public TimeSpan Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        public TweenComponent()
        {
            InitializeComponent();
        }

        public TweenComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
