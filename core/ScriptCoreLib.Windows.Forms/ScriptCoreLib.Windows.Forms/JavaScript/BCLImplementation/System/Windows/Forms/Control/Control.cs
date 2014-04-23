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

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{

    using DOMHandler = global::System.Action<DOM.IEvent>;
    using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;
    using ScriptCoreLib.JavaScript.DOM;



    #region Handler
    [Script]
    public class InternalHandler<A, B>
    {
        public A Event;
        public B EventInternal;

        public static implicit operator bool(InternalHandler<A, B> e)
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
    public partial class __Control : __Component, __IBindableComponent
    {
        // X:\jsc.svn\core\ScriptCoreLibJava.Windows.Forms\ScriptCoreLibJava.Windows.Forms\BCLImplementation\System\Windows\Forms\Control.cs

        #region name
        public string InternalName;
        public string Name { get { return InternalName; } set { InternalName = value; if (InternalNameChanged != null)InternalNameChanged(); } }

        public event Action InternalNameChanged;
        #endregion

        [Obsolete("css")]
        public IStyle outer_style
        {
            get { return this.HTMLTargetRef.style; }
        }

        public bool Capture { get; set; }


        public virtual AnchorStyles Anchor { get; set; }


        public object Invoke(Delegate method)
        {
            // or are we being called by a webworker?
            // if so we have to find ourself on the other side, DOM world and reinvoke the delegate.

            // multithreading!
            ((Action)method)();

            return null;
        }


        [Obsolete("InternalElement")]
        public virtual DOM.HTML.IHTMLElement HTMLTargetRef
        {
            get
            {
                return null;
            }
        }

        [Obsolete("InternalElementContentContainer")]
        public virtual DOM.HTML.IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return HTMLTargetRef;
            }
        }


        //System.Windows.Forms.Control.set_Margin(System.Windows.Forms.Padding)]

        #region Margin
        public event EventHandler MarginChanged;

        public Padding InternalMargin;
        public Padding Margin
        {
            get
            {
                return InternalMargin;
            }
            set
            {
                this.InternalMargin = value;
                if (MarginChanged != null)
                    MarginChanged(this, new EventArgs());

            }
        }
        #endregion



        #region Padding
        public event EventHandler PaddingChanged;

        private Padding InternalPadding;
        public Padding Padding
        {
            get { return InternalPadding; }
            set
            {
                InternalPadding = value;

                if (PaddingChanged != null)
                    PaddingChanged(this, new EventArgs());

            }
        }
        #endregion





        protected void UpdateStyles()
        {
        }
        protected void SetStyle(ControlStyles flag, bool value)
        {
        }



        public static bool InternalDisableSelection = true;


        bool InternalInitializeContextMenuStripOnce = false;
        public void InternalInitializeContextMenuStrip()
        {
            if (InternalInitializeContextMenuStripOnce)
                return;

            InternalInitializeContextMenuStripOnce = true;

            if (InternalDisableSelection)
            {
                this.HTMLTargetRef.onselectstart +=
                    ev =>
                    {
                        ev.stopPropagation();

                        if (this is __TextBoxBase)
                            return;

                        ev.preventDefault();
                    };

                // how much will this slow us down?
                this.HTMLTargetRef.oncontextmenu +=
                     e =>
                     {
                         e.stopPropagation();

                         if (this is __TextBoxBase)
                             return;

                         e.preventDefault();

                         if (this.ContextMenuStrip == null)
                             return;

                         var m = (__ContextMenuStrip)(object)this.ContextMenuStrip;

                         var div = m.HTMLTargetRef.AttachToDocument();

                         div.style.SetLocation(
                             e.CursorX,
                             e.CursorY
                         );

                         div.tabIndex = 0;

                         div.onblur +=
                             delegate
                             {
                                 div.Orphanize();
                             };

                         div.focus();
                     };
            }

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
                //Console.WriteLine("set_Size");
                this.SetBounds(this.x, this.y, value.Width, value.Height, BoundsSpecified.Size);
            }
        }
        #endregion



        public int Bottom
        {
            get
            {
                return this.y + this.height;
            }
            set
            {
                this.SetBounds(this.x, value - this.height, this.width, this.height, BoundsSpecified.Y);
            }
        }

        public int Right
        {
            get
            {
                return this.x + this.width;
            }
            set
            {
                this.SetBounds(value - this.width, this.y, this.width, this.height, BoundsSpecified.X);
            }
        }

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
            #region BoundsSpecified
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
            #endregion

            #region min max
            if (this.MinimumSize.Width > 0)
                width = Math.Max(this.MinimumSize.Width, width);

            if (this.MinimumSize.Height > 0)
                height = Math.Max(this.MinimumSize.Height, height);


            if (this.MaximumSize.Width > 0)
                width = Math.Min(this.MaximumSize.Width, width);

            if (this.MaximumSize.Height > 0)
                height = Math.Min(this.MaximumSize.Height, height);
            #endregion



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


        protected virtual void UpdateBounds(int x, int y, int width, int height/*, int clientWidth, int clientHeight*/)
        {
            InternalUpdateBounds(x, y, width, height);
        }


        public virtual void InternalUpdateBoundsSetLocation(int x, int y)
        {
            this.HTMLTargetRef.style.SetLocation(x, y);
        }

        protected void InternalUpdateBounds(int x, int y, int width, int height/*, int clientWidth, int clientHeight*/)
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

            this.clientWidth = width;
            this.clientHeight = height;

            // this Control is used but not shown.
            if (this.HTMLTargetRef == null)
                return;

            if (flag)
            {
                if (this.Parent is FlowLayoutPanel)
                {
                    // X:\jsc.svn\examples\javascript\forms\Test\TestLinearFlow\TestLinearFlow\ApplicationControl.cs
                    // auto flow
                }
                else
                {
                    InternalUpdateBoundsSetLocation(x, y);
                }

                this.OnLocationChanged(null);
            }

            //Console.WriteLine("before InternalClientSizeChanged " + new { flag2 });
            if (flag2)
            {
                //throw new Exception("Html element not set: " + this.Name);

                var this_as_Form = this as __Form;
                if ((this_as_Form != null) && this_as_Form.WindowState == FormWindowState.Maximized)
                {
                    // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs
                    // skip it for now
                }
                else
                {
                    this.HTMLTargetRef.style.SetSize(width, height);
                }

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131005-chrome-frame-server
                // chrome app? may not have render frames
                Native.setTimeout(
                  delegate
                  {
                      InternalClientSizeChanged();

                  },
                  1
                );
            }


        }

        public void InternalClientSizeChanged()
        {
            //Console.WriteLine("at InternalClientSizeChanged");

            this.clientWidth = this.HTMLTargetContainerRef.clientWidth;
            this.clientHeight = this.HTMLTargetContainerRef.clientHeight;

            //Console.WriteLine("InternalClientSizeChanged " + new { @this = this, clientWidth, clientHeight });


            this.OnSizeChanged(null);
            this.OnClientSizeChanged(null);

            //CommonProperties.xClearPreferredSizeCache(this);
            //LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.Bounds);

            if (InternalLayoutSuspended)
            {
            }
            else
            {
                InternalChildrenAnchorUpdate(
                    clientWidth,
                    clientHeight,
                    dx: 0,
                    dy: 0
                );

            }
        }

        public void InternalChildrenAnchorUpdate(int width, int height, int dx, int dy)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                var item = this.Controls[i];

                InternalChildrenAnchorUpdate(
                    width,
                    height,
                    dx,
                    dy,
                    item
                );
            }
        }
        #endregion




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

            InternalRaiseLocationChanged();
        }

        public void InternalRaiseLocationChanged()
        {
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
            InternalOnSizeChanged();

        }

        public void InternalOnSizeChanged()
        {
            //Console.WriteLine("at InternalOnSizeChanged");


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
        public object Tag { get; set; }

        public bool AutoSize { get; set; }





      



        public virtual void InternalAddGotFocus(EventHandler e)
        {
        }

        public event EventHandler GotFocus
        {
            add
            {
                InternalAddGotFocus(value);
            }
            remove
            {
            }
        }

        public virtual void InternalAddLostFocus(EventHandler e)
        {
        }

        public event EventHandler LostFocus
        {
            add
            {
                InternalAddLostFocus(value);
            }
            remove
            {
            }
        }

        public bool Focus()
        {

            this.HTMLTargetRef.focus();
            return false;
        }




        #region ForeColor

        public event EventHandler ForeColorChanged;

        private Color _ForeColor;

        protected virtual void InternalSetForeColor(Color value)
        {
            this.HTMLTargetRef.style.color = value.ToString();
        }

        public Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                _ForeColor = value;

                InternalSetForeColor(value);

                if (ForeColorChanged != null)
                    ForeColorChanged(this, new EventArgs());
            }
        }
        #endregion

        #region BackColor

        public event EventHandler BackColorChanged;


        protected virtual void InternalSetBackgroundColor(Color value)
        {
            this.HTMLTargetRef.style.backgroundColor = value.ToString();
        }

        private Color _BackColor;

        public Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;

                InternalSetBackgroundColor(value);

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

        //Error	1	Inconsistent accessibility: field type 'ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.Handler<System.EventHandler,System.Action<ScriptCoreLib.JavaScript.DOM.IEvent>>' is less accessible than field 'ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control._Click'	X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\Control.cs	
        // 836	50	ScriptCoreLib.Windows.Forms


        public InternalHandler<EventHandler, DOMHandler> InternalClick = new InternalHandler<EventHandler, DOMHandler>();

        public event EventHandler Click
        {
            add
            {
                var h = InternalClick;
                h.Event += value;
                if (!h) return;
                h.EventInternal = i => { i.StopPropagation(); i.PreventDefault(); h.Event(this, null); };

                this.HTMLTargetRef.onclick += h.EventInternal;
            }
            remove
            {
                var h = InternalClick;
                h.Event -= value;
                if (h) return;
                this.HTMLTargetRef.onclick -= h.EventInternal;
                h.EventInternal = null;
            }
        }
        #endregion

        #region Enter
        InternalHandler<EventHandler, DOMHandler> _Enter = new InternalHandler<EventHandler, DOMHandler>();

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
        InternalHandler<EventHandler, DOMHandler> _Leave = new InternalHandler<EventHandler, DOMHandler>();

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

        InternalHandler<MouseEventHandler, DOMHandler> _MouseDown = new InternalHandler<MouseEventHandler, DOMHandler>();
        InternalHandler<MouseEventHandler, DOMHandler> _MouseUp = new InternalHandler<MouseEventHandler, DOMHandler>();



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
            InternalChildrenAnchorUpdate(
                this.clientWidth,
                this.clientHeight,
                0,
                0
            );

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

  
        protected virtual bool DoubleBuffered { get; set; }

        protected virtual Size DefaultMaximumSize
        {
            get
            {
                return new Size();
            }
        }
        protected virtual Size DefaultMinimumSize
        {
            get
            {
                return new Size();
            }
        }


        Action InternalInvalidate;

        public void Invalidate()
        {
            if (InternalInvalidate != null)
                InternalInvalidate();

        }
        public event PaintEventHandler Paint
        {
            add
            {
                var g = new __Graphics
                {

                };

                g.AfterDrawImage =
                    (b, rect) =>
                    {
                        // we only care the first time :)
                        // as we only support one full frame DrawImage call at this time.
                        g.AfterDrawImage = null;

                        b.InternalCanvas.style.SetLocation(0, 0);

                        this.HTMLTargetContainerRef.appendChild(b.InternalCanvas);

                    };

                var a = new __PaintEventArgs
                {
                    Graphics = (Graphics)(object)g
                };

                InternalInvalidate =
                    delegate
                    {
                        this.HTMLTargetRef.requestAnimationFrame +=
                            delegate
                            {
                                value(this, (PaintEventArgs)(object)a);
                            };
                    };
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public Rectangle ClientRectangle
        {
            get
            {
                var r = new Rectangle();

                r.Width = this.clientWidth;
                r.Height = this.clientHeight;

                return r;
            }
        }

        #region ClientSize
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

        public int clientWidth;
        public int clientHeight;



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
            // are we supposed to honor anchors here?
            // X:\jsc.svn\examples\javascript\forms\FormsCentering\FormsCentering\ApplicationControl.cs

            InternalRaiseClientSizeChanged(e);
        }

        public void InternalRaiseClientSizeChanged(EventArgs e)
        {
            if (ClientSizeChanged != null)
            {
                ClientSizeChanged(this, e);
            }
        }
        #endregion


        public virtual Size MaximumSize { get; set; }
        public virtual Size MinimumSize { get; set; }


        public event KeyEventHandler KeyDown
        {
            add
            {
                this.HTMLTargetRef.onkeydown +=
                    e =>
                    {
                        var a = new KeyEventArgs((Keys)e.KeyCode);

                        value(this, a);

                        if (a.SuppressKeyPress)
                        {
                            // http://stackoverflow.com/questions/1404583/stop-keypress-event
                            e.PreventDefault();
                        }
                    };

            }

            remove
            {

            }
        }

        public event KeyEventHandler KeyUp
        {
            add
            {
                this.HTMLTargetRef.onkeyup +=
                    e =>
                    {
                        value(this, new KeyEventArgs((Keys)e.KeyCode));
                    };

            }

            remove
            {

            }
        }

        public virtual ContextMenuStrip ContextMenuStrip
        {
            get;
            set;
        }

        public virtual void BringToFront()
        {
        }

        protected virtual void OnPaint(PaintEventArgs e)
        {

        }





        [Script]
        class XDataObject : IDataObject
        {

            public object GetData(Type format)
            {
                throw new NotImplementedException();
            }

            public object GetData(string format)
            {
                //if (format == "Text")
                //    format = "text/plain";


                if (format == "FileDrop")
                {
                    var files = this.InternalData.dataTransfer.files;

                    return Enumerable.Range(
                        0,
                        (int)files.length
                    ).Select(k => files[(uint)k].name);

                }


                if (format == "text/plain")
                    format = "Text";



                var value = this.InternalData.dataTransfer.getData(format);

                return value;
            }

            public object GetData(string format, bool autoConvert)
            {
                throw new NotImplementedException();
            }

            public bool GetDataPresent(Type format)
            {
                throw new NotImplementedException();
            }

            public bool GetDataPresent(string format)
            {
                throw new NotImplementedException();
            }

            public bool GetDataPresent(string format, bool autoConvert)
            {
                throw new NotImplementedException();
            }

            public DragEvent InternalData;

            public string[] GetFormats()
            {
                return InternalData.dataTransfer.types;
            }

            public string[] GetFormats(bool autoConvert)
            {
                throw new NotImplementedException();
            }

            public void SetData(object data)
            {
                throw new NotImplementedException();
            }

            public void SetData(Type format, object data)
            {
                throw new NotImplementedException();
            }

            public void SetData(string format, object data)
            {
                throw new NotImplementedException();
            }

            public void SetData(string format, bool autoConvert, object data)
            {
                throw new NotImplementedException();
            }
        }

        #region MouseMove
        //InternalHandler<MouseEventHandler, DOMHandler> _MouseMove = new InternalHandler<MouseEventHandler, DOMHandler>();
        public event MouseEventHandler MouseMove
        {
            add
            {
                // what if the caller delegate will call DoDragDrop?
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201305/2130522-forms-drag

                this.HTMLTargetRef.ondragstart +=
                    e =>
                    {
                        var Buttons = e.GetMouseButton();

                        //Console.WriteLine("ondragstart " + new { Buttons });

                        var a = e.GetMouseEventHandler(Buttons);

                        var MissingDoDragDrop = true;

                        this.InternalDoDragDrop =
                            (data, effects) =>
                            {
                                //Console.WriteLine("InternalDoDragDrop");


                                MissingDoDragDrop = false;

                                // X:\jsc.internal.svn\compiler\jsx.reflector\ReflectorWindow.AddNode.cs
                                // X:\jsc.svn\examples\javascript\DragIntoCRX\DragIntoCRX\Application.cs

                                // hope its a string?

                                // http://msdn.microsoft.com/en-us/library/ie/ms536744(v=vs.85).aspx

                                var dataTransfer = e.dataTransfer;
                                var text = "" + data;

                                // SCRIPT65535: Unexpected call to method or property access. 
                                //dataTransfer.setData("text/plain", text);
                                dataTransfer.setData("Text", text);

                                //Uncaught TypeError: setDragImageFromElement: Invalid first argument 
                                //e.dataTransfer.setDragImage(null, 0, 0);

                            };

                        value(this, a);

                        this.InternalDoDragDrop = null;

                        // can we abort?
                        if (MissingDoDragDrop)
                        {
                            e.preventDefault();
                            e.stopPropagation();
                        }
                    };

                //_MouseMove.Event += value;
                //if (_MouseMove)
                //{
                //    _MouseMove.EventInternal =
                //        i =>
                //        {
                //            this._MouseMove.Event(this, i.GetMouseEventHandler(MouseButtons.None));
                //        };

                //    this.HTMLTargetRef.onmousemove += _MouseMove.EventInternal;
                //}
            }
            remove
            {

                //_MouseMove.Event -= value;
                //if (!_MouseMove)
                //{
                //    this.HTMLTargetRef.onmousemove -= _MouseMove.EventInternal;
                //    _MouseMove.EventInternal = null;
                //}
            }
        }
        #endregion

        Action<object, DragDropEffects> InternalDoDragDrop;

        public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
        {
            if (InternalDoDragDrop != null)
                InternalDoDragDrop(data, allowedEffects);


            return allowedEffects;
        }

        public event EventHandler DragLeave
        {
            add
            {
                this.HTMLTargetRef.ondragleave +=
                  e =>
                  {
                      e.stopPropagation();
                      e.preventDefault();

                      value(this, null);
                  };

            }

            remove { }
        }
        public event DragEventHandler DragDrop
        {
            add
            {

                this.HTMLTargetRef.ondrop +=
                   e =>
                   {
                       e.stopPropagation();
                       e.preventDefault();

                       var files = e.dataTransfer.files;


                       ScriptCoreLib.JavaScript.BCLImplementation.System.IO.__File.InternalFiles =

                                Enumerable.Range(
                                  0,
                                  (int)files.length
                              ).Select(k => files[(uint)k]);

                       var a = new __DragEventArgs
                       {
                           Data = new XDataObject { InternalData = e }
                       };

                       value(this, (DragEventArgs)(object)a);



                   };

            }
            remove { }
        }


        public event DragEventHandler DragOver
        {
            add
            {

                this.HTMLTargetRef.ondragover +=
                    e =>
                    {
                        e.stopPropagation();
                        e.preventDefault();

                        var a = new __DragEventArgs
                        {
                            Data = new XDataObject { InternalData = e },
                            InternalSetEffect =
                                Effect =>
                                {

                                    // translate?
                                    e.dataTransfer.dropEffect = "copy";

                                }
                        };

                        value(this, (DragEventArgs)(object)a);



                    };
            }
            remove { }
        }

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201305/2130522-forms-drag
        public virtual bool AllowDrop { get; set; }


        public Form FindForm()
        {
            var p = (Control)this;

            while (p.Parent != null)
                p = p.Parent;

            return p as Form;
        }




        // X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\ApplicationControl.cs
        public ControlBindingsCollection DataBindings { get; set; }


        public __Control()
        {
            //            arg[0] is typeof System.Windows.Forms.IBindableComponent
            //no matching prototype
            //script: error JSC1000: error at ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control..ctor,
            // assembly: X:\jsc.svn\examples\javascript\forms\FormsDataBindingForEnabled\FormsDataBindingForEnabled\bin\Release\ScriptCoreLib.Windows.Forms.dll
            // type: ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__Control, ScriptCoreLib.Windows.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0056
            //  method:Void .ctor()

            this.DataBindings = new __ControlBindingsCollection(this);

            this.Controls = new Control.ControlCollection(this);

            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            this.MinimumSize = DefaultMinimumSize;
            this.MaximumSize = DefaultMaximumSize;


        }


    }
}
