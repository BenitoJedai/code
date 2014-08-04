using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Drawing;
using System.Drawing;
namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/ToolStripButton.cs

    [Script(Implements = typeof(global::System.Windows.Forms.ToolStripButton))]
    public class __ToolStripButton : __ToolStripItem
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\HTML\IHTMLButton.cs
        public IHTMLButton InternalElement = typeof(__ToolStripButton);

        [Script]
        static class Styles
        {
            static IStyle idle = new IStyle(Native.css[typeof(__ToolStripButton)])
            {
                backgroundColor = "rgba(0,0,255, 0.0)",

                transition = "background-color 300ms linear",

                border = "1px solid transparent",

                //margin = "1px",
                //padding = "1px",

                // http://stackoverflow.com/questions/10850341/vertical-align-center-input-and-button-in-div
                //display = IStyle.DisplayEnum
                verticalAlign = "baseline",

                height = "22px"
            };

            static IStyle hover = new IStyle(Native.css[typeof(__ToolStripButton)].hover)
            {
                border = "1px solid rgba(0,0,255, 0.5)",
                backgroundColor = "rgba(0,0,255, 0.1)"
            };

            static IStyle active = new IStyle(Native.css[typeof(__ToolStripButton)].active)
            {
                border = "1px solid rgba(0,0,255, 0.7)",
                backgroundColor = "rgba(0,0,255, 0.3)"
            };
        }


        public __ToolStripButton()
        {
            (this.InternalElement.style.display as dynamic).display = "table-cell";

            var InternalElementSpan = new IHTMLSpan().AttachTo(InternalElement);
            //InternalElementSpan.style.verticalAlign = "baseline";

            this.InternalElement.style.font = Control.DefaultFont.ToCssString();


            //this.Font = DefaultFont;

            this.InternalImageChanged +=
                delegate
                {

                    if (this.InternalImage == null)
                        return;

                    var i = ((__Bitmap)(object)this.InternalImage).InternalImage;

                    // https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align
                    //i.style.verticalAlign = "baseline";

                    this.InternalElement.insertBefore(
                        i,
                        InternalElementSpan
                    );

                    if (this.DisplayStyle == ToolStripItemDisplayStyle.Image)
                    {
                        i.InvokeOnComplete(
                            delegate
                            {
                                InternalElementSpan.Hide();
                            }
                        );
                    }
                };

            this.InternalElement.onclick +=
                delegate
                {
                    this.RaiseClick();
                };

            this.TextChanged += delegate
            {
                InternalElementSpan.innerText = this.InternalText;
            };

            this.InternalAfterSetOwner +=
                delegate
                {
                    __ToolStrip o = this.Owner;

                    // or contaner?
                    InternalElement.AttachTo(o.InternalElement);


                };

        }


        public override void InternalSetFont(Font value)
        {
            this.InternalElement.style.font = value.ToCssString();
        }


        public Color InternalForeColor;
        public override Color ForeColor
        {
            get
            {
                return InternalForeColor;
            }
            set
            {
                InternalForeColor = value;
                this.InternalElement.style.color = value.ToString();
            }
        }


        // 
        //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ToolStripButton.set_CheckOnClick(System.Boolean)]
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationControl.cs
        public bool CheckOnClick { get; set; }
    }
}
