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
        public static int ACTION_UP;
        public static int ACTION_DOWN;
        public static int ACTION_MASK;
        public static int ACTION_POINTER_DOWN;
        public static int ACTION_MOVE;
        public static int ACTION_OUTSIDE;

        public int getAction()
        {
            return default(int);
        }


        public int getPointerCount()
        {
            throw null;
        }

        public float getRawX()
        {
            throw null;
        }


        public float getRawY()
        {
            throw null;
        }
        public float getX(int pointerIndex)
        {
            throw null;
        }
        public float getY(int pointerIndex)
        {
            throw null;
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
