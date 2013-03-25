using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.PHP;
using SQLiteWithDataGridView.Schema;
using System;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SQLiteWithDataGridView
{
    public interface IApplicationWebService
    {
        // __grid_SelectContent

        void __grid_SelectContent(
         string e,
         Action<string, string, string, string> y,
            /* int? */ string ParentContentKey,
         Action<string> AtTransactionKey = null,
         Action<string> AtError = null,
         Action<string> AtConsole = null
         );

        // GridExample_UpdateItem

        void GridExample_UpdateItem(

                string ContentKey,
                string ContentValue,
                string ContentComment,

                 Action<string> AtTransactionKey = null,

                Action<string> AtConsole = null
            );

        void GridExample_GetTransactionKeyFor(
            string e,

            Action<string> y,
            Action<string> AtConsole = null);

        void GridExample_AddItem(
            string ContentValue,
            string ContentComment,
            /* int? */ string ParentContentKey,

            Action<string> AtContentReferenceKey,

            Action<string> AtConsole = null

            );

        void GridExample_EnumerateItemsChangedBetweenTransactions(
            /* int? */ string ParentContentKey,


            string FromTransaction,
            string ToTransaction,
            Action<string, string, string, string> AtContent,
            Action<string> done,

                Action<string> AtConsole = null

        );

    }
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component,
        IApplicationWebService
    {
        public TheGridTable grid
        {

            get
            {
                // http://stackoverflow.com/questions/1645661/turn-off-warnings-and-errors-on-php-mysql

                return new TheGridTable().With(
                    x =>
                    {
                        var DataSource = "SQLiteWithDataGridView7.sqlite";

                        x.csb_write.DataSource = DataSource;
                        ApplyRestrictedCredentials(x.csb_write);

                        x.csb.DataSource = DataSource;
                        x.csb.ReadOnly = true;
                        // this was expensive to figure out!
                        ApplyRestrictedCredentials(x.csb);

                        x.csb_admin.DataSource = DataSource;
                        ApplyRestrictedCredentials(x.csb_admin, true);
                        x.Create();
                    }
                );
            }
        }



        static partial void ApplyAdministratorCredentials(SQLiteConnectionStringBuilder b);


        static partial void ApplyRestrictedCredentials(SQLiteConnectionStringBuilder b, bool admin = false);



        public void GridExample_GetTransactionKeyFor(
            string e,

            Action<string> y,
            Action<string> AtConsole = null)
        {
            var x = new __ConsoleToDatabaseWriter(AtConsole);
            this.grid.SelectTransactionKey(
                ContentKey =>
                {
                    y("" + ContentKey);
                }
            );
            x.Dispose();

        }

        public void GridExample_AddItem(
            string ContentValue,
            string ContentComment,
            /* int? */ string ParentContentKey,

            Action<string> AtContentReferenceKey,

            Action<string> AtConsole = null

            )
        {
            var x = new __ConsoleToDatabaseWriter(AtConsole);
            Console.WriteLine("inside GridExample_AddItem");



            var xParentContentKey = ParentContentKey == "" ? null : (object)int.Parse(ParentContentKey);

            grid.Insert(
                new TheGridTableQueries.Insert
                {
                    ContentValue = ContentValue,
                    ContentComment = ContentComment,
                    ParentContentKey = xParentContentKey
                },

                ContentReferenceKeyLong =>
                {
                    // jsc does not yet autobox for java 
                    // int cannot be dereferenced
                    var ContentReferenceKey = ContentReferenceKeyLong.ToString();
                    //var ContentReferenceKey = ((object)ContentReferenceKeyLong).ToString();

                    this.grid.InsertLog(
                         new TheGridTableQueries.InsertLog { ContentKey = (int)ContentReferenceKeyLong, ContentComment = "AddItem" }
                     );

                    if (ParentContentKey != "")
                    {
                        this.grid.InsertLog(
                           new TheGridTableQueries.InsertLog { ContentKey = int.Parse(ParentContentKey), ContentComment = "ChildAdded" }
                       );

                    }

                    AtContentReferenceKey(ContentReferenceKey);
                }
            );


            x.Dispose();

        }

        public void GridExample_UpdateItem(

                string ContentKey,
                string ContentValue,
                string ContentComment,

                 Action<string> AtTransactionKey = null,

                Action<string> AtConsole = null
            )
        {
            var x = new __ConsoleToDatabaseWriter(AtConsole);
            Console.WriteLine("inside GridExample_UpdateItem");

            var iContentKey = int.Parse(ContentKey);

            grid.Update(
                new TheGridTableQueries.Update
                {
                    ContentKey = iContentKey,
                    ContentValue = ContentValue,
                    ContentComment = ContentComment
                }
            );


            this.grid.InsertLog(
                new TheGridTableQueries.InsertLog { ContentKey = iContentKey, ContentComment = "UpdateItem" }
            );

            if (AtTransactionKey != null)
                GridExample_GetTransactionKeyFor("", AtTransactionKey);


            Console.WriteLine("exit GridExample_UpdateItem");
            x.Dispose();
        }




        public void GridExample_EnumerateItemsChangedBetweenTransactions(
            /* int? */ string ParentContentKey,


            string FromTransaction,
            string ToTransaction,
            Action<string, string, string, string> AtContent,
            Action<string> done,

                Action<string> AtConsole = null

        )
        {
            var x = new __ConsoleToDatabaseWriter(AtConsole);


            var xParentContentKey = ParentContentKey == "" ? null : (object)int.Parse(ParentContentKey);

            grid.SelectContentUpdates(
                new TheGridTableQueries.SelectContentUpdates
                {
                    FromTransaction = int.Parse(FromTransaction),
                    ToTransaction = int.Parse(ToTransaction),

                    ParentContentKey1 = xParentContentKey,

                    // android 2.2 prepared statements disallow null params? send empty string instead?
                    ParentContentKey3 = xParentContentKey,

                    ParentContentKey2 = xParentContentKey
                },
                reader =>
                {
                    string
                       ContentValue = reader.ContentValue,
                       ContentComment = reader.ContentComment;

                    long
                        ContentKey = reader.ContentReferenceKey,
                        ContentChildren = reader.ContentChildren;

                    AtContent("" + ContentKey, ContentValue, ContentComment, "" + ContentChildren);
                }
            );



            x.Dispose();

            // why does jsc not support parameterless yields?
            done("");
        }




        public void __grid_SelectContent(
            string e,
            Action<string, string, string, string> y,
            /* int? */ string ParentContentKey,
            Action<string> AtTransactionKey = null,
            Action<string> AtError = null,
            Action<string> AtConsole = null
            )
        {
            //ScriptCoreLib.PHP.Native.API.error_reporting(0);

            //GridExample_InitializeDatabase("", delegate { });

            // will we run out of memory now?
            // <b>Fatal error</b>:  Allowed memory size of 8388608 bytes exhausted (tried to allocate 65488 bytes) in <b>B:\inc\SQLiteWithDataGridView.ApplicationWebService.exe\class.SQLiteWithDataGridView.__ConsoleToDatabaseWriter.php</b> on line <b>62</b><br />

            var x = new __ConsoleToDatabaseWriter(AtConsole);
            Console.WriteLine("inside __grid_SelectContent");
            //x.Dispose();

            try
            {
                InternalSelectContent(y, ParentContentKey, AtTransactionKey);

            }
            catch (Exception ex)
            {
                // script: error JSC1000: Java : Opcode not implemented: brtrue at SQLiteWithDataGridView.ApplicationWebService.__grid_SelectContent
                AtErrorOrThrowIt(AtError, ex);

            }

            x.Dispose();
        }

        private void InternalSelectContent(Action<string, string, string, string> y, string ParentContentKey, Action<string> AtTransactionKey)
        {
            Console.WriteLine("enter InternalSelectContent");

            var xParentContentKey = ParentContentKey == "" ? null : (object)int.Parse(ParentContentKey);


            Console.WriteLine(new { xParentContentKey });

            this.grid.SelectContent(
                new TheGridTableQueries.SelectContent
                {
                    ParentContentKey1 = xParentContentKey,
                    ParentContentKey2 = xParentContentKey,
                    ParentContentKey3 = xParentContentKey,
                    ////ParentContentKey2 = xParentContentKey,

                    ////// android 2.2 prepared statements disallow null params? send empty string instead?
                    //ParentContentKey3 = xParentContentKey,
                },
                reader =>
                {
                    string
                        ContentValue = reader.ContentValue,
                        ContentComment = reader.ContentComment;

                    long
                        ContentKey = reader.ContentKey,
                        ContentChildren = reader.ContentChildren;

                    y(
                        "" + ContentKey,
                        ContentValue,
                        ContentComment,
                        "" + ContentChildren
                    );
                }
            );

            Console.WriteLine("exit InternalSelectContent");



            if (AtTransactionKey != null)
                GridExample_GetTransactionKeyFor("", AtTransactionKey);
        }

        private static void AtErrorOrThrowIt(Action<string> AtError, Exception ex)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/2012/20121224-mysql
            //var Message = new { ex.Message, ex.StackTrace }.ToString().SkipUntilLastIfAny("Caused by:");

#if AppEngine
            var Message = new { ex.Message, ex.StackTrace }.ToString().SkipUntilLastIfAny("Caused by:");
#else
            var Message = "" + ex;
#endif

            //    java.lang.NullPointerException
            //at ScriptCoreLibJava.BCLImplementation.System.__String.Replace(__String.java:109)
            //at ScriptCoreLib.Extensions.InternalXMLExtensions.ToXMLString(InternalXMLExtensions.java:16)

            if (AtError != null)
            {
#if AppEngine
                Console.WriteLine(Message);
#endif
                AtError(Message);
            }

            if (AtError == null)
                throw new Exception(Message);
        }


        //            Implementation not found for type import :
        //System.Exception :: Void .ctor(System.String, System.Exception)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!
        //type: SQLiteWithDataGridView.ApplicationWebService offset: 0x0008  method:Void ThrowIt(System.Exception)




    }


    class __ConsoleToDatabaseWriter : TextWriter, IDisposable
    {
        protected override void Dispose(bool disposing)
        {
            if (o == null)
                return;

#if PHP
            // need to use Script Implements instead!
            var x = Native.API.ob_get_contents();
            Native.API.ob_end_clean();
            if (!string.IsNullOrEmpty(x))
                Console.WriteLine(x);
#endif

            Console.SetOut(o);

            // base calls broken for PHP?
            //base.Dispose(disposing);
        }

        public Action<string> AtWrite;

        public override void Write(string value)
        {
            Console.SetOut(o);
            AtWrite(value);
            Console.SetOut(this);
        }

        public override void WriteLine(string value)
        {


            var x = sw.ElapsedMilliseconds + "ms " + value;
            Console.SetOut(o);
            AtWrite(x + Environment.NewLine);
            Console.SetOut(this);
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        Stopwatch sw;

        public __ConsoleToDatabaseWriter(Action<string> xAtWrite)
        {
            if (xAtWrite == null)
                return;


            sw = new Stopwatch();
            sw.Start();

            InitializeAndKeepOriginal(this);

            this.AtWrite += xAtWrite;
        }


        public static void InternalWriteLine(string x)
        {
            InternalWrite(x + Environment.NewLine);
        }

        public static void InternalWrite(string x)
        {
#if DEBUG
            var i = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            //o.Write(new { session, id, x });
            //#endif

            //Log.i("ConsoleByCookie", x);

            if (o == null)
                Console.Out.Write(x);
            else
                o.Write(x);
            //#if DEBUG

            Console.ForegroundColor = i;
#endif
        }

        static TextWriter o;

        private static TextWriter InitializeAndKeepOriginal(__ConsoleToDatabaseWriter w)
        {
            // Console is not really thread safe!
            if (o == null)
                o = Console.Out;

            Console.SetOut(w);

#if PHP
            // need to use Script Implements instead!
            ScriptCoreLib.PHP.Native.API.error_reporting(-1);
            Native.API.ob_start();
#endif

            return o;
        }
    }

}
