using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.WebService;
using System;
using System.Data.SQLite;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.multiscreen.formsexample
{

    public sealed partial class ApplicationXWebService : SQLiteWithDataGridView.IApplicationWebService
    {
        // jsc, serverside ctor needs to be omitted from client code
        //SQLiteWithDataGridView.IApplicationWebService 

        SQLiteWithDataGridView.IApplicationWebService service = new SQLiteWithDataGridView.ApplicationWebService();





        public void __grid_SelectContent(string e, Action<string, string, string, string> y, string ParentContentKey, Action<string> AtTransactionKey = null, Action<string> AtError = null, Action<string> AtConsole = null)
        {
            service.__grid_SelectContent(e, y, ParentContentKey, AtTransactionKey, AtError, AtConsole);
        }

        public void GridExample_UpdateItem(string ContentKey, string ContentValue, string ContentComment, Action<string> AtTransactionKey = null, Action<string> AtConsole = null)
        {
            service.GridExample_UpdateItem(ContentKey, ContentValue, ContentComment, AtTransactionKey, AtConsole);
        }

        public void GridExample_GetTransactionKeyFor(string e, Action<string> y, Action<string> AtConsole = null)
        {
            service.GridExample_GetTransactionKeyFor(e, y, AtConsole);
        }

        public void GridExample_AddItem(string ContentValue, string ContentComment, string ParentContentKey, Action<string> AtContentReferenceKey, Action<string> AtConsole = null)
        {
            service.GridExample_AddItem(ContentValue, ContentComment, ParentContentKey, AtContentReferenceKey, AtConsole);
        }

        public void GridExample_EnumerateItemsChangedBetweenTransactions(string ParentContentKey, string FromTransaction, string ToTransaction, Action<string, string, string, string> AtContent, Action<string> done, Action<string> AtConsole = null)
        {
            service.GridExample_EnumerateItemsChangedBetweenTransactions(ParentContentKey, FromTransaction, ToTransaction, AtContent, done, AtConsole);
        }
    }

}
