using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Abstractatech.JavaScript.FileStorage.Schema
{
    public class FileStorageLogTable : FileStorageLogQueries
    {

        public readonly Action<Action<SQLiteConnection>> WithConnection;


        public FileStorageLogTable()
        {
            this.WithConnection = FileStorageTable.csb.xAsWithConnection();

            WithConnection(
                c =>
                {
                    new Create { }.ExecuteNonQuery(c);
                }
            );
        }



        public void Insert(Insert value, Action<long> yield = null)
        {
            WithConnection(
             c =>
             {
                 value.ExecuteNonQuery(c);

                 if (yield != null)
                     yield(c.LastInsertRowId);
             }
           );
        }




        public void SelectTransaction(Action<dynamic> yield)
        {
            WithConnection(
              c =>
              {
                  new SelectTransaction().ExecuteReader(c).WithEach(yield);
              }
            );
        }

    }
}
