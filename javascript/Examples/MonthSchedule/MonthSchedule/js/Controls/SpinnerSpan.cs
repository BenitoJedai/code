using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;

namespace MonthSchedule.js.Controls
{
    [Script]
    public class SpinnerSpan<T>
    {
        [Script]
        public class SpinnerSpanSettings
        {
            public Func<T, string> GetText;
            public Action Changed;
            public T Value;
            public Func<T, T> GetPrevious;
            public Func<T, T> GetNext;

            public SpinnerSpan<T> Create()
            {
                return new SpinnerSpan<T>(this);
            }
        }

        public readonly SpinnerSpanSettings Settings;

        public readonly IHTMLSpan Control;
        public readonly ITextNode TextNode;

        // compiler bug: field init wrongly emitted at ctor
        T _Value/* = default(T)*/;
        public T Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                this.TextNode.nodeValue = Text;

                if (!Silent)
                    if (this.Settings.Changed != null)
                        this.Settings.Changed();
            }
        }

        public bool Silent;

        public string Text
        {
            get
            {
                return this.Settings.GetText(_Value);
            }
        }

        public readonly IHTMLAnchor GoPreviousControl;
        public readonly IHTMLAnchor GoNextControl;

        public SpinnerSpan(SpinnerSpanSettings i)
        {

            Control = new IHTMLSpan();
            TextNode = new ITextNode();
            GoPreviousControl = new IHTMLAnchor("") { innerHTML = "&laquo;" };
            GoNextControl = new IHTMLAnchor("") { innerHTML = "&raquo;" };

            GoPreviousControl.style.margin = "1em";
            GoNextControl.style.margin = "1em";

            Settings = i;
            Value = i.Value;

            GoPreviousControl.AttachTo(Control);
            TextNode.AttachTo(Control);
            GoNextControl.AttachTo(Control);

            GoPreviousControl.onclick +=
                ev =>
                {
                    ev.PreventDefault();

                    GoPrevious();
                };

            GoNextControl.onclick +=
                ev =>
                {
                    ev.PreventDefault();

                    GoNext();

                };

            this.Control.onmousewheel +=
                ev =>
                {
                    if (ev.WheelDirection > 0)
                        GoNext();
                    else
                        GoPrevious();


                };

        }

        public void GoNext()
        {
            this.Value = this.Settings.GetNext(this.Value);
        }

        public void GoPrevious()
        {
            this.Value = this.Settings.GetPrevious(this.Value);
        }
    }
}
