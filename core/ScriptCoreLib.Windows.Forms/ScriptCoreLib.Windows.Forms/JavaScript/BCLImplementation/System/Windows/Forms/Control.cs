using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    using ScriptCoreLib.JavaScript.Drawing;

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
        protected void UpdateStyles()
        {
        }
        protected void SetStyle(ControlStyles flag, bool value)
        {
        }

        public __Control()
        {
            this.Controls = new Control.ControlCollection(this);


        }

        int x;
        int y;
        int width;
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.SetBounds(this.x, this.y, value, this.height, BoundsSpecified.Width);
            }
        }

        int height;
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.SetBounds(this.x, this.y, this.width, value, BoundsSpecified.Height);
            }
        }

        public Size Size
        {
            get
            {
                return new Size(this.width, this.height);
            }
            set
            {
                this.SetBounds(this.x, this.y, value.Width, value.Height, BoundsSpecified.Size);
            }
        }

        public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((specified & BoundsSpecified.X) == BoundsSpecified.None)
            {
                x = this.x;
            }
            if ((specified & BoundsSpecified.Y) == BoundsSpecified.None)
            {
                y = this.y;
            }
            if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
            {
                width = this.width;
            }
            if ((specified & BoundsSpecified.Height) == BoundsSpecified.None)
            {
                height = this.height;
            }

            var _x = (this.x != x);
            var _y = (this.y != y);
            var _width = (this.width != width);
            var _height = this.height != height;

            var _xy = (_x || _y);
            var _wh = (_width || _height);

            if (_xy || _wh)
            {
                UpdateBounds(x, y, width, height);
                //this.SetBoundsCore(x, y, width, height, specified);
                //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
            //else
            //{
            //    // this.InitScaling(specified);
            //}
        }


        protected void UpdateBounds(int x, int y, int width, int height/*, int clientWidth, int clientHeight*/)
        {
            var _x = (this.x != x);
            var _y = (this.y != y);
            var _width = (this.width != width);
            var _height = this.height != height;

            bool flag = _x|| _y;
            bool flag2 = _width ||_height /*|| (this.clientWidth != clientWidth)) || (this.clientHeight != clientHeight)*/;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            //this.clientWidth = clientWidth;
            //this.clientHeight = clientHeight;
            if (flag)
            {
                //this.OnLocationChanged(EventArgs.Empty);
            }
            if (flag2)
            {
                this.OnSizeChanged(null);
                //this.OnClientSizeChanged(EventArgs.Empty);
                //CommonProperties.xClearPreferredSizeCache(this);
                //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
        }

        public event EventHandler Resize;


        protected virtual void OnResize(EventArgs e)
        {
            if (Resize != null)
                Resize(this, null);
        }

        public event EventHandler SizeChanged;

        protected virtual void OnSizeChanged(EventArgs e)
        {
            this.OnResize(null);

            if (SizeChanged != null)
                SizeChanged(this, null);

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


        public int TabIndex { get; set; }
        public bool AutoSize { get; set; }


        #region ForeColor
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
        #endregion

        #region Font
        private Font _Font;

        public Font Font
        {
            get { return _Font; }
            set
            {
                _Font = value;

                this.HTMLTargetRef.style.font = value.ToCssString();
            }
        }
        #endregion

        #region BackColor
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
        #endregion

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
