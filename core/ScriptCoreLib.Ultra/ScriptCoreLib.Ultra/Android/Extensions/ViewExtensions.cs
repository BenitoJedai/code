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
            public Action h;

            public void onClick(View v)
            {
                h();
            }
        }

        public static T AtClick<T>(this T v, Action<T> h) where T : View
        {
            var x = new OnClickListener
            {
                h = delegate
                {
                    h(v);
                }
            };

            v.setOnClickListener(x);

            return v;
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
