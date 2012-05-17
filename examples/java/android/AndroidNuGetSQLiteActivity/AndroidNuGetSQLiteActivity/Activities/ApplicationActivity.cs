
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


            __SQLiteConnectionHack.c = this;
            __SQLiteConnectionHack.MYDATABASE_NAME = "MY_DATABASE.sqlite";

            #region   Create/Open a SQLite database and fill with dummy content and close it
            using (var c = new SQLiteConnection("Data Source=MY_DATABASE.sqlite;Version=3;"))
            {
                c.Open();

                new SQLiteCommand("create table if not exists MY_TABLE (Content text not null)", c).ExecuteNonQuery();

                new SQLiteCommand("delete from MY_TABLE", c).ExecuteNonQuery();

                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 1')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 2')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 3')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 4')", c).ExecuteNonQuery();
                new SQLiteCommand("insert into MY_TABLE (Content) values ('via sql 5')", c).ExecuteNonQuery();



                c.Close();
            }
            #endregion

            __SQLiteConnectionHack.ForceReadOnly = true;


            #region  Open the same SQLite database read all it's content.

            using (var c = new SQLiteConnection("Data Source=MY_DATABASE.sqlite;Version=3;Read Only=True;"))
            {
                c.Open();

                var contentRead = "-";



                var reader = new SQLiteCommand("select Content from MY_TABLE", c).ExecuteReader();
                while (reader.Read())
                {
                    contentRead += "\n";
                    contentRead += (string)reader["Content"];
                }

                listContent.setText(contentRead);

                c.Close();

            }
            #endregion


            this.ShowToast("http://jsc-solutions.net");
        }

    }


}
