﻿using System;
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
        #region VisibleChanged

        public void Show()
        {
            this.Visible = true;
        }

        public void Hide()
        {
            this.Visible = false;
        }

        public bool Visible
        {
            get
            {
                return this.GetVisibleCore();
            }
            set
            {
                this.SetVisibleCore(value);
            }
        }


        internal virtual bool GetVisibleCore()
        {
            if (!this._Visible)
                return false;

            if (this.ParentInternal == null)
                return false;


            return ((__Control)this.ParentInternal).GetVisibleCore();
        }

        bool _VisibleUndefined = true;
        bool _Visible = true;

        protected virtual void SetVisibleCore(bool value)
        {
            if (_VisibleUndefined || (this.GetVisibleCore() != value))
            {
                _VisibleUndefined = false;
                _Visible = value;

                this.OnVisibleChanged(null /*EventArgs.Empty*/);

            }

            this.HTMLTargetRef.Show(value);
        }

        public event EventHandler VisibleChanged;

        public event Action InternalAtAfterVisibleChanged;

        public virtual void InternalBeforeVisibleChanged(Action yield)
        {
            yield();
        }


        protected virtual void OnVisibleChanged(EventArgs e)
        {
            InternalVisibileChanged(e);

        }

        public void InternalVisibileChanged(EventArgs e)
        {
            var c = this.Controls;
            var visible = this.Visible;

            //Console.WriteLine(this.Name + " InternalVisibileChanged" + new { visible });

            InternalBeforeVisibleChanged(
                delegate
                {

                    if (VisibleChanged != null)
                        VisibleChanged(this, e);


                    if (c != null)
                    {
                        //Console.WriteLine(this.Name + " InternalVisibileChanged" + new { visible, c.Count });

                        for (int i = 0; i < c.Count; i++)
                        {
                            __Control v = c[i];

                            //Console.WriteLine(this.Name + " InternalVisibileChanged " + new { visible, v.Visible, v.Name });


                            //if (v.Visible)
                            //{
                            v.OnParentVisibleChanged(null /* EventArgs.Empty */ );
                            //}

                            if (!visible)
                            {
                                v.OnParentBecameInvisible();
                            }

                        }

                    }

                    InternalInitializeContextMenuStrip();

                    if (InternalAtAfterVisibleChanged != null)
                        InternalAtAfterVisibleChanged();


                }
             );


        }

        protected virtual void OnParentVisibleChanged(EventArgs e)
        {
            InternalVisibileChanged(e);
        }

        internal virtual void OnParentBecameInvisible()
        {


        }
        #endregion

    }
}
