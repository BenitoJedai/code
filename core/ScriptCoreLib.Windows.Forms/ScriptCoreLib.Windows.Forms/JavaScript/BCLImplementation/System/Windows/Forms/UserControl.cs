using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.DOM.HTML;
    [Script(Implements = typeof(global::System.Windows.Forms.UserControl))]
    internal class __UserControl : __ContainerControl
    {

        public IHTMLDiv HTMLTarget { get; set; }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }


        public __UserControl()
        {
            HTMLTarget = new IHTMLDiv();

            this.Size = new global::System.Drawing.Size(150, 150);
            this.BackColor = SystemColors.ButtonFace;
        }


        #region
        static public implicit operator UserControl(__UserControl e)
        {
            return (UserControl)(object)e;
        }

        static public implicit operator __UserControl(UserControl e)
        {
            return (__UserControl)(object)e;
        }
        #endregion
    }
}
