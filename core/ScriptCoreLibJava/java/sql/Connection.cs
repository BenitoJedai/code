using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace java.sql
{
    // http://docs.oracle.com/javase/6/docs/api/java/sql/Connection.html
    [Script(IsNative = true)]
    public interface Connection
    {
        Statement createStatement();
        Statement prepareStatement(string sql);

        void close();
    }
}
