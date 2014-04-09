using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using System.Diagnostics;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.FlowLayoutPanel))]
    internal class __FlowLayoutPanel : __Panel
    {
        public Stopwatch ConstructorStopwatch = Stopwatch.StartNew();


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


        public CSSStyleRuleMonkier InternalPadding_css;

        public __FlowLayoutPanel()
        {
            InternalPadding_css = this.HTMLTargetContainerRef.css;

            ((__ControlCollection)this.Controls).InternalAddToTop = false;
            // tested by
            // X:\jsc.svn\examples\javascript\forms\Test\TestLayoutProblems\TestLayoutProblems\ApplicationControl.cs

            this.PaddingChanged +=
                delegate
                {

                    this.InternalPadding_css.style.paddingLeft = this.Padding.Left + "px";
                    this.InternalPadding_css.style.paddingTop = this.Padding.Top + "px";
                    this.InternalPadding_css.style.paddingRight = this.Padding.Right + "px";
                    this.InternalPadding_css.style.paddingBottom = this.Padding.Bottom + "px";
                };

            // 1917ms { Name =  } ControlAdded { Width = 150, Height = 150 }
            // 2599ms { Name =  } ControlAdded { Width = 100, Height = 18 } 

            // it takes 600 seconds to add controls to flow?

            #region ControlAdded
            this.ControlAdded +=
                (s, e) =>
                {
                    //Console.WriteLine(
                    //    new { this.Name, this.Controls.Count }
                    //    + " FlowLayoutPanel ControlAdded "
                    //    + new
                    //    {
                    //        e.Control.Width,
                    //        e.Control.Height,
                    //        e.Control.Name,
                    //        ControlTypeName = e.Control.GetType().Name
                    //    }
                    //);

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
            #endregion


            this.InternalFlowDirectionChanged +=
                delegate
                {
                    foreach (Control item in this.Controls)
                    {
                        ApplyFlowDirection(item);
                    }

                };


        }

        public bool InternalTrackInitializeComponents = true;

        public override void InternalResumeLayout(bool performLayout)
        {
            if (InternalTrackInitializeComponents)
            {
                InternalTrackInitializeComponents = false;

                //var old = new { Console.BackgroundColor };
                //Console.BackgroundColor = ConsoleColor.Yellow;
                //Console.WriteLine("at FlowLayoutPanel InternalResumeLayout "
                //    + new
                //    {
                //        ConstructorStopwatch.ElapsedMilliseconds,
                //        this.Name,
                //        Controls = this.Controls.Count
                //    });
                //Console.BackgroundColor = old.BackgroundColor;
            }
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
