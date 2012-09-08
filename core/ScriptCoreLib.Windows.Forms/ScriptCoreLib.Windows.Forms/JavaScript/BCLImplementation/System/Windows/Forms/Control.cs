﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Windows.Forms;
using ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel;


namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    using DOMHandler = global::System.Action<DOM.IEvent>;


    #region Handler
    [Script]
    class Handler<A, B>
    {
        public A Event;
        public B EventInternal;

        public static implicit operator bool(Handler<A, B> e)
        {
            if (e.Event == null)
                return false;

            return e.EventInternal == null;
        }

        public void Rewire(Control oldControl, Control newControl)
        {

        }
    }
    #endregion


    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    internal class __Control : __Component
    {
        public static Font DefaultFont
        {
            get
            {
                return new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            }
        }

        public void InternalSetDefaultFont()
        {
            this.Font = DefaultFont;
        }

        public virtual DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return null;
            }
        }

        public virtual DOM.HTML.IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return HTMLTargetRef;
            }
        }

        private Padding padding;
        public Padding Padding
        {
            get { return padding; }
            set { padding = value; }
        }


        #region __ControlCollection
        [Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
        internal class __ControlCollection : Layout.__ArrangedElementCollection
        {
            readonly Control Owner;

            public __ControlCollection(Control owner)
            {
                this.Owner = owner;
            }

            readonly List<Control> Items = new List<Control>();

            public virtual void Remove(Control e)
            {
                throw new global::System.Exception("Not implemented");
            }

            public virtual void Add(Control e)
            {
                Items.Add(e);

                var bg = this.Owner.GetHTMLTargetContainer();

                if (bg.firstChild == null)
                    bg.appendChild(e.GetHTMLTarget());
                else
                    bg.insertBefore(e.GetHTMLTarget(), bg.firstChild);

                var c = (__Control)e;

                c.AssignParent(this.Owner);

                ((__Control)this.Owner).OnControlAdded(new ControlEventArgs(e));

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

            public virtual Control this[int index]
            {
                get
                {
                    if (index < 0)
                        throw new Exception("IndexOutOfRange");

                    if (index >= this.Count)
                        throw new Exception("IndexOutOfRange");

                    return (Control)Items[index];
                }
            }

            public virtual void SetChildIndex(Control child, int newIndex)
            {
                // we should apply the index
            }
        }
        #endregion

        public void PerformLayout()
        {

        }

        bool InternalLayoutSuspended;

        public void SuspendLayout()
        {
            InternalLayoutSuspended = true;

        }

        public void ResumeLayout(bool b)
        {
            InternalLayoutSuspended = false;

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

            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
        }


        protected int x;
        protected int y;

        #region Size
        protected int width;
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

        protected int height;
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
        #endregion

        #region Location
        public int Left
        {
            get
            {
                return this.x;
            }
            set
            {
                this.SetBounds(value, this.y, this.width, this.height, BoundsSpecified.X);
            }
        }

        public int Top
        {
            get
            {
                return this.y;
            }
            set
            {
                this.SetBounds(this.x, value, this.width, this.height, BoundsSpecified.Y);
            }
        }

        public Point Location
        {
            get
            {
                return new Point(this.x, this.y);
            }
            set
            {
                this.SetBounds(value.X, value.Y, this.width, this.height, BoundsSpecified.Location);
            }
        }
        #endregion


        #region SetBounds
        public void SetBounds(int x, int y, int width, int height)
        {
            var _x = (this.x != x);
            var _y = (this.y != y);
            var _width = (this.width != width);
            var _height = this.height != height;

            var _xy = (_x || _y);
            var _wh = (_width || _height);

            if (_xy || _wh)
            {
                UpdateBounds(x, y, width, height);
                //this.SetBoundsCore(x, y, width, height, BoundsSpecified.All);
                //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);
            }
            //else
            //{
            //    this.InitScaling(BoundsSpecified.All);
            //}
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
            // let's remember old size for anchoring..
            var old_width = this.width;
            var old_height = this.height;

            var _x = (this.x != x);
            var _y = (this.y != y);
            var _width = (this.width != width);
            var _height = this.height != height;

            bool flag = _x || _y;
            bool flag2 = _width || _height /*|| (this.clientWidth != clientWidth)) || (this.clientHeight != clientHeight)*/;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            //this.clientWidth = clientWidth;
            //this.clientHeight = clientHeight;
            if (flag)
            {
                this.HTMLTargetRef.style.SetLocation(x, y);


                this.OnLocationChanged(null);
            }
            if (flag2)
            {
                if (this.HTMLTargetRef == null)
                    throw new Exception("Html element not set: " + this.Name);

                this.HTMLTargetRef.style.SetSize(width, height);

                this.OnSizeChanged(null);
                //this.OnClientSizeChanged(EventArgs.Empty);
                //CommonProperties.xClearPreferredSizeCache(this);
                //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);

                if (InternalLayoutSuspended)
                {
                }
                else
                {
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        var item = this.Controls[i];

                        InternalChildrenAnchorUpdate(
                            width,
                            height,
                            old_width,
                            old_height,
                            item
                        );
                    }

                }

            }
        }
        #endregion



        private void InternalChildrenAnchorUpdate(int width, int height, int old_width, int old_height, Control c)
        {

            var IsRight = (c.Anchor & AnchorStyles.Right) == AnchorStyles.Right;
            var IsLeft = (c.Anchor & AnchorStyles.Left) == AnchorStyles.Left;
            var IsBottom = (c.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom;
            var IsTop = (c.Anchor & AnchorStyles.Top) == AnchorStyles.Top;

            var x = width - old_width;
            var y = height - old_height;


            if (IsRight)
            {
                if (IsLeft)
                {
                    c.Width += x;
                }
                else
                {
                    c.Left += x;
                }
            }
            else
            {
                if (IsLeft)
                {
                }
                else
                {
                    c.Left += x / 2;

                }
            }

            if (IsBottom)
            {
                if (IsTop)
                {
                    c.Height += y;
                }
                else
                {
                    c.Top += y;
                }
            }
            else
            {
                if (IsTop)
                {
                }
                else
                {
                    c.Top += y / 2;

                }
            }
        }

        #region Move
        public event EventHandler Move;

        protected void RaiseMove(EventArgs e)
        {
            if (Move != null)
                Move(this, e);
        }

        protected virtual void OnMove(EventArgs e)
        {
            RaiseMove(e);
        }
        #endregion


        #region LocationChanged
        public event EventHandler LocationChanged;

        protected virtual void OnLocationChanged(EventArgs e)
        {
            this.OnMove(null);

            if (LocationChanged != null)
                LocationChanged(this, null);
        }
        #endregion



        public event EventHandler Resize;


        protected virtual void OnResize(EventArgs e)
        {
            if (Resize != null)
                Resize(this, null);
        }

        #region SizeChanged
        public event EventHandler SizeChanged;

        protected virtual void OnSizeChanged(EventArgs e)
        {
            this.OnResize(null);

            if (SizeChanged != null)
                SizeChanged(this, null);

        }
        #endregion

        #region Cursor
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
        #endregion


        public Control.ControlCollection Controls { get; set; }
        public string Name { get; set; }

        #region Text
        string _text;
        public virtual string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnTextChanged(this, new EventArgs());
            }
        }
        #endregion

        public int TabIndex { get; set; }

        public bool AutoSize { get; set; }

        public virtual AnchorStyles Anchor { get; set; }


        #region ForeColor

        public event EventHandler ForeColorChanged;

        private Color _ForeColor;

        public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                _ForeColor = value;
                this.HTMLTargetRef.style.color = value.ToString();

                if (ForeColorChanged != null)
                    ForeColorChanged(this, new EventArgs());
            }
        }
        #endregion

        #region Font


        public event EventHandler FontChanged;

        protected virtual void OnFontChanged(EventArgs e)
        {
            //Console.WriteLine("OnFontChanged");

            if (FontChanged != null)
                FontChanged(this, e);
        }


        private Font _Font;

        public Font Font
        {
            get { return _Font; }
            set
            {
                _Font = value;

                this.HTMLTargetRef.style.font = value.ToCssString();

                OnFontChanged(new EventArgs());
            }
        }
        #endregion

        #region BackColor

        public event EventHandler BackColorChanged;

        private Color _BackColor;

        public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                this.HTMLTargetRef.style.backgroundColor = value.ToString();

                if (BackColorChanged != null)
                    BackColorChanged(this, new EventArgs());
            }
        }
        #endregion

        #region MouseLeave


        EventHandler EventMouseLeave;

        global::System.Action<DOM.IEvent> EventMouseLeaveInternal;

        public event global::System.EventHandler MouseLeave
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
        global::System.EventHandler EventMouseEnter;

        global::System.Action<DOM.IEvent> EventMouseEnterInternal;

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
        protected Handler<EventHandler, DOMHandler> _Click = new Handler<EventHandler, DOMHandler>();

        public event EventHandler Click
        {
            add
            {
                var h = _Click;
                h.Event += value;
                if (!h) return;
                h.EventInternal = i => h.Event(this, null);
                this.HTMLTargetRef.onclick += h.EventInternal;
            }
            remove
            {
                var h = _Click;
                h.Event -= value;
                if (h) return;
                this.HTMLTargetRef.onclick -= h.EventInternal;
                h.EventInternal = null;
            }
        }
        #endregion

        #region Enter
        Handler<EventHandler, DOMHandler> _Enter = new Handler<EventHandler, DOMHandler>();

        public event EventHandler Enter
        {
            add
            {
                var h = _Enter;
                h.Event += value;
                if (!h) return;
                h.EventInternal = i => h.Event(this, null);
                this.HTMLTargetRef.onfocus += h.EventInternal;
            }
            remove
            {
                var h = _Enter;
                h.Event -= value;
                if (h) return;
                this.HTMLTargetRef.onfocus -= h.EventInternal;
                h.EventInternal = null;
            }
        }
        #endregion

        #region Leave
        Handler<EventHandler, DOMHandler> _Leave = new Handler<EventHandler, DOMHandler>();

        public event EventHandler Leave
        {
            add
            {
                var h = _Leave;
                h.Event += value;
                if (!h) return;
                h.EventInternal = i => h.Event(this, null);
                this.HTMLTargetRef.onblur += h.EventInternal;
            }
            remove
            {
                var h = _Leave;
                h.Event -= value;
                if (h) return;
                this.HTMLTargetRef.onblur -= h.EventInternal;
                h.EventInternal = null;
            }
        }

        #endregion

        Handler<MouseEventHandler, DOMHandler> _MouseDown = new Handler<MouseEventHandler, DOMHandler>();
        Handler<MouseEventHandler, DOMHandler> _MouseUp = new Handler<MouseEventHandler, DOMHandler>();

        #region MouseMove
        Handler<MouseEventHandler, DOMHandler> _MouseMove = new Handler<MouseEventHandler, DOMHandler>();
        public event MouseEventHandler MouseMove
        {
            add
            {
                _MouseMove.Event += value;
                if (_MouseMove)
                {
                    _MouseMove.EventInternal =
                        i =>
                        {
                            this._MouseMove.Event(this, i.GetMouseEventHandler(MouseButtons.None));
                        };

                    this.HTMLTargetRef.onmousemove += _MouseMove.EventInternal;
                }
            }
            remove
            {

                _MouseMove.Event -= value;
                if (!_MouseMove)
                {
                    this.HTMLTargetRef.onmousemove -= _MouseMove.EventInternal;
                    _MouseMove.EventInternal = null;
                }
            }
        }
        #endregion

        public event EventHandler TextChanged;

        internal void InternalRaiseTextChanged()
        {
            if (TextChanged != null)
                TextChanged(this, new EventArgs());
        }

        DOM.HTML.IHTMLDocument OwnerDocument
        {
            get
            {
                return (DOM.HTML.IHTMLDocument)(object)this.HTMLTargetRef.ownerDocument;
            }
        }

        global::System.Action _Capture;
        int _CaptureCount;

        #region MouseDown

        public event MouseEventHandler MouseDown
        {
            add
            {
                _MouseDown.Event += value;
                if (_MouseDown)
                {
                    _MouseDown.EventInternal =
                        i =>
                        {
                            if (_MouseUp.Event != null)
                            {
                                if (_CaptureCount == 0)
                                {
                                    _Capture = HTMLTargetRef.CaptureMouse();
                                }

                                _CaptureCount++;

                            }

                            #region workaround

                            //if (_DocumentMouseCounter == 0)
                            //{
                            //    _DocumentMouseMove = j => this._MouseMove.Event(this, j.GetMouseEventHandler(MouseButtons.None));
                            //    _DocumentMouseUp =
                            //        j =>
                            //        {
                            //            Console.WriteLine("c_up: " + _DocumentMouseCounter);


                            //            _DocumentMouseCounter--;

                            //            if (_DocumentMouseCounter == 0)
                            //            {
                            //                var doc_up = OwnerDocument;

                            //                doc_up.onmousemove -= _DocumentMouseMove;
                            //                doc_up.onmouseup -= _DocumentMouseUp;

                            //                _DocumentMouseMove = null;
                            //                _DocumentMouseUp = null;
                            //            }

                            //            this._MouseUp.Event(this, j.GetMouseEventHandler(j.GetMouseButton()));
                            //        };

                            //    var doc = OwnerDocument;

                            //    doc.onmousemove += _DocumentMouseMove;
                            //    doc.onmouseup += _DocumentMouseUp;


                            //}

                            //_DocumentMouseCounter++;

                            //Console.WriteLine("c: " + _DocumentMouseCounter);
                            #endregion

                            // http://blogger.org.cn/blog/more.asp?name=lhwork&id=19173



                            this._MouseDown.Event(this, i.GetMouseEventHandler(i.GetMouseButton()));
                        };

                    this.HTMLTargetRef.onmousedown += _MouseDown.EventInternal;
                }
            }
            remove
            {

                _MouseDown.Event -= value;
                if (!_MouseDown)
                {
                    this.HTMLTargetRef.onmousedown -= _MouseDown.EventInternal;
                    _MouseDown.EventInternal = null;
                }
            }
        }

        #endregion



        #region Mouseup

        public event MouseEventHandler MouseUp
        {
            add
            {
                _MouseUp.Event += value;

                if (_MouseUp)
                {
                    _MouseUp.EventInternal =
                        i =>
                        {
                            Console.WriteLine("mouseup: " + _CaptureCount);

                            this._MouseUp.Event(this, i.GetMouseEventHandler(i.GetMouseButton()));

                            if (_MouseDown.Event != null)
                            {
                                _CaptureCount--;

                                if (_CaptureCount == 0)
                                {
                                    _Capture();
                                    _Capture = null;
                                }

                            }

                        };

                    this.HTMLTargetRef.onmouseup += _MouseUp.EventInternal;
                }

            }
            remove
            {

                _MouseUp.Event -= value;
                if (!_MouseUp)
                {
                    this.HTMLTargetRef.onmouseup -= _MouseUp.EventInternal;
                    _MouseUp.EventInternal = null;
                }

            }
        }

        #endregion

        public event ControlEventHandler ControlAdded;

        static int ControlKeyStatic = 0;
        static Dictionary<object, int> ControlKeys = new Dictionary<object, int>();

        internal string ControlGroupName
        {
            get
            {
                int v = 0;


                if (ControlKeys.ContainsKey(this))
                {
                    v = ControlKeys[this];

                    //Console.WriteLine("old : " + v);
                }
                else
                {
                    v = ControlKeyStatic;

                    //Console.WriteLine("new : " + v);

                    ControlKeys[this] = v;

                    ControlKeyStatic++;
                }

                return "$g" + v;
            }
        }

        protected virtual void OnControlAdded(ControlEventArgs e)
        {
            //Console.WriteLine("__Control OnControlAdded: " + e.Control.Name);

            if (ControlAdded != null)
                ControlAdded(this, e);
        }

        protected void OnTextChanged(object o, EventArgs e)
        {
            if (TextChanged != null)
                TextChanged(this, e);
        }


        public virtual bool Enabled
        {
            get { return true; }
            set { }
        }

        #region operators
        static public implicit operator Control(__Control e)
        {
            return (Control)(object)e;
        }

        static public implicit operator __Control(Control e)
        {
            return (__Control)(object)e;
        }
        #endregion

        #region Parent
        Control _parent;

        internal void AssignParent(Control control)
        {
            _parent = control;

            //BUG: there seems to be a bug loading static field has rerouted implementation

            this.OnParentChanged(null);
        }

        public event EventHandler ParentChanged;

        protected void RaiseParentChanged(EventArgs e)
        {
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
        }

        public event EventHandler VisibleChanged;

        public virtual void InternalBeforeVisibleChanged()
        {

        }


        protected virtual void OnVisibleChanged(EventArgs e)
        {
            InternalVisibileChanged(e);
        }

        private void InternalVisibileChanged(EventArgs e)
        {
            var c = this.Controls;
            var visible = this.Visible;

            //Console.WriteLine(this.Name + " InternalVisibileChanged" + new { visible });

            InternalBeforeVisibleChanged();

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
        }

        protected virtual void OnParentVisibleChanged(EventArgs e)
        {
            InternalVisibileChanged(e);
        }

        internal virtual void OnParentBecameInvisible()
        {


        }

        public Size ClientSize
        {
            get
            {
                return new Size(this.clientWidth, this.clientHeight);
            }
            set
            {
                this.SetClientSizeCore(value);
            }
        }

        private int clientWidth;
        private int clientHeight;



        protected virtual Size SizeFromClientSize(Size clientSize)
        {
            return new Size(clientSize.Width, clientSize.Height);
        }






        protected virtual void SetClientSizeCore(Size s)
        {
            this.Size = this.SizeFromClientSize(s);
            this.clientWidth = x;
            this.clientHeight = y;
            this.OnClientSizeChanged(null /* bug */);
        }

        public event EventHandler ClientSizeChanged;

        protected virtual void OnClientSizeChanged(EventArgs e)
        {
            if (ClientSizeChanged != null)
            {
                ClientSizeChanged(this, e);
            }
        }





    }
}
