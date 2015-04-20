using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using android.view;
using java.lang;
using ScriptCoreLib;
using android.text.method;
using android.text;

namespace android.widget
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/widget/EditText.java
    // http://developer.android.com/reference/android/widget/EditText.html
    [Script(IsNative = true)]
    public class EditText : TextView
    {
        public Editable getText()
        {
            return default(Editable);
        }

        public EditText(Context c)
            : base(c)
        {

        }

        // X:\jsc.svn\core\ScriptCoreLibAndroid.Windows.Forms\ScriptCoreLibAndroid.Windows.Forms\Android\BCLImplementation\System\Windows\Forms\TextBox.cs

        public void setWidth(int pixels)
        {

        }

        public void setHeight(int pixels)
        {

        }

        public void setInputType(int type)
        {
        }

        public void setTransformationMethod(TransformationMethod method)
        {

        }


        // members and types are to be extended by jsc at release build
    }
}
