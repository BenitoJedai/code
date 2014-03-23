using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace AccountExperiment.Schema
{
    public class MySession : MySessionQueries
    {
        public readonly Action<Action<SQLiteConnection>> WithConnection;

        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            Version = 3,
            DataSource = "file:AccountExperiment.sqlite"
        };

        public MySession()
        {
            this.WithConnection = csb.AsWithConnection(

                Initializer:
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

        public void DeleteByCookie(DeleteByCookie value)
        {
            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);

                }
            );
        }

        public long SelectCount()
        {
            long value = 0;

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
