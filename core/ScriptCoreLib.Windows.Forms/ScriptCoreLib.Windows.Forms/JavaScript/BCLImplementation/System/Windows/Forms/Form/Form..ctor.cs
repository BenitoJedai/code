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
using System.Xml.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    public partial class __Form
    {
        [Obsolete("pending css guidline review")]
        public CSSStyleRuleMonkier outer_css;

        public Stopwatch ConstructorStopwatch = Stopwatch.StartNew();

        public __Form()
        {

            Console.WriteLine("event: enter new " + this.GetType().Name + "()");
            //IStyleSheet.all.disabled = true;

            this.InternalTrackInitializeComponents = delegate
            {
                ConstructorStopwatch.Stop();

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140205
                // will this help us? no?
                //IStyleSheet.all.disabled = false;

                //Console.WriteLine("event: new " + new { this.GetType().Name } + " InternalResumeLayout " + new { ConstructorStopwatch.ElapsedMilliseconds });
                Console.WriteLine("event: new " + this.GetType().Name + "() " + new { ConstructorStopwatch.ElapsedMilliseconds });


                //474ms new { Name = FooActivity } InternalResumeLayout { ElapsedMilliseconds = 464 } 


                new XAttribute(
                    "ConstructorStopwatch",
                    new { ConstructorStopwatch.ElapsedMilliseconds }.ToString()
                ).AttachTo(this.HTMLTargetRef);
            };

            //25:584ms event: exit DataGridView .ctor { ElapsedMilliseconds = 525 } view-source:35983
            //48:1043ms event: exit DataGridView .ctor { ElapsedMilliseconds = 445 } view-source:35983
            //95:1509ms event: exit DataGridView .ctor { ElapsedMilliseconds = 420 } view-source:35983
            //96:1713ms at FlowLayoutPanel InternalResumeLayout { ElapsedMilliseconds = 628, Name = flowLayoutPanel1, Controls = 2 } view-source:35955
            //97:1724ms at FlowLayoutPanel InternalResumeLayout { ElapsedMilliseconds = 1674, Name = flowLayoutPanel1, Controls = 9 } view-source:35955
            //97:1724ms at FlowLayoutPanel InternalResumeLayout { ElapsedMilliseconds = 1137, Name = flowLayoutPanel2, Controls = 3 } view-source:35996
            //97:1724ms at FlowLayoutPanel InternalResumeLayout { ElapsedMilliseconds = 1128, Name = flowLayoutPanel3, Controls = 1 } view-source:35996
            //97:1724ms event: new FooActivity() { ElapsedMilliseconds = 1706 } 

            this.StartPosition = FormStartPosition.CenterScreen;

            var TargetElement = new IHTMLDiv
            {

                // do this ahead of time
                // type inheritance?
                className = typeof(Form).Name + " " + this.GetType().Name
            };

            // thanks host, but we do our own resizers
            // http://stackoverflow.com/questions/13224184/css-resize-handles-with-resize-both-property
            // http://www.w3schools.com/cssref/css3_pr_resize.asp
            //TargetElement.style.resize = "none !important";

            TargetElement.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            TargetElement.style.left = "0px";
            TargetElement.style.top = "0px";

            HTMLTarget = TargetElement;

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131222-form
            outer_css = TargetElement.css;

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

            // ?
            //TargetInnerBorder.style.backgroundColor = "#D6D3CE";

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

            Caption.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            Caption.style.left = 0 + "px";
            Caption.style.top = 0 + "px";
            Caption.style.right = 0 + "px";
            Caption.style.height = "26px";
            Caption.style.margin = "0px";

            //Caption.style.paddingTop = "6px";
            //Caption.style.paddingLeft = "26px";
            Caption.style.font = new Font("Segoe UI", 9.0F, FontStyle.Regular, GraphicsUnit.Point, 0).ToCssString();

            CaptionShadow = (IHTMLDiv)Caption.cloneNode(false);
            CaptionShadow.style.backgroundColor = JSColor.Black;
            CaptionShadow.style.Opacity = 0;



            CaptionContent = new IHTMLHeader1();
            CaptionContent.style.color = Shared.Drawing.Color.White;

            CaptionContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            CaptionContent.style.left = 0 + "px";
            CaptionContent.style.top = 0 + "px";
            CaptionContent.style.right = 0 + "px";
            CaptionContent.style.height = "26px";
            CaptionContent.style.margin = "0px";

            CaptionContent.style.fontSize = "1em";
            CaptionContent.style.fontWeight = "100";

            CaptionContent.style.backgroundColor = JSColor.None;

            CaptionContent.style.font = DefaultFont.ToCssString();

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

            // is this needed?
            //ContentContainer.style.zIndex = 1000;

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
                    // is this enough to capture resizer in a chrome AppWindow?
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



            #region CloseButton
            InternalCloseButton = new IHTMLDiv { name = "CloseButton" };
            InternalCloseButton.style.textAlign = IStyle.TextAlignEnum.center;
            InternalCloseButton.style.fontWeight = "bold";
            //InternalCloseButton.style.cursor = IStyle.CursorEnum.@default;
            InternalCloseButton.style.cursor = IStyle.CursorEnum.pointer;

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

            InternalCloseButton.css.hover[IHTMLElement.HTMLElementEnum.div].style.color = "red";

            //InternalCloseButton.onmouseover +=
            //     delegate
            //     {
            //         InternalCloseButtonContent.style.color = JSColor.Red;
            //     };

            //InternalCloseButton.onmouseout +=
            //    delegate
            //    {
            //        InternalCloseButtonContent.style.color = JSColor.None;
            //    };

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

                ContentContainerPadding,



                 Caption, CaptionShadow, CaptionContent,
                icon, CaptionForeground

                , InternalCloseButton

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



            this.MaximizeBox = true;
            this.InternalControlBox = true;

            #region DragStart
            InternalCaptionDrag.DragStartValidate +=
                e =>
                {
                    // if there is no max, then there is no restore. then do not allow to
                    // drag back either
                    e.Value = this.MaximizeBox;
                };

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

                    //Console.WriteLine(new { BeforePosition, DragStartMaximized });

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
                    //var MinimizeY = InternalHostHeight - 26;
                    var y = InternalCaptionDrag.Position.Y;
                    //Console.WriteLine(new { InternalHostHeight, y });
                    //y = Math.Min(MinimizeY, Math.Max(-4, y));
                    y = Math.Max(-4, y);


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

                    //Console.WriteLine(new { Location });

                    if (y < 0)
                    {
                        CaptionShadow.Show();
                    }
                    //else
                    //{

                    //    if (y < MinimizeY)
                    //        CaptionShadow.Hide();
                    //    else
                    //        CaptionShadow.Show();
                    //}
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

                    // tested by
                    // X:\jsc.svn\examples\javascript\MatrixTransformBExample\MatrixTransformBExperiment\Application.cs

                    //var Location = this.Location;

                    //this.Text = new { drag.Position.X, drag.Position.Y }.ToString();

                    TargetNoBorder.style.cursor = IStyle.CursorEnum.@default;
                    CaptionForeground.style.cursor = IStyle.CursorEnum.@default;
                    Native.document.body.style.cursor = IStyle.CursorEnum.@default;


                    //var MinimizeY = InternalHostHeight - 26;
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
                    //else if (y >= MinimizeY)
                    //{
                    //    if (this.WindowState != FormWindowState.Minimized)
                    //    {
                    //        // do we need this?

                    //        var cs = this.ClientSize;
                    //        var ll = this.Location;

                    //        //drag.Position = BeforePosition;
                    //        //this.Location = new Point(BeforePosition.X, BeforePosition.Y);

                    //        this.WindowState = FormWindowState.Minimized;

                    //        //this.InternalRestoreClientSIze = cs;
                    //        this.InternalRestoreLocation = ll;
                    //    }
                    //}
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

                    if (!this.MaximizeBox)
                    {
                        // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs
                        // not supposed to restore
                        return;
                    }

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

                    if (!this.MaximizeBox)
                    {
                        // X:\jsc.svn\examples\javascript\forms\FormsWithVisibleTitle\FormsWithVisibleTitle\Application.cs
                        // not supposed to restore
                        return;
                    }

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






            // to be replaiced with #shadow-root content select?
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


            //HTMLTarget.style.backgroundColor = "#B0B0B0";
            this.BackColor = Color.FromArgb(0xD6, 0xD3, 0xCE);

            this.Size = new Size(400, 400);


            FormStyler.RaiseAtFormCreated(
                this.InternalStyler
            );


            //this.ControlAdded +=
            //    (sender, args) =>
            //    {
            //        if (InternalTrackInitializeComponents)
            //        {

            //            // report what controls are being added and 

            //            Console.WriteLine("at Form ControlAdded " + new
            //            {
            //                ConstructorStopwatch.ElapsedMilliseconds,
            //                Control = args.Control.GetType().Name,
            //                this.GetType().Name
            //            });
            //        }
            //    };

            //312ms at Form InternalResumeLayout { FormConstructorStopwatch = 0.00:00:00, Name = FooActivity } 
            //Console.WriteLine("exit Form .ctor " + new { ConstructorStopwatch.ElapsedMilliseconds, this.GetType().Name });
        }

        //public bool InternalTrackInitializeComponents = true;

        public Action InternalTrackInitializeComponents;

        public override void InternalResumeLayout(bool performLayout)
        {
            if (InternalTrackInitializeComponents != null)
                InternalTrackInitializeComponents();

            InternalTrackInitializeComponents = null;


        }
    }


}
