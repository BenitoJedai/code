using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FormsExample.js
{
    
    [ScriptCoreLib.Script]
    public partial class SimpleTweenComponent : Component
    {
        private bool _Enabled;

        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;

                if (!this.DesignMode)
                {
                    this.timer1.Enabled = value;
                }
            }
        }

        private double _Step;

        public double Step
        {
            get { return _Step; }
            set { _Step = value; }
        }

        private double _TargetValue;

        public double TargetValue
        {
            get { return _TargetValue; }
            set { _TargetValue = value; }
        }


        private double _Value;

        public double Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private int _Duration = 1000;

        public int Duration
        {
            get { return _Duration; }
            set { _Duration = value; }
        }

        public SimpleTweenComponent()
        {
            InitializeComponent();
        }

        public SimpleTweenComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
