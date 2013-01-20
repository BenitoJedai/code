using java.lang;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // http://developer.android.com/reference/android/view/MotionEvent.html
    [Script(IsNative = true)]
    public class MotionEvent
    {
        public int getAction()
        {
            return default(int);
        }

        public float getX()
        {
            return default(float);
        }

        public float getY()
        {
            return default(float);
        }
    }
}
