using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.ScrollableControl))]
    public class __ScrollableControl : __Control
    {
        public HScrollProperties HorizontalScroll { get; set; }
        public VScrollProperties VerticalScroll { get; set; }

        // ScrollableControl.get_VerticalScroll

        public __ScrollableControl()
        {
            this.HorizontalScroll = (HScrollProperties)(object)new __HScrollProperties();
            this.VerticalScroll = (VScrollProperties)(object)new __VScrollProperties();
        }


        public event ScrollEventHandler Scroll
        {
            add
            {
                this.HTMLTargetContainerRef.onscroll +=
                    e =>
                    {
                        var args = (ScrollEventArgs)(object)new __ScrollEventArgs();

                        value(this, args);
                    };

            }

            remove
            {

            }
        }

        #region AutoScroll
        private bool InternalAutoScroll;

        public virtual bool AutoScroll
        {
            get { return InternalAutoScroll; }
            set
            {
                // idsabled in for
                // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\ToolStrip\ToolStripContentPanel.cs


                InternalAutoScroll = value;

                Console.WriteLine(new { this.Name, InternalAutoScroll });


                if (value)
                {
                    this.HTMLTargetContainerRef.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;

                    // tested by X:\jsc.svn\examples\javascript\forms\FormsHueControl\FormsHueControl\Library\HueControl.cs
                    this.HTMLTargetContainerRef.onscroll +=
                      delegate
                      {
                          this.HorizontalScroll.Value = this.HTMLTargetContainerRef.scrollLeft;
                      };

                    return;
                }

                this.HTMLTargetContainerRef.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

            }
        }
        #endregion

        #region AutoScrollMinSize
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
        #endregion


    }
}
