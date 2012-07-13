using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using ScriptCoreLib.Android.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    internal partial class __Control : __Component
    {
        public __Control.__ControlCollection Controls { get; internal set; }


        public __Control()
        {
            this.Controls = new __ControlCollection
            {
                InternalContainer = this
            };
        }

        public virtual void InternalBeforeSetContext(Context c)
        {
        }

        public virtual void InternalAfterSetContext(Context c)
        {

        }
        public void InternalSetContext(Context c)
        {
            InternalBeforeSetContext(c);

            var a = this.Controls.InternalItems;

            for (int i = 0; i < a.size(); i++)
            {
                var x = a.get(i);

                var u = x as __Control;
                if (u != null)
                    u.InternalSetContext(c);
            }

            InternalAfterSetContext(c);

            InternalAttachChildren();
        }

        private void InternalAttachChildren()
        {
            var a = this.Controls.InternalItems;
            var Container = this.InternalGetContainer();

            if (Container != null)
            {
                // while System.Windows.Forms.DockStyle.Top the order changes itself!
                for (int i = a.size(); i > 0; i--)
                {
                    var x = a.get(i - 1);

                    var u = x as __Control;
                    if (u != null)
                    {
                        var Child = u.InternalGetElement();
                        if (Child != null)
                            Container.addView(Child);
                    }
                }
            }
        }

        public virtual View InternalGetElement()
        {
            return null;
        }

        public virtual ViewGroup InternalGetContainer()
        {
            return null;
        }


        public __Control Parent { get; set; }

        public global::System.Drawing.Point Location { get; set; }
        public global::System.Drawing.Size Size { get; set; }

        public string Name { get; set; }

        public bool AutoSize { get; set; }


        public virtual void InternalSetText(string value)
        {

        }

        public virtual string Text
        {
            get
            { return ""; }
            set { InternalSetText(value); }
        }

        public int TabIndex { get; set; }
        public virtual global::System.Windows.Forms.DockStyle Dock { get; set; }

        bool InternalLayoutSuspended;

        public void SuspendLayout()
        {
            InternalLayoutSuspended = true;

        }

        public void ResumeLayout(bool b)
        {
            InternalLayoutSuspended = false;
        }

        public void PerformLayout()
        { }
    }
}
