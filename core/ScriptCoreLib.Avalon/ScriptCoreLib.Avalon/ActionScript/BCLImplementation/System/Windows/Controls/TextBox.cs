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
using System.Windows;

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
					type = TextFieldType.INPUT,
					background = true,
					backgroundColor = 0xffffffff
				};
		}

		public override void InternalSetWidth(double value)
		{
			this.InternalTextField.autoSize = TextFieldAutoSize.NONE;
			this.InternalTextField.width = value;
		}

		public override void InternalSetHeight(double value)
		{
			this.InternalTextField.autoSize = TextFieldAutoSize.NONE;
			this.InternalTextField.height = value;
		}


		public override void InternalSetAcceptsReturn(bool value)
		{
			this.InternalTextField.multiline = value;
		}

		public override void InternalSetFontSize(double value)
		{
			InternalTextField.defaultTextFormat = new TextFormat { size = Convert.ToInt32(value) };


		}

		public override void InternalSetBorderThickness(global::System.Windows.Thickness value)
		{
			__Thickness v = value;

			if (v.InternalValue == 0)
			{
				this.InternalTextField.border = false;

				return;
			}

			if (v.InternalValue == 1)
			{
				this.InternalTextField.border = true;

				return;
			}

			throw new NotSupportedException();
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

		public override ScriptCoreLib.ActionScript.flash.display.InteractiveObject InternalGetDisplayObject()
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

		public override void InternalSetIsReadOnly(bool value)
		{
			if (value)
			{
				this.InternalTextField.type = TextFieldType.DYNAMIC;
			}
			else
			{
				this.InternalTextField.type = TextFieldType.INPUT;
			}
		}

		public TextWrapping TextWrapping
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (value == TextWrapping.NoWrap)
				{
					this.InternalTextField.wordWrap = false;

					return;
				}

				if (value == TextWrapping.Wrap)
				{
					this.InternalTextField.wordWrap = true;

					return;
				}

				throw new NotSupportedException();
			}
		}
	}
}
