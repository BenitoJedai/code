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
    public sealed partial class ApplicationWebService : Component
    {

        public const string MyDataSource = "SQLiteWithDataGridView51.sqlite";

        // http://social.msdn.microsoft.com/Forums/en-US/netfxbcl/thread/ada5def5-0d80-43d6-ab5d-9fb1934e6556/
        //public const SecureString MyDataSource = "SQLiteWithDataGridView51.sqlite";

        public void Table1_Last(Action<string> y)
        {
            new Table1(MyDataSource).With(
                Table1 =>
                {
                    Table1.Last(
                        value => y("" + value)
                    );
                }
           );
        }

        public void WebMethod2(string e, Action<string> y)
        {
            try
            {
                new Table1(MyDataSource).With(
                    Table1 =>
                    {
                        if (!string.IsNullOrEmpty(e))
                        {
                            var Last = -1L;

                            Table1.Last(
                                //value => Last
                                value => Last = value
                            );

                            Table1.Add(
                                // new Tables.Table1.AddQueryParameters
                                new Table1Queries.Insert
                                {
                                    // implicit?
                                    ContentValue = new { Last, e }.ToString()
                                }
                            );
                        }

                        Console.WriteLine("before Enumerate");

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

                        Console.WriteLine("after Enumerate");

                    }
                );
            }
            catch
            {
                throw;
            }
        }


    }


    public static partial class XX
    {
        public static Action<Action<SQLiteConnection>> AsWithConnection(this string DataSource, int Version = 3)
        {
            Console.WriteLine("AsWithConnection...");

            return y =>
            {
                Console.WriteLine("AsWithConnection... invoke");

                using (var c = DataSource.ToConnection(Version))
                {
                    c.Open();

                    try
                    {
                        y(c);
                    }
                    catch (Exception ex)
                    {
                        var message = new { ex.Message, ex.StackTrace }.ToString();

                        Console.WriteLine("AsWithConnection... error: " + message);

                        throw new InvalidOperationException(message);
                    }
                }
            };
        }

        public static SQLiteConnection ToConnection(this string DataSource, int Version = 3)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = Version
            };

            var c = new SQLiteConnection(csb.ConnectionString);

            return c;
        }
    }



}
