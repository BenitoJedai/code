using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.text;
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
                    // X:\jsc.svn\examples\javascript\android\Test\TestPINDialog\TestPINDialog\ApplicationWebService.cs

                    InternalEditText.setInputType(3);

                    //InternalEditText.setInputType(
                    //    android.text.InputType.TYPE_CLASS_NUMBER |
                    //    android.text.InputType.TYPE_NUMBER_VARIATION_PASSWORD);

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

        public override string InternalGetText()
        {
            if (InternalEditText != null)
            {
                // Caused by: java.lang.ClassCastException: android.text.SpannableStringBuilder cannot be cast to java.lang.String

                var text = this.InternalEditText.getText() + "";
                return text;

            }

            return InternalText;
        }

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

            //InternalEditText.addTextChangedListener(
            //    new xTextWatcher
            //{
            //    yield = delegate
            //    {
            //        this.InternalText = InternalEditText.getText();
            //    }
            //}
            //);


            PasswordChar = InternalPasswordChar;
            Height = InternalHeight;
            Width = InternalWidth;
        }
    }

    [Script]
    class xTextWatcher : TextWatcher
    {
        public Action<Editable> yield;

        public void afterTextChanged(Editable value)
        {
            yield(value);
        }

        public void beforeTextChanged(CharSequence arg0, int arg1, int arg2, int arg3)
        {
        }

        public void onTextChanged(CharSequence arg0, int arg1, int arg2, int arg3)
        {
        }
    }
}
