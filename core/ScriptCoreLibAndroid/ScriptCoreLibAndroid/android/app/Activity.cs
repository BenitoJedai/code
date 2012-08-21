﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;

namespace android.app
{
    // http://developer.android.com/reference/android/app/Activity.html
    [Script(IsNative = true)]
    public class Activity : ContextThemeWrapper
    {
        // members and types are to be extended by jsc at release build

        // http://developer.android.com/reference/android/app/Activity.html#setContentView(android.view.View)
        public virtual void setContentView(View savedInstanceState)
        { 
        }

        protected virtual void onCreate(Bundle savedInstanceState)
        { 
        
        }


        // http://developer.android.com/reference/android/app/Activity.html#requestWindowFeature(int)
        public virtual bool requestWindowFeature(int e)
        {
            return default(bool);
        }


        public virtual bool onKeyDown(int keyCode, KeyEvent @event)
        {
            return default(bool);
        }

        public  void setTitle(string e)
        {

        }
    }
}
