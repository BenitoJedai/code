using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Data;
using ScriptCoreLib.Extensions;
using System.Diagnostics;
namespace ScriptCoreLib.Library
{
    public static class StringConversionsForDataTable
    {
        // tested by
        // X:\jsc.svn\examples\javascript\forms\Test\TestDataTableToJavascript\TestDataTableToJavascript\ApplicationWebService.cs


        #region DataTable
        public static string ConvertToString(DataTable e)
        {
            if (e == null)
                return null;


            // http://www.w3schools.com/tags/tag_th.asp
            var table = new XElement("DataTable");

            table.Add(
                new XAttribute("TableName", e.TableName)
            );

            {
                var tr = new XElement("Columns");
                table.Add(tr);

                foreach (DataColumn item in e.Columns)
                {
                    var th = new XElement("DataColumn",
                        new XAttribute(
                            "ReadOnly",
                            item.ReadOnly
                        ),
                        item.ColumnName
                    );

                    tr.Add(th);
                }
            }

            foreach (DataRow row in e.Rows)
            {
                var tr = new XElement("DataRow");
                table.Add(tr);

                foreach (DataColumn item in e.Columns)
                {
                    var value = row[item.ColumnName];

                    Console.WriteLine(
                        new { item.ColumnName, value }
                    );

                    var th = new XElement("DataColumn",
                        // is it a string?

                        value
                    );

                    tr.Add(th);
                }
            }

            // X:\jsc.svn\examples\javascript\appengine\WebNotificationsViaDataAdapter\WebNotificationsViaDataAdapter\ApplicationWebService.cs
            //Console.WriteLine(
            //    new { table }
            //    );

            return table.ToString();
        }

        public static DataTable ConvertFromString(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            var n = new DataTable();

            // DataTable.ReadXML?
            var x = XElement.Parse(e);

            n.TableName = x.Attribute("TableName").Value;

            // "<DataTable TableName="DoEnterData { reason = new state }">  
            // <Columns>    
            // <DataColumn>Column 1</DataColumn>    
            // <DataColumn>Column 2</DataColumn>  
            // </Columns>  
            // <DataRow>    <DataColumn>#0</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#1</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#2</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#3</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#4</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#5</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#6</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#7</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#8</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#9</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#10</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#11</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#12</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#13</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#14</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#15</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#16</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#17</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#18</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#19</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#20</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#21</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#22</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#23</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#24</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#25</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#26</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#27</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#28</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#29</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#30</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow>  <DataRow>    <DataColumn>#31</DataColumn>    <DataColumn>{ reason = new state, CallerMemberName = .ctor, CallerLineNumber = 131, CallerFilePath = x:\jsc.svn\examples\javascript\PageNavigationExperiment\PageNavigationExperiment\Application.cs }</DataColumn>  </DataRow></DataTable>"


            var xColumns = x.Element("Columns");

            // [19:12:52.332] "Elements { name = Columns, LocalName = datarow }
            // firefox whats up?
            //if (xColumns == null)
            //    Debugger.Break();

            var xDataColumns = xColumns.Elements("DataColumn");


            var Columns = xDataColumns.Select(
                k =>
                    new DataColumn
                    {
                        ColumnName = k.Value,
                        ReadOnly = Convert.ToBoolean(k.Attribute("ReadOnly").Value)
                    }).ToArray();

            n.Columns.AddRange(Columns);

            x.Elements("DataRow").WithEach(
                r =>
                {
                    var nr = n.NewRow();


                    r.Elements("DataColumn").WithEachIndex(
                        (c, ci) =>
                        {
                            nr[Columns[ci]] = c.Value;
                        }
                    );

                    n.Rows.Add(nr);
                }
            );

            return n;
        }

        #endregion


        [Obsolete("used by assets compiler")]
        public static DataTable ParseCSV(string content)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131027-csv

            var t = new DataTable();


            //{ content = Year,Make,Model,Description,Price
            //1997,Ford,E350,"ac, abs, moon",3000
            //1999,Chevy,"Venture ""Extended Edition""",,4900
            //1999,Chevy,"Venture ""Extended Edition, Very Large""",,5000
            //1996,Jeep,Grand Cherokee1,"MUST SELL!
            //air, moon roof, loaded",4799
            // }


            //{ item = Year }
            //{ item = Make }
            //{ item = Model }
            //{ item = Description }
            //{ item = Price }
            //{ item =  }
            //{ item = 1997 }
            //{ item = Ford }
            //{ item = E350 }
            //{ item = ac, abs, moon }
            //{ item = 3000 }
            //{ item =  }
            //{ item = 1999 }
            //{ item = Chevy }
            //{ item = Venture "Extended Edition" }
            //{ item =  }
            //{ item = 4900 }
            //{ item =  }
            //{ item = 1999 }
            //{ item = Chevy }
            //{ item = Venture "Extended Edition, Very Large" }
            //{ item =  }
            //{ item = 5000 }
            //{ item =  }
            //{ item = 1996 }
            //{ item = Jeep }
            //{ item = Grand Cherokee1 }
            //{ item = MUST SELL!
            //air, moon roof, loaded }
            //{ item = 4799 }
            //{ item =  }
            //{ item =  }

            Action<string> yield = ColumnName =>
            {
                t.Columns.Add(
                    new DataColumn { ColumnName = ColumnName }
                );

            };

            foreach (var item in ParseCSVTokens(content))
            {
                if (item != null)
                {
                    yield(item);
                    continue;
                }

                var row = default(DataRow);
                var c = -1;

                yield = CellValue =>
                {
                    if (row == null)
                    {
                        row = t.NewRow();

                        t.Rows.Add(row);
                    }
                    c++;

                    row[c] = CellValue;
                };
            }



            //Console.WriteLine(
            //    new { content }
            //    );

            return t;
        }

        // have we not written this before?
        public static IEnumerable<string> ParseCSVTokens(string content)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\appengine\NextPageFromWebService\NextPageFromWebService\Application.cs
            var yield = new List<string>();



            // comma shall trigger buffer

            var w = new StringBuilder();
            var q = default(StringBuilder);

            for (int i = 0; i < content.Length; i++)
            {
                if (q == null)
                {
                    if (content[i] == '\"')
                    {
                        // enter q mode

                        q = w;
                        w = null;
                        //continue;
                    }
                    else if (content[i] == ',')
                    {
                        //yield return w.ToString();
                        yield.Add(w.ToString());

                        // script: error JSC1000: No implementation found for this native method, please implement [System.Text.StringBuilder.Clear()]
                        //w.Clear();
                        w = new StringBuilder();

                        //continue;
                    }
                    else if (content[i] == '\r')
                    {
                        //yield return w.ToString();
                        yield.Add(w.ToString());
                        //w.Clear();
                        w = new StringBuilder();

                        //yield return null;
                        yield.Add(null);

                        if (i + 1 < content.Length)
                        {
                            // aint over yet
                            if (content[i + 1] == '\n')
                            {
                                i++;
                            }
                        }

                        //continue;
                    }
                    else
                    {
                        w.Append(content[i]);
                    }
                }
                else
                {
                    if (content[i] == '\"')
                    {
                        // exit q mode?

                        var eq = (i + 1 < content.Length) && (content[i + 1] == '\"');


                        if (eq)
                        {
                            // aint over yet
                            // escaped quote?
                            q.Append('\"');
                            i++;
                        }
                        else
                        {

                            w = q;
                            q = null;
                        }

                        //continue;
                    }
                    else
                    {
                        q.Append(content[i]);
                    }

                }
            }

            //yield return null;
            yield.Add(null);


            return yield;
        }
    }

}
