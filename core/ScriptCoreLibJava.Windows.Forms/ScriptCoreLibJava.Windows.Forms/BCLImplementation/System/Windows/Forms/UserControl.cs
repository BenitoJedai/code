using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.UserControl))]
	internal class __UserControl : __ContainerControl
	{
		javax.swing.JPanel InternalElement = new javax.swing.JPanel();

		public __UserControl()
		{
			this.InternalElement.setLayout(null);
		}

		public override java.awt.Component InternalGetElement()
		{
			return this.InternalElement;
		}
	}
}
