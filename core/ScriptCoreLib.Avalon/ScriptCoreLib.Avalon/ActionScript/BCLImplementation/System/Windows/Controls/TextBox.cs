using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls.Primitives;
using ScriptCoreLib.ActionScript.flash.text;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.TextBox))]
	internal class __TextBox : __TextBoxBase
	{
		public readonly TextField InternalTextField;

		public __TextBox()
		{

			InternalTextField = new TextField
				{
					autoSize = TextFieldAutoSize.LEFT,
					type = TextFieldType.INPUT
				};
		}


		public override event TextChangedEventHandler TextChanged
		{
			add
			{

				InternalTextField.change +=
					(Event e) =>
					{
						value(null, null);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public override ScriptCoreLib.ActionScript.flash.display.DisplayObject InternalGetDisplayObject()
		{
			return InternalTextField;
		}

		public string Text
		{
			get
			{
				return InternalTextField.text;
			}
			set
			{
				InternalTextField.text = value;
			}
		}

		public static implicit operator __TextBox(TextBox e)
		{
			return (__TextBox)(object)e;
		}
	}
}
