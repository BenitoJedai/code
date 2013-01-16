using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace System.Data
{
    public static class IDataReaderExtensions
    {
        public static IEnumerable<string> FieldNames(this IDataReader reader)
        {
            return Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i));
        }


     
            public static void WithEach(this IDataReader reader, Action<dynamic> y)
            {
                using (reader)
                {
                    while (reader.Read())
                    {
                        y(new DynamicDataReader(reader));
                    }
                }
            }





    }
}
