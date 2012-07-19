
using System;
using System.Data.SQLite;
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.webkit;
using android.widget;
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
        const string DataSource = "AndroidVersionNotifierActivityV2.sqlite";

        #region MyDataTable { ActivityCounter, BootCounter, InternalVersionCounter, InternalVersion, InternalVersionString }
        public class MyDataTable
        {
            InternalSQLiteKeyValueGenericTable Table;

            public int ActivityCounter
            {
                get { return Table.Int32["ActivityCounter"]; }
                set { Table.Int32["ActivityCounter"] = value; }
            }

            public int BootCounter
            {
                get { return Table.Int32["BootCounter"]; }
                set { Table.Int32["BootCounter"] = value; }
            }

            public int InternalVersionCounter
            {
                get { return Table.Int32["InternalVersionCounter"]; }
                set { Table.Int32["InternalVersionCounter"] = value; }
            }

            public int InternalVersion
            {
                get { return Table.Int32["InternalVersion"]; }
                set { Table.Int32["InternalVersion"] = value; }
            }

            public string InternalVersionString
            {
                get { return Table.String["InternalVersionString"]; }
                set { Table.String["InternalVersionString"] = value; }
            }

            public MyDataTable(SQLiteConnection c)
            {
                this.Table = new InternalSQLiteKeyValueGenericTable { Connection = c, Name = "MyDataTable" };
            }

            public override string ToString()
            {
                var w = "{ ";
                
                //w += "ActivityCounter: ";
                w += ((object)this.ActivityCounter).ToString();
                w += ", ";
                //w += "BootCounter: ";
                w += ((object)this.BootCounter).ToString();
                w += ", ";
                //w += "InternalVersionCounter: ";
                w += ((object)this.InternalVersionCounter).ToString();
                w += ", ";
                //w += "InternalVersion: ";
                w += ((object)this.InternalVersion).ToString();
                w += ", ";
                //w += "InternalVersionString: ";
                w += this.InternalVersionString;
                w += "}";


                return w;
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

        public void Invoke(MyDataTable MyDataTable)
        {
            var InternalVersionCounter = MyDataTable.InternalVersionCounter;

            var LiteralVersion = 100;
            var LiteralVersionString = "T-200";

            #region increment InternalVersionCounter in case literal version changes
            if (MyDataTable.InternalVersion != LiteralVersion)
            {
                MyDataTable.InternalVersion = LiteralVersion;
                MyDataTable.InternalVersionCounter++;
            }

            if (!MyDataTable.InternalVersionString.StringEquals(LiteralVersionString))
            {
                MyDataTable.InternalVersionString = LiteralVersionString;
                MyDataTable.InternalVersionCounter++;
            }

            #endregion



            if (MyDataTable.InternalVersionCounter != InternalVersionCounter)
            {
                Notify("New Version Trigger", "InternalVersionCounter has changed!");
            }
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

                Invoke(MyDataTable);

                Notify("InvokeAfterActivity", MyDataTable.ToString());

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

                Invoke(MyDataTable);

                Notify("InvokeAfterActivity", MyDataTable.ToString());


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
        public class Int32Indexer
        {
            public InternalSQLiteKeyValueGenericTable Context;

            public int this[string Key]
            {
                set
                {
                    Context.Create();

                    if (Context.Connection.SQLiteCountByColumnName(Context.Name, "Key", Key) == 0)
                    {
                        var sql = "insert into ";
                        sql += Context.Name;
                        sql += " (Key, ValueInt32) values (";
                        sql += "'";
                        sql += Key;
                        sql += "'";
                        sql += ", ";
                        sql += ((object)value).ToString();
                        sql += ")";


                        android.util.Log.wtf("AndroidVersionNotifierActivity", sql);

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
                    sql += Context.Name;
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


        public Int32Indexer Int32
        {
            get
            {
                return new Int32Indexer { Context = this };
            }
        }
        #endregion

        // XElement is next? :)

        #region String
        public class StringIndexer : InternalSQLiteKeyValueGenericTable
        {
            public InternalSQLiteKeyValueGenericTable Context;

            public string this[string Key]
            {
                set
                {
                    Context.Create();

                    if (Context.Connection.SQLiteCountByColumnName(Context.Name, "Key", Key) == 0)
                    {
                        var sql = "insert into ";
                        sql += Context.Name;
                        sql += " (Key, ValueString) values (";
                        sql += "'";
                        sql += Key;
                        sql += "'";
                        sql += ", ";
                        sql += "'";
                        sql += value;
                        sql += "'";
                        sql += ")";
                        
                        android.util.Log.d("AndroidVersionNotifierActivity", sql);


                        new SQLiteCommand(sql, Context.Connection).ExecuteNonQuery();

                        return;
                    }

                    #region update
                    {
                        var sql = "update ";
                        sql += Context.Name;
                        sql += " set ValueString = ";
                        sql += "'";
                        sql += value;
                        sql += "'";
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


                    var sql = "select ValueString from ";
                    sql += Context.Name;
                    sql += " where Key = ";
                    sql += "'";
                    sql += Key;
                    sql += "'";

                    //new SQLiteCommand(sql, Connection).ExecuteScalar();

                    var value = default(string);
                    var reader = new SQLiteCommand(sql, Context.Connection).ExecuteReader();

                    if (reader.Read())
                    {
                        value = reader.GetString(0);
                    }

                    return value;
                }
            }
        }


        public StringIndexer String
        {
            get
            {
                return new StringIndexer { Context = this };
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
