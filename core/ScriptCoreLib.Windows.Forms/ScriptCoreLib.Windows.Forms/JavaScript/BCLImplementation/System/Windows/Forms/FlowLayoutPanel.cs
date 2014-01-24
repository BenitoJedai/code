using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.FlowLayoutPanel))]
    internal class __FlowLayoutPanel : __Panel
    {
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.FlowLayoutPanel.SetFlowBreak(System.Windows.Forms.Control, System.Boolean)]
        [Obsolete("nice, extension properties for component designer?")]
        public void SetFlowBreak(Control control, bool value)
        {
        }

        #region FlowDirection
        public event Action InternalFlowDirectionChanged;
        public FlowDirection InternalFlowDirection;
        public FlowDirection FlowDirection
        {
            get { return InternalFlowDirection; }
            set
            {
                this.InternalFlowDirection = value;
                if (InternalFlowDirectionChanged != null)
                    InternalFlowDirectionChanged();

            }
        }
        #endregion


        public bool WrapContents { get; set; }




        public __FlowLayoutPanel()
        {

            this.PaddingChanged +=
                delegate
                {

                    this.HTMLTargetContainerRef.style.paddingLeft = this.Padding.Left + "px";
                    this.HTMLTargetContainerRef.style.paddingTop = this.Padding.Top + "px";
                    this.HTMLTargetContainerRef.style.paddingRight = this.Padding.Right + "px";
                    this.HTMLTargetContainerRef.style.paddingBottom = this.Padding.Bottom + "px";
                };


            this.ControlAdded +=
                (s, e) =>
                {
                    //Console.WriteLine("__FlowLayoutPanel ControlAdded");

                    var x = e.Control.GetHTMLTarget();


                    x.style.left = "";
                    x.style.top = "";

                    // gdluck and goodspeed
                    x.style.position = DOM.IStyle.PositionEnum.relative;

                    ApplyFlowDirection(e.Control);


                    e.Control.MarginChanged +=
                        delegate
                        {
                            x.style.marginLeft = e.Control.Margin.Left + "px";
                            x.style.marginTop = e.Control.Margin.Top + "px";
                            x.style.marginRight = e.Control.Margin.Right + "px";
                            x.style.marginBottom = e.Control.Margin.Bottom + "px";
                        };
                };


            this.InternalFlowDirectionChanged +=
                delegate
                {
                    foreach (Control item in this.Controls)
                    {
                        ApplyFlowDirection(item);
                    }

                };
        }

        private void ApplyFlowDirection(Control item)
        {
            var x = item.GetHTMLTarget();

            // counter productive?
            //x.style.verticalAlign = "top";

            if (FlowDirection == global::System.Windows.Forms.FlowDirection.LeftToRight)
            {
                x.style.display = DOM.IStyle.DisplayEnum.inline_block;

            }
            else
            {
                x.style.display = DOM.IStyle.DisplayEnum.block;
            }
        }
    }
}
