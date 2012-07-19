
using System;
using System.Data.SQLite;
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.webkit;
using android.widget;
using AndroidVersionNotifierActivity.Library;
using java.lang;
using ScriptCoreLib;
using ScriptCoreLib.Android;

namespace AndroidVersionNotifierActivity.Activities
{
    public class AndroidVersionNotifierActivity : Activity
    {
        protected override void onCreate(global::android.os.Bundle savedInstanceState)
        {
            // this activity 
            // check for db table
            // 
            //  shall resume on boot
            //  shall recheck every n seconds
            //  shall remember the last result
            //  shall remember how many iterations
            //  shall remember how many boots

            base.onCreate(savedInstanceState);

            #region setContentView
            ScrollView sv = new ScrollView(this);
            LinearLayout ll = new LinearLayout(this);
            ll.setOrientation(LinearLayout.VERTICAL);
            sv.addView(ll);

            var startservice = new Button(this);
            startservice.setText("Yay");

            ll.addView(startservice);




            this.setContentView(sv);
            #endregion



            var intent = new Intent(this, NotifyServiceFromActivity.Class);
            this.startService(intent);

        }



    }

    #region IntentFilter
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    sealed class IntentFilterAttribute : Attribute
    {
        // jsc does not support properties yet? are they even allowed in java?

        public string Action;
    }
    #endregion

    public abstract class AbstractNotifyService : Service
    {
        const string DataSource = "AndroidVersionNotifierActivity.sqlite";

        #region MyDataTable { ActivityCounter, BootCounter }
        class MyDataTable
        {
            InternalSQLiteKeyValueInt32Table Table;

            public int ActivityCounter
            {
                get { return Table["ActivityCounter"]; }
                set { Table["ActivityCounter"] = value; }
            }

            public int BootCounter
            {
                get { return Table["BootCounter"]; }
                set { Table["BootCounter"] = value; }
            }

            public MyDataTable(SQLiteConnection c)
            {
                this.Table = new InternalSQLiteKeyValueInt32Table { Connection = c, Name = "MyDataTable" };
            }
        }
        #endregion


        public void Notify(string Title, string Content)
        {
            // Send Notification
            var notificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);

            var myNotification = new Notification(
                android.R.drawable.star_on,
                (CharSequence)(object)Title,
                java.lang.System.currentTimeMillis()
            );

            Context context = getApplicationContext();

            Intent myIntent = new Intent(Intent.ACTION_VIEW, android.net.Uri.parse("http://www.jsc-solutions.net"));

            PendingIntent pendingIntent
              = PendingIntent.getActivity(getBaseContext(),
                0, myIntent,
                Intent.FLAG_ACTIVITY_NEW_TASK);
            myNotification.defaults |= Notification.DEFAULT_SOUND;
            myNotification.flags |= Notification.FLAG_AUTO_CANCEL;
            myNotification.setLatestEventInfo(context,
                    (CharSequence)(object)Title,
                    (CharSequence)(object)Content,
               pendingIntent);
            notificationManager.notify(1, myNotification);
        }

        public void InvokeAfterActivity()
        {
            __SQLiteConnectionHack.Context = this;

            using (var c = new SQLiteConnection(

                new SQLiteConnectionStringBuilder
                {
                    DataSource = DataSource,
                    Version = 3,
                }.ConnectionString
                )
             )
            {
                c.Open();

                var MyDataTable = new MyDataTable(c);

                var i = MyDataTable;

                MyDataTable.ActivityCounter++;

                var status = "ActivityCounter: ";
                status += ((object)MyDataTable.ActivityCounter).ToString();
                status += " BootCounter: ";
                status += ((object)MyDataTable.BootCounter).ToString();


                Notify("InvokeAfterActivity", status);

                c.Close();

            }
        }

