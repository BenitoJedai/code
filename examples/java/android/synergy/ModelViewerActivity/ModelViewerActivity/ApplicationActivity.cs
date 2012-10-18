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
using org.jtb.modelview.ui;

namespace org.jtb.modelview
{
    public class ApplicationActivity : BrowseActivity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;

        // http://code.google.com/p/modelview-android/
        // "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\googlecode\modelview-android\res -M "Y:\opensource\googlecode\modelview-android\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J Y:\jsc.svn\examples\java\android\ModelViewerActivity\ModelViewerActivity

        //E/AndroidRuntime( 1669): Caused by: java.lang.SecurityException: Permission Denial: killBackgroundProcesses() from pid=1669, uid=10109 requires android.permission.KILL_BACKGROUND_PROCESSES
        //E/AndroidRuntime( 1669):        at android.os.Parcel.readException(Parcel.java:1425)
        //E/AndroidRuntime( 1669):        at android.os.Parcel.readException(Parcel.java:1379)
        //E/AndroidRuntime( 1669):        at android.app.ActivityManagerProxy.killBackgroundProcesses(ActivityManagerNative.java:3174)
        //E/AndroidRuntime( 1669):        at android.app.ActivityManager.killBackgroundProcesses(ActivityManager.java:1645)
        //E/AndroidRuntime( 1669):        at org.jtb.modelview.ui.BrowseActivity$1.run(BrowseActivity.java:42)
        //E/AndroidRuntime( 1669):        at org.jtb.modelview.ui.BrowseActivity.onResume(BrowseActivity.java:39)
        //E/AndroidRuntime( 1669):        at android.app.Instrumentation.callActivityOnResume(Instrumentation.java:1184)
        //E/AndroidRuntime( 1669):        at android.app.Activity.performResume(Activity.java:5082)
        //E/AndroidRuntime( 1669):        at android.app.ActivityThread.performResumeActivity(ActivityThread.java:2565)
        //E/AndroidRuntime( 1669):        ... 12 more
        //W/ActivityManager(  349):   Force finishing activity org.jtb.modelview/.ApplicationActivity
        //W/ActivityManager(  349): Activity pause timeout for ActivityRecord{42130978 org.jtb.modelview/.ApplicationActivity}
        //I/ActivityManager(  349): No longer want com.skype.raider (pid 26479): hidden #16



        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);


            Toast.makeText(
                 this,
                 "http://jsc-solutions.net",
                 Toast.LENGTH_LONG
             ).show();
        }


    }


}
