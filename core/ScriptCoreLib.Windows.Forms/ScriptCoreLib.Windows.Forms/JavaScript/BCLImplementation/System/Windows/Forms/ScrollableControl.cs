using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

        Size InternalAutoScrollMinSize;

        public Size AutoScrollMinSize
        {
            get
            {
                return InternalAutoScrollMinSize;
            }
            set
            {
                InternalAutoScrollMinSize = value;

                this.HTMLTargetContainerRef.style.minWidth = value.Width + "px";
                this.HTMLTargetContainerRef.style.minHeight = value.Height + "px";
            }
        }

    }
}
