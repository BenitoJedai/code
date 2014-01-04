using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.JavaScript.Extensions;
using System.Drawing;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Drawing;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [ScriptCoreLib.Script(Implements = typeof(global::System.Windows.Forms.PictureBox))]
    public class __PictureBox : __Control, __ISupportInitialize
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131217-picturebox

        public IHTMLElement HTMLTarget { get; set; }

        public IHTMLImage InternalElement;

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return HTMLTarget;
            }
        }

        public __PictureBox()
        {
            // http://stackoverflow.com/questions/18389224/how-to-style-html5-range-input-to-have-different-color-before-and-after-slider

            this.HTMLTarget = new IHTMLDiv
            {

            };



            this.InternalElement = new IHTMLImage
            {
            }.AttachTo(this.HTMLTarget);


            // keep only borders
            this.InternalElement.style.height = "100%";
            this.InternalElement.style.width = "100%";






            this.Size = new global::System.Drawing.Size(200, 200);
        }

        public Image InternalImage;
        public Image Image
        {
            get { return this.InternalImage; }
            set
            {
                //Console.WriteLine("__PictureBox set_Image" + new { value });

                this.InternalImage = value;


                var i = ((object)value) as __Bitmap;
                if (i != null)
                {
                    if (i.InternalImage != null)
                    {
                        //Console.WriteLine("__PictureBox set_Image" + new { i.InternalImage.src });

                        this.InternalElement.src = i.InternalImage.src;

                    }
                }
            }
        }


        public bool TabStop { get; set; }

        public PictureBoxSizeMode SizeMode { get; set; }

        public void BeginInit()
        {
        }

        public void EndInit()
        {
        }
    }

}
