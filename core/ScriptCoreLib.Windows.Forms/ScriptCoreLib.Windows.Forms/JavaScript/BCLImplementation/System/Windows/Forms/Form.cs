using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    internal class __Form : __ContainerControl
    {
        object __FormTypeHint;

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

        ScriptCoreLib.JavaScript.Controls.DragHelper drag;

        public __Form()
        {

            HTMLTarget = new IHTMLDiv();
            //HTMLTarget.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            HTMLTarget.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			HTMLTarget.style.borderLeft = "1px solid #E0E0E0";
			HTMLTarget.style.borderTop = "1px solid #E0E0E0";
			HTMLTarget.style.borderBottom = "1px solid black";
			HTMLTarget.style.borderRight = "1px solid black";


            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            HTMLTarget.style.padding = "0";

            var innerborder = 2;

			var icon = new IHTMLImage("assets/ScriptCoreLib.Windows.Forms/App.ico");
			
			icon.style.SetLocation(7, 7, 16, 16);

            caption.style.backgroundColor = Shared.Drawing.Color.Blue;
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
            caption_foreground.style.Opacity = 0.7;
			caption_foreground.className = "caption";

            // http://dojotoolkit.org/pipermail/dojo-checkins/2005-December/002867.html


            new IFunction(@"
                try { this.style.MozUserSelect = 'none'; } catch (e) { }
                try { this.style.KhtmlUserSelect = 'none'; } catch (e) { }
                try { this.unselectable = 'on'; } catch (e) { }
                "
			).apply(caption_foreground);


			// http://developer.apple.com/mac/library/documentation/AppleApplications/Reference/Dashboard_Ref/Dashboard_Ref.pdf
			// for some reason we cannot exclude caption
			// from apple dashboard
			
			//caption_foreground.style.appleDashboardRegion = "none";

			//container.style.backgroundColor = "#A0A0A0";
            container.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;

			//container.style.appleDashboardRegion = "dashboard-region(control rectangle)";

            container.style.left = innerborder + "px";
            container.style.top = (20 + innerborder) + "px";
            container.style.right = innerborder +"px";
            container.style.bottom = innerborder +"px";
            container.style.overflow = IStyle.OverflowEnum.hidden;

			//HTMLTarget.style.backgroundColor = "#B0B0B0";
			this.BackColor = SystemColors.ButtonFace;


			HTMLTarget.appendChild(caption, icon, caption_foreground, container);

            drag = new ScriptCoreLib.JavaScript.Controls.DragHelper(caption_foreground);

            // http://forum.mootools.net/topic.php?id=534
            // disable text selection
            // look at http://forkjavascript.com/

            
            drag.Enabled = true;
            drag.DragMove +=
                delegate
                {
                    HTMLTarget.style.SetLocation(drag.Position.X, drag.Position.Y);
                };

            HTMLTarget.AttachToDocument();
        }



        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
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
    }
}
