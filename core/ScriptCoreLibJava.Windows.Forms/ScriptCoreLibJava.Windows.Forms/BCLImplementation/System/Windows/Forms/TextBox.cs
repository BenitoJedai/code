using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TextBox))]
	internal class __TextBox : __TextBoxBase
	{
		// see: http://answers.yahoo.com/question/index?qid=20080405030738AAJcKjU


		//javax.swing.JTextField InternalElement = new javax.swing.JTextField();
		javax.swing.JTextArea InternalElement = new javax.swing.JTextArea();

		public __TextBox()
		{
			//this.InternalElement.
		}

		public override java.awt.Component InternalGetElement()
		{
			return InternalElement;
		}

		public override bool Multiline { get; set; }
		public bool AcceptsReturn { get; set; }

		public override string Text
		{
			get
			{
				return InternalElement.getText();
			}
			set
			{
				InternalElement.setText(value);
			}
		}
	}
}
