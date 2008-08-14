using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls.Primitives;
using ScriptCoreLib.ActionScript.flash.text;
using System.Windows.Controls;
using ScriptCoreLib.ActionScript.flash.events;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;

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

		public override void InternalSetForeground(Brush value)
		{
			var AsSolidColorBrush = value as SolidColorBrush;

			if (AsSolidColorBrush != null)
			{
				var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
				var _Color = (__Color)_SolidColorBrush.Color;

				InternalTextField.textColor = _Color;
			}
		}

		public override void InternalSetBackground(Brush value)
		{
			var AsSolidColorBrush = value as SolidColorBrush;

			if (AsSolidColorBrush != null)
			{
				var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
				uint _Color = (__Color)_SolidColorBrush.Color;

				if (_SolidColorBrush.Color.A == 0)
				{
					InternalTextField.background = false;
				}
				else
				{
					InternalTextField.background = true;
					InternalTextField.backgroundColor = _Color;
				}
			}
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

		public override void InternalAppendText(string textData)
		{
			InternalTextField.appendText(textData);
		}

		public static implicit operator __TextBox(TextBox e)
		{
			return (__TextBox)(object)e;
		}
	}
}
