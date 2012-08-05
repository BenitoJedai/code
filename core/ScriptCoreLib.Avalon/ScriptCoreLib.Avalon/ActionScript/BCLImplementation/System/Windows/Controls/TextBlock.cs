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
    internal class __TextBlock : __FrameworkElement
    {
        // where is this used?
        // Y:\jsc.svn\examples\actionscript\AvalonFlashLinqToObjects\AvalonFlashLinqToObjects\ApplicationCanvas.cs

        public readonly TextField InternalTextField;
        public readonly Sprite InternalTextFieldContainer;

        public override ScriptCoreLib.ActionScript.flash.display.InteractiveObject InternalGetDisplayObject()
        {
            return InternalTextFieldContainer;
        }


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

        #region FontSize
        public virtual double InternalGetFontSize()
        {
            throw new NotImplementedException();
        }

        public virtual void InternalSetFontSize(double value)
        {
            throw new NotImplementedException();
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



    }
}
