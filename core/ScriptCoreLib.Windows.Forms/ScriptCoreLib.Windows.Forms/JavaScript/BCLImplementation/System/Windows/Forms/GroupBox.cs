using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;
    [Script(Implements = typeof(global::System.Windows.Forms.GroupBox))]
    internal class __GroupBox : __Control
    {
        IHTMLElement fieldset;
        IHTMLElement legend;

		IHTMLDiv InternalContainer;

        public __GroupBox()
        {
            fieldset = new IHTMLElement(IHTMLElement.HTMLElementEnum.fieldset);
			fieldset.style.padding = "0";
			fieldset.style.margin = "0";

            legend = new IHTMLElement(IHTMLElement.HTMLElementEnum.legend);
			legend.style.marginLeft = "0.5em";

            fieldset.appendChild(legend);

			this.InternalContainer = new IHTMLDiv();
			fieldset.appendChild(this.InternalContainer);

			this.InternalSetDefaultFont();
        }

	

        public override string Text
        {
            get
            {
                return legend.innerText;
            }
            set
            {
                legend.innerText = value;
            }
        }

        public bool TabStop { get; set; }

        public override ScriptCoreLib.JavaScript.DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return fieldset;
            }
        }

		public override IHTMLElement HTMLTargetContainerRef
		{
			get
			{
				return this.InternalContainer;
			}
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			
		}

    }
}
