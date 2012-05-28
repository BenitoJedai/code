
using System.Data.SQLite;
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.webkit;
using android.widget;
using AndroidNuGetSQLiteActivity.Library;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidNuGetSQLiteActivity.Activities
{
    public class AndroidNuGetSQLiteActivity : Activity
    {
        // inspired by http://android-er.blogspot.com/2011/06/simple-example-using-androids-sqlite.html
        // http://blog.kurtschindler.net/post/getting-started-with-sqlite-and-net

        // C:\util\android-sdk-windows\tools\android.bat create project --package AndroidNuGetSQLiteActivity.Activities --activity AndroidNuGetSQLiteActivity  --target 2  --path y:\jsc.svn\examples\java\android\AndroidNuGetSQLiteActivity\AndroidNuGetSQLiteActivity\staging\


        // running it in emulator:
        // start C:\util\android-sdk-windows\tools\android.bat avd
        // "C:\util\android-sdk-windows\platform-tools\adb.exe" install -r  "y:\jsc.svn\examples\java\android\AndroidNuGetSQLiteActivity\AndroidNuGetSQLiteActivity\staging\bin\AndroidNuGetSQLiteActivity-debug.apk"

        // note: rebuild could auto reinstall

        // running it on device:
        // attach device to usb
        //Z:\jsc.svn\examples\java\android\HelloAndroid>C:\util\android-sdk-windows\platform-tools\adb.exe devices
        //List of devices attached
        //3330A17632C000EC        device 

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
