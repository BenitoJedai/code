using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Security;
using System.Xml.Linq;
using TestSQLiteParameter.Tables;

namespace TestSQLiteParameter
{


    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component, ITable1
    {

        public const string MyDataSource = "SQLiteWithDataGridView51.sqlite";

        // http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/ada5def5-0d80-43d6-ab5d-9fb1934e6556/
        //public const SecureString MyDataSource = "SQLiteWithDataGridView51.sqlite";

        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void WebMethod2(string e, Action<string> y)
        {
            try
            {
                new Table1(MyDataSource).With(
                    Table1 =>
                    {
                        var Last = -1L;

                        Table1.Last(
                            //value => Last
                            value => Last = value
                        );

                        Table1.Add(
                            // new Tables.Table1.AddQueryParameters
                            new Tables.Table1.AddQuery
                            {
                                // implicit?
                                ContentValue = new { Last, e }.ToString()
                            }
                        );

                        Table1.Enumerate(
                            // dynamic until we can actually infer what
                            // fields we are getting
                            reader =>
                            {
                                var data = new { reader.ContentKey, reader.ContentValue };

                                // Send it back to the caller.
                                y(data.ToString());
                            }
                        );
                    }
                );
            }
            catch
            {
                throw;
            }
        }

#if DEBUG
        // we need type conversion support
        public void Add(Tables.Table1.AddQuery value)
        {
            new Table1(MyDataSource).Add(value);
        }


        // jsc cannot translate dynamic or can it?
        public void Enumerate(Action<dynamic> yield)
        {
            new Table1(MyDataSource).Enumerate(yield);
        }
#endif

    }


    public static partial class XX
    {
        public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource)
        {
            return y =>
            {
                using (var c = DataSource.ToConnection())
                {
                    c.Open();

                    y(c);
                }
            };
        }

        public static SQLiteConnection ToConnection(this string DataSource)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }
    }



}
