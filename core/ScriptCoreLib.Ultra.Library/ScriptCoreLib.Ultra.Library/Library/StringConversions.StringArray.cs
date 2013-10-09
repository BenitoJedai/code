using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Data;
using ScriptCoreLib.Extensions;

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

            var Columns = x.Element("Columns").Elements("DataColumn").Select(k => new DataColumn { ColumnName = k.Value }).ToArray();
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
