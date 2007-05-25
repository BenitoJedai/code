using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using DOM.HTML;

    [Script(Implements = typeof(global::System.Windows.Forms.Panel))]
    internal class __Panel : __ScrollableControl
    {
        public IHTMLDiv HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }


        public __Panel()
        {
            HTMLTarget = new IHTMLDiv();
            HTMLTarget.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

            this.Size = new global::System.Drawing.Size(150, 150);
            this.BackColor = SystemColors.ButtonFace;
        }


        #region
        static public implicit operator Panel(__Panel e)
        {
            return (Panel)(object)e;
        }

        static public implicit operator __Panel(Panel e)
        {
            return (__Panel)(object)e;
        }
        #endregion
    }
}
