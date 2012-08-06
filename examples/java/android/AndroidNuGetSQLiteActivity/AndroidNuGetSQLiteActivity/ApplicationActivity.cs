
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

namespace AndroidNuGetSQLiteActivity.Activities
{

    [Script(IsNative = true)]
    public static class R
    {


        [Script(IsNative = true)]
        public static class layout
        {
            public static int main;
        }

        [Script(IsNative = true)]
        public static class id
        {
            public static int contentlist;


        }
    }

    public class AndroidNuGetSQLiteActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2011/06/simple-example-using-androids-sqlite.html
        // http://blog.kurtschindler.net/post/getting-started-with-sqlite-and-net

       

        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {

            base.onCreate(savedInstanceState);

            setContentView(R.layout.main);

            TextView listContent = (TextView)findViewById(R.id.contentlist);


            __SQLiteConnectionHack.Context = this;

            MyDatabase.Write();


            var contentRead = "-";

            contentRead = MyDatabase.Read(contentRead);

            listContent.setText(contentRead);

            this.ShowToast("http://jsc-solutions.net");
        }

        

    }

}
