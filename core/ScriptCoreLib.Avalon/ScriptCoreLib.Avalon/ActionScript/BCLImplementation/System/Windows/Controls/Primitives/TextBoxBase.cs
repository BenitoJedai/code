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

		public virtual void InternalAppendText(string textData)
		{
			throw new NotImplementedException();
		}

		public void AppendText(string textData)
		{
			InternalAppendText(textData);
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

		public virtual void InternalSetIsReadOnly(bool value)
		{
			throw new NotImplementedException();
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				InternalSetIsReadOnly(value);
			}
		}
	}
}
