using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Form))]
    internal class __Form : __ContainerControl
    {
        object __FormTypeHint;

        public IHTMLDiv HTMLTarget { get; set; }

        IHTMLDiv caption = new IHTMLDiv();
        IHTMLDiv container = new IHTMLDiv();

        public override IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return container;

            }
        }

        public __Form()
        {

            HTMLTarget = new IHTMLDiv();
            //HTMLTarget.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            HTMLTarget.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            HTMLTarget.style.border = "1px solid red";


            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            HTMLTarget.style.padding = "0";



            caption.style.backgroundColor = Shared.Drawing.Color.System.ActiveCaption;
            caption.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            caption.style.left = "1px";
            caption.style.top = "1px";
            caption.style.right = "1px";
            caption.style.height = "20px";


            container.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            container.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            container.style.left = "1px";
            container.style.top = "22px";
            container.style.right = "1px";
            container.style.bottom = "1px";

            HTMLTarget.appendChild(caption, container);

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


    }
}
