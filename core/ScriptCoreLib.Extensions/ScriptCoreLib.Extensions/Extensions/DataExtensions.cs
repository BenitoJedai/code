using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// whats the activating namespace?
//namespace System.Data
namespace ScriptCoreLib.Extensions
{
    public static class DataExtensions
    {
        // documentation site shall help with discoverabilty
        


        public static IEnumerable<DataRow> AsEnumerable(this DataRowCollection c)
        {
            var a = new List<DataRow>();

            foreach (DataRow item in c)
            {
                a.Add(item);
            }
            return a;
        }




        public static IEnumerable<DataColumn> AsEnumerable(this DataColumnCollection c)
        {
            var a = new List<DataColumn>();

            foreach (DataColumn item in c)
            {
                a.Add(item);   
            }
            return a;
        }

        public static IEnumerable<DataTable> AsEnumerable(this DataTableCollection c)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131125-xslx-key?pageMoved=201311

            var a = new List<DataTable>();

            foreach (DataTable item in c)
            {
                a.Add(item);
            }

            return a;
        }
    }
}
