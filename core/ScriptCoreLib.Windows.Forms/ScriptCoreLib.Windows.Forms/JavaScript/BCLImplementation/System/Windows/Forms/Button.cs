using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;

    [Script(Implements = typeof(global::System.Windows.Forms.Button ))]
    internal class __Button : __ButtonBase
    {
        object __ButtonTypeHint;

        public IHTMLButton HTMLTarget { get; set; }

        public __Button()
        {
            HTMLTarget = new IHTMLButton();
        }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        public override string Text
        {
            get
            {
                return HTMLTarget.innerText;
            }
            set
            {
                HTMLTarget.innerText =  value;
            }
        }

        #region
        static public implicit operator Button(__Button e)
        {
            return (Button)(object)e;
        }

        static public implicit operator __Button(Button e)
        {
            return (__Button)(object)e;
        }
        #endregion

    }
}
