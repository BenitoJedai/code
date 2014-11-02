using java.lang;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.text
{
    // http://developer.android.com/reference/android/text/TextWatcher.html

    [Script(IsNative = true)]
    public interface TextWatcher
    {
        void afterTextChanged(Editable s);
        void beforeTextChanged(CharSequence s, int start, int count, int after);
        void onTextChanged(CharSequence s, int start, int before, int count);

    }
}
