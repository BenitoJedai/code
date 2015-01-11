using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: System.Reflection.Obfuscation(Feature = "script")]
namespace Test453CallWithMultipleDelegates
{
    class Program
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/test453callwithmultipledelegates

        //0x0079 callvirt[ScriptCoreLib] ScriptCoreLib.JavaScript.DOM.SQLTransaction.executeSql(sqlStatement : string, arguments : object[] = null, callback : SQLStatementCallback(SQLTransaction, SQLResultSet) -> void = null, errorCallback : SQLStatementErrorCallback(SQLTransaction, SQLError) -> void = null) : void

        static void executeSql(string sqlStatement, Action<object> callback, Action<object> errorCallback)
        {
        }

        static void ExecuteNonQueryAsync()
        {
            executeSql(
                sqlStatement: "",

                 callback:
                    xtx =>
                    {
                    },

                 errorCallback:
                    xtx =>
                    {

                    }
            );

        }

        static void Main(string[] args)
        {
        }
    }
}
