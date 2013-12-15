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

        //        arg[0] is typeof System.Windows.Forms.ScrollEventHandler
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ScrollableControl.add_Scroll(System.Windows.Forms.ScrollEventHandler)]



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
        private bool _AutoScroll;

        public bool AutoScroll
        {
            get { return _AutoScroll; }
            set
            {
                _AutoScroll = value;

                if (value)
                {
                    this.HTMLTargetContainerRef.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.auto;

                    // tested by X:\jsc.svn\examples\javascript\forms\FormsHueControl\FormsHueControl\Library\HueControl.cs
                    this.HTMLTargetContainerRef.onscroll +=
                      delegate
                      {
                          this.HorizontalScroll.Value = this.HTMLTargetContainerRef.scrollLeft;
                      };
                }
                else
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
