using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.os;
using android.view;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Android.Extensions;
using ScriptCoreLib.Android.Manifest;

namespace AndroidShowExtras.Activities
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

            {
                var b = new Button(this).AttachTo(ll);
                b.WithText("extras:");
            }

            // http://about-android.blogspot.com/2009/12/passing-data-or-parameter-to-another_02.html

            this.getIntent().With(
                intent =>
                {
                    intent.getExtras().With(
                        e =>
                        {
                            var keys = e.keySet().toArray();
                            foreach (string key in keys)
                            {
                                var b = new Button(this).AttachTo(ll);
                                b.WithText(new { key }.ToString());
                            }

                        }
                    );
                }
            );

            this.setContentView(sv);
        }


    }


}
