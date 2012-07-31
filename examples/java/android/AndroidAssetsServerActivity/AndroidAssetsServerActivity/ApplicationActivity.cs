using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;

namespace AndroidAssetsServerActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        // see also: y:\jsc.svn\examples\java\android\AndroidLacasCameraServerActivity\AndroidLacasCameraServerActivity\com\lacas\testsocket\TestSocketActivity.java

        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


        protected override void onCreate(Bundle savedInstanceState)
        {
            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            //ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);


         
            var b2 = new Button(this);
            b2.setText((java.lang.CharSequence)(object)"The other button!");
            ll.addView(b2);

            this.setContentView(sv);
        }

  
    }

  
}
