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

        #region DocumentListener
        // http://stackoverflow.com/questions/4836224/how-to-know-whether-any-changes-in-the-jtextarea-have-been-made-or-not
        [Script]
        class MyDocumentListener : javax.swing.@event.DocumentListener
        {
            public Action Invoke;

            public void changedUpdate(javax.swing.@event.DocumentEvent value)
            {
                Invoke();
            }

            public void insertUpdate(javax.swing.@event.DocumentEvent value)
            {
                Invoke();

            }

            public void removeUpdate(javax.swing.@event.DocumentEvent value)
            {
                Invoke();

            }
        }
        #endregion


        public __TextBox()
		{
			//this.InternalElement.

			this.InternalContent = new javax.swing.JTextArea();

            this.InternalContent.getDocument().addDocumentListener(
                new MyDocumentListener
                {
                    Invoke = this.InternalRaiseTextChanged
                }
            );

			this.InternalContainer = new javax.swing.JScrollPane(this.InternalContent);
		}

        public override bool InternalGetEnabled()
        {
            return this.InternalContent.isEnabled();

        }
        public override void InternalSetEnabled(bool value)
        {
            this.InternalContent.setEnabled(value);
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

        public override bool InternalGetReadOnly()
        {
            return !this.InternalContent.isEditable();
        }

        public override void InternalSetReadOnly(bool value)
        {
            this.InternalContent.setEditable(!value);
        }

        public override string[] InternalGetLines()
        {
            return Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
