using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.TextBlock))]
    internal class __TextBlock : __FrameworkElement
	{
        public IHTMLDiv InternalContainer;
        public IHTMLLabel InternalElement;

        public override IHTMLElement InternalGetDisplayObject()
        {
            return this.InternalContainer;
        }

        public __TextBlock()
        {
             this.InternalContainer = new IHTMLDiv();

            this.InternalContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            this.InternalContainer.name = "__TextBlock";

            this.InternalContainer.style.left = "0px";
            this.InternalContainer.style.top = "0px";


            this.InternalElement = new IHTMLLabel();
            this.InternalElement.AttachTo(this.InternalContainer);
        }

        public string Text
        {
            get
            {
                return this.InternalElement.innerText;
            }
            set
            {
                this.InternalElement.innerText = value;
            }
        }
	}
}
