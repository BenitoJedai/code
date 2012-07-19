
using System;
using System.Data.SQLite;
using android.app;
using android.content;
using android.database;
using android.database.sqlite;
using android.provider;
using android.view;
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

            // http://stackoverflow.com/questions/6522792/get-list-of-active-pendingintents-in-alarmmanager
            var myIntent = new Intent(this, NotifyServiceFromTimer.Class);
            this.pendingIntent = PendingIntent.getService(this, 0, myIntent, 0);

            
            AlarmManager alarmManager = (AlarmManager)this.getSystemService(ALARM_SERVICE);
            alarmManager.cancel(this.pendingIntent);

                #region startservice
             startservice = new Button(this);
            startservice.setText("Start Timer");
            startservice.setOnClickListener(
                new startservice_onclick { that = this }
            );
            ll.addView(startservice);
            #endregion

            #region stopservice
             stopservice = new Button(this);
            stopservice.setText("Stop Timer");
            stopservice.setEnabled(false);
            stopservice.setOnClickListener(
                new stopservice_onclick { that = this }
            );
            ll.addView(stopservice);
            #endregion




            this.setContentView(sv);
            #endregion



            var intent = new Intent(this, NotifyServiceFromActivity.Class);
            this.startService(intent);

        }

        public Button startservice;
        public Button stopservice;
        public PendingIntent pendingIntent;
        #region startservice_onclick
        class startservice_onclick : android.view.View.OnClickListener
        {
            public AndroidVersionNotifierActivity that;

            public void onClick(View v)
            {


                AlarmManager alarmManager = (AlarmManager)that.getSystemService(ALARM_SERVICE);



                //alarmManager.set(AlarmManager.RTC, 1000 * 5, that.pendingIntent);
                alarmManager.setInexactRepeating(
                    AlarmManager.RTC,
                    1000 * 5,
                    1000 * 25, 
                    that.pendingIntent
                );

                that.startservice.setEnabled(false);
                that.stopservice.setEnabled(true);
            }
        }
        #endregion

        #region stopservice_onclick
        class stopservice_onclick : android.view.View.OnClickListener
        {
            public AndroidVersionNotifierActivity that;


            public void onClick(View v)
            {
                AlarmManager alarmManager = (AlarmManager)that.getSystemService(ALARM_SERVICE);
                alarmManager.cancel(that.pendingIntent);

                that.startservice.setEnabled(true);
                that.stopservice.setEnabled(false);

            }
        }
        #endregion


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

            public int InvokeCounter
            {
                get { return Table.Int32["InvokeCounter"]; }
                set { Table.Int32["InvokeCounter"] = value; }
            }

            public int ActivityCounter
            {
                get { return Table.Int32["ActivityCounter"]; }
                set { Table.Int32["ActivityCounter"] = value; }
            }

            public int TimerCounter
            {
                get { return Table.Int32["TimerCounter"]; }
                set { Table.Int32["TimerCounter"] = value; }
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

                w += "i";
                w += ((object)this.InvokeCounter).ToString();
                w += ", ";

                //w += "ActivityCounter: ";
                w += "a";
                w += ((object)this.ActivityCounter).ToString();
                w += ", ";
                //w += "BootCounter: ";
                w += "t";
                w += ((object)this.TimerCounter).ToString();
                w += ", ";

                w += "b";
                w += ((object)this.BootCounter).ToString();
                w += ", ";
                //w += "InternalVersionCounter: ";
                w += "v";
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


        public void Notify(string Title, string Content, int id = 0)
        {
            // Send Notification
            var notificationManager = (NotificationManager)getSystemService(Context.NOTIFICATION_SERVICE);

            var w = Title + " ";
            w += Content;

            var myNotification = new Notification(
                android.R.drawable.star_on,
                (CharSequence)(object)w,
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

            notificationManager.notify(id, myNotification);
        }

        public void Invoke(MyDataTable MyDataTable)
        {
            var InternalVersionCounter = MyDataTable.InternalVersionCounter;

            var LiteralVersion = 101;
            var LiteralVersionString = "T-201";

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

        public void InvokeAfterTimer()
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

                MyDataTable.TimerCounter++;
                MyDataTable.InvokeCounter++;

                Invoke(MyDataTable);

                Notify("InvokeAfterTimer", MyDataTable.ToString(), MyDataTable.InvokeCounter);

                c.Close();

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
                MyDataTable.InvokeCounter++;

                Invoke(MyDataTable);

                Notify("InvokeAfterActivity", MyDataTable.ToString(), MyDataTable.InvokeCounter);

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
                MyDataTable.InvokeCounter++;

                Invoke(MyDataTable);

                Notify("InvokeAfterBootComplete", MyDataTable.ToString(), MyDataTable.InvokeCounter);


                c.Close();

            }
        }
    }

    #region NotifyServiceFromTimer
    public sealed class NotifyServiceFromTimer : AbstractNotifyService
    {
        public static Class Class
        {
            [Script(OptimizedCode = "return NotifyServiceFromTimer.class;")]
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
            this.InvokeAfterTimer();

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

    #region NotifyServiceAfterBoot
    public sealed class NotifyServiceAfterBoot : AbstractNotifyService
    {
        public static Class Class
        {
            [Script(OptimizedCode = "return NotifyServiceAfterBoot.class;")]
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

            var intent = new Intent(that, NotifyServiceAfterBoot.Class);
            that.startService(intent);
        }
    }


}
