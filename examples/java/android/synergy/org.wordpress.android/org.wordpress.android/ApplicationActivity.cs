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

namespace org.wordpress.android
{
    public class ApplicationActivity : Activity
    {
        // "C:\util\android-sdk-windows\platform-tools\aapt.exe"  package -v -f -m -S Y:\opensource\other\android.svn.wordpress.org\res -M "Y:\opensource\other\android.svn.wordpress.org\AndroidManifest.xml" -I "C:\util\android-sdk-windows\platforms\android-16\android.jar" -J Y:\jsc.svn\examples\java\android\org.wordpress.android\org.wordpress.android\


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText("before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );

            var b2 = new Button(this);
            b2.setText("The other button!");
            ll.addView(b2);

            this.setContentView(sv);
        }


    }


}
