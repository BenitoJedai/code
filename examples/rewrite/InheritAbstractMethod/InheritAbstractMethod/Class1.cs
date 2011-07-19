using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view.inputmethod;


namespace android.view.inputmethod
{
    public class InputBinding
    {
    }

    public class InputMethodSession
    {

    }


    public interface InputMethod_SessionCallback
    {
        void sessionCreated(InputMethodSession session);
    }

    public interface InputMethod
    {
        // http://developer.android.com/reference/android/view/inputmethod/InputMethod.html

        void createSession(InputMethod_SessionCallback callback);

        void bindInput(InputBinding binding);
    }
}


namespace android.inputmethodservice
{
    public class AbstractInputMethodService
    {
        public abstract class AbstractInputMethodImpl : InputMethod
        {
            public void createSession(InputMethod_SessionCallback callback)
            {

            }

            // watch this
            public abstract void bindInput(InputBinding binding);

        }
    }


    public class InputMethodService
    {
        public class InputMethodImpl : AbstractInputMethodService.AbstractInputMethodImpl, InputMethod
        {
            public override void bindInput(InputBinding binding)
            {

            }
        }
    }
}
