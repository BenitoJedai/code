using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using SQLiteWithDataGridView.Schema;
using System;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace SQLiteWithDataGridView
{

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        public TheGridTable grid
        {

            get
            {
                // http://stackoverflow.com/questions/1645661/turn-off-warnings-and-errors-on-php-mysql
                ScriptCoreLib.PHP.Native.API.error_reporting(0);

                return new TheGridTable().With(
                    x =>
                    {
                        var DataSource = "SQLiteWithDataGridView7.sqlite";

                        x.csb_write.DataSource = DataSource;
                        ApplyRestrictedCredentials(x.csb_write);

                        x.csb.DataSource = DataSource;
                        x.csb.ReadOnly = true;
                        ApplyRestrictedCredentials(x.csb_write);

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

            Action<string> y)
        {
            this.grid.SelectTransactionKey(
                ContentKey =>
                {
                    y("" + ContentKey);
                }
            );


        }

        public void GridExample_AddItem(
            string ContentValue,
            string ContentComment,
            /* int? */ string ParentContentKey,

            Action<string> AtContentReferenceKey
            )
        {

            //ApplyRestrictedCredentials(csb);


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

        }

        public void GridExample_UpdateItem(

                string ContentKey,
                string ContentValue,
                string ContentComment,

                 Action<string> AtTransactionKey = null
            )
        {
            //Console.WriteLine("AddItem enter");

            //var csb = new SQLiteConnectionStringBuilder
            //{
            //    DataSource = DataSource,
            //    Version = 3
            //};

            //ApplyRestrictedCredentials(csb);

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
            // Send it back to the caller.
            //Console.WriteLine("AddItem exit");
        }




        public void GridExample_EnumerateItemsChangedBetweenTransactions(
            /* int? */ string ParentContentKey,


            string FromTransaction,
            string ToTransaction,
            Action<string, string, string, string> AtContent,
            Action<string> done
        )
        {
            //GridExample_InitializeDatabase("", delegate { });

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



            // why does jsc not support parameterless yields?
            done("");
        }




        public void __grid_SelectContent(
            string e,
            Action<string, string, string, string> y,
            /* int? */ string ParentContentKey,
            Action<string> AtTransactionKey = null,
            Action<string> AtError = null
            )
        {
            //GridExample_InitializeDatabase("", delegate { });

            try
            {
                InternalSelectContent(y, ParentContentKey, AtTransactionKey);

            }
            catch (Exception ex)
            {
                // script: error JSC1000: Java : Opcode not implemented: brtrue at SQLiteWithDataGridView.ApplicationWebService.__grid_SelectContent
                AtErrorOrThrowIt(AtError, ex);

            }
        }

        private void InternalSelectContent(Action<string, string, string, string> y, string ParentContentKey, Action<string> AtTransactionKey)
        {
            var xParentContentKey = ParentContentKey == "" ? null : (object)int.Parse(ParentContentKey);

            this.grid.SelectContent(
                new TheGridTableQueries.SelectContent
                {
                    ParentContentKey1 = xParentContentKey,
                    ParentContentKey2 = xParentContentKey,

                    // android 2.2 prepared statements disallow null params? send empty string instead?
                    ParentContentKey3 = xParentContentKey,
                },
                reader =>
                {
                    string
                        ContentValue = reader.ContentValue,
                        ContentComment = reader.ContentComment;

                    long
                        ContentKey = reader.ContentKey,
                        ContentChildren = reader.ContentChildren;

                    y("" + ContentKey, ContentValue, ContentComment, "" + ContentChildren);
                }
            );




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


}
