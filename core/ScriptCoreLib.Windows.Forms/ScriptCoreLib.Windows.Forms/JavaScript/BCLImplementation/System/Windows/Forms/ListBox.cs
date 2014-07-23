using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Windows.Forms.ListBox))]
    internal class __ListBox : __ListControl
    {
        // tested by?



        public IHTMLDiv HTMLTarget { get; set; }

        public __ListBox()
        {
            //HTMLTarget = new IHTMLSelect();
            //HTMLTarget.multiple = true;

            this.HTMLTarget = new IHTMLDiv();
            this.HTMLTarget.style.backgroundColor = JSColor.System.Window;
            this.HTMLTarget.style.cursor = DOM.IStyle.CursorEnum.@default;
            this.HTMLTarget.style.border = "1px solid gray";
            this.HTMLTarget.style.overflow = DOM.IStyle.OverflowEnum.auto;
            this.HTMLTarget.style.padding = "0.2em";
            this.HTMLTarget.onselectstart +=
                e =>
                {
                    e.PreventDefault();
                };

            Items = new __ObjectCollection { Owner = this };

            this.InternalSetDefaultFont();
        }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        public __ObjectCollection Items { get; private set; }

        [Script(Implements = typeof(global::System.Windows.Forms.ListBox.ObjectCollection))]
        internal class __ObjectCollection
        {
            public __ListBox Owner;

            public int Add(object e)
            {
                var i = new IHTMLDiv { innerText = e.ToString() };

                var IsSelected = false;


                i.onclick +=
                    delegate
                    {
                        IsSelected = !IsSelected;

                        if (IsSelected)
                        {
                            i.style.color = JSColor.System.HighlightText;
                            i.style.backgroundColor = JSColor.System.Highlight;
                        }
                        else
                        {
                            i.style.color = JSColor.None;
                            i.style.backgroundColor = JSColor.None;
                        }
                    };


                Owner.HTMLTarget.Add(i);

                return 0;
            }

            public void Clear()
            {
                while (Owner.HTMLTarget.childNodes.Length > 0)
                    Owner.HTMLTarget.removeChild(Owner.HTMLTarget.firstChild);

            }

            public void AddRange(object[] items)
            {
                foreach (var v in items)
                    Add(v);
            }
        }

        public override bool Enabled
        {
            get
            {
                return true;

                //return !this.HTMLTarget.disabled;
            }
            set
            {
                //this.HTMLTarget.disabled = !value;
            }
        }
    }
}
