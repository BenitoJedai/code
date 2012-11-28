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

        void setInt(int parameterIndex, int x);
        void setLong(int parameterIndex, long x);
        void setString(int parameterIndex, string x);
    }
}
