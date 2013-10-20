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
        public IHTMLDiv CaptionShadow;
        IHTMLDiv CaptionContent;
        public IHTMLDiv CaptionForeground;

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

        public IHTMLDiv InternalCloseButton;
        public IHTMLDiv InternalCloseButtonContent;

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
        public IHTMLDiv TargetOuterBorder;

        [Description("Hide iframes from mouse to workaround event leaks.")]
        public static event Action InternalMouseCapured;
        public static event Action InternalMouseReleased;

        public IHTMLDiv ResizeGripElement;

        //IHTMLImage icon = "assets/ScriptCoreLib/jsc.ico";
        IHTMLImage icon = "assets/ScriptCoreLib/jsc.png";

        public ScriptCoreLib.JavaScript.Controls.DragHelper ResizeGripDrag;

        public bool ShowIcon
        {
            get { return icon.style.display != IStyle.DisplayEnum.none; }
            set { icon.Show(value); }
        }

        public __Form()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            var TargetElement = new IHTMLDiv();

            // thanks host, but we do our own resizers
            // http://stackoverflow.com/questions/13224184/css-resize-handles-with-resize-both-property
            // http://www.w3schools.com/cssref/css3_pr_resize.asp
            //TargetElement.style.resize = "none !important";

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

            (CaptionContent.style as dynamic).textOverflow = "ellipsis";

            //CaptionContent.style.WithDynamic(
            //    style => style.textOverflow = "ellipsis"
            //);

            CaptionContent.style.lineHeight = "26px";
            CaptionContent.style.left = "26px";
            CaptionContent.style.right = "30px";

            CaptionContent.style.overflow = IStyle.OverflowEnum.hidden;

            CaptionForeground = (IHTMLDiv)Caption.cloneNode(false);
            CaptionForeground.style.backgroundColor = ScriptCoreLib.Shared.Drawing.Color.FromRGB(255, 0, 255);
            CaptionForeground.style.Opacity = 0;
            CaptionForeground.className = "caption";
            CaptionForeground.style.right = "26px";

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
            //ContentContainerPadding.title = "ContentContainerPadding";
            ContentContainerPadding.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ContentContainerPadding.style.left = 0 + "px";
            ContentContainerPadding.style.top = (26 + innerborder + 0) + "px";
            ContentContainerPadding.style.right = 0 + "px";
            ContentContainerPadding.style.bottom = 0 + "px";

            ContentContainer = new IHTMLDiv().AttachTo(ContentContainerPadding);
            //ContentContainer.title = "ContentContainer";
            ContentContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ContentContainer.style.left = 0 + "px";
            ContentContainer.style.top = 0 + "px";
            ContentContainer.style.right = 0 + "px";
            ContentContainer.style.bottom = 0 + "px";
            ContentContainer.style.overflow = IStyle.OverflowEnum.hidden;
            ContentContainer.style.zIndex = 1000;

            var ContentContainerShadow = new IHTMLDiv().AttachTo(ContentContainerPadding);
            //ContentContainerShadow.title = "ContentContainerShadow";
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
            ResizeGripElement = new IHTMLDiv().AttachTo(ContentContainerPadding);
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
            ResizeGripDrag = new ScriptCoreLib.JavaScript.Controls.DragHelper(ResizeGripElement);

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
            InternalCloseButton = new IHTMLDiv { name = "CloseButton" };
            InternalCloseButton.style.textAlign = IStyle.TextAlignEnum.center;
            InternalCloseButton.style.fontWeight = "bold";
            InternalCloseButton.style.cursor = IStyle.CursorEnum.@default;

            InternalCloseButtonContent = new IHTMLDiv { }.AttachTo(InternalCloseButton);

            InternalCloseButtonContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            InternalCloseButtonContent.style.left = "0px";
            InternalCloseButtonContent.style.top = "0px";
            InternalCloseButtonContent.style.bottom = "0";
            InternalCloseButtonContent.style.right = "0";



            InternalCloseButtonContent.title = "Close";
            InternalCloseButtonContent.innerHTML = "&times";



            InternalCloseButton.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

            //CloseButton.style.backgroundColor = JSColor.System.ThreeDFace;
            InternalCloseButton.style.backgroundColor = "#D6D3CE";

            InternalCloseButton.style.height = "18px";
            InternalCloseButton.style.width = "18px";
            InternalCloseButton.style.right = (innerborder + 3) + "px";
            InternalCloseButton.style.top = (innerborder + 2) + "px";

            InternalCloseButton.style.borderWidth = "1px";
            InternalCloseButton.style.borderStyle = "solid";

            InternalCloseButton.style.borderRightColor = "#424142";
            InternalCloseButton.style.borderBottomColor = "#424142";
            InternalCloseButton.style.borderLeftColor = "#FFFFFF";
            InternalCloseButton.style.borderTopColor = "#FFFFFF";



            InternalCloseButtonContent.style.borderWidth = "1px";
            InternalCloseButtonContent.style.borderStyle = "solid";

            InternalCloseButtonContent.style.borderLeftColor = "#D6D3CE";
            InternalCloseButtonContent.style.borderTopColor = "#D6D3CE";
            InternalCloseButtonContent.style.borderRightColor = "#848284";
            InternalCloseButtonContent.style.borderBottomColor = "#848284";

            #region CloseButton onclick
            InternalCloseButton.onmouseover +=
                 delegate
                 {
                     InternalCloseButtonContent.style.color = JSColor.Red;
                 };

            InternalCloseButton.onmouseout +=
                delegate
                {
                    InternalCloseButtonContent.style.color = JSColor.None;
                };

            InternalCloseButton.onmousedown +=
                e =>
                {
                    e.stopPropagation();
                    e.preventDefault();
                };

            InternalCloseButton.onclick +=
                delegate
                {


                    InternalClose(reason: CloseReason.UserClosing);
                };
            #endregion

            #endregion

            TargetNoBorder.appendChild(
                Caption, CaptionShadow, CaptionContent,
                icon, CaptionForeground,
                ContentContainerPadding, InternalCloseButton
            );

            CaptionForeground.oncontextmenu +=
                e =>
                {
                    // can we show our own menu?
                    e.preventDefault();
                    e.stopPropagation();
                };

            #region InternalCaptionDrag
            InternalCaptionDrag = new ScriptCoreLib.JavaScript.Controls.DragHelper(CaptionForeground);

            // http://forum.mootools.net/topic.php?id=534
            // disable text selection
            // look at http://forkjavascript.com/

            InternalCaptionDrag.Enabled = true;
            #endregion

            var BeforePosition = new Shared.Drawing.Point(0, 0);

            var FirstMove = false;
            var AnyMove = false;

            var DragStartMaximized = false;
            var DragStartMaximizedY = 0;

            #region DragStart
            InternalCaptionDrag.DragStart +=
                delegate
                {
                    TargetNoBorder.style.cursor = IStyle.CursorEnum.move;
                    CaptionForeground.style.cursor = IStyle.CursorEnum.move;
                    Native.Document.body.style.cursor = IStyle.CursorEnum.move;



                    BeforePosition = InternalCaptionDrag.Position;

                    FirstMove = true;
                    DragStartMaximized = this.WindowState == FormWindowState.Maximized;
                    //if (DragStartMaximized)
                    //    DragStartMaximizedY = this.InternalRestoreLocation.Y;

                    // offsetWidth: 1045

                    Console.WriteLine(new { BeforePosition, DragStartMaximized });

                    Capture = true;
                    InternalMouseCapured();

                };
            #endregion

            #region WindowState
            Action InternalEnterFullscreen =
                delegate
                {
                    Console.WriteLine("InternalEnterFullscreen WindowState <- Maximized");
                    this.WindowState = FormWindowState.Maximized;

                };


            Action InternalExitFullscreen =
                delegate
                {
                    Console.WriteLine("InternalExitFullscreen WindowState <- Normal");

                    this.WindowState = FormWindowState.Normal;

                };
            #endregion


            #region DragMove
            InternalCaptionDrag.DragMove +=
                delegate
                {
                    if (!Capture)
                        return;


                    //Console.WriteLine(new { InternalCaptionDrag.Position, BeforePosition, DragStartMaximized });

                    // { Position = [192, 205.5], BeforePosition = [192, 205.5], DragStartMaximized = true } 

                    if (InternalCaptionDrag.Position.X == BeforePosition.X)
                        if (InternalCaptionDrag.Position.Y == BeforePosition.Y)
                        {
                            //Console.WriteLine("FirstMove without move");
                            return;
                        }

                    // { InternalHostHeight = 0, y = 301 } 
                    var MinimizeY = InternalHostHeight - 26;
                    var y = InternalCaptionDrag.Position.Y;
                    Console.WriteLine(new { InternalHostHeight, y });
                    y = Math.Min(MinimizeY, Math.Max(-4, y));


                    if (FirstMove)
                    {
                        FirstMove = false;

                        if (this.WindowState == FormWindowState.Maximized)
                        {
                            //Capture = false;

                            //this.InternalRestoreLocation = new Point(InternalCaptionDrag.Position.X, y);


                            // we have a small glitch here
                            Console.WriteLine("InternalCaptionDrag.DragMove FirstMove " + new { this.InternalRestoreLocation });

                            InternalExitFullscreen();

                            //return;
                        }
                    }


                    AnyMove = true;


                    //if (Native.Document.fullscreenElement == TargetNoBorder)
                    this.Location = new Point(InternalCaptionDrag.Position.X, y);

                    Console.WriteLine(new { Location });

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
                    if (!Capture)
                        return;


                    Capture = false;
                    InternalMouseReleased();

                    if (!AnyMove)
                        return;

                    //var Location = this.Location;

                    //this.Text = new { drag.Position.X, drag.Position.Y }.ToString();

                    TargetNoBorder.style.cursor = IStyle.CursorEnum.@default;
                    CaptionForeground.style.cursor = IStyle.CursorEnum.@default;
                    Native.document.body.style.cursor = IStyle.CursorEnum.@default;


                    var MinimizeY = InternalHostHeight - 26;
                    var y = InternalCaptionDrag.Position.Y;

                    //if (this.HTMLTarget.parentNode != Native.document.body)
                    //{
                    //    // this window is in a nested element, or even in another window.
                    //    // bail!

                    //    return;
                    //}

                    if (y < 0)
                    {
                        InternalCaptionDrag.Position = BeforePosition;

                        InternalEnterFullscreen();

                        this.InternalRestoreLocation = new Point(BeforePosition.X, BeforePosition.Y);
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


            #region MiddleClick
            InternalCaptionDrag.MiddleClick +=
                delegate
                {
                    Capture = false;

                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                        return;
                    }

                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        Console.WriteLine("InternalCaptionDrag.MiddleClick  InternalExitFullscreen");

                        InternalExitFullscreen();
                    }
                    else
                    {
                        Console.WriteLine("InternalCaptionDrag.MiddleClick  InternalEnterFullscreen");
                        InternalEnterFullscreen();
                    }
                };
            #endregion


            #region ondblclick
            CaptionForeground.ondblclick +=
                delegate
                {
                    Capture = false;

                    if (this.WindowState == FormWindowState.Minimized)
                    {
                        this.WindowState = FormWindowState.Normal;
                        return;
                    }


                    if (this.WindowState == FormWindowState.Maximized)
                        InternalExitFullscreen();
                    else
                        InternalEnterFullscreen();
                };
            #endregion


            this.Size = new Size(400, 400);

            this.InternalStyler = new FormStyler
            {
                Context = this,

                Caption = Caption,
                CaptionContent = CaptionContent,
                CaptionShadow = CaptionShadow,

                CloseButton = InternalCloseButton,
                CloseButtonContent = InternalCloseButtonContent,

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
            }
        }

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

        // allow to create system window and attach to that instead
        // see also: X:\jsc.svn\examples\javascript\chrome\ChromeAppWindowFrameNoneExperiment\ChromeAppWindowFrameNoneExperiment\Application.cs
        public static Action<__Form, Action<bool>> InternalHTMLTargetAttachToDocument =
            (that, yield) =>
            {
                if (that.HTMLTarget.parentNode == null)
                    that.HTMLTarget.AttachTo(
                        Native.document.body.parentNode
                    );

                // animate!
                yield(true);
            };

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

                // IE fk u
                var value = Native.window.Height;

                if (host == Native.document.body.parentNode)
                {
                    //if (host.clientHeight > 0)
                    //    if (host.scrollHeight > 0)
                    //        value = host.clientHeight < host.scrollHeight ? host.scrollHeight : host.clientHeight;
                }
                else
                {
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
                        Console.WriteLine("WindowState do InternalMaximizedForms " + new { this.Location, this.ClientSize });

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

                                Native.window.onframe +=
                                   delegate
                                   {
                                       // overhead?
                                       //if (!anitimer.Enabled)
                                       if (!anitimer_Enabled)
                                           return;

                                       InternalClientSizeChanged();
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
                                    InternalClientSizeChanged();


                                    if (this.WindowState == FormWindowState.Maximized)
                                        return;


                                    Native.window.onresize -= onresize;

                                };

                            Native.window.onresize += onresize;

                            InternalClientSizeChanged();

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








        #region Close
        public void Close()
        {
            InternalClose();
        }

        public event Action<FormClosingEventArgs> InternalBeforeFormClosing;

        public void InternalClose(CloseReason reason = CloseReason.None)
        {
            var a = new FormClosingEventArgs(reason, false);

            if (InternalBeforeFormClosing != null)
                InternalBeforeFormClosing(a);

            if (a.Cancel)
                return;

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


            //-webkit-transition: -webkit-transform 200ms linear; transition: -webkit-transform 200ms linear;
            // -webkit-filter: opacity(0.8); 
            // -webkit-transform: scale(0.7);


            #region fadeout
            this.HTMLTarget.style.transition = "none";
            (this.HTMLTarget.style as dynamic).webkitFilter = " opacity(1.0)";
            (this.HTMLTarget.style as dynamic).webkitTransform = " scale(1.0)";

            this.HTMLTarget.style.transition = "-webkit-transform 50ms linear, -webkit-filter 50ms linear";

            (this.HTMLTarget.style as dynamic).webkitFilter = " opacity(0.0)";
            (this.HTMLTarget.style as dynamic).webkitTransform = " scale(0.95)";

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {

                    HTMLTarget.Orphanize();

                    this.HTMLTarget.style.transition = "none";

                    (this.HTMLTarget.style as dynamic).webkitFilter = "";
                    (this.HTMLTarget.style as dynamic).webkitTransform = "";
                }
            ).StartTimeout(100);
            #endregion


            // allow to be showed again?!
            InternalBeforeVisibleChangedDone = false;


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







        bool InternalBeforeVisibleChangedDone = false;
        public override void InternalBeforeVisibleChanged(Action yield)
        {
            Console.WriteLine("__Form.InternalBeforeVisibleChanged");

            if (InternalBeforeVisibleChangedDone)
            {
                yield();
                return;
            }

            InternalBeforeVisibleChangedDone = true;




            InternalHTMLTargetAttachToDocument(
                this,
                Animate =>
                {

                    #region CenterScreen
                    if (this.WindowState == FormWindowState.Normal)
                        if (this.StartPosition == FormStartPosition.CenterScreen)
                        {
                            this.Width = Math.Min(InternalHostWidth, this.Width);
                            this.Height = Math.Min(InternalHostHeight, this.Height);

                            //Console.WriteLine(new { this.height, host_Bounds });

                            this.Location = new Point
                            {
                                X = (InternalHostWidth - this.Width) / 2,
                                Y = Math.Max(0, (InternalHostHeight - this.Height) / 2)
                            };
                        }
                    #endregion

                    #region fadein

                    // allow animation to be skipped by custom hosts
                    // like chrom AppWindow
                    // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs
                    if (Animate)
                    {
                        // http://www.w3schools.com/css3/css3_transitions.asp
                        // X:\jsc.svn\examples\javascript\css\CSSShaderGrayScale\CSSShaderGrayScale\Application.cs

                        this.HTMLTarget.style.transition = "none";

                        var old_webkitFilter = (this.HTMLTarget.style as dynamic).webkitFilter;

                        (this.HTMLTarget.style as dynamic).webkitFilter = " opacity(0.1)";
                        (this.HTMLTarget.style as dynamic).webkitTransform = " scale(0.9)";


                        new ScriptCoreLib.JavaScript.Runtime.Timer(
                            delegate
                            {
                                // InternalBeforeVisibleChanged before requestAnimationFrame { node = [object HTMLDocument], ownerDocument = [object HTMLDocument], same = true }
                                Console.WriteLine("InternalBeforeVisibleChanged after requestAnimationFrame");

                                this.HTMLTarget.style.transition = "-webkit-transform 150ms linear, -webkit-filter 150ms linear";

                                (this.HTMLTarget.style as dynamic).webkitFilter = " opacity(1.0)";
                                (this.HTMLTarget.style as dynamic).webkitTransform = " scale(1.0)";

                                new ScriptCoreLib.JavaScript.Runtime.Timer(
                                    delegate
                                    {
                                        this.HTMLTarget.style.transition = "none";

                                        (this.HTMLTarget.style as dynamic).webkitFilter = old_webkitFilter;
                                        (this.HTMLTarget.style as dynamic).webkitTransform = "";
                                    }
                                ).StartTimeout(150);
                            }
                        ).StartTimeout(11);
                    }
                    #endregion


                    Console.WriteLine("InternalRaiseLoad");
                    InternalRaiseLoad();

                    InternalUpdateZIndex(HTMLTarget);

                    Console.WriteLine("InternalRaiseShown");
                    InternalRaiseShown();

                    // let child controls know
                    yield();


                    #region Focus
                    var length = this.Controls.Count;

                    for (int i = 0; i < length; i++)
                    {
                        var item = this.Controls[i];

                        if (item.TabIndex == 0)
                            item.Focus();
                    }
                    #endregion


                    this.HTMLTarget.requestAnimationFrame +=
                        delegate
                        {
                            InternalWindowStateAnimated = true;
                        };

                }
            );


        }



    }

    [Script]
    public static partial class InternalDiagnostics
    {
        public static bool BreakAtWindowState;
    }


}
