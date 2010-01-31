using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.awt;
using System.Windows.Forms;
using ScriptCoreLibJava.BCLImplementation.System.Windows.Forms;

namespace ScriptCoreLibJava.Windows.Forms
{
	[Script]
	public static class Extensions
	{
		public static Control AttachTo(this Control e, Container parent)
		{
			parent.add(e.GetTargetElement());

			return e;
		}

		public static Control AttachTo(this Control e, ContainerControl parent)
		{
			parent.Controls.Add(e);

			return e;
		}

		static public Component GetTargetElement(this Control e)
		{
			__Control x = e;

			var r = x.InternalGetElement();

			if (r == null)
				// we are in the java world. remember to use exceptions
				// which are actually unchecked exceptions like this one
				throw new NotSupportedException("Element has not been set for this control.");

			return r;
		}
	}
}
