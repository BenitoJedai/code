using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;

namespace DemoApplet.Java
{
    partial class DemoApplet
    {
		public Button Button1;

        void InitializeComponents()
        {
            // this class is to be generated with the designer

            this.Button1 = new Button();
			this.Button1.setLabel("Click this button");
			this.Button1.WithEvents().Click += this.Button1_Clicked;
			this.Button1.WithEvents().MouseEnter += this.Button1_MouseEnter;
			this.Button1.WithEvents().MouseExit += this.Button1_MouseExit;

			base.add(Button1);
        }

    



	}

}
