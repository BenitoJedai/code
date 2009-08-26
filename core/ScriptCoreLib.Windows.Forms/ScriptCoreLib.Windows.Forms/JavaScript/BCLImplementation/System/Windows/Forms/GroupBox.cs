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

        public __GroupBox()
        {
            fieldset = new IHTMLElement(IHTMLElement.HTMLElementEnum.fieldset);
            legend = new IHTMLElement(IHTMLElement.HTMLElementEnum.legend);

            fieldset.appendChild(legend);

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
    }
}
