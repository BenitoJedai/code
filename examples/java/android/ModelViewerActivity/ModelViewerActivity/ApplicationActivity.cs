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

        // "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\googlecode\modelview-android\res -M "Y:\opensource\googlecode\modelview-android\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J Y:\jsc.svn\examples\java\android\ModelViewerActivity\ModelViewerActivity


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
