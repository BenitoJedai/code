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
    public static partial class StringConversions
    {
        // used by JVM/CLR 
        // X:\jsc.internal.svn\compiler\jsc.internal\jsc.internal\meta\Library\ILStringConversions.cs

        public static string UTF8FromBase64StringOrDefault(string e)
        {
            // allow 0 char do be sent
            if (e == null)
                return null;

            var bytes = Convert.FromBase64String(e);

            return Encoding.UTF8.GetString(bytes);
        }

        public static string UTF8ToBase64StringOrDefault(string e)
        {
            if (e == null)
                return null;

            var bytes = Encoding.UTF8.GetBytes(e);

            return Convert.ToBase64String(bytes);
        }



        public static byte[] FromBase64StringOrDefault(this string e)
        {
            if (e == null)
                return null;

            return Convert.FromBase64String(e);
        }

        public static string ToBase64StringOrDefault(this byte[] e)
        {
            if (e == null)
                return null;

            return Convert.ToBase64String(e);
        }



        public static int[] Int32ArrayFromBase64StringOrDefault(this string e)
        {
            if (e == null)
                return null;

            var m = new MemoryStream(Convert.FromBase64String(e));
            var r = new BinaryReader(m);

            var length = m.Length / 4;

            var a = new int[length];

            for (int i = 0; i < length; i++)
            {
                a[i] = r.ReadInt32();
            }

            return a;
        }

        public static string Int32ArrayToBase64StringOrDefault(this int[] e)
        {
            if (e == null)
                return null;

            var m = new MemoryStream();
            var w = new BinaryWriter(m);

            foreach (var item in e)
            {
                w.Write(item);
            }

            return Convert.ToBase64String(m.ToArray());
        }

        #region XElement
        public static string ConvertXElementToString(XElement e)
        {
            if (e == null)
                return null;

            return e.ToString();
        }

        public static XElement ConvertStringToXElement(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            return XElement.Parse(e);
        }
        #endregion

        #region FileInfo
        public static string ConvertFileInfoToString(FileInfo e)
        {
            if (e == null)
                return null;

            return e.FullName;
        }

        public static FileInfo ConvertStringToFileInfo(string e)
        {
            if (string.IsNullOrEmpty(e))
                return null;

            return new FileInfo(e);
        }
        #endregion
    }

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
                    var th = new XElement("DataColumn",
                        // is it a string?
                        row[item]
                    );

                    tr.Add(th);
                }
            }

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
            if (xColumns == null)
                Debugger.Break();

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
    }
}
