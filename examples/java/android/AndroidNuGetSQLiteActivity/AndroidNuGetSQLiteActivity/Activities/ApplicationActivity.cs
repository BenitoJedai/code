
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



            #region   Create/Open a SQLite database and fill with dummy content and close it
            using (var c = new __SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;", this, "MY_DATABASE.sqlite"))
            {
                c.Open();

                new __SQLiteCommand(c, "create table if not exists MY_TABLE (Content text not null)").ExecuteNonQuery();

                new __SQLiteCommand(c, "delete from MY_TABLE").ExecuteNonQuery();

                new __SQLiteCommand(c, "insert into MY_TABLE (Content) values ('via sql 1')").ExecuteNonQuery();
                new __SQLiteCommand(c, "insert into MY_TABLE (Content) values ('via sql 2')").ExecuteNonQuery();
                new __SQLiteCommand(c, "insert into MY_TABLE (Content) values ('via sql 3')").ExecuteNonQuery();



                c.Close();
            }
            #endregion

            #region  Open the same SQLite database read all it's content.

            using (var c = new __SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;", this, "MY_DATABASE.sqlite"))
            {
                c.Open(ReadOnly: true);

                var contentRead = c.queueAll();
                c.Close();


                listContent.setText(contentRead);

            }
            #endregion


            this.ShowToast("http://jsc-solutions.net");
        }

    }

    public class __SQLiteCommand
    {
        __SQLiteConnection c;
        string sql;

        public __SQLiteCommand(__SQLiteConnection c, string sql)
        {
            this.c = c;
            this.sql = sql;
        }

        public int ExecuteNonQuery()
        {
            c.db.execSQL(sql);
            return 0;
        }
    }

    public abstract class __DbConnection : System.IDisposable
    {

        public abstract void Dispose();
    }

    public class __SQLiteConnection : __DbConnection
    {
        public const string MYDATABASE_TABLE = "MY_TABLE";
        public const string KEY_CONTENT = "Content";

        //create table MY_DATABASE (ID integer primary key, Content text not null);
        private const string SCRIPT_CREATE_DATABASE = "create table MYDATABASE_TABLE (Content text not null);";

        private AtCreate h;
        public SQLiteDatabase db;


        public __SQLiteConnection(string connectionstring, Context c, string MYDATABASE_NAME)
        {
            this.h = new AtCreate(c, MYDATABASE_NAME);
        }

        public __SQLiteConnection Open(bool ReadOnly = false)
        {
            if (ReadOnly)
                db = h.getReadableDatabase();
            else
                db = h.getWritableDatabase();

            return this;
        }



        public void Close()
        {
            h.close();
        }

        public int deleteAll()
        {
            return db.delete(MYDATABASE_TABLE, null, null);
        }

        public string queueAll()
        {
            var columns = new[] { KEY_CONTENT };
            Cursor cursor = db.query(MYDATABASE_TABLE, columns,
              null, null, null, null, null);

            var result = new java.lang.StringBuilder();

            int index_CONTENT = cursor.getColumnIndex(KEY_CONTENT);
            for (cursor.moveToFirst(); !(cursor.isAfterLast()); cursor.moveToNext())
            {
                result.append(cursor.getString(index_CONTENT)).append("\n");
            }

            return result.ToString();
        }

        public class AtCreate : SQLiteOpenHelper
        {

            public AtCreate(Context context, string name, android.database.sqlite.SQLiteDatabase.CursorFactory factory = null, int version = 1)
                : base(context, name, factory, version)
            {

            }

            public override void onCreate(SQLiteDatabase db)
            {
                // TODO Auto-generated method stub
                //db.execSQL(SCRIPT_CREATE_DATABASE);
            }

            public override void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
            {
                // TODO Auto-generated method stub

            }

        }


        public override void Dispose()
        {
            this.Close();
        }
    }

}
