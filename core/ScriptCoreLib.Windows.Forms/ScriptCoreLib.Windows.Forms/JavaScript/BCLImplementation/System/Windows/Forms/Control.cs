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
    using ScriptCoreLib.JavaScript.Windows.Forms;


    using DOMHandler = Shared.EventHandler<DOM.IEvent>;
	using ScriptCoreLib.JavaScript.BCLImplementation.System.ComponentModel;



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

    [Script(Implements = typeof(global::System.Windows.Forms.Control))]
    internal class __Control  : __Component
    {
		public void InternalSetDefaultFont()
		{
			this.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(186)));
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

        [Script(Implements = typeof(global::System.Windows.Forms.Control.ControlCollection))]
        internal class __ControlCollection : Layout.__ArrangedElementCollection
        {
            readonly Control Owner;

            public __ControlCollection(Control owner)
            {
                this.Owner = owner;
            }

            readonly List<Control> Items = new List<Control>();

            public void Remove(Control e)
            {
                throw new global::System.Exception("Not implemented");
            }

            public void Add(Control e)
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

        Handler<MouseEventHandler, DOMHandler> _MouseMove = new Handler<MouseEventHandler, DOMHandler>();
        Handler<MouseEventHandler, DOMHandler> _MouseDown = new Handler<MouseEventHandler, DOMHandler>();
        Handler<MouseEventHandler, DOMHandler> _MouseUp = new Handler<MouseEventHandler, DOMHandler>();

        #region MouseMove

        public event MouseEventHandler MouseMove
        {
            add
            {
                _MouseMove.Event += value;
                if (_MouseMove)
                {
                    _MouseMove.EventInternal = i =>
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




        DOM.HTML.IHTMLDocument OwnerDocument
        {
            get
            {
                return (DOM.HTML.IHTMLDocument)(object)this.HTMLTargetRef.ownerDocument;
            }
        }

        ScriptCoreLib.Shared.InternalAction _Capture;
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
            if (ControlAdded != null)
                ControlAdded(this, e);
        }


        public virtual bool Enabled
        {
            get { return true; }
            set { }
        }

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

        bool _Visible;

        protected virtual void SetVisibleCore(bool value)
        {
            if (this.GetVisibleCore() != value)
            {
                _Visible = value;

                this.OnVisibleChanged(null /*EventArgs.Empty*/);

            }
        }

        public event EventHandler VisibleChanged;


        protected virtual void OnVisibleChanged(EventArgs e)
        {
            var visible = this.Visible;


            if (VisibleChanged != null)
                VisibleChanged(this, e);

            var c = this.Controls;

            if (c != null)
            {
                for (int i = 0; i < c.Count; i++)
                {
                    __Control v = c[i];

                    if (v.Visible)
                    {
                        v.OnParentVisibleChanged(null /* EventArgs.Empty */ );
                    }

                    if (!visible)
                    {
                        v.OnParentBecameInvisible();
                    }

                }

            }
        }

        protected virtual void OnParentVisibleChanged(EventArgs e)
        {
            /*
            if (this.GetState(2))
            {
                this.OnVisibleChanged(e);
            }
            */
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
