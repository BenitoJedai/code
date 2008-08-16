using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls.Primitives;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.JavaScript.DOM;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.TextBox))]
	internal class __TextBox : __TextBoxBase
	{
		public readonly IHTMLInput InternalTextField;

		public __TextBox()
		{

			InternalTextField = new IHTMLInput( ScriptCoreLib.Shared.HTMLInputTypeEnum.text)
				{
					//autoSize = TextFieldAutoSize.LEFT,
					//type = TextFieldType.INPUT
				};

			//InternalTextField.style.width = "auto";
		}

		public override void InternalSetBorderThickness(global::System.Windows.Thickness value)
		{
			__Thickness v = value;

			if (v.InternalValue == 0)
			{
				this.InternalTextField.style.borderWidth = "0";

				return;
			}

			if (v.InternalValue == 1)
			{
				this.InternalTextField.style.borderWidth = "1px";

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
				__Color _Color = _SolidColorBrush.Color;

				InternalTextField.style.color = _Color;
				//InternalTextField.textColor = _Color;
			}
		}

		public override void InternalSetBackground(Brush value)
		{
			var AsSolidColorBrush = value as SolidColorBrush;

			if (AsSolidColorBrush != null)
			{
				var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
				__Color _Color =_SolidColorBrush.Color;

				if (_SolidColorBrush.Color.A == 0)
				{
					InternalTextField.style.backgroundColor = "transparent";
				}
				else
				{
					InternalTextField.style.backgroundColor = _Color;
				}
			}
		}

		public override event TextChangedEventHandler TextChanged
		{
			add
			{

				InternalTextField.onchange +=
					(IEvent e) =>
					{
						value(null, null);
					};
			}
			remove
			{
				throw new NotImplementedException();
			}
		}

		public override IHTMLElement InternalGetDisplayObject()
		{
			return InternalTextField;
		}

		public string Text
		{
			get
			{
				return InternalTextField.value;
			}
			set
			{
				InternalTextField.value = value;
			}
		}

		public override void InternalAppendText(string textData)
		{
			InternalTextField.value += textData;
		}

		public static implicit operator __TextBox(TextBox e)
		{
			return (__TextBox)(object)e;
		}

		public override void InternalSetIsReadOnly(bool value)
		{
			this.InternalTextField.readOnly = value;
		}
	}
}
