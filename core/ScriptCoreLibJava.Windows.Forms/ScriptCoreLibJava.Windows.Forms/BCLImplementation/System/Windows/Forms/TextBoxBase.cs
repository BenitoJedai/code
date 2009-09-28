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
	}
}
