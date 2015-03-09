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
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/Form.cs

    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    public partial class __Form : __ContainerControl
    {
		// could we run in glsl?
		// http://zephyrosanemos.com/windstorm/current/live-demo.html

		// http://www.windows93.net/

		// https://code.google.com/p/chromium/issues/detail?id=240719
		// keep an eye on <x-titlebar-controls/>
		// https://code.google.com/p/chromium/issues/detail?id=384612


		// alternative service providers:
		// see: http://dhtmlx.com/docs/products/dhtmlxWindows/index.shtml

		object __FormTypeHint;

        public FormStyler InternalStyler;

        public event FormClosedEventHandler FormClosed;
        public event FormClosingEventHandler FormClosing;

        // http://msdn.microsoft.com/en-us/library/system.windows.forms.form.closed.aspx
        [Obsolete(" use the FormClosed event instead.")]
        public event EventHandler Closed;

        public IHTMLDiv HTMLTarget { get; set; }

        IHTMLDiv Caption = new IHTMLDiv();
        public IHTMLDiv CaptionShadow;
        public IHTMLElement CaptionContent;
        public IHTMLDiv CaptionForeground;

        IHTMLDiv ContentContainerPadding = new IHTMLDiv();
        IHTMLDiv ContentContainer;

        // #shadow-dom #content ?
        public override IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return ContentContainer;

            }
        }

        // #host ?
        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }



        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.Form.set_AutoSizeMode(System.Windows.Forms.AutoSizeMode)]

        public AutoSizeMode AutoSizeMode { get; set; }

        public ScriptCoreLib.JavaScript.Controls.DragHelper InternalCaptionDrag;

        public IHTMLDiv InternalCloseButton;
        public IHTMLDiv InternalCloseButtonContent;

        const int innerborder = 1;

        IHTMLDiv TargetNoBorder;

        protected override void InternalSetBackgroundColor(Color value)
        {
            InternalStyler.TargetInnerBorder.style.backgroundColor = value.ToString();
            ContentContainerPadding.style.backgroundColor = value.ToString();

            //TargetOuterBorder.style.backgroundColor = value.ToString();
            //ContentContainer.style.backgroundColor = value.ToString();

            //// for firefox fullscren
            //TargetNoBorder.style.backgroundColor = value.ToString();
        }

        IHTMLDiv TargetResizerPadding;
        public IHTMLDiv TargetOuterBorder;



        // tested by?
        // shall we use it for chrome AppWindow webviews too?
        [Description("Hide iframes from mouse to workaround event leaks.")]
        public static event Action InternalMouseCapured;
        public static event Action InternalMouseReleased;

        public IHTMLDiv ResizeGripElement;

        //IHTMLImage icon = "assets/ScriptCoreLib/jsc.ico";
        // each app should be able to have their own app level favicon
        IHTMLImage icon = "assets/ScriptCoreLib/jsc.png";

        public ScriptCoreLib.JavaScript.Controls.DragHelper ResizeGripDrag;


        // when can we have it as XAttribute? with #shadow-dom update?
        public bool ShowIcon
        {
            get { return icon.style.display != IStyle.DisplayEnum.none; }
            set { icon.Show(value); }
        }

        #region ControlBox
        public bool InternalControlBox;
        public bool ControlBox
        {
            get
            {
                return InternalControlBox;
            }
            set
            {
                InternalControlBox = value;

                this.InternalCloseButton.Show(value);

                icon.Show(value);
            }
        }
        #endregion


        public static int __FormZIndex = 0;



        public Size ClientSize
        {
            get
            {
                // Form.SizeFromClientSize
                return base.ClientSize;
            }
            set
            {
                base.ClientSize = value;
            }
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            //Console.WriteLine("Form.SizeFromClientSize " + new { clientSize });

            // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs

            return new Size(
                clientSize.Width + innerborder * 2 + 2,
                clientSize.Height + innerborder * 3 + 26
                //clientSize.Height + innerborder * 3 + 42
            );
        }




        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(64, 32);
            }
        }



        // will chrome AppWindow use it?
        public bool TopMost { get; set; }

        // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs

        public bool MaximizeBox { get; set; }
        public bool MinimizeBox { get; set; }






        public override string Text
        {
            get
            {
                return CaptionContent.innerText;
            }
            set
            {
                CaptionContent.innerText = value;

                InternalRaiseTextChanged();
            }
        }



        #region Opacity
        public double InternalOpacity;
        public double Opacity
        {
            get
            {
                return InternalOpacity;
            }
            set
            {
                this.InternalOpacity = value;
                this.HTMLTarget.style.Opacity = value;
            }
        }
        #endregion


        protected override void OnMove(EventArgs e)
        {
            // compiler bug: buggy implementation when it comes to handling structs

            var Location = this.Location;

            InternalCaptionDrag.Position = new Shared.Drawing.Point(Location.X, Location.Y);

            base.RaiseMove(e);
        }


        #region Load


        public int InternalHostWidth
        {
            get
            {
                var host = (IHTMLElement)this.HTMLTarget.parentNode;

                var value = Native.window.Width;

                // tested by
                // X:\jsc.svn\examples\javascript\HistoryStatesViaWebService\HistoryStatesViaWebService\Application.cs
                if (host == Native.document.body.parentNode)
                {
                    //if (host.clientWidth > 0)
                    //    if (host.scrollWidth > 0)
                    //        value = host.clientWidth < host.scrollWidth ? host.scrollWidth : host.clientWidth;
                }
                else
                {
                    if (host.clientWidth > 0)
                        value = host.clientWidth;
                }

                return value;
            }
        }

        public int InternalHostHeight
        {
            get
            {
                var host = (IHTMLElement)this.HTMLTarget.parentNode;

                var value = Native.window.Height;

                if (host == Native.document.body.parentNode)
                {
                    //if (host.clientHeight > 0)
                    //    if (host.scrollHeight > 0)
                    //        value = host.clientHeight < host.scrollHeight ? host.scrollHeight : host.clientHeight;
                }
                else
                {
                    if (host.clientHeight > 0)
                        value = host.clientHeight;
                }

                return value;
            }
        }


        public override void BringToFront()
        {
            this.InternalUpdateZIndex();
        }


        private void InternalUpdateZIndex(IHTMLElement e = null)
        {
            if (e == null)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    if (this.FormBorderStyle == global::System.Windows.Forms.FormBorderStyle.None)
                        e = this.ContentContainer;

                    else
                        e = this.TargetNoBorder;

                }
                else
                {
                    e = HTMLTarget;
                }
            }

            __FormZIndex++;
            //Text = new { __FormZIndex }.ToString();

            e.style.zIndex = __FormZIndex;

            foreach (__Form item in this.OwnedForms)
            {
                item.InternalUpdateZIndex();
            }
        }

        public void InternalRaiseLoad()
        {
            if (Load != null)
                Load(this, new EventArgs());
        }

        

        // called by ?
        public void InternalRaiseShown()
        {
            if (Shown != null)
                Shown(this, new EventArgs());
        }

        public event EventHandler Load;
        public event EventHandler Shown;
        #endregion

        public SizeGripStyle InternalSizeGripStyle = SizeGripStyle.Show;

        public SizeGripStyle SizeGripStyle
        {
            get
            {
                return this.InternalSizeGripStyle;
            }
            set
            {
                this.InternalSizeGripStyle = value;

                this.ResizeGripElement.Show(value != global::System.Windows.Forms.SizeGripStyle.Hide);

            }
        }

        public FormStartPosition StartPosition { get; set; }

        int __PreviousWidth;
        int __PreviousHeight;

        #region WindowState

        static List<__Form> InternalMaximizedForms = new List<__Form>();


        public bool InternalWindowStateAnimated = false;

        FormWindowState InternalWindowState;

        public FormWindowState WindowState
        {
            get
            {
                if (InternalMaximizedForms.Contains(this))
                    return FormWindowState.Maximized;

                if (InternalWindowState == FormWindowState.Minimized)
                    return FormWindowState.Minimized;

                return FormWindowState.Normal;
            }
            set
            {
                if (InternalDiagnostics.BreakAtWindowState)
                    Debugger.Break();

                if (WindowState == value)
                    return;


                if (value == FormWindowState.Normal)
                {
                    CaptionShadow.Hide();

                    if (InternalWindowState == FormWindowState.Minimized)
                    {
                        this.MinimumSize = this.DefaultMinimumSize;
                        this.MaximumSize = this.DefaultMaximumSize;

                        Location = this.InternalRestoreLocation;
                        ClientSize = this.InternalRestoreClientSIze;
                    }
                    else if (InternalMaximizedForms.Contains(this))
                    {
                        #region was fullscreen
                        //Console.WriteLine("set_WindowState <- Normal, InternalMaximizedForms.Remove");

                        InternalMaximizedForms.Remove(this);



                        //if (this.FormBorderStyle == global::System.Windows.Forms.FormBorderStyle.None)
                        //{
                        //    this.ContentContainer.Orphanize().AttachTo(this.HTMLTarget);
                        //    this.ContentContainer.style.zIndex = 0;
                        //}
                        //else
                        //{
                        // unhide our old frame
                        //this.HTMLTarget.style.display = IStyle.DisplayEnum.block;

                        //this.TargetNoBorder.Orphanize().AttachTo(TargetResizerPadding);
                        //this.TargetNoBorder.style.zIndex = 0;

                        Console.WriteLine("WindowState undo InternalMaximizedForms " + new
                        {
                            this.InternalRestoreLocation,
                            this.InternalRestoreClientSIze
                        }
                        );

                        this.HTMLTarget.style.right = "";
                        this.HTMLTarget.style.bottom = "";

                        this.ResizeGripElement.Show();




                        this.HTMLTarget.style.SetLocation(
                            this.InternalRestoreLocation.X,
                            this.InternalRestoreLocation.Y
                        );

                        //    this.width,
                        //    this.height
                        //);



                        this.ClientSize = this.InternalRestoreClientSIze;


                        Console.WriteLine("WindowState undo InternalMaximizedForms " + new
                        {
                            this.Location,
                        }
        );

                        InternalCaptionDrag.OffsetPosition.Y = 12;
                        InternalCaptionDrag.OffsetPosition.X = this.Width / 2;

                        //if (InternalMaximizedForms.Count == 0)
                        //{
                        //    // exit only if we are not maximized again
                        //    Native.window.requestAnimationFrame +=
                        //        delegate
                        //        {
                        //            if (InternalMaximizedForms.Count == 0)
                        //            {
                        //                //Console.WriteLine("set_WindowState exitFullscreen InternalMaximizedForms.Count == 0");


                        //                Native.document.exitFullscreen();
                        //            }
                        //        };

                        //}

                        this.HTMLTarget.requestAnimationFrame +=
                            delegate
                            {
                                InternalClientSizeChanged0();
                            };
                        #endregion
                    }



                }
                else if (value == FormWindowState.Maximized)
                {
                    if (InternalWindowState == FormWindowState.Minimized)
                    {
                        this.MinimumSize = this.DefaultMinimumSize;
                        this.MaximumSize = this.DefaultMaximumSize;

                        Location = this.InternalRestoreLocation;
                        ClientSize = this.InternalRestoreClientSIze;
                    }
                    else if (!InternalMaximizedForms.Contains(this))
                    {
                        //Console.WriteLine("set_WindowState <- Maximized, InternalMaximizedForms.Add");
                        InternalMaximizedForms.Add(this);

                        //if (this.FormBorderStyle == global::System.Windows.Forms.FormBorderStyle.None)
                        //{
                        //    this.ContentContainer.Orphanize().AttachToDocument();
                        //    InternalUpdateZIndex(this.ContentContainer);
                        //}
                        //else
                        //{

                        //this.TargetNoBorder.Orphanize().AttachTo(host);

                        //// hide our old frame
                        //this.HTMLTarget.style.display = IStyle.DisplayEnum.none;

                        //this.HTMLTarget.style.position = IStyle.PositionEnum.absolute;

                        // do InternalMaximizedForms { Location = [object Object], ClientSize = [object Object] }
                        //Console.WriteLine("WindowState do InternalMaximizedForms " + new { this.Location, this.ClientSize });

                        //this.internalre
                        //this.ResizeGripDrag.Enabled = false;
                        this.ResizeGripElement.Hide();


                        //                        InternalEnterFullscreen WindowState <- Maximized
                        // view-source:27892
                        //WindowState do InternalMaximizedForms { Location = { X = 407.5, Y = 0 }, ClientSize = { Width = 400, Height = 320 } }
                        // view-source:27892
                        //{ BeforePosition = [407.5, 0], DragStartMaximized = true } view-source:27892

                        // view-source:27892
                        //InternalCaptionDrag.MiddleClick  InternalExitFullscreen
                        // view-source:27892
                        //InternalEnterFullscreen WindowState <- Normal
                        // view-source:27892
                        //WindowState undo InternalMaximizedForms { InternalRestoreLocation = { X = 407.5, Y = 0 }, InternalRestoreClientSIze = { Width = 400, Height = 320 } }


                        this.InternalRestoreLocation = this.Location;
                        this.InternalRestoreClientSIze = this.ClientSize;

                        #region 100ms maximize

                        this.HTMLTarget.style.width = "";
                        this.HTMLTarget.style.height = "";

                        if (this.HTMLTarget.parentNode != null)
                        {

                            this.HTMLTarget.style.right = (InternalHostWidth - this.Right) + "px";
                            this.HTMLTarget.style.bottom = (InternalHostHeight - this.Bottom) + "px";




                            if (InternalWindowStateAnimated)
                            {
                                this.HTMLTarget.style.transition = "left 100ms linear, top 100ms linear, right 100ms linear, bottom 100ms linear";


                                var anitimer_Enabled = true;

                                var anitimer = new ScriptCoreLib.JavaScript.Runtime.Timer(
                                    delegate
                                    {

                                        anitimer_Enabled = false;
                                        this.HTMLTarget.style.transition = "";
                                    }
                                );

                                anitimer.StartTimeout(100 + 20);


                                // X:\jsc.svn\examples\javascript\chrome\apps\ChromeEarth\ChromeEarth\Application.cs

                                Native.window.onframe +=
                                   delegate
                                   {
                                       // overhead?
                                       //if (!anitimer.Enabled)
                                       if (!anitimer_Enabled)
                                           return;

                                       InternalClientSizeChanged0();
                                   };
                            }
                        }

                        // where we want to be in 100ms
                        this.HTMLTarget.style.left = "0px";
                        this.HTMLTarget.style.top = "0px";



                        this.HTMLTarget.style.right = "0px";
                        this.HTMLTarget.style.bottom = "0px";
                        #endregion


                        //InternalUpdateZIndex(this.TargetNoBorder);
                        //}

                        CaptionShadow.Show();



                        if (InternalMaximizedForms.Count == 1)
                        {

                            Action<IEvent> onresize = null;

                            onresize =
                                delegate
                                {
                                    InternalClientSizeChanged0();


                                    if (this.WindowState == FormWindowState.Maximized)
                                        return;


                                    Native.window.onresize -= onresize;

                                };

                            Native.window.onresize += onresize;

                            InternalClientSizeChanged0();

                            //Console.WriteLine("set_WindowState requestFullscreen");
                            //Native.Document.body.requestFullscreen();


                        }



                    }
                }
                else if (value == FormWindowState.Minimized)
                {
                    #region Minimized
                    CaptionShadow.Show();

                    this.InternalRestoreLocation = this.Location;
                    this.InternalRestoreClientSIze = this.ClientSize;
                    this.MinimumSize = this.DefaultMinimumSize;
                    this.ClientSize = new Size(200, 0);

                    this.HTMLTarget.requestAnimationFrame +=
                        delegate
                        {

                            this.MinimumSize = this.Size;
                            this.MaximumSize = new Size(InternalHostWidth, this.Height);

                            this.Top = InternalHostHeight - 26;
                        };
                    #endregion

                }


                InternalWindowState = value;

                InternalRaiseLocationChanged();
            }
        }
        #endregion

        Size InternalRestoreClientSIze;
        Point InternalRestoreLocation;

        #region FormBorderStyle
        public FormBorderStyle InternalFormBorderStyle = FormBorderStyle.Sizable;

        public FormBorderStyle FormBorderStyle
        {
            get
            {
                return InternalFormBorderStyle;
            }
            set
            {
                if (InternalFormBorderStyle == value)
                    return;

                InternalFormBorderStyle = value;

                if (value == global::System.Windows.Forms.FormBorderStyle.None)
                {
                    //Console.WriteLine("set_FormBorderStyle <- None");

                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.TargetNoBorder.style.zIndex = 0;
                        this.TargetNoBorder.Orphanize().AttachTo(TargetResizerPadding);

                        this.ContentContainer.Orphanize().AttachToDocument();
                        InternalUpdateZIndex(this.ContentContainer);
                    }
                    else
                    {
                        this.TargetOuterBorder.Orphanize();
                        this.ContentContainer.style.zIndex = 0;
                        this.ContentContainer.Orphanize().AttachTo(this.HTMLTarget);
                        InternalUpdateZIndex(this.HTMLTarget);
                    }
                }

                if (value == global::System.Windows.Forms.FormBorderStyle.Sizable)
                {
                    //Console.WriteLine("set_FormBorderStyle <- Sizable");

                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.ContentContainer.style.zIndex = 0;
                        this.ContentContainer.Orphanize().AttachTo(this.ContentContainerPadding);

                        this.TargetNoBorder.Orphanize().AttachToDocument();
                        InternalUpdateZIndex(this.TargetNoBorder);
                    }
                    else
                    {
                        this.ContentContainer.style.zIndex = 0;
                        this.ContentContainer.Orphanize().AttachTo(this.ContentContainerPadding);
                        this.TargetOuterBorder.AttachTo(this.HTMLTarget);
                        InternalUpdateZIndex(this.HTMLTarget);
                    }
                }
            }
        }
        #endregion


        public override Size MaximumSize { get; set; }
        public override Size MinimumSize { get; set; }


        #region operators
        public static implicit operator __Form(Form e)
        {
            return (__Form)(object)e;
        }

        public static implicit operator Form(__Form e)
        {
            return (Form)(object)e;
        }
        #endregion


        protected override void UpdateBounds(int x, int y, int width, int height)
        {
            // form titlebar shall remain visible
            y = Math.Max(y, -4);

            InternalUpdateBounds(x, y, width, height);
        }

















    }

    [Script]
    public static partial class InternalDiagnostics
    {
        public static bool BreakAtWindowState;
    }


}
