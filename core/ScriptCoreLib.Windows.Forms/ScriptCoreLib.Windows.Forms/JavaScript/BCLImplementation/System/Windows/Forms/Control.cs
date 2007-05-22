using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    internal class __Control // : ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel.__Component
    {
        public virtual DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return null;
            }
        }

        [Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
        internal class __ControlCollection : Layout.__ArrangedElementCollection
        {
            readonly Control Owner;

            public __ControlCollection(Control owner)
            {
                this.Owner = owner;
            }

            readonly List<Control> Items = new List<Control>();

            public void Add(Control e)
            {
                Items.Add(e);
            }

            public override IEnumerator GetEnumerator()
            {
                return Items.GetEnumerator();
            }

            public override int Count
            {
                get
                {
                    return Items.Count;
                }
            }
        }

        public void PerformLayout()
        {

        }
        public void SuspendLayout()
        {

        }
        public void ResumeLayout(bool b)
        {

        }

        public __Control()
        {
            this.Controls = new Control.ControlCollection(this);


        }

        private __Cursor _Cursor;

        public Cursor Cursor
        {
            get { return _Cursor; }
            set
            {
                _Cursor = value;

                this.HTMLTargetRef.style.cursor = _Cursor.Value;
            }
        }


        public Control.ControlCollection Controls { get; set; }
        public Point Location { get; set; }
        public string Name { get; set; }
        public virtual string Text { get; set; }
        public Size Size { get; set; }
        public int TabIndex { get; set; }
        public bool AutoSize { get; set; }



        private Color _ForeColor;

        public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                _ForeColor = value;
                this.HTMLTargetRef.style.color = value.ToString();
            }
        }

        private Color _BackColor;

        public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                this.HTMLTargetRef.style.backgroundColor = value.ToString();
            }
        }

        #region MouseLeave


        EventHandler EventMouseLeave;

        Shared.EventHandler<DOM.IEvent> EventMouseLeaveInternal;

        public event EventHandler MouseLeave
        {
            add
            {
                EventMouseLeave += value;

                if (EventMouseLeave != null && EventMouseLeaveInternal == null)
                {
                    EventMouseLeaveInternal = i => this.EventMouseLeave(this, null);

                    this.HTMLTargetRef.onmouseout += EventMouseLeaveInternal;
                }
            }
            remove
            {
                EventMouseLeave -= value;


                if (EventMouseLeave == null && EventMouseLeaveInternal != null)
                {
                    this.HTMLTargetRef.onmouseout -= EventMouseLeaveInternal;

                    EventMouseLeaveInternal = null;
                }
            }
        }
        #endregion

        #region MouseEnter
        EventHandler EventMouseEnter;

        Shared.EventHandler<DOM.IEvent> EventMouseEnterInternal;

        public event EventHandler MouseEnter
        {
            add
            {
                EventMouseEnter += value;

                if (EventMouseEnter != null && EventMouseEnterInternal == null)
                {
                    EventMouseEnterInternal = i => this.EventMouseEnter(this, null);

                    this.HTMLTargetRef.onmouseover += EventMouseEnterInternal;
                }
            }
            remove
            {
                EventMouseEnter -= value;


                if (EventMouseEnter == null && EventMouseEnterInternal != null)
                {
                    this.HTMLTargetRef.onmouseover -= EventMouseEnterInternal;

                    EventMouseEnterInternal = null;
                }
            }
        }
        #endregion

        #region Click
        EventHandler EventClick;

        Shared.EventHandler<DOM.IEvent> EventClickInternal;

        public event EventHandler Click
        {
            add
            {
                EventClick += value;

                if (EventClick != null && EventClickInternal == null)
                {
                    EventClickInternal = i => this.EventClick(this, null);

                    this.HTMLTargetRef.onclick += EventClickInternal;
                }
            }
            remove
            {
                EventClick -= value;


                if (EventClick == null && EventClickInternal != null)
                {
                    this.HTMLTargetRef.onclick -= EventClickInternal;

                    EventClickInternal = null;
                }
            }
        }
        #endregion

        #region
        static public implicit operator Control(__Control e)
        {
            return (Control)(object)e;
        }

        static public implicit operator __Control(Control e)
        {
            return (__Control)(object)e;
        }
        #endregion

    }
}
