using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestSearchComponent.SearchComponent
{
    class SearchComponent
    {
        public DataTable getSqlDataTable(string sql, string sqlConnectionString)
        {
            var dataAdapter = new SqlDataAdapter(sql, sqlConnectionString);
            var table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            return table;
        }

        public Query getSqlQueryFromDb(string queryID, string dbTable, string sqlConnectionString)
        {
            Query query = null;
            try
            {
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select Top 1 * From Queries Where QueryID =" + queryID + " AND IsAccepted = 1 Order by EditedTime desc", conn);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //Console.WriteLine(reader.ToString());
                    query = new Query(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[6].ToString());
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            query.queryParams = getQueryParams(query.queryID, sqlConnectionString);
            return query;
        }

        public List<Parameter> getQueryParams(string queryID, string sqlConnectionString)
        {
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select ParameterName, ParameterValue From Parameters Where QueryID=" + queryID, conn);
            var reader = cmd.ExecuteReader();
            var paramList = new List<Parameter>();
            while (reader.Read())
            {
                paramList.Add(new Parameter(reader[0].ToString(), reader[1].ToString()));
            }
            conn.Close();
            return paramList;
        }

        public string[] findFilesFromPath(string path, string fileNameParam, SearchOption searchOp)
        {
            string[] filenames = null;
            filenames = Directory.GetFiles(@path, "*" + fileNameParam + "*", searchOp);

            return filenames;
        }

        public IEnumerable<SearchResult> findFileRowsInPath(string path, string fromFileSearchParam, SearchOption searchOp, string fileExtension)
        {
            string[] filenames = null;
            filenames = Directory.GetFiles(path, "*" + fileExtension, searchOp);

            var fileContents = from file in filenames
                               from line in File.ReadLines(file)
                               where line.Contains(fromFileSearchParam)
                               select new SearchResult(file, line);
            return fileContents;
        }

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

public class SearchResult
{
    public string fileName;
    public string fileRow;

    public SearchResult(string name, string row)
    {
        this.fileName = name;
        this.fileRow = row;
    }
}
public class Query
{
    public string queryID;
    public string serverID;
    public string databaseID;
    public string queryName;
    public string querySQL;
    public string description;

    public List<Parameter> queryParams = null;

    public Query(string id, string serverId, string databaseId, string queryName, string querySql, string description)
    {
        this.queryID = id;
        this.querySQL = querySql;
        this.serverID = serverId;
        this.databaseID = databaseId;
        this.queryName = queryName;
        this.description = description;
    }
    public XElement getQueryXml()
    {

        var row = new XElement("Query");

        var id = new XElement("ID");
        id.Value = this.queryID;
        var sql = new XElement("SQL");
        sql.Value = this.querySQL;
        var server = new XElement("ServerId");
        server.Value = this.serverID;
        var db = new XElement("DatabaseId");
        db.Value = this.databaseID;
        var name = new XElement("QueryName");
        name.Value = this.queryName;
        var description = new XElement("Description");
        description.Value = this.description;
        if (queryParams.Count > 0)
        {
            var queryParamsXml = new XElement("Parameters");
            foreach (var i in queryParams)
            {
                var param = new XElement(i.paramName);
                param.Value = i.ParamValue;
                queryParamsXml.Add(param);
            }
            row.Add(queryParamsXml);
        }

        row.Add(id, sql, server, db, name, description);

        return row;
    }

}
public class Parameter
{
    public string paramName;
    public string ParamValue;

    public Parameter(string name, string value)
    {
        this.paramName = name;
        this.ParamValue = value;
    }
}