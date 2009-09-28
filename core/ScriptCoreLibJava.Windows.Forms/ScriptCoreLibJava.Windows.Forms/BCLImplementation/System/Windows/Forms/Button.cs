using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.awt.@event;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.Button))]
	internal class __Button : __ButtonBase
	{
		//readonly java.awt.Button InternalElement;
		readonly javax.swing.JButton InternalElement = new javax.swing.JButton();

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public __Button()
		{
			this.InternalElement.setBounds(0, 0, 100, 24);

			this.InternalElement.addActionListener(
				new __ActionListener
				{
					Action = RaiseClick
				}
			);
		}


		[Script]
		public class __ActionListener : ActionListener
		{
			public Action Action;

			#region ActionListener Members

			public void actionPerformed(ActionEvent e)
			{
				if (Action != null)
					Action();
			}

			#endregion
		}

		public override void InternalSetText(string e)
		{
			this.InternalElement.setLabel(e);
		}

	}
}
