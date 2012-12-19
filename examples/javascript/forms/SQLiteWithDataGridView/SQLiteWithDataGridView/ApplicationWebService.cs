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
    public sealed partial class ApplicationWebService : Component //, IApplicationWebService
    {


        public TheGridTable grid = new TheGridTable();

        //const string DataSource = "SQLiteWithDataGridView.4.sqlite";
        const string DataSource = "SQLiteWithDataGridView5";

        const string TableName = "TheGridTable";

        static partial void ApplyRestrictedCredentials(SQLiteConnectionStringBuilder b, bool admin = false);

        public void GridExample_InitializeDatabase(string e, Action<string> y)
        {


            //Console.WriteLine("AddItem enter");

            var csb = new SQLiteConnectionStringBuilder
             {
                 DataSource = DataSource,
                 Version = 3


             };

            ApplyRestrictedCredentials(csb, true);

            // Send it back to the caller.
            y(e);
            //Console.WriteLine("AddItem exit");
        }

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
            //Console.WriteLine("AddItem enter");

            var csb = new SQLiteConnectionStringBuilder
             {
                 DataSource = DataSource,
                 Version = 3
             };

            ApplyRestrictedCredentials(csb);


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

            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            ApplyRestrictedCredentials(csb);

            using (var c = new SQLiteConnection(csb.ConnectionString))
            {
                c.Open();

                var iContentKey = int.Parse(ContentKey);

                new TheGridTableQueries.Update
                {
                    ContentKey = iContentKey,
                    ContentValue = ContentValue,
                    ContentComment = ContentComment
                }.ExecuteNonQuery(c);

                this.grid.InsertLog(
                    new TheGridTableQueries.InsertLog { ContentKey = iContentKey, ContentComment = "UpdateItem" }
                );

            }


            if (AtTransactionKey != null)
                GridExample_GetTransactionKeyFor(TableName, AtTransactionKey);
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
            GridExample_InitializeDatabase("", delegate { });

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




        public void GridExample_EnumerateItems(
            string e,
            Action<string, string, string, string> y,
            /* int? */ string ParentContentKey,
            Action<string> AtTransactionKey = null
            )
        {
            GridExample_InitializeDatabase("", delegate { });

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
                GridExample_GetTransactionKeyFor(TableName, AtTransactionKey);


        }



    }


}
