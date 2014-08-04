using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.DOM.HTML;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/UserControl.cs

    [Script(Implements = typeof(global::System.Windows.Forms.UserControl))]
    internal class __UserControl : __ContainerControl
    {
        public readonly IHTMLDiv InternalElement = typeof(__UserControl);

        [Obsolete("rename to get_InternalElement?")]
        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }


        public __UserControl()
        {
            // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs
            // Are we being inserted to HTML?
            this.outer_style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            this.outer_style.overflow = DOM.IStyle.OverflowEnum.hidden;

            //HTMLTarget.style.border = "1px dotted gray";
            this.InternalSetDefaultFont();

            this.Size = new global::System.Drawing.Size(100, 100);
            this.BackColor = SystemColors.ButtonFace;
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


        #region operators
        static public implicit operator UserControl(__UserControl e)
        {
            return (UserControl)(object)e;
        }

        static public implicit operator __UserControl(UserControl e)
        {
            return (__UserControl)(object)e;
        }
        #endregion

        #region Load
        bool InternalBeforeVisibleChangedDone = false;
        public override void InternalBeforeVisibleChanged(Action yield)
        {
            if (InternalBeforeVisibleChangedDone)
                return;
            InternalBeforeVisibleChangedDone = true;

            InternalRaiseLoad();
            yield();
        }

        public void InternalRaiseLoad()
        {
            if (Load != null)
                Load(this, new EventArgs());
        }

        public event EventHandler Load;
        #endregion

    
    }
}
