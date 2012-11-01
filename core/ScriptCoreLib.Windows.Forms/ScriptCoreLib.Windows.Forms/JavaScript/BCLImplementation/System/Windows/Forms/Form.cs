using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Runtime;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    internal class __Form : __ContainerControl
    {
        // alternative service providers:
        // see: http://dhtmlx.com/docs/products/dhtmlxWindows/index.shtml

        object __FormTypeHint;

        public event FormClosedEventHandler FormClosed;
        public event FormClosingEventHandler FormClosing;

        // http://msdn.microsoft.com/en-us/library/system.windows.forms.form.closed.aspx
        [Obsolete(" use the FormClosed event instead.")]
        public event EventHandler Closed;

        public IHTMLDiv HTMLTarget { get; set; }

        IHTMLDiv caption = new IHTMLDiv();
        IHTMLDiv caption_foreground;

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


        ScriptCoreLib.JavaScript.Controls.DragHelper drag;
        IHTMLDiv CloseButton;


        const int innerborder = 1;

        IHTMLDiv TargetNoBorder;

        protected override void InternalSetBackgroundColor(Color value)
        {
            TargetOuterBorder.style.backgroundColor = value.ToString();
            ContentContainer.style.backgroundColor = value.ToString();

            // for firefox fullscren
            TargetNoBorder.style.backgroundColor = value.ToString();
        }

        static List<__Form> InternalMaximizedForms = new List<__Form>();
        IHTMLDiv TargetResizerPadding;
        IHTMLDiv TargetOuterBorder;

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
            TargetOuterBorder.style.borderColor = JSColor.System.ThreeDDarkShadow;
            TargetOuterBorder.style.borderLeftColor = JSColor.System.ButtonFace;
            TargetOuterBorder.style.borderTopColor = JSColor.System.ButtonFace;

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
            TargetInnerBorder.style.borderLeftColor = JSColor.System.ButtonHighlight;
            TargetInnerBorder.style.borderTopColor = JSColor.System.ButtonHighlight;
            TargetInnerBorder.style.borderRightColor = JSColor.System.ButtonShadow;
            TargetInnerBorder.style.borderBottomColor = JSColor.System.ButtonShadow;

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
            IHTMLImage icon = "assets/ScriptCoreLib.Windows.Forms/App.ico";

            icon.style.SetLocation(5, 5, 16, 16);

            //caption.style.backgroundColor = JSColor.System.ActiveCaption;
            caption.style.backgroundColor = JSColor.FromRGB(0, 0, 0x7F);
            caption.style.color = Shared.Drawing.Color.White;
            caption.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            caption.style.left = 0 + "px";
            caption.style.top = 0 + "px";
            caption.style.right = 0 + "px";
            caption.style.height = "20px";
            caption.style.paddingTop = "6px";
            caption.style.paddingLeft = "26px";
            caption.style.font = new Font("Segoe UI", 9.0F, FontStyle.Regular, GraphicsUnit.Point, 0).ToCssString();

            caption_foreground = (IHTMLDiv)caption.cloneNode(false);
            caption_foreground.style.backgroundColor = ScriptCoreLib.Shared.Drawing.Color.FromRGB(255, 0, 255);
            caption_foreground.style.Opacity = 0;
            caption_foreground.className = "caption";

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

            ContentContainerPadding.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ContentContainerPadding.style.left = 0 + "px";
            ContentContainerPadding.style.top = (26 + innerborder + 0) + "px";
            ContentContainerPadding.style.right = 0 + "px";
            ContentContainerPadding.style.bottom = 0 + "px";

            ContentContainer = new IHTMLDiv().AttachTo(ContentContainerPadding);
            ContentContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ContentContainer.style.left = 0 + "px";
            ContentContainer.style.top = 0 + "px";
            ContentContainer.style.right = 0 + "px";
            ContentContainer.style.bottom = 0 + "px";

            ContentContainer.style.overflow = IStyle.OverflowEnum.hidden;



            var ResizeGripElement = new IHTMLDiv().AttachTo(ContentContainerPadding);
            ResizeGripElement.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            ResizeGripElement.style.width = "12px";
            ResizeGripElement.style.height = "12px";
            ResizeGripElement.style.bottom = "0px";
            ResizeGripElement.style.right = "0px";
            ResizeGripElement.style.cursor = IStyle.CursorEnum.se_resize;
            new IHTMLImage { src = "assets/ScriptCoreLib.Windows.Forms/FormResizeGrip.png" }.ToBackground(ResizeGripElement);

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

            ResizeGripDrag.DragMove +=
                delegate
                {
                    this.Size = new Size(ResizeGripDrag.Position.X, ResizeGripDrag.Position.Y);
                };
            #endregion

            //HTMLTarget.style.backgroundColor = "#B0B0B0";
            this.BackColor = SystemColors.ButtonFace;

            #region CloseButton
            CloseButton = new IHTMLDiv { name = "CloseButton" };
            CloseButton.style.textAlign = IStyle.TextAlignEnum.center;
            CloseButton.style.fontWeight = "bold";
            CloseButton.style.cursor = IStyle.CursorEnum.@default;
            CloseButton.onmouseover +=
                delegate
                {
                    CloseButton.style.color = JSColor.Red;
                };

            CloseButton.onmouseout +=
                delegate
                {
                    CloseButton.style.color = JSColor.None;
                };

            var CloseButtonContent = new IHTMLDiv { }.AttachTo(CloseButton);

            CloseButtonContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            CloseButtonContent.style.left = "0px";
            CloseButtonContent.style.top = "0px";
            CloseButtonContent.style.bottom = "0";
            CloseButtonContent.style.right = "0";



            CloseButtonContent.title = "Close";
            CloseButtonContent.innerHTML = "&times";


            CloseButton.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            CloseButton.style.backgroundColor = JSColor.System.ThreeDFace;

            CloseButton.style.height = "18px";
            CloseButton.style.width = "18px";
            CloseButton.style.right = (innerborder + 3) + "px";
            CloseButton.style.top = (innerborder + 2) + "px";

            CloseButton.style.borderWidth = "1px";
            CloseButton.style.borderStyle = "solid";
            CloseButton.style.borderColor = JSColor.System.ThreeDDarkShadow;
            CloseButton.style.borderLeftColor = JSColor.System.ButtonHighlight;
            CloseButton.style.borderTopColor = JSColor.System.ButtonHighlight;



            CloseButtonContent.style.borderWidth = "1px";
            CloseButtonContent.style.borderStyle = "solid";
            CloseButtonContent.style.borderLeftColor = JSColor.System.ButtonFace;
            CloseButtonContent.style.borderTopColor = JSColor.System.ButtonFace;

            CloseButtonContent.style.borderRightColor = JSColor.System.ButtonShadow;
            CloseButtonContent.style.borderBottomColor = JSColor.System.ButtonShadow;


            CloseButton.onclick +=
                delegate
                {
                    Close();
                };
            #endregion

            TargetNoBorder.appendChild(caption, icon, caption_foreground, ContentContainerPadding, CloseButton);


            drag = new ScriptCoreLib.JavaScript.Controls.DragHelper(caption_foreground);

            // http://forum.mootools.net/topic.php?id=534
            // disable text selection
            // look at http://forkjavascript.com/

            drag.Enabled = true;

            var BeforePosition = new Shared.Drawing.Point(0, 0);

            var FirstMove = false;

            drag.DragStart +=
                delegate
                {
                    TargetNoBorder.style.cursor = IStyle.CursorEnum.move;
                    caption_foreground.style.cursor = IStyle.CursorEnum.move;
                    Native.Document.body.style.cursor = IStyle.CursorEnum.move;



                    BeforePosition = drag.Position;

                    FirstMove = true;
                };


            Action InternalEnterFullscreen =
                delegate
                {
                    this.WindowState = FormWindowState.Maximized;

                };


            Action InternalExitFullscreen =
                delegate
                {
                    this.WindowState = FormWindowState.Normal;

                };


            drag.DragMove +=
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

                    var y = Math.Max(-4, drag.Position.Y);

                    //if (Native.Document.fullscreenElement == TargetNoBorder)
                    this.Location = new Point(drag.Position.X, y);



                    if (y < 0)
                    {
                        caption.style.backgroundColor = JSColor.Black;

                    }
                    else
                    {
                        caption.style.backgroundColor = JSColor.FromRGB(0, 0, 0x7F);
                    }
                };

            drag.DragStop +=
                delegate
                {
                    //var Location = this.Location;

                    //this.Text = new { drag.Position.X, drag.Position.Y }.ToString();

                    TargetNoBorder.style.cursor = IStyle.CursorEnum.@default;
                    caption_foreground.style.cursor = IStyle.CursorEnum.@default;
                    Native.Document.body.style.cursor = IStyle.CursorEnum.@default;

                    if (drag.Position.Y < 0)
                    {
                        drag.Position = BeforePosition;
                        this.Location = new Point(BeforePosition.X, BeforePosition.Y);

                        InternalEnterFullscreen();
                    }

                };


            drag.MiddleClick +=
                delegate
                {
                    if (TargetNoBorder.parentNode != TargetResizerPadding)
                        InternalExitFullscreen();
                    else
                        InternalEnterFullscreen();
                };

            caption_foreground.ondblclick +=
                delegate
                {


                    if (TargetNoBorder.parentNode != TargetResizerPadding)
                        InternalExitFullscreen();
                    else
                        InternalEnterFullscreen();
                };


            this.Size = new Size(400, 400);
        }

        public static int __FormZIndex = 0;

        #region Close
        public void Close()
        {
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
                return caption.innerText;
            }
            set
            {
                caption.innerText = value;
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

            drag.Position = new Shared.Drawing.Point(Location.X, Location.Y);

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
                    Y = (Native.Window.Height - this.Height) / 2
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
        public FormWindowState WindowState
        {
            get
            {
                if (InternalMaximizedForms.Contains(this))
                    return FormWindowState.Maximized;

                return FormWindowState.Normal;
            }
            set
            {
                if (WindowState == value)
                    return;

                if (value == FormWindowState.Normal)
                {
                    if (InternalMaximizedForms.Contains(this))
                    {
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

                        caption.style.backgroundColor = JSColor.FromRGB(0, 0, 0x7F);

                        drag.OffsetPosition.Y = 12;
                        drag.OffsetPosition.X = this.Width / 2;

                        if (InternalMaximizedForms.Count == 0)
                        {
                            // exit only if we are not maximized again
                            Native.Window.requestAnimationFrame +=
                                delegate
                                {
                                    if (InternalMaximizedForms.Count == 0)
                                        Native.Document.exitFullscreen();
                                };

                        }

                        Native.Window.requestAnimationFrame +=
                            delegate
                            {

                                InternalChildrenAnchorUpdate(
                                        this.width,
                                      this.height,
                                      __PreviousWidth,
                                      __PreviousHeight
                               );


                            };
                    }

                }
                else if (value == FormWindowState.Maximized)
                {
                    if (!InternalMaximizedForms.Contains(this))
                    {
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

                        caption.style.backgroundColor = JSColor.Black;

                        if (InternalMaximizedForms.Count == 1)
                        {
                            Native.Document.body.requestFullscreen();


                        }

                        Native.Window.requestAnimationFrame +=
                            delegate
                            {
                                __PreviousWidth = ContentContainer.Bounds.Width;
                                __PreviousHeight = ContentContainer.Bounds.Height;

                                InternalChildrenAnchorUpdate(
                                  __PreviousWidth,
                                  __PreviousHeight,
                                  this.width,
                                  this.height
                               );


                            };
                    }
                }



                InternalRaiseLocationChanged();
            }
        }
        #endregion

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

    }
}
