using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android.Extensions;

namespace wei.mark.example
{
    public class ApplicationActivity : StandOutExampleActivity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        /*
Y:\opensource\github\StandOut\library\src\wei\mark\standout\StandOutWindow.java:2112: cannot find symbol
symbol  : class FrameLayout
location: class wei.mark.standout.StandOutWindow
        public class Window extends FrameLayout {         
         
         * Y:\opensource\github\StandOut\library\src\wei\mark\standout\StandOutWindow.java:1137: package R does not exist
                                        R.layout.drop_down_list_item, null);
         * 
         * "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\github\StandOut\library\res -M "Y:\opensource\github\StandOut\library\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J y:\jsc.svn\examples\java\android\StandOutActivity\StandOutActivity\

         * Y:\opensource\github\StandOut\example\src\wei\mark\example\MultiWindow.java:54: package R does not exist
                View view = inflater.inflate(R.layout.body, frame, true);
         * 
         * "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\github\StandOut\example\res -M "Y:\opensource\github\StandOut\example\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J y:\jsc.svn\examples\java\android\StandOutActivity\StandOutActivity\

W/ActivityManager(  349): Unable to start service Intent { act=CLOSE_ALL cmp=wei.mark.example/.SimpleWindow }: not found
W/ActivityManager(  349): Unable to start service Intent { act=CLOSE_ALL cmp=wei.mark.example/.MultiWindow }: not found
W/ActivityManager(  349): Unable to start service Intent { act=CLOSE_ALL cmp=wei.mark.example/.WidgetsWindow }: not found
W/ActivityManager(  349): Unable to start service Intent { act=SHOW cmp=wei.mark.example/.SimpleWindow (has extras) }: not found
W/ActivityManager(  349): Unable to start service Intent { act=SHOW cmp=wei.mark.example/.MultiWindow (has extras) }: not found
W/ActivityManager(  349): Unable to start service Intent { act=SHOW cmp=wei.mark.example/.WidgetsWindow (has extras) }: not found
         * 
         * 
E/AndroidRuntime( 2974): Caused by: java.lang.NullPointerException
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow$Window.getSystemDecorations(StandOutWindow.java:2447)
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow$Window.<init>(StandOutWindow.java:2181)
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow.show(StandOutWindow.java:1416)
E/AndroidRuntime( 2974):        at wei.mark.standout.StandOutWindow.onStartCommand(StandOutWindow.java:716)
E/AndroidRuntime( 2974):        at android.app.ActivityThread.handleServiceArgs(ActivityThread.java:2490) 
         */

        public override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

        }


    }


}
