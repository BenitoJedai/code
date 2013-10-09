using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{


    [Script(Implements = typeof(global::System.Windows.Forms.Timer))]
    internal class __Timer : __Component
    {
        Timer Target;

        public __Timer()
            : this(null)
        {
            //Console.WriteLine("__Timer.ctor");
        }

        public __Timer(IContainer e)
        {
            //Console.WriteLine("__Timer.ctor IContainer");


            Target = new Timer();
            Target.Tick += t =>
                {
                    //Console.WriteLine("Target.Tick");

                    if (this.Tick != null)
                        this.Tick(this, null);
                };

            _Interval = 100;
        }

        private bool _Enabled;

        public bool Enabled
        {
            get { return _Enabled; }
            set
            {
                _Enabled = value;

                if (value)
                {
                    //Console.WriteLine("__Timer.StartInterval");

                    Target.StartInterval(_Interval);
                }
                else
                    Target.Stop();
            }
        }


        public void Start()
        {
            this.Enabled = true;
        }

        public void Stop()
        {
            this.Enabled = false;
        }


        private int _Interval;

        public int Interval
        {
            get { return _Interval; }
            set
            {
                _Interval = value;
                if (_Enabled)
                    Target.StartInterval(_Interval);

            }
        }


        public event EventHandler Tick;
    }
}
