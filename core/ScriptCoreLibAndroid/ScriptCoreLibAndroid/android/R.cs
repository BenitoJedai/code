using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace android
{
    // what about the R
    // for apps the R package name is linked to
    // X:\jsc.internal.git\compiler\jsc.internal\jsc.internal\meta\Commands\Reference\ReferenceAssetsLibrary.cs
    //     * "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\github\StandOut\library\res -M "Y:\opensource\github\StandOut\library\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J y:\jsc.svn\examples\java\android\StandOutActivity\StandOutActivity\
    
    [Script(IsNative = true)]
    public sealed class R
    {
        [Script(IsNative = true)]
        public sealed class drawable
        {
            public const int ic_menu_view = 17301591;


            public static int ic_menu_close_clear_cancel;


            public static int star_on;
        }
    }
}
