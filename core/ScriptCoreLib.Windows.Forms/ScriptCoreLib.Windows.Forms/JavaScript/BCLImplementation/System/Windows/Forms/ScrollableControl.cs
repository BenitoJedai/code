using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ScrollableControl))]
    internal class __ScrollableControl : __Control
    {

        private bool _AutoScroll;

        public bool AutoScroll
        {
            get { return _AutoScroll; }
            set
            {
                _AutoScroll = value;

                if (value)
                    this.HTMLTargetRef.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;
                else
                    this.HTMLTargetRef.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

            }
        }


    }
}
