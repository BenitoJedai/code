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
using ScriptCoreLib.Shared.Lambda;
using System.Windows;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Controls
{
	[Script(Implements = typeof(global::System.Windows.Controls.TextBox))]
	internal class __TextBox : __TextBoxBase
	{
		public IHTMLInput InternalTextField;
		public IHTMLTextArea InternalTextField_MultiLine;


		public __TextBox()
		{

			this.InternalTextField = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text)
			{
				
			};

			this.InternalTextField.style.paddingTop = "0";
			this.InternalTextField.style.paddingBottom = "0";
			this.InternalTextField.style.border = "1px solid gray";
		}

		public override void InternalSetWidth(double value)
		{
			var s = this.InternalGetDisplayObject().style;
			s.width = value + "px";
		}

		public override void InternalSetHeight(double value)
		{
			var s = this.InternalGetDisplayObject().style;
			s.height = value + "px";
		}

		public override double InternalGetWidth()
		{
			var e = this.InternalGetDisplayObject();

			return e.Bounds.Width;
		}

		public override double InternalGetHeight()
		{
			var e = this.InternalGetDisplayObject();

			return e.Bounds.Height;
		}

		public override void InternalSetAcceptsReturn(bool value)
		{
			if (value)
				if (InternalTextField != null)
					if (InternalTextField_MultiLine == null)
					{
						// known situation

						this.InternalTextField_MultiLine = new IHTMLTextArea(this.InternalTextField.value)
						{
							readOnly = this.InternalTextField.readOnly,
							wrap = "off"
						};


						this.InternalTextField_MultiLine.style.overflow = IStyle.OverflowEnum.hidden;

						var p = this.InternalTextField.parentNode;

						// we should actually just norify our collection about this change
						// but instead we do the exchange here at the moment 
						if (p != null)
						{
							p.insertBefore(this.InternalTextField_MultiLine, this.InternalTextField);

							p.removeChild(this.InternalTextField);
						}

						return;
					}

			throw new NotImplementedException();
		}

		public override void InternalSetFontSize(double value)
		{
			var s = this.InternalGetDisplayObject().style;
			s.fontSize = Convert.ToInt32(value) + "px";
		}

		public override void InternalSetBorderThickness(global::System.Windows.Thickness value)
		{
			__Thickness v = value;

			var s = this.InternalGetDisplayObject().style;

			if (v.InternalValue == 0)
			{
				s.borderWidth = "0";

				return;
			}

			if (v.InternalValue == 1)
			{
				s.borderWidth = "1px";

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
				__Color _Color = _SolidColorBrush.Color;
				var s = this.InternalGetDisplayObject().style;

				s.color = _Color;
				//InternalTextField.textColor = _Color;
			}
		}
		#endregion

		public override void InternalSetBackground(Brush value)
		{
			var AsSolidColorBrush = value as SolidColorBrush;

			if (AsSolidColorBrush != null)
			{
				var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
				__Color _Color = _SolidColorBrush.Color;

				var s = this.InternalGetDisplayObject().style;

				if (_SolidColorBrush.Color.A == 0)
				{
					s.backgroundColor = "transparent";
				}
				else
				{
					s.backgroundColor = _Color;
				}
			}
		}


		public override event TextChangedEventHandler TextChanged
		{
			add
			{
				this.InternalGetDisplayObject().onchange +=
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
			if (this.InternalTextField_MultiLine != null)
				return this.InternalTextField_MultiLine;

			return InternalTextField;
		}

		int InternalTextKnownLength = 0;
		bool InternalTextNewLineMangling = false;

		public string Text
		{
			get
			{
				if (this.InternalTextField_MultiLine != null)
				{
					#region detect newline changes
					var c = this.InternalTextField.value.Length;

					if (c != this.InternalTextKnownLength)
					{
						this.InternalTextKnownLength = c;
						this.InternalTextNewLineMangling = this.InternalTextField_MultiLine.value.Count("\n") > this.InternalTextField_MultiLine.value.Count(Environment.NewLine);
					}
					#endregion


					if (this.InternalTextNewLineMangling)
						return this.InternalTextField_MultiLine.value.Replace("\n", Environment.NewLine);

					return this.InternalTextField_MultiLine.value;
				}

				return InternalTextField.value;
			}
			set
			{
				if (this.InternalTextField_MultiLine != null)
				{
					// we need to detect newline mangling

					this.InternalTextField_MultiLine.value = value;

					#region detect newline changes
					this.InternalTextKnownLength = this.InternalTextField_MultiLine.value.Length;
					this.InternalTextNewLineMangling = this.InternalTextKnownLength < value.Length;
					#endregion

					return;
				}

				InternalTextField.value = value;
			}
		}

		public override void InternalAppendText(string textData)
		{
			this.Text += textData;
		}

		public static implicit operator __TextBox(TextBox e)
		{
			return (__TextBox)(object)e;
		}

		public override void InternalSetIsReadOnly(bool value)
		{
			if (this.InternalTextField_MultiLine != null)
			{
				this.InternalTextField_MultiLine.readOnly = value;

				return;
			}


			this.InternalTextField.readOnly = value;
		}

		public TextWrapping TextWrapping
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				if (this.InternalTextField_MultiLine == null)
					new NotSupportedException();

				// http://www.idocs.com/tags/forms/_TEXTAREA_WRAP.html
				// http://msdn.microsoft.com/en-us/library/ms535152(VS.85).aspx

				if (value == TextWrapping.NoWrap)
				{
					this.InternalTextField_MultiLine.wrap = "off";
					//this.InternalTextField_MultiLine.style.whiteSpace = IStyle.WhiteSpaceEnum.nowrap;

					return;
				}

				if (value == TextWrapping.Wrap)
				{
					// Default. Text is displayed with wordwrapping and submitted without carriage returns and line feeds.
					this.InternalTextField_MultiLine.wrap = "soft";
					//this.InternalTextField_MultiLine.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;

					return;
				}

				throw new NotSupportedException();
			}
		}

		public TextAlignment TextAlignment
		{
			get
			{
				var align = this.InternalGetDisplayObjectDirect().style.textAlign;

				// jsc will replace local enum assignment with their string values...
				var right = IStyle.TextAlignEnum.right;
				if (align == right)
					return TextAlignment.Right;

				var center = IStyle.TextAlignEnum.center;
				if (align == center)
					return TextAlignment.Center;

				var justify = IStyle.TextAlignEnum.justify;
				if (align == justify)
					return TextAlignment.Justify;

				return TextAlignment.Left;
			}
			set
			{
				// http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/text/TextFormatAlign.html


				var s = this.InternalGetDisplayObjectDirect().style;


				if (value == TextAlignment.Left)
					s.textAlign = IStyle.TextAlignEnum.left;
				if (value == TextAlignment.Right)
					s.textAlign = IStyle.TextAlignEnum.right;
				else if (value == TextAlignment.Center)
					s.textAlign = IStyle.TextAlignEnum.center;
				else if (value == TextAlignment.Justify)
					s.textAlign = IStyle.TextAlignEnum.justify;
			}
		}

		public void Clear()
		{
			this.Text = "";
		}

		[Script(HasNoPrototype = true)]
		internal class IStyle_fontFamily
		{
			public string fontFamily;
		}

		public override void InternalSetFontFamily(FontFamily value_)
		{
			var value = ((__FontFamily)(object)value_).InternalFamilyName;
			var s = (IStyle_fontFamily)(object)this.InternalGetDisplayObjectDirect().style;

			// we could let consolas override courier new

			s.fontFamily = value;
		
		}
	}
}
