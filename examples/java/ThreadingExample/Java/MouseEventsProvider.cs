using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.awt;
using java.awt.@event;

namespace ThreadingExample.Java
{
	[Script]
	public delegate void VoidAction();

	[Script]
	public class MouseEventsProvider
	{
		[Script]
		class ClickHandler : ActionListener
		{
			public VoidAction Handler;

			public  void actionPerformed(ActionEvent e)
			{
				Handler();
			}
		}

		public Button Target;

		public event VoidAction Click
		{
			add
			{
				this.Target.addActionListener(new ClickHandler { Handler = value });
				
			}
			remove
			{

			}
		}

		public event VoidAction MouseEnter
		{
			add
			{
				this.Target.addMouseListener(new MouseListenerHandler { HandlerMouseEnter = value });

			}
			remove
			{

			}
		}

		public event VoidAction MouseExit
		{
			add
			{
				this.Target.addMouseListener(new MouseListenerHandler { HandlerMouseExit = value });

			}
			remove
			{

			}
		}

		[Script]
		class MouseListenerHandler : MouseListener
		{
			public VoidAction HandlerMouseEnter;
			public VoidAction HandlerMouseExit;

			#region MouseListener Members

			public void mouseEntered(MouseEvent e)
			{
				if (HandlerMouseEnter != null)
					HandlerMouseEnter();
			}

			public void mouseExited(MouseEvent e)
			{
				if (HandlerMouseExit != null)
					HandlerMouseExit();
			}

			public void mousePressed(MouseEvent e)
			{
			}

			public void mouseClicked(MouseEvent e)
			{
			}

			public void mouseReleased(MouseEvent e)
			{
			}

			#endregion
		}

	

	}

	[Script]
	public static class MouseEventsProviderExtensions
	{
		public static MouseEventsProvider WithEvents(this Button e)
		{
			return new MouseEventsProvider { Target = e };
		}
	}

}
