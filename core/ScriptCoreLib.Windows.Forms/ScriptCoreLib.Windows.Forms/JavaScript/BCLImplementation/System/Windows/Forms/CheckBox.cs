using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;

    using DOMHandler = Shared.EventHandler<DOM.IEvent>;

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

        IHTMLInput check;
        IHTMLLabel label;



        public __CheckBox()
        {
            HTMLTarget = new IHTMLDiv();
            HTMLTarget.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.nowrap;

            check = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, "");
            label = new IHTMLLabel("", check);

            HTMLTarget.appendChild(check, label);
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
                    HTMLTarget.appendChild(label, check);
                    HTMLTarget.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.right;
                }
                else
                {
                    HTMLTarget.appendChild(check, label);
                    HTMLTarget.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.left;
                }
            }
        }
        #endregion


        public override bool Enabled
        {
            get
            {
                return !check.disabled;
            }
            set
            {
                check.disabled = !value;
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
            get { return check.@checked; }
            set { check.@checked = value; }
        }

        Handler<EventHandler, DOMHandler> _CheckedChanged = new Handler<EventHandler, DOMHandler>();

        #region CheckedChanged

        public event EventHandler CheckedChanged
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

                    this.HTMLTargetRef.onchange += _CheckedChanged.EventInternal;
                }

            }
            remove
            {

                _CheckedChanged.Event -= value;
                if (!_CheckedChanged)
                {
                    this.HTMLTargetRef.onchange -= _CheckedChanged.EventInternal;
                    _CheckedChanged.EventInternal = null;
                }

            }
        }

        #endregion

    }
}
