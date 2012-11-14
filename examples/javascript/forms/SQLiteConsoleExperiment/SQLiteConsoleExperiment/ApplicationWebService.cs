using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SQLiteConsoleExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        public const string DataSource = "SQLiteWithDataGridView5";



        public void ExecuteReaderAsync(string sql, Action<string> y)
        {
            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            //ApplyRestrictedCredentials(csb);

            try
            {
                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    c.Open();


                    using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var w = new StringBuilder();

                            w.Append("{ ");
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (i > 0)
                                    w.Append(", ");

                                var n = reader.GetName(i);

                                w.Append(n + ": ");

                                var ft = reader.GetFieldType(i);

                                if (ft == typeof(string))
                                {
                                    w.Append("'");
                                    w.Append(reader.GetString(i));
                                    w.Append("'");
                                }
                                else if (ft == typeof(int))
                                {
                                    w.Append(reader.GetInt32(i));
                                }
                                else if (ft == typeof(long))
                                {
                                    w.Append(reader.GetInt64(i));
                                }
                                else
                                    w.Append("?");


                            }
                            w.Append("}");

                            y(w.ToString());
                        }
                    }

                    c.Close();
                }

            }
            catch (Exception ex)
            {
                y("error:\r\n  " + ex.Message + "\r\n  " + ex.StackTrace
                    //.TakeUntilOrEmpty("\n")

                    );
            }

        }

    }
}
