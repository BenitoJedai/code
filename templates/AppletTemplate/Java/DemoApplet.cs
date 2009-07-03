using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;

namespace DemoApplet.Java
{
    [Script]
    public partial class DemoApplet : Applet
    {
        public override void init()
        {
            this.InitializeComponents();

            base.resize(Settings.DefaultWidth, Settings.DefaultHeight);
        }

        static Color GetBlue(double b)
        {
            int u = (int)( 0xff * b );

            return new Color(u);
        }

        public override void paint(global::java.awt.Graphics g)
        {
            // old school gradient :)

            var h = this.getHeight();
            var w = this.getWidth();

            for (int i = 0; i < h; i++)
            {

                g.setColor(GetBlue(1 - (double)i / (double)h));
                g.drawLine(0, i, w, i);
            }
            
            g.setColor(new Color(0xffffff));
            g.drawString("hello world, this is the sample applet", 16, 64);
        }

    
        public void Button1_Clicked()
        {
			this.Button1.setLabel("Click!");

			EvaluateJavaScript(this, "document.title = 'powered by jsc';");

			EvaluateJavaScript(this, "alert('script was evaluated!');");

        }

	
		public void Button1_MouseEnter()
		{
			this.Button1.setLabel("MouseEnter!");


		}


	
		public void Button1_MouseExit()
		{
			this.Button1.setLabel("MouseExit!");


		}
    }
}
