using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;
using android.widget;

namespace ScriptCoreLib.Android.Extensions
{
    public static class ViewExtensions
    {
        class OnClickListener : View.OnClickListener
        {
            public Action<View> h;

            public void onClick(View v)
            {
                h(v);
            }
        }

        public static void AtClick(this View v, Action<View> h)
        {
            v.setOnClickListener(
                new OnClickListener { h = h }
            );
        }

        public static T WithText<T>(this T v, string value) where T : Button
        {
            v.setText(value);

            return v;
        }

        public static T AttachTo<T>(this T v, ViewGroup g) where T : View
        {
            g.addView(v);

            return v;
        }
    }
}
