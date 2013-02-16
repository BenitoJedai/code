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
    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    public class __Form : __ContainerControl
    {
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
        IHTMLDiv CaptionShadow;
        IHTMLDiv CaptionContent;
        IHTMLDiv CaptionForeground;

        IHTMLDiv ContentContainerPadding = new IHTMLDiv();
        IHTMLDiv ContentContainer;


        public override IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return ContentContainer;

            }
        }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }


        public ScriptCoreLib.JavaScript.Controls.DragHelper InternalCaptionDrag;
        IHTMLDiv CloseButton;


        const int innerborder = 1;

        IHTMLDiv TargetNoBorder;

        protected override void InternalSetBackgroundColor(Color value)
        {
            ContentContainerPadding.style.backgroundColor = value.ToString();

            //TargetOuterBorder.style.backgroundColor = value.ToString();
            //ContentContainer.style.backgroundColor = value.ToString();

            //// for firefox fullscren
            //TargetNoBorder.style.backgroundColor = value.ToString();
        }

        IHTMLDiv TargetResizerPadding;
        IHTMLDiv TargetOuterBorder;

        [Description("Hide iframes from mouse to workaround event leaks.")]
        public static event Action InternalMouseCapured;
        public static event Action InternalMouseReleased;

        public __Form()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            var TargetElement = new IHTMLDiv();

            TargetElement.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            TargetElement.style.left = "0px";
            TargetElement.style.top = "0px";

            HTMLTarget = TargetElement;

            #region TargetOuterBorder
            TargetOuterBorder = new IHTMLDiv().AttachTo(TargetElement);
            //HTMLTarget.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            TargetOuterBorder.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            TargetOuterBorder.style.left = "0px";
            TargetOuterBorder.style.top = "0px";
            TargetOuterBorder.style.bottom = "0px";
            TargetOuterBorder.style.right = "0px";

            TargetOuterBorder.style.borderWidth = "1px";
            TargetOuterBorder.style.borderStyle = "solid";
            TargetOuterBorder.style.borderBottomColor = "#424142";
            TargetOuterBorder.style.borderRightColor = "#424142";
            TargetOuterBorder.style.borderLeftColor = "#D6D3CE";
            TargetOuterBorder.style.borderTopColor = "#D6D3CE";
            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            TargetOuterBorder.style.padding = "0";
            #endregion

            #region TargetInnerBorder
            var TargetInnerBorder = new IHTMLDiv().AttachTo(TargetOuterBorder);
            ////HTMLTarget.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            TargetInnerBorder.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            TargetInnerBorder.style.left = "0px";
            TargetInnerBorder.style.top = "0px";
            TargetInnerBorder.style.bottom = "0px";
            TargetInnerBorder.style.right = "0px";

            TargetInnerBorder.style.borderWidth = "1px";
            TargetInnerBorder.style.borderStyle = "solid";
            TargetInnerBorder.style.borderLeftColor = "#FFFFFF";
            TargetInnerBorder.style.borderTopColor = "#FFFFFF";
            TargetInnerBorder.style.borderRightColor = "#848284";
            TargetInnerBorder.style.borderBottomColor = "#848284";

            TargetInnerBorder.style.backgroundColor = "#D6D3CE";

            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            #endregion


            #region TargetResizerPadding
            TargetResizerPadding = new IHTMLDiv().AttachTo(TargetInnerBorder);
            TargetResizerPadding.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            TargetResizerPadding.style.left = "2px";
            TargetResizerPadding.style.top = "2px";
            TargetResizerPadding.style.bottom = "2px";
            TargetResizerPadding.style.right = "2px";
            #endregion

            #region TargetNoBorder
            TargetNoBorder = new IHTMLDiv().AttachTo(TargetResizerPadding);
            TargetNoBorder.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            TargetNoBorder.style.left = "0px";
            TargetNoBorder.style.top = "0px";
            TargetNoBorder.style.bottom = "0";
            TargetNoBorder.style.right = "0";
            #endregion


            TargetOuterBorder.style.boxShadow = "black 3px 3px 6px -3px";


            #region caption
            //IHTMLImage icon = "assets/ScriptCoreLib.Windows.Forms/App.ico";
            IHTMLImage icon = "assets/ScriptCoreLib/jsc.ico";

            icon.style.SetLocation(5, 5, 16, 16);

            //caption.style.backgroundColor = JSColor.System.ActiveCaption;
            Caption.style.backgroundColor = JSColor.FromRGB(0x08, 0x24, 0x6B);

            Caption.style.color = Shared.Drawing.Color.White;
            Caption.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

            Caption.style.left = 0 + "px";
            Caption.style.top = 0 + "px";
            Caption.style.right = 0 + "px";
            Caption.style.height = "26px";

            //Caption.style.paddingTop = "6px";
            //Caption.style.paddingLeft = "26px";
            Caption.style.font = new Font("Segoe UI", 9.0F, FontStyle.Regular, GraphicsUnit.Point, 0).ToCssString();

            CaptionShadow = (IHTMLDiv)Caption.cloneNode(false);
            CaptionShadow.style.backgroundColor = JSColor.Black;
            CaptionShadow.style.Opacity = 0;

            CaptionContent = (IHTMLDiv)Caption.cloneNode(false);
            CaptionContent.style.backgroundColor = JSColor.None;

            CaptionContent.style.WithDynamic(
                style => style.textOverflow = "ellipsis"
            );
            CaptionContent.style.lineHeight = "26px";
            CaptionContent.style.left = "26px";
            CaptionContent.style.right = "30px";

            CaptionContent.style.overflow = IStyle.OverflowEnum.hidden;

            CaptionForeground = (IHTMLDiv)Caption.cloneNode(false);
            CaptionForeground.style.backgroundColor = ScriptCoreLib.Shared.Drawing.Color.FromRGB(255, 0, 255);
            CaptionForeground.style.Opacity = 0;
            CaptionForeground.className = "caption";

            // http://dojotoolkit.org/pipermail/dojo-checkins/2005-December/002867.html


            //            new IFunction(@"
            //                try { this.style.MozUserSelect = 'none'; } catch (e) { }
            //                try { this.style.KhtmlUserSelect = 'none'; } catch (e) { }
            //                try { this.unselectable = 'on'; } catch (e) { }
            //                "
            //            ).apply(caption_foreground);

            //            caption_foreground.onselectstart +=
            //                (e) =>
            //                {
            //                    e.PreventDefault();
            //                    e.StopPropagation();
            //                };
            #endregion

            // http://developer.apple.com/mac/library/documentation/AppleApplications/Reference/Dashboard_Ref/Dashboard_Ref.pdf
            // for some reason we cannot exclude caption
            // from apple dashboard

            //caption_foreground.style.appleDashboardRegion = "none";

            //container.style.backgroundColor = "#A0A0A0";
            //container.style.appleDashboardRegion = "dashboard-region(control rectangle)";

            #region ContentContainer
            ContentContainerPadding.title = "ContentContainerPadding";
            ContentContainerPadding.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ContentContainerPadding.style.left = 0 + "px";
            ContentContainerPadding.style.top = (26 + innerborder + 0) + "px";
            ContentContainerPadding.style.right = 0 + "px";
            ContentContainerPadding.style.bottom = 0 + "px";

            ContentContainer = new IHTMLDiv().AttachTo(ContentContainerPadding);
            ContentContainer.title = "ContentContainer";
            ContentContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ContentContainer.style.left = 0 + "px";
            ContentContainer.style.top = 0 + "px";
            ContentContainer.style.right = 0 + "px";
            ContentContainer.style.bottom = 0 + "px";
            ContentContainer.style.overflow = IStyle.OverflowEnum.hidden;
            ContentContainer.style.zIndex = 1000;

            var ContentContainerShadow = new IHTMLDiv().AttachTo(ContentContainerPadding);
            ContentContainerShadow.title = "ContentContainerShadow";
            ContentContainerShadow.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ContentContainerShadow.style.left = 0 + "px";
            ContentContainerShadow.style.top = 0 + "px";
            ContentContainerShadow.style.right = 0 + "px";
            ContentContainerShadow.style.bottom = 0 + "px";
            ContentContainerShadow.style.overflow = IStyle.OverflowEnum.hidden;
            ContentContainerShadow.style.backgroundColor = JSColor.Red;
            ContentContainerShadow.style.Opacity = 0.0;
            ContentContainerShadow.style.display = IStyle.DisplayEnum.none;
            ContentContainerShadow.style.zIndex = 1001;
            #endregion

            InternalMouseCapured +=
                delegate
                {
                    ContentContainerShadow.style.display = IStyle.DisplayEnum.block;
                };

            InternalMouseReleased +=
                delegate
                {
                    ContentContainerShadow.style.display = IStyle.DisplayEnum.none;
                };


            #region ResizeGripElement
            var ResizeGripElement = new IHTMLDiv().AttachTo(ContentContainerPadding);
            ResizeGripElement.title = "ResizeGripElement";
            ResizeGripElement.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ResizeGripElement.style.width = "12px";
            ResizeGripElement.style.height = "12px";
            ResizeGripElement.style.bottom = "0px";
            ResizeGripElement.style.right = "0px";
            ResizeGripElement.style.cursor = IStyle.CursorEnum.se_resize;
            new IHTMLImage { src = "assets/ScriptCoreLib.Windows.Forms/FormResizeGrip.png" }.ToBackground(ResizeGripElement);
            ResizeGripElement.style.zIndex = 1002;
            #endregion

            #region ResizeGripDrag
            var ResizeGripDrag = new ScriptCoreLib.JavaScript.Controls.DragHelper(ResizeGripElement);

            ResizeGripDrag.Enabled = true;

            #region AtSizeChanged
            Action AtSizeChanged = delegate
            {
                var Size = this.Size;

                ResizeGripDrag.Position = new Shared.Drawing.Point(Size.Width, Size.Height);
            };

            this.SizeChanged +=
                delegate
                {
                    AtSizeChanged();
                };
            AtSizeChanged();
            #endregion

            ResizeGripDrag.DragStart +=
                delegate
                {
                    Capture = true;
                    InternalMouseCapured();
                };
            ResizeGripDrag.DragMove +=
                delegate
                {
                    this.Size = new Size(ResizeGripDrag.Position.X, ResizeGripDrag.Position.Y);
                };

            ResizeGripDrag.DragStop +=
              delegate
              {
                  Capture = false;
                  InternalMouseReleased();

              };
            #endregion

            //HTMLTarget.style.backgroundColor = "#B0B0B0";
            this.BackColor = Color.FromArgb(0xD6, 0xD3, 0xCE);

            #region CloseButton
            CloseButton = new IHTMLDiv { name = "CloseButton" };
            CloseButton.style.textAlign = IStyle.TextAlignEnum.center;
            CloseButton.style.fontWeight = "bold";
            CloseButton.style.cursor = IStyle.CursorEnum.@default;

            var CloseButtonContent = new IHTMLDiv { }.AttachTo(CloseButton);

            CloseButtonContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            CloseButtonContent.style.left = "0px";
            CloseButtonContent.style.top = "0px";
            CloseButtonContent.style.bottom = "0";
            CloseButtonContent.style.right = "0";



            CloseButtonContent.title = "Close";
            CloseButtonContent.innerHTML = "&times";



            CloseButton.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

            //CloseButton.style.backgroundColor = JSColor.System.ThreeDFace;
            CloseButton.style.backgroundColor = "#D6D3CE";

            CloseButton.style.height = "18px";
            CloseButton.style.width = "18px";
            CloseButton.style.right = (innerborder + 3) + "px";
            CloseButton.style.top = (innerborder + 2) + "px";

            CloseButton.style.borderWidth = "1px";
            CloseButton.style.borderStyle = "solid";

            CloseButton.style.borderRightColor = "#424142";
            CloseButton.style.borderBottomColor = "#424142";
            CloseButton.style.borderLeftColor = "#FFFFFF";
            CloseButton.style.borderTopColor = "#FFFFFF";



            CloseButtonContent.style.borderWidth = "1px";
            CloseButtonContent.style.borderStyle = "solid";

            CloseButtonContent.style.borderLeftColor = "#D6D3CE";
            CloseButtonContent.style.borderTopColor = "#D6D3CE";
            CloseButtonContent.style.borderRightColor = "#848284";
            CloseButtonContent.style.borderBottomColor = "#848284";

            #region CloseButton onclick
            CloseButton.onmouseover +=
                 delegate
                 {
                     CloseButtonContent.style.color = JSColor.Red;
                 };

            CloseButton.onmouseout +=
                delegate
                {
                    CloseButtonContent.style.color = JSColor.None;
                };

            CloseButton.onmousedown +=
                e =>
                {
                    e.StopPropagation();
                    e.PreventDefault();
                };

            CloseButton.onclick +=
                delegate
                {
              

                    Close();
                };
            #endregion

            #endregion

            TargetNoBorder.appendChild(
                Caption, CaptionShadow, CaptionContent,
                icon, CaptionForeground,
                ContentContainerPadding, CloseButton
            );


            #region drag
            InternalCaptionDrag = new ScriptCoreLib.JavaScript.Controls.DragHelper(CaptionForeground);

            // http://forum.mootools.net/topic.php?id=534
            // disable text selection
            // look at http://forkjavascript.com/

            InternalCaptionDrag.Enabled = true;

            var BeforePosition = new Shared.Drawing.Point(0, 0);

            var FirstMove = false;


            InternalCaptionDrag.DragStart +=
                delegate
                {
                    TargetNoBorder.style.cursor = IStyle.CursorEnum.move;
                    CaptionForeground.style.cursor = IStyle.CursorEnum.move;
                    Native.Document.body.style.cursor = IStyle.CursorEnum.move;



                    BeforePosition = InternalCaptionDrag.Position;

                    FirstMove = true;

                    Capture = true;
                    InternalMouseCapured();

                };

            #region WindowState
            Action InternalEnterFullscreen =
                delegate
                {
                    //Console.WriteLine("InternalEnterFullscreen WindowState <- Maximized");
                    this.WindowState = FormWindowState.Maximized;

                };


            Action InternalExitFullscreen =
                delegate
                {
                    //Console.WriteLine("InternalEnterFullscreen WindowState <- Normal");
                    this.WindowState = FormWindowState.Normal;

                };
            #endregion

            #region DragMove
            InternalCaptionDrag.DragMove +=
                delegate
                {
                    if (FirstMove)
                    {
                        FirstMove = false;

                        if (TargetNoBorder.parentNode != TargetResizerPadding)
                        {
                            InternalExitFullscreen();


                        }

                        InternalUpdateZIndex(HTMLTarget);
                    }

                    var MinimizeY = Native.Window.Height - 26;

                    var y = Math.Min(MinimizeY, Math.Max(-4, InternalCaptionDrag.Position.Y));

                    //if (Native.Document.fullscreenElement == TargetNoBorder)
                    this.Location = new Point(InternalCaptionDrag.Position.X, y);



                    if (y < 0)
                    {
                        CaptionShadow.Show();
                    }
                    else
                    {

                        if (y < MinimizeY)
                            CaptionShadow.Hide();
                        else
                            CaptionShadow.Show();
                    }
                };
            #endregion

            #region DragStop
            InternalCaptionDrag.DragStop +=
                delegate
                {
                    Capture = false;
                    InternalMouseReleased();


                    //var Location = this.Location;

                    //this.Text = new { drag.Position.X, drag.Position.Y }.ToString();

                    TargetNoBorder.style.cursor = IStyle.CursorEnum.@default;
                    CaptionForeground.style.cursor = IStyle.CursorEnum.@default;
                    Native.Document.body.style.cursor = IStyle.CursorEnum.@default;

                    var MinimizeY = Native.Window.Height - 26;
                    var y = InternalCaptionDrag.Position.Y;

                    if (y < 0)
                    {
                        InternalCaptionDrag.Position = BeforePosition;
                        this.Location = new Point(BeforePosition.X, BeforePosition.Y);

                        InternalEnterFullscreen();
                    }
                    else if (y >= MinimizeY)
                    {
                        if (this.WindowState != FormWindowState.Minimized)
                        {
                            // do we need this?

                            var cs = this.ClientSize;
                            var ll = this.Location;

                            //drag.Position = BeforePosition;
                            //this.Location = new Point(BeforePosition.X, BeforePosition.Y);

                            this.WindowState = FormWindowState.Minimized;

                            //this.InternalRestoreClientSIze = cs;
                            this.InternalRestoreLocation = ll;
                        }
                    }
                    else
                    {
                        //Console.WriteLine("to Normal ? " + new { this.WindowState });

                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            var cs = this.ClientSize;
                            var ll = this.Location;

                            this.WindowState = FormWindowState.Normal;

                            //this.ClientSize = cs;
                            this.Location = ll;
                        }
                    }

                };
            #endregion


            InternalCaptionDrag.MiddleClick +=
                delegate
                {
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                        return;
                    }

                    if (TargetNoBorder.parentNode != TargetResizerPadding)
                        InternalExitFullscreen();
                    else
                        InternalEnterFullscreen();
                };
            #endregion

            CaptionForeground.ondblclick +=
                delegate
                {
                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                        return;
                    }


                    if (TargetNoBorder.parentNode != TargetResizerPadding)
                        InternalExitFullscreen();
                    else
                        InternalEnterFullscreen();
                };


            this.Size = new Size(400, 400);

            this.InternalStyler = new FormStyler
            {
                Context = this,

                Caption = Caption,
                CaptionContent = CaptionContent,

                CloseButton = CloseButton,
                CloseButtonContent = CloseButtonContent,

                TargetInnerBorder = TargetInnerBorder,
                TargetOuterBorder = TargetOuterBorder,

                ContentContainerPadding = ContentContainerPadding,

                TargetResizerPadding = TargetResizerPadding
            };

            FormStyler.RaiseAtFormCreated(
                this.InternalStyler
            );
        }

        public static int __FormZIndex = 0;

        #region Close
        public void Close()
        {
            var a = new FormClosingEventArgs(CloseReason.UserClosing, false);

            if (FormClosing != null)
                FormClosing(this, a);

            if (a.Cancel)
                return;

            foreach (var item in this.OwnedForms)
            {
                item.Close();
            }

            this.Owner = null;

            this.WindowState = FormWindowState.Normal;

            HTMLTarget.Orphanize();

            if (this.Closed != null)
                this.Closed(this, new EventArgs());

            RaiseFormClosed();
        }

        public void RaiseFormClosed()
        {
            if (this.FormClosed != null)
                this.FormClosed(this, new FormClosedEventArgs(CloseReason.None));

        }
        #endregion

        protected override Size SizeFromClientSize(Size clientSize)
        {
            return new Size(clientSize.Width + innerborder * 2, clientSize.Height + innerborder * 3 + 26);
        }




        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(64, 32);
            }
        }

        public bool TopMost { get; set; }
        public bool ControlBox { get; set; }

        public Size ClientSize
        {
            get
            {
                return base.ClientSize;
            }
            set
            {
                base.ClientSize = value;
            }
        }


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

        public List<Form> InternalOwnedForms = new List<Form>();

        public Form[] OwnedForms
        {
            get
            {
                return InternalOwnedForms.ToArray();
            }
        }

        #region Owner
        public __Form InternalOwner;
        public Form Owner
        {
            get { return InternalOwner; }
            set
            {
                if (InternalOwner != null)
                    InternalOwner.InternalOwnedForms.Remove(this);

                InternalOwner = value;

                if (InternalOwner != null)
                {
                    InternalOwner.InternalOwnedForms.Add(this);
                    InternalOwner.InternalUpdateZIndex();
                }
            }
        }
        #endregion

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
        bool InternalBeforeVisibleChangedDone = false;
        public override void InternalBeforeVisibleChanged()
        {
            if (InternalBeforeVisibleChangedDone)
                return;
            InternalBeforeVisibleChangedDone = true;

            InternalRaiseLoad();

            if (this.StartPosition == FormStartPosition.CenterScreen)
            {
                this.Location = new Point
                {
                    X = (Native.Window.Width - this.Width) / 2,
                    Y = Math.Max(0, (Native.Window.Height - this.Height) / 2)
                };
            }

            InternalUpdateZIndex(HTMLTarget);

            this.HTMLTarget.AttachToDocument();



            InternalRaiseShown();


            var length = this.Controls.Count;

            for (int i = 0; i < length; i++)
            {
                var item = this.Controls[i];

                if (item.TabIndex == 0)
                    item.Focus();
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

        public void InternalRaiseShown()
        {
            if (Shown != null)
                Shown(this, new EventArgs());
        }

        public event EventHandler Load;
        public event EventHandler Shown;
        #endregion

        public SizeGripStyle SizeGripStyle { get; set; }
        public FormStartPosition StartPosition { get; set; }

        int __PreviousWidth;
        int __PreviousHeight;

        #region WindowState

        static List<__Form> InternalMaximizedForms = new List<__Form>();

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



                        if (this.FormBorderStyle == global::System.Windows.Forms.FormBorderStyle.None)
                        {
                            this.ContentContainer.Orphanize().AttachTo(this.HTMLTarget);
                            this.ContentContainer.style.zIndex = 0;
                        }
                        else
                        {
                            this.TargetNoBorder.Orphanize().AttachTo(TargetResizerPadding);
                            this.TargetNoBorder.style.zIndex = 0;
                        }


                        InternalCaptionDrag.OffsetPosition.Y = 12;
                        InternalCaptionDrag.OffsetPosition.X = this.Width / 2;

                        if (InternalMaximizedForms.Count == 0)
                        {
                            // exit only if we are not maximized again
                            Native.Window.requestAnimationFrame +=
                                delegate
                                {
                                    if (InternalMaximizedForms.Count == 0)
                                    {
                                        //Console.WriteLine("set_WindowState exitFullscreen InternalMaximizedForms.Count == 0");


                                        Native.Document.exitFullscreen();
                                    }
                                };

                        }

                        Native.Window.requestAnimationFrame +=
                            delegate
                            {
                                InternalClientSizeChanged();
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

                        if (this.FormBorderStyle == global::System.Windows.Forms.FormBorderStyle.None)
                        {
                            this.ContentContainer.Orphanize().AttachToDocument();
                            InternalUpdateZIndex(this.ContentContainer);
                        }
                        else
                        {
                            this.TargetNoBorder.Orphanize().AttachToDocument();
                            InternalUpdateZIndex(this.TargetNoBorder);
                        }

                        CaptionShadow.Show();



                        if (InternalMaximizedForms.Count == 1)
                        {

                            Action<IEvent> onresize = null;

                            onresize =
                                delegate
                                {
                                    Native.Window.onresize -= onresize;

                                    InternalClientSizeChanged();

                                    #region UnmaximzeWhenLostFullscreen
                                    var UnmaximzeWhenLostFullscreen = default(Action);

                                    UnmaximzeWhenLostFullscreen =
                                        delegate
                                        {
                                            // how much cpu does this check take?
                                            // if significant then refactor 

                                            if (WindowState == FormWindowState.Normal)
                                            {
                                                return;
                                            }

                                            dynamic window = Native.Window;


                                            int innerHeight = window.innerHeight;
                                            int outerHeight = window.outerHeight;

                                            if (innerHeight != outerHeight)
                                            {
                                                // how to deal with zoom?

                                                Console.WriteLine("UnmaximzeWhenLostFullscreen " + new { innerHeight, outerHeight });

                                                this.WindowState = FormWindowState.Normal;
                                                return;
                                            }


                                            Native.Window.requestAnimationFrame += UnmaximzeWhenLostFullscreen;
                                        };

                                    Native.Window.requestAnimationFrame += UnmaximzeWhenLostFullscreen;
                                    #endregion
                                };

                            Native.Window.onresize += onresize;

                            //Console.WriteLine("set_WindowState requestFullscreen");
                            Native.Document.body.requestFullscreen();


                        }



                    }
                }
                else if (value == FormWindowState.Minimized)
                {
                    CaptionShadow.Show();

                    this.InternalRestoreLocation = this.Location;
                    this.InternalRestoreClientSIze = this.ClientSize;
                    this.MinimumSize = this.DefaultMinimumSize;
                    this.ClientSize = new Size(200, 0);

                    Native.Window.requestAnimationFrame +=
                        delegate
                        {
                            this.MinimumSize = this.Size;
                            this.MaximumSize = new Size(Native.Window.Width, this.Height);

                            this.Top = Native.Document.body.scrollHeight - 26;
                        };

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

    [Script]
    internal static partial class InternalExtensions
    {
        public static T WithDynamic<T>(this T e, Action<dynamic> y)
        {
            y(e);

            return e;
        }
    }
}
