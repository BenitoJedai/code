extern alias globalandroid;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using globalandroid::android.view;
using globalandroid::android.widget;
using System.Threading.Tasks;

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

        [Obsolete("useful for 2012 web, await .async.onclick. CTP6 support will follow...")]
        public static Task<T> AtClickAsync<T>(this T v) where T : View
        {
            var c = new TaskCompletionSource<T>();


            v.AtClick(c.SetResult);

            return c.Task;
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
