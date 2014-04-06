using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SQLiteConsoleExperiment
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {

        public const string DataSource = "file:SQLiteWithDataGridView50.sqlite";



        public Task<DataTable> ExecuteReaderAsync(
            string sql,
            Action<string> y,
            Action<XElement> AtDataGridContent = null
            )
        {
            // when can we have events, out/ref and structs/sealed classes?

            var csb = new SQLiteConnectionStringBuilder
            {
                DataSource = DataSource,
                Version = 3
            };

            //ApplyRestrictedCredentials(csb);

            //var xdata = new DataTable();

            var data = new DataTable { TableName = new { sql }.ToString() };
            var columns = new List<DataColumn>();
            try
            {
                var table = default(XElement);

                using (var c = new SQLiteConnection(csb.ConnectionString))
                {
                    c.Open();


                    var cmd = new SQLiteCommand(sql, c);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var LastInsertRowId = c.LastInsertRowId;

                        Console.WriteLine(new { LastInsertRowId });



                        //xdata.Load(reader);

                        while (reader.Read())
                        {
                            #region tr
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

#if DEBUG
                                            var th = new XElement("th");
                                            th.Value = n;
                                            header.Add(th);
#endif


                                            //Implementation not found for type import :
                                            //type: System.Data.DataColumnCollection
                                            //method: System.Data.DataColumn get_Item(Int32)
                                            //Did you forget to add the [Script] attribute?
                                            //Please double check the signature!
                                            var cc = new DataColumn { ColumnName = n };


                                            columns.Add(cc);

                                            data.Columns.Add(
                                                cc
                                            );
                                        }
#if DEBUG
                                        table.Add(header);
#endif
                                    }
                                );
                            }

#if DEBUG
                            var tr = new XElement("tr");

                            table.Add(tr);
#endif
                            //                            Cannot find column 0.
                            //at System.Data.DataColumnCollection.get_Item(Int32 index)

                            var row = data.NewRow();
                            data.Rows.Add(row);

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
                                    td.Value = reader.GetString(i) ?? "";
                                    tr.Add(td);
#endif


                                    w.Append("'");
                                    w.Append(reader.GetString(i));
                                    w.Append("'");

                                    row[
                                        columns[i]
                                    ] = reader.GetString(i);

                                }
                                else if (ft == typeof(int))
                                {
#if DEBUG
                                    var td = new XElement("td");
                                    td.Value = reader.GetInt32(i) + "";
                                    tr.Add(td);
#endif

                                    w.Append(reader.GetInt32(i));

                                    row[
                                         columns[i]
                                     ] = "" + reader.GetInt32(i);
                                }
                                else if (ft == typeof(long))
                                {
#if DEBUG
                                    var td = new XElement("td");
                                    td.Value = reader.GetInt64(i) + "";
                                    tr.Add(td);
#endif
                                    w.Append(reader.GetInt64(i));

                                    row[
                                           columns[i]
                                       ] = "" + reader.GetInt64(i);
                                }
                                else
                                    w.Append("?");


                            }
                            w.Append("}");

                            y(w.ToString());
                            #endregion

                        }
                    }

                    //c.Close();
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


            return Task.FromResult(data);
        }

    }
}
