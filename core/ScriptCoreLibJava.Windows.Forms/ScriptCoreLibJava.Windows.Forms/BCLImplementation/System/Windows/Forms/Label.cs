using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using javax.swing;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.Label))]
	internal class __Label : __Control
	{
		JLabel InternalElement = new JLabel();

		public __Label()
		{
			//this.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25F, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, ((byte)(186)));

			//this.InternalElement.setFont(
			//    new java.awt.Font("Microsoft Sans Serif", 0, 12)
			//);
		}

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public  bool AutoSize { get; set; }

		public override string Text
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				this.InternalElement.setText(value);
			}
		}
	}
}
