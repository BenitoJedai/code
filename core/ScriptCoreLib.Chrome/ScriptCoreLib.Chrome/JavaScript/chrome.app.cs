using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript
{
    static partial class chrome
    {
        static partial class app
        {
            static partial class window
            {
                // http://developer.chrome.com/trunk/apps/app.window.html

                public static AppWindow current()
                {
                    return default(AppWindow);
                }
            }
        }
    }
}
