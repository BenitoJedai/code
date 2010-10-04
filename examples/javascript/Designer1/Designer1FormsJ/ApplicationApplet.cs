using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Java.Extensions;
using java.applet;

namespace Designer1FormsJ
{
    internal sealed class ApplicationApplet : Applet
    {
         ApplicationControl content;

        public override void init()
        {
            this.EnableVisualStyles();

            this.content = new ApplicationControl();

            this.ReplaceContentWith(content);

            this.content.Size = new System.Drawing.Size(this.getWidth(), this.getHeight());
        }

        public override void resize(int width, int height)
        {
            if (content == null)
                return;

            content.Size = new System.Drawing.Size(width, height);
        }
    }
	
}