        public void InvokeAfterBootComplete()
        {
            __SQLiteConnectionHack.Context = this;

            using (var c = new SQLiteConnection(

                new SQLiteConnectionStringBuilder
                {
                    DataSource = DataSource,
                    Version = 3,
                }.ConnectionString
                )
             )
            {
                c.Open();

                var MyDataTable = new MyDataTable(c);

                var i = MyDataTable;

                MyDataTable.BootCounter++;

                var status = "ActivityCounter: ";
                status += ((object)MyDataTable.ActivityCounter).ToString();
                status += " BootCounter: ";
                status += ((object)MyDataTable.BootCounter).ToString();

                Notify("InvokeAfterBootComplete", status);

                c.Close();

            }
        }
    }

    #region NotifyServiceFromActivity
    public sealed class NotifyServiceFromActivity : AbstractNotifyService
    {
        public static Class Class
        {
            [Script(OptimizedCode = "return NotifyServiceFromActivity.class;")]
            get
            {
                return null;
            }
        }


        public override void onCreate()
        {
            base.onCreate();
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
            this.InvokeAfterActivity();

            this.stopSelf();

            return 0;
        }

        public override void onDestroy()
        {
            base.onDestroy();
        }



        public override android.os.IBinder onBind(Intent value)
        {
            return null;
        }

    }
    #endregion

    #region NotifyService
    public sealed class NotifyService : AbstractNotifyService
    {
        public static Class Class
        {
            [Script(OptimizedCode = "return NotifyService.class;")]
            get
            {
                return null;
            }
        }


        public override void onCreate()
        {
            base.onCreate();
        }

        public override int onStartCommand(Intent value0, int value1, int value2)
        {
            this.InvokeAfterBootComplete();

            this.stopSelf();

            return 0;
        }

        public override void onDestroy()
        {
            base.onDestroy();
        }



        public override android.os.IBinder onBind(Intent value)
        {
            return null;
        }

    }
    #endregion



    [IntentFilter(Action = "android.intent.action.BOOT_COMPLETED")]
    public class AtBootCompleted : BroadcastReceiver
    {
        public override void onReceive(Context c, Intent i)
        {
            var that = c;

            //that.ShowToast("AtBootCompleted");

            var intent = new Intent(that, NotifyService.Class);
            that.startService(intent);
        }
    }

    class InternalSQLiteKeyValueGenericTable
    {
        // CRUD

        public SQLiteConnection Connection { get; set; }
        public string Name { get; set; }

        public bool Exists
        {
            get { return Connection.SQLiteTableExists(Name); }
        }

        public void Create()
        {
            if (this.Exists)
                return;

            // key value table!

            var sql = "create table if not exists ";

            sql += Name;
            sql += " (Key text not null, ValueString text, ValueInt32 integer)";

            new SQLiteCommand(sql, Connection).ExecuteNonQuery();

            // http://www.sqlite.org/datatype3.html
        }

        #region Int32
        public class InternalSQLiteKeyValueInt32Table : InternalSQLiteKeyValueGenericTable
        {
            public InternalSQLiteKeyValueGenericTable Context;

            public int this[string Key]
            {
                set
                {
                    Context.Create();

                    if (Context.Connection.SQLiteCountByColumnName(this.Name, "Key", Key) == 0)
                    {
                        var sql = "insert into ";
                        sql += Name;
                        sql += "(ValueInt32) values (";
                        sql += ((object)value).ToString();
                        sql += ")";

                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();

                        return;
                    }

                    #region update
                    {
                        var sql = "update ";
                        sql += Context.Name;
                        sql += " set ValueInt32 = ";
                        sql += ((object)value).ToString();
                        sql += " where Key = ";
                        sql += "'";
                        sql += Key;
                        sql += "'";

                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();
                    }
                    #endregion

                }

                get
                {
                    Context.Create();


                    var sql = "select ValueInt32 from ";
                    sql += Name;
                    sql += " where Key = ";
                    sql += "'";
                    sql += Key;
                    sql += "'";

                    //new SQLiteCommand(sql, Connection).ExecuteScalar();

                    var value = 0;
                    var reader = new SQLiteCommand(sql, Context.Connection).ExecuteReader();

                    if (reader.Read())
                    {
                        value = reader.GetInt32(0);
                    }

                    return value;
                }
            }
        }


        public InternalSQLiteKeyValueInt32Table Int32
        {
            get 
            {
                return new InternalSQLiteKeyValueInt32Table { Context = this };
            }
        }
        #endregion
    }

    static class X
    {

        public static int SQLiteCountByColumnName(this SQLiteConnection Connection, string Name, string ByColumnName, string ValueString)
        {
            var sql = "select count(*) from ";
            sql += Name;
            sql += " where ";
            sql += ByColumnName;
            sql += " = ";
            sql += "'";
            sql += ValueString;
            sql += "'";

            var value = 0;
            var reader = new SQLiteCommand(sql, Connection).ExecuteReader();

            if (reader.Read())
            {
                value = reader.GetInt32(0);
            }

            return value;
        }

        public static bool SQLiteTableExists(this SQLiteConnection c, string name)
        {
            var w = "select name from sqlite_master where type='table' and name=";
            w += "'";
            w += name;
            w += "'";


            var reader = new SQLiteCommand(w, c).ExecuteReader();


            while (reader.Read())
            {
                return true;
            }

            return false;
        }
    }

}
