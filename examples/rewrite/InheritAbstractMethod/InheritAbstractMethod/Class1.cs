using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view.inputmethod;


namespace android.view.inputmethod
{
    public class InputMethodSession
    {

    }


    public interface InputMethod
    {
        // http://developer.android.com/reference/android/view/inputmethod/InputMethod.html

        void setSessionEnabled(InputMethodSession session, bool enabled);
    }
}


namespace android.inputmethodservice
{
    public class AbstractInputMethodService
    {
        public class AbstractInputMethodImpl : InputMethod
        {
            public void setSessionEnabled(InputMethodSession session, bool enabled)
            {

            }
        }
    }


    public class InputMethodService
    {
        public class InputMethodImpl : AbstractInputMethodService.AbstractInputMethodImpl, InputMethod
        {
        }
    }
}
