using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    [Script(Implements = typeof(global::System.Windows.Forms.Panel))]
    public class __Panel : __ScrollableControl
    {
        public IHTMLDiv InternalElement = typeof(__Panel);


        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }


        public __Panel()
        {
            this.InternalElement.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;

            this.Size = new global::System.Drawing.Size(150, 150);

            // take the one from parent?
            this.BackColor = Color.Transparent;
        }

        #region BorderStyle
        BorderStyle _BorderStyle;
        public BorderStyle BorderStyle
        {
            get
            {
                return this._BorderStyle;
            }
            set
            {
                this._BorderStyle = value;

                this.HTMLTargetRef.ApplyBorderStyle(value);
            }
        }
        #endregion

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
