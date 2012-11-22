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

        public const string DataSource = "SQLiteWithDataGridView50.sqlite";



        public void ExecuteReaderAsync(string sql, Action<string> y, Action<XElement> AtDataGridContent = null)
        {
            // when can we have events, out/ref and structs/sealed classes?

            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            //ApplyRestrictedCredentials(csb);

            try
            {
                var table = default(XElement);

                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    c.Open();


                    using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            #region tr
#if DEBUG
                            if (table == null)
                            {
                                table = new XElement("table");

                                new XElement("tr").With(
                                    header =>
                                    {
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            var n = reader.GetName(i);

                                            // http://www.w3schools.com/tags/tag_th.asp

                                            var th = new XElement("th");
                                            th.Value = n;
                                            header.Add(th);
                                        }

                                        table.Add(header);
                                    }
                                );
                            }

                            var tr = new XElement("tr");

                            table.Add(tr);
#endif


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
#if DEBUG
                                    var td = new XElement("td");
                                    td.Value = reader.GetString(i);
                                    tr.Add(td);
#endif


                                    w.Append("'");
                                    w.Append(reader.GetString(i));
                                    w.Append("'");
                                }
                                else if (ft == typeof(int))
                                {
#if DEBUG
                                    var td = new XElement("td");
                                    td.Value = reader.GetInt32(i) + "";
                                    tr.Add(td);
#endif

                                    w.Append(reader.GetInt32(i));
                                }
                                else if (ft == typeof(long))
                                {
#if DEBUG
                                    var td = new XElement("td");
                                    td.Value = reader.GetInt64(i) + "";
                                    tr.Add(td);
#endif
                                    w.Append(reader.GetInt64(i));
                                }
                                else
                                    w.Append("?");


                            }
                            w.Append("}");

                            y(w.ToString());
                            #endregion

                        }
                    }

                    c.Close();
                }

                if (table != null)
                    if (AtDataGridContent != null)
                        AtDataGridContent(table);

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
