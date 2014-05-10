using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.sql
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/sql/ResultSetMetaData.html
    [Script(IsNative = true)]
    public interface ResultSetMetaData
    {
        int getColumnCount();

        int getColumnType(int column);
        string getColumnName(int column);
        string getColumnLabel(int column);
    }
}
