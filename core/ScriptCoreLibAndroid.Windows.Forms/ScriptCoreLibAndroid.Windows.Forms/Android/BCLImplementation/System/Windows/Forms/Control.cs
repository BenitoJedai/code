using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using android.content;
using android.view;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    public partial class __Control : __Component
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Control\Control.cs

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
        public int Width { get; set; }
        public int Height { get; set; }

        public bool AutoSize { get; set; }



        public virtual string InternalGetText()
        {
            return "";
        }

        public virtual void InternalSetText(string value)
        {

        }

        public virtual string Text
        {
            get { return InternalGetText(); }
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

        [Script]
        class InternalClickHandler : View.OnClickListener
        {
            public __Control that;
            public EventHandler value;

            public void onClick(View v)
            {
                value(that, new EventArgs());
            }
        }

        EventHandler InternalClickPending;

        public void InternalAddClick()
        {
            if (InternalClickPending == null)
                return;

            var a = this.InternalGetElement();

            if (a == null)
                return;

            a.setOnClickListener(
                new InternalClickHandler { that = this, value = InternalClickPending }
            );
        }

        public event EventHandler Click
        {
            add
            {
                InternalClickPending = value;
                InternalAddClick();
            }
            remove { }
        }


        #region operators
        static public implicit operator global::System.Windows.Forms.Control(__Control e)
        {
            return (global::System.Windows.Forms.Control)(object)e;
        }

        static public implicit operator __Control(global::System.Windows.Forms.Control e)
        {
            return (__Control)(object)e;
        }
        #endregion
    }
}
