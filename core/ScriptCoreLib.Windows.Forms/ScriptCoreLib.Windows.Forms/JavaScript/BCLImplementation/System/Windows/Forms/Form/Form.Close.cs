using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    public partial class __Form
    {
        #region Close
        public void Close()
        {
            InternalClose();
        }

        public event Action<FormClosingEventArgs> InternalBeforeFormClosing;

        public void InternalClose(CloseReason reason = CloseReason.None)
        {
            var a = new FormClosingEventArgs(reason, false);

            if (InternalBeforeFormClosing != null)
                InternalBeforeFormClosing(a);

            if (a.Cancel)
                return;

            if (FormClosing != null)
                FormClosing(this, a);

            if (a.Cancel)
                return;

            foreach (var item in this.OwnedForms)
            {
                item.Close();
            }

            this.Owner = null;

            // why go normal?
            // conflict tested by
            // X:\jsc.svn\examples\javascript\InlinePageActionButtonExperiment\InlinePageActionButtonExperiment\Application.cs
            //this.WindowState = FormWindowState.Normal;


            //-webkit-transition: -webkit-transform 200ms linear; transition: -webkit-transform 200ms linear;
            // -webkit-filter: opacity(0.8); 
            // -webkit-transform: scale(0.7);


            #region fadeout
            this.outer_css.style.transition = "none";
            (this.outer_css.style as dynamic).webkitFilter = " opacity(1.0)";
            this.outer_css.style.transform = " scale(1.0)";

            this.outer_css.style.transition = "-webkit-transform 50ms linear, -webkit-filter 50ms linear";

            (this.outer_css.style as dynamic).webkitFilter = " opacity(0.0)";
            this.outer_css.style.transform = " scale(0.95)";

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {

                    HTMLTarget.Orphanize();

                    this.outer_css.style.transition = "none";

                    (this.outer_css.style as dynamic).webkitFilter = "";
                    this.outer_css.style.transform = "";
                }
            ).StartTimeout(100);
            #endregion


            // allow to be showed again?!
            InternalBeforeVisibleChangedDone = false;


            if (this.Closed != null)
                this.Closed(this, new EventArgs());

            RaiseFormClosed();

        }

        public void RaiseFormClosed()
        {
            if (this.FormClosed != null)
                this.FormClosed(this, new FormClosedEventArgs(CloseReason.None));

        }
        #endregion
    }


}
