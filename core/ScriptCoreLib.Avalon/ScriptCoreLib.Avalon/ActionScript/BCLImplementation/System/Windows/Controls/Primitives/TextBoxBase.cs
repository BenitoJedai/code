using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.text;
using System.Windows.Controls;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls.Primitives
{
	[Script(Implements = typeof(global::System.Windows.Controls.Primitives.TextBoxBase))]
	internal class __TextBoxBase : __Control
	{
		public __TextBoxBase()
		{

		}



		public virtual event TextChangedEventHandler TextChanged
		{
			add
			{
			}

			remove
			{
			}
		}
	}
}
