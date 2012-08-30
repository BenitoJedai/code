
using System.Data.SQLite;
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.webkit;
using android.widget;
using ScriptCoreLib;
using ScriptCoreLib.Android;
using ScriptCoreLib.Android.Extensions;

namespace AndroidNuGetSQLiteActivity.Activities
{

    public class AndroidNuGetSQLiteActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2011/06/simple-example-using-androids-sqlite.html
        // http://blog.kurtschindler.net/post/getting-started-with-sqlite-and-net

       

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {

            base.onCreate(savedInstanceState);

            var ll = new LinearLayout(this);


            setContentView(ll);

            TextView listContent = new TextView(this).AttachTo(ll);


            __SQLiteConnectionHack.Context = this;

            MyDatabase.Write();


            var contentRead = "-";

            contentRead = MyDatabase.Read(contentRead);

            listContent.setText(contentRead);

            this.ShowToast("http://jsc-solutions.net");
        }

        

    }

}
