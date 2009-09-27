using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibJava.BCLImplementation.System.ComponentModel;
using ScriptCoreLib;
using System.Windows.Forms;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.Control))]
	internal partial class __Control : __Component
	{
		public virtual java.awt.Component InternalGetElement()
		{
			throw new NotImplementedException();
		}

		public __Control()
		{
			this.Controls = new Control.ControlCollection(this);
		}

		public virtual string Text { get; set; }

		public virtual void InternalSetVisible(bool e)
		{
			throw new NotImplementedException();
		}

		public bool Visible
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				InternalSetVisible(value);
			}
		}

		public void Show()
		{
			Visible = true;
		}


		public Control.ControlCollection Controls { get; set; }


		static public implicit operator Control(__Control e)
		{
			return (Control)(object)e;
		}

		static public implicit operator __Control(Control e)
		{
			return (__Control)(object)e;
		}

	}
}
