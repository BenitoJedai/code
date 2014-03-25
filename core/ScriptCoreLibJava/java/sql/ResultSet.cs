using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.sql
{
    // http://docs.oracle.com/javase/6/docs/api/java/sql/ResultSet.html
    [Script(IsNative = true)]
    public interface ResultSet
    {
        void close();

        bool next();

        string getString(int value);
        int getInt(int value);
        long getLong(int columnIndex);
        sbyte[] getBytes(int columnIndex);

        int findColumn(string value);

        ResultSetMetaData getMetaData();
    }
}
