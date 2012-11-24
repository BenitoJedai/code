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
using ScriptCoreLib.Android.Manifest;

namespace TestJavaSourceWithinAssetsLibrary.Activities
{
    public class ApplicationActivity : Activity
    {

        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var b = new Button(this).AttachTo(ll);



            b.WithText(Foo.Bar("hello via .java"));
            b.AtClick(
                v =>
                {
                    b.WithText(Foo.Bar("click!"));
                }
            );



            this.setContentView(sv);
        }


    }


}
