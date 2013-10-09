using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Extensions;

namespace SearchComponent
{
    class SearchComponentClass
    {
        public DataTable getSqlQueryResultAsDataTable(string sql, string sqlConnectionString)
        {
            var dataAdapter = new SqlDataAdapter(sql, sqlConnectionString);
            var table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            return table;
        }

        public string[] findFilesFromPath(string path, string fileNameParam, SearchOption searchOp)
        {
            string[] filenames = null;
            filenames = Directory.GetFiles(@path, "*" + fileNameParam + "*", searchOp);

            return filenames;
        }

        //public List<string> findFileRowsInPath(string path, string fromFileSearchParam, SearchOption searchOp, string fileExtension)
        //{
        //    string[] filenames = null;
        //    filenames = Directory.GetFiles(path, "*" + fileExtension, searchOp);

        //    var fileContents = from file in filenames
        //                       from line in File.ReadLines(file)
        //                       where line.Contains(fromFileSearchParam)
        //                       select new SearchResult(file, line);

        //    List<string> ret = new List<string>();
        //    fileContents.WithEach(elem =>
        //    {
        //        ret.Add(elem.fileName + " " + elem.fileRow);
        //    });
        //    return ret;
        //}

        public string ConvertStringArrayToString(string[] array)
        {
            var builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(' ');
            }
            return builder.ToString();
        }
    }
}
