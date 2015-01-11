using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: System.Reflection.Obfuscation(Feature = "script")]

namespace Test453CallWithMultipleDelegatesSharedContext
{
    class Program : ScriptCoreLib.Shared.IAssemblyReferenceToken
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201501/20150111/test453callwithmultipledelegates

        //0x0079 callvirt[ScriptCoreLib] ScriptCoreLib.JavaScript.DOM.SQLTransaction.executeSql(sqlStatement : string, arguments : object[] = null, callback : SQLStatementCallback(SQLTransaction, SQLResultSet) -> void = null, errorCallback : SQLStatementErrorCallback(SQLTransaction, SQLError) -> void = null) : void

        static void executeSql(string sqlStatement, Action<object> callback, Action<object> errorCallback)
        {
        }

        static void ExecuteNonQueryAsync(object context)
        {
            //c = new ctor$BQAABk0TCji3K4HjfSPffQ();
            //c.context = b;
            //AQAABrgX7z2kM1eMjLm0aw('', new ctor$_0yAABmRqvT_anyrqn5BDJ5w(c, 'BgAABk0TCji3K4HjfSPffQ'), new ctor$_0yAABmRqvT_anyrqn5BDJ5w(c, 'BwAABk0TCji3K4HjfSPffQ'));

            executeSql(
                sqlStatement: "",

                 callback:
                    xtx =>
                    {
                        var _context = context;
                    },

                 errorCallback:
                    xtx =>
                    {
                        var _context = context;

                    }
            );

        }

        static void Main(string[] args)
        {
        }
    }
}
