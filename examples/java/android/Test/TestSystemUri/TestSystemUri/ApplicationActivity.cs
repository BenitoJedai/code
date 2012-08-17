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

namespace TestSystemUri.Activities
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

            var uri = new Uri("http://download.jsc-solutions.net/foo/bar.txt?a=1&b=2#frag1/frag2");


            b.WithText("uri: " + new
            {
                uri.OriginalString,
                uri.Scheme,
                uri.PathAndQuery,
                uri.Host,
                uri.Fragment,
                uri.Query,
                uri.AbsolutePath,
                uri.Port
            });

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
