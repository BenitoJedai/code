using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.awt;
using java.applet;
using System.Windows.Forms;
using javax.swing;
using java.awt.@event;

namespace ScriptCoreLib.Java.Extensions
{
	public static class WindowsFormsExtensions
	{
        class __AutoSizeTo : ComponentListener
        {
            public Control content;

            public void componentHidden(ComponentEvent e)
            {
            }

            public void componentMoved(ComponentEvent e)
            {

            }

            public void componentResized(ComponentEvent e)
            {
                var c = e.getComponent();

                content.Size = new System.Drawing.Size(c.getWidth(), c.getHeight());
            }

            public void componentShown(ComponentEvent e)
            {
            }
        }

        public static void AutoSizeTo(this Control content, Component c)
        {
            content.Size = new System.Drawing.Size(c.getWidth(), c.getHeight());

            c.addComponentListener(
               new __AutoSizeTo { content = content }
           );
        }

		public static void EnableVisualStyles(this Applet a)
		{
			// a dirty redirect.
			Application.EnableVisualStyles();

            SwingUtilities.updateComponentTreeUI(a);
		}

		public static void ReplaceContentWith(this Applet a, UserControl u)
		{
			var s = u.Size;

			a.setSize(s.Width, s.Height);

			u.AttachTo(a);
		}

        public static System.Windows.Forms.Control AttachTo(this System.Windows.Forms.Control e, Container parent)
		{
			parent.setLayout(null);

			ScriptCoreLibJava.Windows.Forms.Extensions.AttachTo(e, parent);

			return e;
		}
	}
}
