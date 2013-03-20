using android.content;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.view
{
    // http://developer.android.com/reference/android/view/MenuItem.html
    [Script(IsNative = true)]
    public interface MenuItem
    {

        MenuItem setOnMenuItemClickListener(MenuItem_OnMenuItemClickListener menuItemClickListener);


        MenuItem setIcon(int value);
        MenuItem setIntent(Intent value);
    }


    // http://developer.android.com/reference/android/view/MenuItem.OnMenuItemClickListener.html
    [Script(IsNative = true)]
    public interface MenuItem_OnMenuItemClickListener
    {
        bool onMenuItemClick(MenuItem value);

    }
}
