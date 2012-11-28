using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteParameterCollection))]
    internal class __SQLiteParameterCollection : __DbParameterCollection
    {
        //public __SQLiteCommand Context;

        public List<__SQLiteParameter> InternalParameters = new List<__SQLiteParameter>();

        public __SQLiteParameter AddWithValue(string name, object value)
        {
            var n = new __SQLiteParameter { ParameterName = name, Value = value };

            InternalParameters.Add(n);

            return n;
        }

    }
}
