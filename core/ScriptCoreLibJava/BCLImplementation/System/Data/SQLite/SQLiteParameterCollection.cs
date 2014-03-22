using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
//using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Data.SQLite
{
    //[Script(Implements = typeof(global::System.Data.SQLite.SQLiteParameterCollection))]
    [Script(ImplementsViaAssemblyQualifiedName = "System.Data.SQLite.SQLiteParameterCollection")]
    internal class __SQLiteParameterCollection : __DbParameterCollection
    {
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\Common\DbParameterCollection.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140322
        //public __SQLiteCommand Context;

        public List<__SQLiteParameter> InternalParameters = new List<__SQLiteParameter>();

        public override int InternalAdd(object value)
        {
            var p = value as __SQLiteParameter;

            AddWithValue(
                p.ParameterName,
                p.Value
            );


            return InternalParameters.Count;
        }
        public __SQLiteParameter AddWithValue(string name, object value)
        {
            // X:\jsc.svn\core\ScriptCoreLib.Extensions\ScriptCoreLib.Extensions\Extensions\IDbConnectionExtensions.cs

            var n = new __SQLiteParameter { ParameterName = name, Value = value };

            InternalParameters.Add(n);

            return n;
        }


        public override int Count
        {
            get { return this.InternalParameters.Count; }
        }

        public static implicit operator DbParameterCollection(__SQLiteParameterCollection e)
        {
            return (DbParameterCollection)(object)e;
        }
    }
}
