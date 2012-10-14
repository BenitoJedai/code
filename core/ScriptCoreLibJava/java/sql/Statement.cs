using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.sql
{
    // http://docs.oracle.com/javase/1.4.2/docs/api/java/sql/Statement.html
    [Script(IsNative = true)]
    public interface Statement
    {
        ResultSet executeQuery(string value);
        int executeUpdate(string value);
    }
}
