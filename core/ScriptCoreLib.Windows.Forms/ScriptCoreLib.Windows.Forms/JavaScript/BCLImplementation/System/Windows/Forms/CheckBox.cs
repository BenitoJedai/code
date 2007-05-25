using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public __CheckBox()
        {
            HTMLTarget = new IHTMLDiv();

            check = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox, "");
            label = new IHTMLLabel("", check);

            HTMLTarget.appendChild(check, label);
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

        private bool myVar;

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
