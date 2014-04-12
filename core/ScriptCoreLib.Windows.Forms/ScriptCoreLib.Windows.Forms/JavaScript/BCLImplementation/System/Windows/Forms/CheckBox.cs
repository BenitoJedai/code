using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;

    using DOMHandler = global::System.Action<DOM.IEvent>;

    [Script(Implements = typeof(global::System.Windows.Forms.CheckBox))]
    internal class __CheckBox : __ButtonBase
    {
        public IHTMLDiv HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        IHTMLInput InternalInputElement;
        IHTMLLabel label;



        public __CheckBox()
        {
            HTMLTarget = new IHTMLDiv();
            HTMLTarget.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;

            InternalInputElement = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, "");
            InternalInputElement.style.margin = "0";
            InternalInputElement.style.verticalAlign = "middle";

            label = new IHTMLLabel("", InternalInputElement);
            label.style.verticalAlign = "middle";
            label.style.marginLeft = "0.5em";

            label.onmousedown +=
                e =>
                {
                    e.PreventDefault();
                };

            this.CheckStateChanged +=
                delegate
                {
                    if (this.ThreeState)
                    {
                        this.InternalInputElement.indeterminate = (this.InternalCheckState == global::System.Windows.Forms.CheckState.Indeterminate);
                    }
                };

            this.InternalInputElement.onchange +=
                e =>
                {
                    // http://shamsmi.blogspot.com/2008/12/tri-state-checkbox-using-javascript.html



                    if (this.ThreeState)
                    {
                        if (this.InternalCheckState == global::System.Windows.Forms.CheckState.Checked)
                        {

                            // the 3rd mode
                            // http://jsfiddle.net/ysangkok/UhQc8/

                            //InternalInputElement.style.Opacity = 0.5;
                            //e.preventDefault();
                            //e.stopPropagation();
                            this.CheckState = global::System.Windows.Forms.CheckState.Indeterminate;
                            return;
                        }

                    }


                    if (this.InternalCheckState == global::System.Windows.Forms.CheckState.Unchecked)
                    {
                        this.CheckState = global::System.Windows.Forms.CheckState.Checked;
                        return;
                    }

                    this.CheckState = global::System.Windows.Forms.CheckState.Unchecked;

                };
            HTMLTarget.appendChild(InternalInputElement, label);

            this.InternalSetDefaultFont();
        }

        #region CheckAlign
        private ContentAlignment _CheckAlign;

        public ContentAlignment CheckAlign
        {
            get { return _CheckAlign; }
            set
            {
                _CheckAlign = value;

                if (_CheckAlign == ContentAlignment.MiddleRight)
                {
                    HTMLTarget.appendChild(label, InternalInputElement);
                    HTMLTarget.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.right;
                }
                else
                {
                    HTMLTarget.appendChild(InternalInputElement, label);
                    HTMLTarget.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.left;
                }
            }
        }
        #endregion


        public override bool Enabled
        {
            get
            {
                return !InternalInputElement.disabled;
            }
            set
            {
                InternalInputElement.disabled = !value;
            }
        }
        public override string Text
        {
            get
            {
                return label.innerText;
            }
            set
            {
                label.innerText = value;
            }
        }


        public bool Checked
        {
            get { return InternalInputElement.@checked; }
            set { InternalInputElement.@checked = value; }
        }

        public CheckState InternalCheckState;

        public CheckState CheckState
        {
            get { return InternalCheckState; }
            set
            {
                this.InternalCheckState = value;

                if (CheckStateChanged != null)
                    CheckStateChanged(this, new EventArgs());

            }
        }



        public bool ThreeState { get; set; }

        // X:\jsc.svn\examples\javascript\forms\test\TestTriState\TestTriState\ApplicationControl.Designer.cs
        public event EventHandler CheckStateChanged;


        #region CheckedChanged
        InternalHandler<global::System.EventHandler, DOMHandler> _CheckedChanged = new InternalHandler<global::System.EventHandler, DOMHandler>();


        public event global::System.EventHandler CheckedChanged
        {
            add
            {
                _CheckedChanged.Event += value;

                if (_CheckedChanged)
                {
                    _CheckedChanged.EventInternal =
                        i =>
                        {
                            this._CheckedChanged.Event(this, null);

                        };

                    this.InternalInputElement.onchange += _CheckedChanged.EventInternal;
                }

            }
            remove
            {

                _CheckedChanged.Event -= value;
                if (!_CheckedChanged)
                {
                    this.InternalInputElement.onchange -= _CheckedChanged.EventInternal;
                    _CheckedChanged.EventInternal = null;
                }

            }
        }

        #endregion

    }
}
