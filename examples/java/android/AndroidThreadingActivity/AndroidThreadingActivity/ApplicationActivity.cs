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
using System.Threading;
using android.util;

namespace AndroidThreadingActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        ScriptCoreLib.Android.IAssemblyReferenceToken ref1;


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

            Action __throw =
                delegate
                {
                    throw new InvalidOperationException();
                };

            var t1 = new Thread(
                () =>
                {
                    //Action<string> w = x => Log.wtf("ApplicationActivity", "thread " + new { id = 1, x });
                    Action<string> w = x => Log.wtf("ApplicationActivity", "thread 1 " + x);

                    w(" start");

                    for (int i = 0; i < 10; i++)
                    {
                        w(" before sleep");
                        Thread.Sleep(700);
                        w(" after sleep");
                    }

                    w(" exit");
                }
            );
            t1.Start();

            var t2 = new Thread(
                () =>
                {
                    Action<string> w = x => Log.wtf("ApplicationActivity", "thread 2 " + x);

                    w(" start");

                    for (int i = 0; i < 10; i++)
                    {
                        w(" before sleep");
                        Thread.Sleep(500);
                        w(" after sleep");
                    }

                    w(" exit");
                }
            );
            t2.Start();

            new Thread(
                () =>
                {
                    Action<string> w = x => Log.wtf("ApplicationActivity", "thread 3 " + x);

                    w(" start");

                    t1.Join();
                    t2.Join();

                    w(" exit");
                }
            ).Start();

            this.setContentView(sv);
        }


    }


}
