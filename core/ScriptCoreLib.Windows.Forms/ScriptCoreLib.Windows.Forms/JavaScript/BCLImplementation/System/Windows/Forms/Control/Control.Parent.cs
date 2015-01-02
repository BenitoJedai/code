using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    using DOMHandler = global::System.Action<DOM.IEvent>;
    using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
    using ScriptCoreLib.JavaScript.DOM;

    public partial class __Control : __Component
    {
        #region Parent
        Control _parent;


        internal void InternalAssignParent(Control control)
        {
            _parent = control;

            //BUG: there seems to be a bug loading static field has rerouted implementation


            this.OnParentChanged(null);
        }

        public event EventHandler ParentChanged;

        protected void RaiseParentChanged(EventArgs e)
        {
            //Console.WriteLine("RaiseParentChanged " + new { this.Name, this.Dock });

            if (this.Dock != DockStyle.None)
            {
                // update the docking controls! why? :D
                InternalChildrenAnchorUpdate0();
            }

            if (ParentChanged != null)
                ParentChanged(this, e);
        }

        protected virtual void OnParentChanged(EventArgs e)
        {
            RaiseParentChanged(e);
        }

        public Control Parent
        {
            get
            {
                return this.ParentInternal;
            }
            set
            {
                this.ParentInternal = value;
            }
        }

        internal virtual Control ParentInternal
        {
            get
            {
                return this._parent;
            }
            set
            {
                if (this._parent == value)
                    return;

                if (value == null)
                {
                    this._parent.Controls.Remove(this);
                    return;
                }

                value.Controls.Add(this);
            }
        }






        #endregion

    }
}
