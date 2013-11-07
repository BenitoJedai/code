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
    public static class StringConversionsForDataSet
    {
        // tested by
        // X:\jsc.svn\examples\javascript\Test\WithExcel\WithExcel\Program.cs

        public static string ConvertToString(DataSet e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131107/assetslibrary

            if (e == null)
                return null;


            // http://www.w3schools.com/tags/tag_th.asp
            var x = new XElement("DataSet");

            // what else should we save for dataset?

            foreach (DataTable item in e.Tables)
            {
                var z = XElement.Parse(
                    StringConversionsForDataTable.ConvertToString(
                        item
                    )
                );

                x.Add(z);
            }




            return x.ToString();
        }

        public static DataSet ConvertFromString(string e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131107/assetslibrary

            if (string.IsNullOrEmpty(e))
                return null;

            var n = new DataSet();

            var u = XElement.Parse(e);


            u.Elements("DataTable").WithEach(
                DataTableElement =>
                {
                    var t = StringConversionsForDataTable.ConvertFromString(
                        DataTableElement.ToString()
                    );

                    n.Tables.Add(t);
                }
            );


            return n;
        }



    }

}
