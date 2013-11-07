﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;

    [Script(Implements = typeof(global::System.Windows.Forms.ComboBox))]
    internal class __ComboBox : __ListControl
    {
        public event EventHandler SelectedIndexChanged;

        public IHTMLSelect HTMLTarget { get; set; }

        public __ComboBox()
        {
            this.HTMLTarget = new IHTMLSelect();
            this.HTMLTarget.onchange +=
                e =>
                {

                    RaiseSelectedIndexChanged();
                };

            this.Items = new __ObjectCollection { Owner = this };

            this.InternalSetDefaultFont();
        }

        private void RaiseSelectedIndexChanged()
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, new EventArgs());
        }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        public override bool Enabled
        {
            get
            {
                return !HTMLTarget.disabled;
            }
            set
            {
                HTMLTarget.disabled = !value;
            }
        }

        public override void InternalSetSelectedIndex(int value)
        {
            this.HTMLTarget.selectedIndex = value;

            RaiseSelectedIndexChanged();
        }

        public override int InternalGetSelectedIndex()
        {
            return this.HTMLTarget.selectedIndex;
        }

        public override string Text
        {
            get
            {
                // IE8 forced us to use this:
                return this.HTMLTarget[this.HTMLTarget.selectedIndex].value;
            }
            set
            {
                this.HTMLTarget.value = value;
            }
        }
        [Script(Implements = typeof(global::System.Windows.Forms.ComboBox.ObjectCollection))]
        internal class __ObjectCollection
        {
            public __ComboBox Owner;

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

        public __ObjectCollection Items { get; protected set; }


        #region
        static public implicit operator ComboBox(__ComboBox e)
        {
            return (ComboBox)(object)e;
        }

        static public implicit operator __ComboBox(ComboBox e)
        {
            return (__ComboBox)(object)e;
        }
        #endregion

        public ComboBoxStyle DropDownStyle { get; set; }
    }
}
