using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows.Controls
{
    [Script(Implements = typeof(global::System.Windows.Controls.PasswordBox))]
    internal class __PasswordBox : __Control
    {





        public readonly TextField InternalTextField;
        public readonly Sprite InternalTextFieldContainer;

        public override ScriptCoreLib.ActionScript.flash.display.InteractiveObject InternalGetDisplayObject()
        {
            return InternalTextFieldContainer;
        }




        const int InternalOffsetY = -1;

        public __PasswordBox()
        {
            // X:\jsc.svn\examples\actionscript\air\AIRAvalonPasswordField\AIRAvalonPasswordField\ApplicationCanvas.cs

            InternalTextFieldContainer = new Sprite();


            InternalTextField = new TextField
            {
                autoSize = TextFieldAutoSize.LEFT,
                type = TextFieldType.INPUT,
                displayAsPassword = true,

                background = true,
                backgroundColor = 0xffffffff,
                alwaysShowSelection = true,
                border = true,
                borderColor = 0x808080
                // http://code.hellokeita.in/public/trunk/as3/br/hellokeita/utils/TextFieldColor.as

            };

            InternalTextField.y = InternalOffsetY;



            InternalTextFieldContainer = new Sprite();
            InternalTextFieldContainer.addChild(InternalTextField);

            // http://www.typetester.org/
            //InternalTextField.defaultTextFormat.font = "Verdana";
            //LocalInternalSetFonFamily(new FontFamily("Verdana"));
        }


        public event RoutedEventHandler PasswordChanged;

        public string Password
        {
            get
            {
                return InternalTextField.text.Replace("\r", Environment.NewLine);
            }
            set
            {
                // http://blog.madebyderek.com/archives/2005/08/26/textfield_newline_and_crlf/
                InternalTextField.text = value.Replace(Environment.NewLine, "\n");


                if (PasswordChanged != null)
                    PasswordChanged(this, new RoutedEventArgs());

                //InternalRaiseTextChanged();
            }
        }



        #region width, height
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
        #endregion
    }
}
