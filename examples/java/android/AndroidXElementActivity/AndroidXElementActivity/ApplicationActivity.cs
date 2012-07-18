using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using android.app;
using android.provider;
using android.webkit;
using android.widget;
using AndroidXElementActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidXElementActivity.Activities
{
    public class ApplicationActivity : Activity
    {
        public const string Version = "http://www.jsc-solutions.net/assets/PromotionWebApplication1/jsc.configuration.application";



        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // http://www.dreamincode.net/forums/topic/130521-android-part-iii-dynamic-layouts/

            base.onCreate(savedInstanceState);

            ScrollView sv = new ScrollView(this);

            LinearLayout ll = new LinearLayout(this);

            ll.setOrientation(LinearLayout.VERTICAL);

            sv.addView(ll);

            //// http://stackoverflow.com/questions/9784570/webview-inside-scrollview-disappears-after-zooming
            //// http://stackoverflow.com/questions/8123804/unable-to-add-web-view-dynamically
            //// http://developer.android.com/reference/android/webkit/WebView.html



            TextView tv = new TextView(this);

            tv.setText("What would you like to create today?");

            ll.addView(tv);



            this.setContentView(sv);

            var w = "";

            try
            {
                var url = new java.net.URL(Version);
                var i = new java.io.InputStreamReader(url.openStream(), "UTF-8");
                var reader = new java.io.BufferedReader(i);

                // can't we just read to the end?
                var line = reader.readLine();
                while (line != null)
                {
                    w += line;
                    w += "\n";

                    line = reader.readLine();
                }
                reader.close();
            }
            catch
            {
                // oops
            }

            {
                var value = w;
                var offset = 0;

                var i = ((java.lang.String)(object)value).indexOf("version=\"", offset) + "version=\"".Length;
                var j = ((java.lang.String)(object)value).indexOf("\"", i);

                var ii = ((java.lang.String)(object)value).indexOf("version=\"", j) + "version=\"".Length;
                var jj = ((java.lang.String)(object)value).indexOf("\"", ii);

                var version = ((java.lang.String)(object)value).substring(ii, jj);


                this.ShowLongToast("http://jsc-solutions.net " + version);
            }
        }


    }


    [Script(
    Implements = typeof(global::System.String),
    ImplementationType = typeof(global::java.lang.String),
    InternalConstructor = true

    )]
    internal class __String
    {
        public int Length
        {
            [Script(ExternalTarget = "length")]
            get
            {
                return default(int);
            }
        }

        [Script(ExternalTarget = "indexOf")]
        public int IndexOf(string str, int pos)
        {
            return default(int);
        }
    }


}

namespace java.lang
{
    [Script(IsNative = true)]
    public sealed class String
    {
        public int indexOf(string @str, int @fromIndex)
        {
            return default(int);
        }

        public string substring(int @beginIndex, int @endIndex)
        {
            return default(string);
        }
    }
}