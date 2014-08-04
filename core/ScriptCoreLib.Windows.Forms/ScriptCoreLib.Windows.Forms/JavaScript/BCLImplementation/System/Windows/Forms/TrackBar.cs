using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/TrackBar.cs

    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.TrackBar))]
    internal class __TrackBar : __Control, __ISupportInitialize
    {
        // tested by X:\jsc.svn\examples\javascript\forms\ChartExperiment\ChartExperiment\ApplicationControl.cs

        // or should we have custom elements instead of class ? whats the difference for us?
        public IHTMLDiv InternalElement = typeof(__TrackBar);

        // how will it work for worker threads?
        static IStyle

            InternalElementStyle = new IStyle(Native.css[typeof(__TrackBar)])
            {
                overflow = IStyle.OverflowEnum.hidden,
            },

            InternalContentElementStyle = new IStyle(Native.css[typeof(__TrackBar)][IHTMLElement.HTMLElementEnum.input])
            {
                borderTop = "1px solid rgba(0,0,0,0.4)",
                borderBottom = "1px solid rgba(255,255,255,0.4)",

                // keep only borders
                height = "0px",
                width = "100%",
            };

        public IHTMLInput InternalContentElement;

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        // X:\jsc.svn\examples\javascript\css\CSSTransform\CSSTransform\Application.cs
        public TickStyle TickStyle { get; set; }

        public __TrackBar()
        {
            // http://stackoverflow.com/questions/18389224/how-to-style-html5-range-input-to-have-different-color-before-and-after-slider




            this.InternalContentElement = new IHTMLInput
            {
                type = Shared.HTMLInputTypeEnum.range,
            }.AttachTo(this.InternalElement);




            this.InternalContentElement.style.setProperty("-webkit-appearance", "none", "");
            this.InternalContentElement.style.setProperty("-moz-apperance", "none", "");

            this.Minimum = 0;
            this.Maximum = 10;


            this.InternalContentElement.onmousemove +=
                e =>
                {
                    if (e.MouseButton == DOM.IEvent.MouseButtonEnum.Left)
                    {
                        InternalRaiseValueChanged();
                    }
                };

            this.InternalContentElement.onchange +=
                delegate
                {
                    InternalRaiseValueChanged();
                };


            this.Size = new global::System.Drawing.Size(80, 20);
        }

        public event EventHandler Scroll;

        #region Value
        public event EventHandler ValueChanged;
        public int Value
        {
            get { return Convert.ToInt32(this.InternalContentElement.value); }
            set
            {
                (this.InternalContentElement as dynamic).value = value;
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
        #endregion


        public int Maximum
        {
            get { return this.InternalContentElement.max; }
            set
            {
                this.InternalContentElement.max = value;
            }
        }
        public int Minimum
        {
            get
            {
                return this.InternalContentElement.min;
            }
            set
            {
                this.InternalContentElement.min = value;
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
