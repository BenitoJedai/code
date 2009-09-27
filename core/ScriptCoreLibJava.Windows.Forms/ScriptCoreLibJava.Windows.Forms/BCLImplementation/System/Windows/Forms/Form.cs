using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.swing;
using System.Windows.Forms;
using java.awt.@event;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.Form))]
	internal class __Form : __ContainerControl
	{
		// see: http://java.sun.com/docs/books/tutorial/uiswing/components/frame.html
		// see: http://java.sun.com/docs/books/tutorial/uiswing/events/windowlistener.html
		// see: http://www.dreamincode.net/forums/showtopic66100.htm
		// see: http://dev.eclipse.org/newslists/news.eclipse.tools.ve/msg00053.html
		// see: http://inversionconsulting.blogspot.com/2008/03/java-jdialog-and-jprogressbar-example.html

		//public JFrame InternalElement;
		public JDialog InternalElement;

		public event FormClosedEventHandler FormClosed;
		public event FormClosingEventHandler FormClosing;

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public __Form()
		{
			//this.InternalElement = new JFrame();
			this.InternalElement = new JDialog();
			this.InternalElement.setSize(300, 300);
			
			this.InternalElement.getContentPane().setLayout(null);

			// fixme: jsc should make delegate methods public!
			// java cannot call them otherwise

			this.InternalElement.addWindowListener(
				new __WindowListener
				{
					Closed = RaiseFormClosed,
					Closing = RaiseFormClosing,
				}

			);
		}

		public void RaiseFormClosed(WindowEvent e)
		{
			if (this.FormClosed != null)
				this.FormClosed(this, new FormClosedEventArgs(CloseReason.None));
						
		}

		public void RaiseFormClosing(WindowEvent e)
		{
			var args = new FormClosingEventArgs(CloseReason.None, false);

			if (this.FormClosing != null)
				this.FormClosing(this, args);

			if (args.Cancel)
				return;

			// If the program does not explicitly hide or dispose the window while 
			// processing this event, the window close operation will be cancelled.
			
			
			//this.Dispose();
		}
		[Script]
		public delegate void __WindowListenerDelegate(WindowEvent e);

		[Script]
		public class __WindowListener : WindowListener
		{
			public __WindowListenerDelegate Closed;
			public __WindowListenerDelegate Closing;

			#region WindowListener Members

			public void windowActivated(WindowEvent e)
			{
			}

			public void windowClosed(WindowEvent e)
			{
				if (Closed != null)
					Closed(e);
			}

			public void windowClosing(WindowEvent e)
			{
				if (Closing != null)
					Closing(e);
			}

			public void windowDeactivated(WindowEvent e)
			{
			}

			public void windowDeiconified(WindowEvent e)
			{
			}

			public void windowIconified(WindowEvent e)
			{
			}

			public void windowOpened(WindowEvent e)
			{
			}

			#endregion
		}

		public override string Text
		{
			get
			{
				return InternalElement.getTitle();
			}
			set
			{
				InternalElement.setTitle(value);
			}
		}


		public override void InternalSetVisible(bool e)
		{
			this.InternalElement.setVisible(e);
		}

		protected override void Dispose(bool e)
		{
			this.InternalElement.dispose();
		}

		public DialogResult ShowDialog()
		{
			this.InternalElement.setModal(true);
			this.InternalElement.show(true);

			return DialogResult.OK;
		}
	}
}
