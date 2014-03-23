using ScriptCoreLib.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AccountExperiment.Schema
{
    public class MyAccount : MyAccountQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            Version = 3,
            DataSource = "file:AccountExperiment.sqlite"
        };

        public MyAccount()
        {
            this.WithConnection = csb.AsWithConnection();

            WithConnection(
               c =>
               {
                   new MyAccountQueries.Create { }.ExecuteNonQuery(c);
                   new MySessionQueries.Create { }.ExecuteNonQuery(c);
               }
           );
        }


        public long Insert(Insert value)
        {
            var id = -1L;

            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);

                    id = c.LastInsertRowId;
                }
            );

            return id;
        }

        public void SelectByPassword(SelectByPassword value, Action<long> yield)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteReader(c).WithEach(
                        r =>
                        {
                            long id = r.id;

                            yield(id);
                        }
                    );
                }
            );
        }

        public void SelectByCookie(SelectByCookie value, Action<dynamic> yield)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteReader(c).WithEach(yield);
                }
            );
        }

        public long SelectCount()
        {
            long value = 0;

            // { Message = SQL logic error or missing database
            //no such table: MyAccount,

            WithConnection(
                c =>
                {
                    new SelectCount { }.ExecuteReader(c).WithEach(
                        r =>
                        {
                            long count = r.count;

                            value = count;
                        }
                    );

                }
            );

            return value;
        }
    }

}
