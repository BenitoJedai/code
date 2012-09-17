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

        public event EventHandler Closed;

        public IHTMLDiv HTMLTarget { get; set; }

        IHTMLDiv caption = new IHTMLDiv();
        IHTMLDiv caption_foreground;

        IHTMLDiv container = new IHTMLDiv();

        public override IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return container;

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

        public __Form()
        {
            #region target0
            var target0 = new IHTMLDiv();
            //HTMLTarget.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            target0.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            target0.style.left = "0px";
            target0.style.top = "0px";

            target0.style.borderWidth = "1px";
            target0.style.borderStyle = "solid";
            target0.style.borderColor = JSColor.System.ThreeDDarkShadow;
            target0.style.borderLeftColor = JSColor.System.ButtonFace;
            target0.style.borderTopColor = JSColor.System.ButtonFace;

            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            target0.style.padding = "0";
            #endregion

            #region target1
            var target1 = new IHTMLDiv().AttachTo(target0);
            ////HTMLTarget.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            target1.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            target1.style.left = "0px";
            target1.style.top = "0px";
            target1.style.bottom = "0";
            target1.style.right = "0";

            target1.style.borderWidth = "1px";
            target1.style.borderStyle = "solid";
            target1.style.borderLeftColor = JSColor.System.ButtonHighlight;
            target1.style.borderTopColor = JSColor.System.ButtonHighlight;
            target1.style.borderRightColor = JSColor.System.ButtonShadow;
            target1.style.borderBottomColor = JSColor.System.ButtonShadow;

            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            target1.style.padding = "0";
            #endregion

            target0.style.boxShadow = "black 3px 3px 6px -3px";

            HTMLTarget = target0;

            #region caption
            IHTMLImage icon = "assets/ScriptCoreLib.Windows.Forms/App.ico";

            icon.style.SetLocation(7, 7, 16, 16);

            //caption.style.backgroundColor = JSColor.System.ActiveCaption;
            caption.style.backgroundColor = JSColor.FromRGB(0, 0, 0x7F);
            caption.style.color = Shared.Drawing.Color.White;
            caption.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            caption.style.left = innerborder + "px";
            caption.style.top = innerborder + "px";
            caption.style.right = innerborder + "px";
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
            container.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

            //container.style.appleDashboardRegion = "dashboard-region(control rectangle)";

            container.style.left = innerborder + "px";
            container.style.top = (26 + innerborder + innerborder) + "px";
            container.style.right = innerborder + "px";
            container.style.bottom = innerborder + "px";
            container.style.overflow = IStyle.OverflowEnum.hidden;

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
            CloseButton.style.top = (innerborder + 3) + "px";

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

            target1.appendChild(caption, icon, caption_foreground, container, CloseButton);

            caption_foreground.onmousedown +=
           delegate
           {
               __FormZIndex++;

               HTMLTarget.style.zIndex = __FormZIndex;
           };

            drag = new ScriptCoreLib.JavaScript.Controls.DragHelper(caption_foreground);

            // http://forum.mootools.net/topic.php?id=534
            // disable text selection
            // look at http://forkjavascript.com/

            drag.Enabled = true;

            drag.DragStart +=
                delegate
                {
                    caption_foreground.style.cursor = IStyle.CursorEnum.move;
                };
            drag.DragMove +=
                delegate
                {
                    HTMLTarget.style.SetLocation(drag.Position.X, drag.Position.Y);
                };
            drag.DragStop +=
                delegate
                {
                    caption_foreground.style.cursor = IStyle.CursorEnum.@default;
                };




        }

        public static int __FormZIndex = 0;


        public void Close()
        {
            // Orphanize
            HTMLTarget.Dispose();

            if (this.Closed != null)
                this.Closed(this, new EventArgs());
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            return new Size(clientSize.Width + innerborder * 2, clientSize.Height + innerborder * 3 + 26);
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
                return caption.innerText;
            }
            set
            {
                caption.innerText = value;
            }
        }

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

            this.Location = new Point
            {
                X = (Native.Window.Width - this.Width) / 2,
                Y = (Native.Window.Height - this.Height) / 2
            };
            __FormZIndex++;

            HTMLTarget.style.zIndex = __FormZIndex;
            this.HTMLTarget.AttachToDocument();

            

            InternalRaiseShown();

            foreach (Control item in this.Controls)
            {
                if (item.TabIndex == 0)
                    item.Focus();
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

    }
}
