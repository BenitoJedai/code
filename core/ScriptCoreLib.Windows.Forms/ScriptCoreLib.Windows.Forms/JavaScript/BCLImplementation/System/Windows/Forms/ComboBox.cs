using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;

    [Script(Implements = typeof(global::System.Windows.Forms.ComboBox))]
    internal class __ComboBox : __ListControl
    {
        public event EventHandler SelectedIndexChanged;

        public IHTMLSelect InternalElement;
        public IHTMLDiv InternalContainer;
        public IHTMLDiv InternalShadow;


        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalContainer;
            }
        }

        //public override IHTMLElement HTMLTargetContainerRef
        //{
        //    get
        //    {
        //        return InternalContainer;
        //    }
        //}

        public __ComboBox()
        {
            //Changes made due to select after not working
            //http://stackoverflow.com/questions/3532649/problem-with-select-and-after-with-css-in-webkit

            //Size comes from Control size
            #region InternalContainer
            this.InternalContainer = new IHTMLDiv();

            this.InternalContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            this.InternalContainer.name = "CSSComboBox";

            this.InternalContainer.style.left = "0px";
            this.InternalContainer.style.top = "0px";
            #endregion

            #region InternalContainer
            this.InternalShadow = new IHTMLDiv();
            

            this.InternalShadow.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            this.InternalShadow.name = "CSSComboBoxAfter";

            this.InternalShadow.style.left = "0px";
            this.InternalShadow.style.top = "0px";
            this.InternalShadow.style.right = "0px";
            this.InternalShadow.style.bottom = "0px";
            #endregion

            this.InternalShadow.AttachTo(InternalContainer);


            this.InternalElement = new IHTMLSelect().AttachTo(InternalContainer);
            this.InternalElement.style.left = "0px";
            this.InternalElement.style.top = "0px";
            this.InternalElement.style.width = "100%";
            this.InternalElement.style.height = "100%";
            this.InternalElement.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;




            this.InternalElement.onchange +=
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


        public override bool Enabled
        {
            get
            {
                return !InternalElement.disabled;
            }
            set
            {
                InternalElement.disabled = !value;
            }
        }

        public override void InternalSetSelectedIndex(int value)
        {
            this.InternalElement.selectedIndex = value;

            RaiseSelectedIndexChanged();
        }

        public override int InternalGetSelectedIndex()
        {
            return this.InternalElement.selectedIndex;
        }

        public override string Text
        {
            get
            {
                // IE8 forced us to use this:
                return this.InternalElement[this.InternalElement.selectedIndex].value;
            }
            set
            {
                this.InternalElement.value = value;
            }
        }
        [Script(Implements = typeof(global::System.Windows.Forms.ComboBox.ObjectCollection))]
        internal class __ObjectCollection
        {
            public __ComboBox Owner;

            public int Add(object e)
            {
                Owner.InternalElement.Add(e.ToString());

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
