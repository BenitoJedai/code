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

namespace TestGenericArray.Activities
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



            b.WithText("before AtClick");
            b.AtClick(
                v =>
                {
                    b.setText("AtClick");
                }
            );

       
            this.setContentView(sv);



            var x = new __List<foo>
            {
                Item1 = new foo(),
                Item2 = new foo(),
            };

            //object[] z = new object[0];
            //// Error	1	Cannot implicitly convert type 'object[]' to 'TestGenericArray.Activities.foo[]'. An explicit conversion exists (are you missing a cast?)	Y:\jsc.svn\examples\java\android\Test\TestGenericArray\TestGenericArray\Program.cs	24	23	TestGenericArray
            //// Unable to cast object of type 'System.Object[]' to type 'TestGenericArray.Activities.foo[]'.
            //foo[] y = (foo[])/* recreate array? */(object)z;
            var a = x.ToArray();



            b.WithText("done! " + a.Length);
         

        }


    }

    class foo
    {
    }

    class __List<__object>
    {
        public __object Item1;
        public __object Item2;


        public __object[] ToArray()
        {
            return new[] { Item1, Item2 };
        }
    }


}
