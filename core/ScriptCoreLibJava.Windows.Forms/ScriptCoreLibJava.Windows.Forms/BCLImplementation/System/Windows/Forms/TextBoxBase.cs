using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.TextBoxBase))]
	internal class __TextBoxBase : __Control
	{
		public virtual bool Multiline { get; set; }

		public void AppendText(string text)
		{
			this.Text += text;
		}

		public virtual void InternalSetReadOnly(bool value)
		{
		}

		public virtual bool InternalGetReadOnly()
		{
			return default(bool);
		}

		public bool ReadOnly
		{
			get
			{
				return InternalGetReadOnly();
			}
			set
			{
				InternalSetReadOnly(value);
			}
		}
	}
}
