using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Markup;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Media;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
    [Script(Implements = typeof(global::System.Windows.Controls.TextBlock))]
    public class __TextBlock : __FrameworkElement
    {
        // where is this used?
        // Y:\jsc.svn\examples\actionscript\AvalonFlashLinqToObjects\AvalonFlashLinqToObjects\ApplicationCanvas.cs


        //Implementation not found for type import :
        //type: System.Windows.Controls.TextBlock
        //method: Void set_Foreground(System.Windows.Media.Brush)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        //Implementation not found for type import :
        //type: System.Windows.Controls.TextBlock
        //method: Void set_TextAlignment(System.Windows.TextAlignment)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!


        public readonly TextField InternalTextField;
        public readonly Sprite InternalTextFieldContainer;

        public override ScriptCoreLib.ActionScript.flash.display.InteractiveObject InternalGetDisplayObject()
        {
            return InternalTextFieldContainer;
        }

        const int InternalOffsetY = -1;

        public __TextBlock()
        {

            InternalTextField = new TextField
            {
                autoSize = TextFieldAutoSize.LEFT,
                type = TextFieldType.DYNAMIC,
                selectable = false,
                //background = true,
                //backgroundColor = 0xffffffff,
                //alwaysShowSelection = true,
                //border = true,
                //borderColor = 0x808080
                // http://code.hellokeita.in/public/trunk/as3/br/hellokeita/utils/TextFieldColor.as

            };

            //InternalTextField.y = InternalOffsetY;



            InternalTextFieldContainer = new Sprite();
            InternalTextFieldContainer.addChild(InternalTextField);

            // http://www.typetester.org/
            //InternalTextField.defaultTextFormat.font = "Verdana";
            LocalInternalSetFonFamily(new FontFamily("Verdana"));
        }



        #region width, height
        public override void InternalSetWidth(double value)
        {
            var __keepit = this.InternalTextField.height;

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
        #endregion

        #region FontSize
        public virtual double InternalGetFontSize()
        {
            throw new NotImplementedException();
        }

        public virtual void InternalSetFontSize(double value)
        {
            //throw new NotImplementedException();
        }

        public double FontSize { get { return InternalGetFontSize(); } set { InternalSetFontSize(value); } }

        #endregion


        #region FontFamily
        public virtual FontFamily InternalGetFontFamily()
        {
            throw new NotImplementedException();
        }



        public FontFamily FontFamily { get { return InternalGetFontFamily(); } set { InternalSetFontFamily(value); } }

        #endregion

        #region Text
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


        #endregion

        #region InternalSetFontFamily

        void InternalChangeTextFormat(TextFormat e)
        {
            InternalTextField.defaultTextFormat = e;
            InternalTextField.setTextFormat(e);
        }

        public virtual void InternalSetFontFamily(FontFamily value_)
        {
            // fixme: jsc should fully support base and this calls
            LocalInternalSetFonFamily(value_);
        }

        private void LocalInternalSetFonFamily(FontFamily value_)
        {
            var value = (__FontFamily)(object)value_;

            this.InternalChangeTextFormat(
                    new TextFormat
                    {
                        font = value.InternalFamilyName
                    }
            );
        }

        #endregion

        public Brush Foreground { get { return InternalGetForeground(); } set { InternalSetForeground(value); } }

        #region InternalForeground
        Brush InternalForeground;

        public Brush InternalGetForeground()
        {
            return InternalForeground;
        }

        public void InternalSetForeground(Brush value)
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

        public Brush Background { get { return Brushes.White; } set { InternalSetBackground(value); } }


        #region InternalSetBackground
        public void InternalSetBackground(Brush value)
        {
            var AsSolidColorBrush = value as SolidColorBrush;

            if (AsSolidColorBrush != null)
            {
                var _SolidColorBrush = (__SolidColorBrush)AsSolidColorBrush;
                uint _Color = (__Color)_SolidColorBrush.Color;

                var IsTransparent = _SolidColorBrush.Color.A == Colors.Transparent.A;

                if (IsTransparent)
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
        #endregion


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

    }
}
