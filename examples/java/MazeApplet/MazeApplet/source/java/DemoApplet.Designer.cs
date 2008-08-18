using ScriptCoreLib;

using java.lang;
using java.applet;
using java.awt;
using java.awt.@event;
using javax.common.runtime;

namespace MazeApplet.source.java
{
	partial class MazeApplet
	{
		void InitializeComponents()
		{
			// this class is to be generated with the designer

			var a = new Button();

			a.setLabel("Click this button");

			var a_handler = new Button1_Clicked_Handler { Target = this };

			a.addActionListener(a_handler);

			base.add(a);
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


	}
}
