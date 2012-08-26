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
using android.content;

namespace AndroidMenuActivity.Activities
{

    public class ApplicationActivity : Activity
    {
        public event Action<MenuItem> AtOption;

        // inspired by http://thedevelopersinfo.com/2009/10/20/dynamically-change-options-menu-items-in-android/
        public override bool onOptionsItemSelected(MenuItem value)
        {
            if (AtOption != null)
                AtOption(value);


            return false;
        }

        public event Action<Menu> AtPrepareOptions;

        public override bool onPrepareOptionsMenu(Menu value)
        {
            if (AtPrepareOptions != null)
                AtPrepareOptions(value);



            return base.onPrepareOptionsMenu(value);
        }

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


            AtPrepareOptions +=
                value =>
                {
                    value.clear();

                    var item1 = value.add(
                         (java.lang.CharSequence)(object)"http://abstractatech.com"
                    );



                    item1.setIcon(android.R.drawable.ic_menu_view);

                    var item2 = value.add(
                        (java.lang.CharSequence)(object)"http://jsc-solutions.net"
                    );

                    item2.setIcon(android.R.drawable.ic_menu_edit);

                    var i = new Intent(Intent.ACTION_VIEW,
                        android.net.Uri.parse("http://jsc-solutions.net")
                    );

                    // http://vaibhavsarode.wordpress.com/2012/05/14/creating-our-own-activity-launcher-chooser-dialog-android-launcher-selection-dialog/
                    var ic = Intent.createChooser(i, "http://jsc-solutions.net");


                    item2.setIntent(
                        ic
                    );
                };

            AtOption +=
                item =>
                {

                    b.WithText("menu was clicked!" + (string)(object)item.getTitle());
                };

            var b2 = new Button(this);
            b2.setText("The other button!");
            ll.addView(b2);

            this.setContentView(sv);
        }


    }


}
