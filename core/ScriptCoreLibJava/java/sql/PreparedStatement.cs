using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.sql
{
    // http://docs.oracle.com/javase/6/docs/api/java/sql/PreparedStatement.html
    [Script(IsNative = true)]
    public interface PreparedStatement : Statement
    {
        ResultSet executeQuery();
        int executeUpdate();

        // http://stackoverflow.com/questions/4243513/why-does-preparedstatement-setnull-requires-sqltype
        void setObject(int parameterIndex, object x);
        void setInt(int parameterIndex, int x);
        void setLong(int parameterIndex, long x);
        void setDouble(int parameterIndex, double x);
        void setString(int parameterIndex, string x);
        void setBytes(int parameterIndex, sbyte[] x);
    }
}
