using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWebGLEarthShadowDomXML.Design;

namespace TestWebGLEarthShadowDomXML
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        public void ReferenceDeclaration()
        {
            Type sqlLitec = typeof(SQLiteConnection);
            Type ext = typeof(System.Data.SQLite.SQLiteConnectionStringBuilderExtensions);
        }

        public async Task<DataTable> GetAllCities()
        {

            //if (new Cities.City().Count() == 0)
            //{
            //    try
            //    {
            //        var city = new Cities.City();
            //        var sourceTable = Cities.GetDataSet();
            //        sourceTable.Tables["City"].Rows.AsEnumerable().WithEach(r => city.Insert(r));
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }

            //}


            var sourceTable = Cities.GetDataSet();
            return sourceTable.Tables["CityTable"];


            //return (from c in new Cities.City()
            //        orderby c.Key descending
            //        select c).AsDataTable();
        }

    }
}
