using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.view;

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
    }
}
