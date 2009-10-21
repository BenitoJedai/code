﻿using System;
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
		public readonly javax.swing.JScrollPane InternalContainer;
		public readonly javax.swing.JTextArea InternalContent;

		public __TextBox()
		{
			//this.InternalElement.

			this.InternalContent = new javax.swing.JTextArea();
			this.InternalContainer = new javax.swing.JScrollPane(this.InternalContent);
		}
		
		public override java.awt.Component InternalGetElement()
		{
			return InternalContainer;
		}

		public override bool Multiline { get; set; }
		public bool AcceptsReturn { get; set; }

		public override string Text
		{
			get
			{
				return InternalContent.getText();
			}
			set
			{
				InternalContent.setText(value);
			}
		}
	}
}
