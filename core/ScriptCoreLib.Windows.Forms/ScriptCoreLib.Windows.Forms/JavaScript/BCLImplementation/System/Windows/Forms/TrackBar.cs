using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.TrackBar))]
    internal class __TrackBar : __Control, __ISupportInitialize
    {
        public IHTMLElement HTMLTarget { get; set; }

        public IHTMLInput InternalElement;

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.TrackBar.set_TickStyle(System.Windows.Forms.TickStyle)]
        // X:\jsc.svn\examples\javascript\css\CSSTransform\CSSTransform\Application.cs
        public TickStyle TickStyle { get; set; }

        public __TrackBar()
        {
            // http://stackoverflow.com/questions/18389224/how-to-style-html5-range-input-to-have-different-color-before-and-after-slider

            this.HTMLTarget = new IHTMLDiv
            {

            };



            this.InternalElement = new IHTMLInput
            {
                type = Shared.HTMLInputTypeEnum.range,
                min = 0,
                max = 100
            }.AttachTo(this.HTMLTarget);


            this.InternalElement.style.borderTop = "1px solid rgba(0,0,0,0.4)";
            this.InternalElement.style.borderBottom = "1px solid rgba(255,255,255,0.4)";

            // keep only borders
            this.InternalElement.style.height = "0px";
            this.InternalElement.style.width = "100%";

            this.InternalElement.style.setProperty("-webkit-appearance", "none", "");
            this.InternalElement.style.setProperty("-moz-apperance", "none", "");

            this.Minimum = 0;
            this.Maximum = 10;


            this.InternalElement.onmousemove +=
                e =>
                {
                    if (e.MouseButton == DOM.IEvent.MouseButtonEnum.Left)
                    {
                        InternalRaiseValueChanged();
                    }
                };

            this.InternalElement.onchange +=
                delegate
                {
                    InternalRaiseValueChanged();
                };


            this.Size = new global::System.Drawing.Size(80, 20);
        }

        public event EventHandler Scroll;

        public event EventHandler ValueChanged;
        public int Value
        {
            get { return Convert.ToInt32(this.InternalElement.value); }
            set
            {
                (this.InternalElement as dynamic).value = value;
                InternalRaiseValueChanged();
            }
        }

        private void InternalRaiseValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());

            // X:\jsc.svn\examples\javascript\Test\TestULongShift\TestULongShift\ApplicationControl.cs
            if (Scroll != null)
                Scroll(this, new EventArgs());
        }

        public int Maximum
        {
            get { return this.InternalElement.max; }
            set
            {
                this.InternalElement.max = value;
            }
        }
        public int Minimum
        {
            get
            {
                return this.InternalElement.min;
            }
            set
            {
                this.InternalElement.min = value;
            }
        }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }

}
