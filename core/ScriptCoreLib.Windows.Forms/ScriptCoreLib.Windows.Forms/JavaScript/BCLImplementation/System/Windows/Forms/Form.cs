﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Drawing;
using ScriptCoreLib.JavaScript.DOM;

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
            HTMLTarget.style.border = "1px solid black";


            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            HTMLTarget.style.padding = "0";

            var innerborder = 2;


            caption.style.backgroundColor = Shared.Drawing.Color.Blue;
            caption.style.color = Shared.Drawing.Color.White;
            caption.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            caption.style.left = innerborder + "px";
            caption.style.top = innerborder + "px";
            caption.style.right = innerborder + "px";
            caption.style.height = "20px";
            caption.style.padding = "2px";


            caption_foreground = (IHTMLDiv)caption.cloneNode(false);
            caption_foreground.style.backgroundColor = ScriptCoreLib.Shared.Drawing.Color.FromRGB(255, 0, 255);
            caption_foreground.style.Opacity = 0;

            // http://dojotoolkit.org/pipermail/dojo-checkins/2005-December/002867.html


            new IFunction( @"
                try { this.style.MozUserSelect = 'none'; } catch (e) { }
                try { this.style.KhtmlUserSelect = 'none'; } catch (e) { }
                try { this.unselectable = 'on'; } catch (e) { }
                "
            ).apply(caption_foreground);


            container.style.backgroundColor = Shared.Drawing.Color.Gray;
            container.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;


            container.style.left = innerborder + "px";
            container.style.top = (20 + innerborder) + "px";
            container.style.right = innerborder +"px";
            container.style.bottom = innerborder +"px";
            container.style.overflow = IStyle.OverflowEnum.hidden;

            HTMLTarget.style.backgroundColor = ScriptCoreLib.Shared.Drawing.Color.Red;
            HTMLTarget.appendChild(caption, caption_foreground, container);

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

            HTMLTarget.attachToDocument();
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
            drag.Position = new Shared.Drawing.Point(this.Location.X, this.Location.Y);

            base.RaiseMove(e);
        }
    }
}
