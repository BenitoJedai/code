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
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.TextBox))]
	internal class __TextBox : __TextBoxBase
	{
		public readonly TextField InternalTextField;
		public readonly Sprite InternalTextFieldContainer;

		public __TextBox()
		{

			InternalTextField = new TextField
			{
				autoSize = TextFieldAutoSize.LEFT,
				type = TextFieldType.INPUT,
				background = true,
				backgroundColor = 0xffffffff,
				alwaysShowSelection = true,
				border = true,
				borderColor = 0x808080
				// http://code.hellokeita.in/public/trunk/as3/br/hellokeita/utils/TextFieldColor.as

			};

			InternalTextField.y = InternalOffsetY;

			// http://www.typetester.org/
			InternalTextField.defaultTextFormat.font = "Verdana";

			InternalTextFieldContainer = new Sprite();
			InternalTextFieldContainer.addChild(InternalTextField);
		}

		// this is needed for small fonts...
		//const int InternalOffsetY = -3;
		const int InternalOffsetY = -1;

		public override void InternalSetWidth(double value)
		{
			this.InternalTextField.autoSize = TextFieldAutoSize.NONE;
			this.InternalTextField.width = value;
		}

		public override void InternalSetHeight(double value)
		{
			this.InternalTextField.autoSize = TextFieldAutoSize.NONE;
			this.InternalTextField.height = value - InternalOffsetY;
		}

		public override double InternalGetWidth()
		{
			return this.InternalTextField.width;
		}

		public override double InternalGetHeight()
		{
			return this.InternalTextField.height;
		}

		public override void InternalSetAcceptsReturn(bool value)
		{
			this.InternalTextField.multiline = value;
		}

		public override void InternalSetFontSize(double value)
		{
			InternalChangeTextFormat(new TextFormat { size = Convert.ToInt32(value - 1) });
		}

		void InternalChangeTextFormat(TextFormat e)
		{
			InternalTextField.defaultTextFormat = e;
			InternalTextField.setTextFormat(e);
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

		#region InternalForeground
		Brush InternalForeground;

		public override Brush InternalGetForeground()
		{
			return InternalForeground;
		}

		public override void InternalSetForeground(Brush value)
		{
			InternalForeground = value;

			var AsSolidColorBrush = value as SolidColorBrush;

			if (AsSolidColorBrush != null)
			{
				var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
				var _Color = (__Color)_SolidColorBrush.Color;

				InternalTextField.textColor = _Color;
			}
		}
		#endregion

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
			return InternalTextFieldContainer;
		}


		public string Text
		{
			get
			{
				return InternalTextField.text.Replace("\r", Environment.NewLine);
			}
			set
			{
				// http://blog.madebyderek.com/archives/2005/08/26/textfield_newline_and_crlf/
				InternalTextField.text = value.Replace(Environment.NewLine, "\n");
			}
		}

		public override void InternalAppendText(string textData)
		{
			InternalTextField.appendText(textData.Replace(Environment.NewLine, "\n"));
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

		public TextAlignment TextAlignment
		{
			get
			{
				var align = this.InternalTextField.defaultTextFormat.align;


				if (align == TextFormatAlign.RIGHT)
					return TextAlignment.Right;

				if (align == TextFormatAlign.CENTER)
					return TextAlignment.Center;

				if (align == TextFormatAlign.JUSTIFY)
					return TextAlignment.Justify;

				return TextAlignment.Left;
			}
			set
			{
				// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/text/TextFormatAlign.html
				var align = TextFormatAlign.LEFT;

				if (value != TextAlignment.Left)
					if (value == TextAlignment.Right)
						align = TextFormatAlign.RIGHT;
					else if (value == TextAlignment.Center)
						align = TextFormatAlign.CENTER;
					else if (value == TextAlignment.Justify)
						align = TextFormatAlign.JUSTIFY;

				this.InternalChangeTextFormat(
					new TextFormat
					{
						align = align
					}
				);
			}
		}

		public void Clear()
		{
			this.Text = "";
		}

		public override void InternalSetFontFamily(FontFamily value_)
		{
			var value = (__FontFamily)(object)value_;

			this.InternalChangeTextFormat(
					new TextFormat
					{
						font = value.InternalFamilyName
					}
			);
		}
	}
}
