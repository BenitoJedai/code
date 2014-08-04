using android.content;
using android.widget;
using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.appwidget
{
    // https://github.com/android/platform_frameworks_base/blob/master/core/java/android/appwidget/AppWidgetHostView.java

    [Script(IsNative = true)]
    public class AppWidgetHostView : FrameLayout
    {
        public AppWidgetHostView(Context context) : base(context)
        {
        }
    }
}
