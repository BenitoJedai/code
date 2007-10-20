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

        public __Form()
        {
            HTMLTarget = new IHTMLDiv();
            HTMLTarget.style.backgroundColor = Shared.Drawing.Color.System.ThreeDFace;
            HTMLTarget.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
            HTMLTarget.style.borderWidth = "2px";
            HTMLTarget.style.borderStyle = "outset";

            //HTMLTarget.style.SetLocation(64, 64, 100, 100);
            HTMLTarget.style.padding = "0";

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




    }
}
