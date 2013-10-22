using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

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
    }
}
