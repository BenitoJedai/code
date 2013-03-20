using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android.graphics.drawable
{
    // http://developer.android.com/reference/android/graphics/drawable/Drawable.html
    [Script(IsNative = true)]
    public abstract class Drawable
    {


        // http://developer.android.com/reference/android/graphics/drawable/Drawable.Callback.html
        [Script(IsNative = true)]
        public interface Callback
        {
        }
    }
}
