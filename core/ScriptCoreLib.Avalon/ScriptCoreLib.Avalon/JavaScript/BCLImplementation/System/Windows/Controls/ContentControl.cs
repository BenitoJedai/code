using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.ContentControl))]
	internal class __ContentControl : __Control
	{
		protected virtual void InternalSetContent(object e)
		{

		}

		protected virtual object InternalGetContent()
		{
			return null;
		}

		public object Content
		{
			get
			{
				return InternalGetContent();
			}
			set
			{
				InternalSetContent(value);
			}
		}
	}
}
