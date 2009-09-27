using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Windows.Forms
{
	[Script(Implements = typeof(global::System.Windows.Forms.ButtonBase))]
	internal class __ButtonBase : __Control
	{
		public virtual void InternalSetText(string e)
		{
			throw new NotImplementedException();
		}

		public override string Text
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				InternalSetText(value);
			}
		}
	}
}
