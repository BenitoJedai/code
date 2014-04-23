using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;
using android.widget;
using java.lang;

namespace ScriptCoreLib.Android.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.TextBox))]
    public class __TextBox : __TextBoxBase
    {
        public EditText InternalEditText;

        public int Width
        {
            get
            {
                return Width;
            }
            set
            {
                if (value == default(int))
                    return;
                if (InternalEditText != null)
                {
                    InternalEditText.setWidth(value);
                }
                else
                    InternalWidth = value;
            }
        }
        public int Height
        {
            get
            {
                return Height;
            }
            set
            {
                if (value == default(int))
                    return;
                if (InternalEditText != null)
                {
                    InternalEditText.setHeight(value);
                }
                else
                    InternalHeight = value;
            }
        }

        public char PasswordChar
        {
            get
            {
                return PasswordChar;
            }
            set
            {
                if (value == default(char))
                    return;

                // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\android\widget\EditText.cs

                if (InternalEditText != null)
                {
                    InternalEditText.setInputType(3);
                    InternalEditText.setTransformationMethod(android.text.method.PasswordTransformationMethod.getInstance());
                }
                else
                    InternalPasswordChar = value;
            }
        }

       
        public override View InternalGetElement()
        {
            return InternalEditText;
        }

        public string InternalText;
        public char InternalPasswordChar;
        public int InternalWidth, InternalHeight;

        public override void InternalSetText(string value)
        {
            if (InternalEditText != null)
                InternalEditText.setText((CharSequence)(object)value);
            else
                InternalText = value;
        }

        public override void InternalBeforeSetContext(android.content.Context c)
        {
            InternalEditText = new EditText(c);
            InternalSetText(InternalText);
            PasswordChar = InternalPasswordChar;
            Height = InternalHeight;
            Width = InternalWidth;
        }
    }
}
