using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.Button ))]
    internal class __Button : __ButtonBase
	{
		readonly java.awt.Button InternalElement;

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public __Button()
		{
			this.InternalElement = new java.awt.Button();
			this.InternalElement.setBounds(0, 0, 100, 24);
		}

		public override void InternalSetText(string e)
		{
			this.InternalElement.setLabel(e);
		}
	
	}
}
