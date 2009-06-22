using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;

namespace ThreadingExample.Java
{
	partial class ThreadingExample
	{
		public Button Button1;
		public Button Button2;
		public Button Button3;

		void InitializeComponents()
		{
			// this class is to be generated with the designer

			this.Button1 = new Button();
			this.Button1.setLabel("Start computing");
			this.Button1.addActionListener(new Button1_Clicked_Handler { Target = this });
			//this.Button1.addMouseListener(new Button1_MouseEnter_Handler { Target = this });
			//this.Button1.addMouseListener(new Button1_MouseExit_Handler { Target = this });

			base.add(Button1);

			this.Button2 = new Button { Enabled = false };
			this.Button2.setLabel("Stop computing");
			this.Button2.addActionListener(new Button2_Clicked_Handler { Target = this });
			//this.Button1.addMouseListener(new Button1_MouseEnter_Handler { Target = this });
			//this.Button1.addMouseListener(new Button1_MouseExit_Handler { Target = this });

			base.add(Button2);

			this.Button3 = new Button {};
			this.Button3.setLabel("Compute");
			this.Button3.addMouseListener(new Button3_MouseEnter_Handler { Target = this });
			this.Button3.addMouseListener(new Button3_MouseExit_Handler { Target = this });

			base.add(Button3);
		}

		#region delegate void ActionDelegate()

		[Script]
		abstract class AnonymouseDelegate :
			ActionListener
		{
			#region ActionListener Members

			public virtual void actionPerformed(ActionEvent e)
			{

			}

			#endregion
		}

		#endregion


		[Script]
		public class MouseListener_MouseEnter : MouseListener
		{
			protected virtual void Invoke()
			{

			}

			#region MouseListener Members

			public void mouseEntered(MouseEvent e)
			{
				Invoke();
			}

			public void mouseExited(MouseEvent e)
			{
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

		[Script]
		public class MouseListener_MouseExit : MouseListener
		{
			protected virtual void Invoke()
			{

			}

			#region MouseListener Members

			public void mouseEntered(MouseEvent e)
			{
			}

			public void mouseExited(MouseEvent e)
			{
				Invoke();
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

}
