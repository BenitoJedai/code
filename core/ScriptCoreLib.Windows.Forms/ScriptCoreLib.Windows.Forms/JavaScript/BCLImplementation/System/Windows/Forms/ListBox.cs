using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;

    [Script(Implements = typeof(global::System.Windows.Forms.ListBox))]
    internal class __ListBox : __ListControl
    {
        public IHTMLSelect HTMLTarget { get; set; }

        public __ListBox()
        {
            HTMLTarget = new IHTMLSelect();
            HTMLTarget.multiple = true;

            Items = new __ObjectCollection { Owner = this };
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
                Owner.HTMLTarget.Add(e.ToString());

                return 0;
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
                return !this.HTMLTarget.disabled;
            }
            set
            {
                this.HTMLTarget.disabled = !value;
            }
        }
    }
}
