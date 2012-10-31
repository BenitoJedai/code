using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLiteWithDataGridView
{
    public interface IApplicationWebService
    {

        void GridExample_InitializeDatabase(string e, Action<string> y, string TableName);

        void GridExample_GetTransactionKeyFor(
            string TableName,

            Action<string> y);


        void GridExample_AddItem(
             string ContentValue,
             string ContentComment,
            /* int? */ string ParentContentKey,

             Action<string> AtContentReferenceKey,
             string TableName);

        void GridExample_UpdateItem(
            string TableName,

            string ContentKey,
            string ContentValue,
            string ContentComment,

             Action<string> AtTransactionKey = null
        );

        void GridExample_EnumerateItemsChangedBetweenTransactions(
           string TableName,
            /* int? */ string ParentContentKey,


           string FromTransaction,
           string ToTransaction,
           Action<string, string, string, string> AtContent,
           Action<string> done
        );

        void GridExample_EnumerateItems(
             string e,
             Action<string, string, string, string> y,
             string TableName,
            /* int? */ string ParentContentKey,
             Action<string> AtTransactionKey = null
             );
    }

}
