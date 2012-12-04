using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
