using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Java.Extensions;
using java.applet;
using java.awt.@event;

namespace Designer1FormsJ
{
    internal sealed class ApplicationApplet : Applet
    {
        readonly ApplicationControl content = new ApplicationControl();

        public override void init()
        {
            content.AttachTo(this);
            content.AutoSizeTo(this);
            
            this.EnableVisualStyles();

           
        }

      
    }
	
}
