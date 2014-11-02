using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.content;
using ScriptCoreLib;

namespace android.view
{
    // https://android.googlesource.com/platform/frameworks/base.git/+/master/core/java/android/view/View.java
    // http://developer.android.com/reference/android/view/View.html
    [Script(IsNative = true)]
    public class View
    {
        public void setPadding(int left, int top, int right, int bottom)
        {
        }

        public ViewParent getParent()
        {
            return default(ViewParent);
        }

        public virtual void setScrollBarStyle(int style)
        {
        }

        public virtual bool dispatchKeyEvent(KeyEvent @event)
        {
            throw null;

        }


        public virtual bool onTouchEvent(MotionEvent @event)
        {
            throw null;
        }

        // http://developer.android.com/reference/android/view/View.OnTouchListener.html
        [Script(IsNative = true)]
        public interface OnTouchListener
        {
            bool onTouch(View v, MotionEvent @event);
        }

        public void setOnTouchListener(View.OnTouchListener l)
        { }

        // members and types are to be extended by jsc at release build

        public View(Context c)
        {

        }

        public int getWidth()
        {
            throw null;
        }

        public int getHeight()
        {
            throw null;
        }

        public void setBackgroundColor(int color)
        { }


        public virtual ViewGroup.LayoutParams getLayoutParams()
        {
            throw null;
        }
        public virtual void setLayoutParams(ViewGroup.LayoutParams @params)
        { }

        public void setTag(Object tag)
        {
        }

        public Object getTag()
        {
            throw null;
        }

        [Script(IsNative = true)]
        public interface OnClickListener
        {
            void onClick(View v);
        }

        public void setOnClickListener(OnClickListener h)
        {

        }

        public void setClickable(bool clickable)
        {
        }


        public Context getContext()
        {
            throw null;
        }
    }
}
