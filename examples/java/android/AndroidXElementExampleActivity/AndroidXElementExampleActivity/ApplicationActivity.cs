using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using android.app;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;
using System.Xml.Linq;

namespace AndroidXElementExampleActivity.Activities
{
    public class AndroidXElementExampleActivity : Activity
    {
        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            var sv = new ScrollView(this);
            var ll = new LinearLayout(this);
            ll.setOrientation(0);
            sv.addView(ll);

            var key = new TextView(this).AttachTo(ll);
            var value = new TextView(this).AttachTo(ll);
            var xml = new TextView(this).AttachTo(ll);

            key.setText("foo");
            value.setText("bar");

            xml.setText(
             new XElement("KeyValuePair",
                 new XAttribute("Key", "foo"),
                 new XElement("Value", "bar")
             ).ToString()
            );

            setContentView(sv);

            //this.ShowToast("http://jsc-solutions.net");
        }



    }


}
