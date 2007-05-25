using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;

    [Script(Implements = typeof(global::System.Windows.Forms.Label))]
    internal class __Label : __Control
    {
        public IHTMLLabel HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }


        public __Label()
        {
            HTMLTarget = new IHTMLLabel();

            this.Size = new global::System.Drawing.Size(100, 20);
            
        }

        public override string Text
        {
            get
            {
                return HTMLTarget.innerText;
            }
            set
            {
                HTMLTarget.innerText = value;
            }
        }
        #region
        static public implicit operator Label(__Label e)
        {
            return (Label)(object)e;
        }

        static public implicit operator __Label(Label e)
        {
            return (__Label)(object)e;
        }
        #endregion
    }
}
