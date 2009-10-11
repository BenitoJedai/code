using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;
    using ScriptCoreLib.JavaScript.Runtime;
	using ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel;
    
    [Script(Implements = typeof(global::System.Windows.Forms.Timer))]
    internal class __Timer : __Component
    {
        Timer Target;

        public __Timer(IContainer e)
        {
            Target = new Timer();
            Target.Tick += t => this.Tick(this, null);

            _Interval = 100;
        }

        private bool _Enabled;

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value;

                if (value)
                    Target.StartInterval(_Interval);
                else
                    Target.Stop();
            }
        }

        private int _Interval;

        public int Interval
        {
            get { return _Interval; }
            set { _Interval = value; 
                if (_Enabled)
                    Target.StartInterval(_Interval);

            }
        }


        public event EventHandler Tick;
    }
}
