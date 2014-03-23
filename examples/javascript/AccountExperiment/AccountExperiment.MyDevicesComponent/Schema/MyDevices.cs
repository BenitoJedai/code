using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountExperiment.MyDevicesComponent.Schema
{
    public class MyDevices : MyDevicesQueries
    {
        // allow to override
        public Action<Action<SQLiteConnection>> WithConnectionOverride;

        readonly Action<Action<SQLiteConnection>> WithConnection;

        public SQLiteConnectionStringBuilder csb = new SQLiteConnectionStringBuilder
        {
            Version = 3,
            DataSource = "file:AccountExperiment.sqlite"
        };

        public MyDevices()
        {
            this.WithConnectionOverride = csb.AsWithConnection();

            this.WithConnection =
                y =>
                {
                    this.WithConnectionOverride(
                        c =>
                        {
                            new Create { }.ExecuteNonQuery(c);
                            y(c);
                        }
                    );
                };
        }


        public Task<long> Insert(Insert value)
        {
            var id = -1L;

            WithConnection(
                c =>
                {
                    value.ExecuteNonQuery(c);

                    id = c.LastInsertRowId;
                }
            );

            return Task.FromResult(id);
        }

        public void SelectByAccount(SelectByAccount e, Action<dynamic> y)
        {
            WithConnection(
                c =>
                {
                    var r = e.ExecuteReader(c);

                    //var data = new DataTable();
                    //data.Load(r);

                    r.WithEach(y);

                }
            );
        }

        public Task Update(Update value)
        {
            //Console.WriteLine("enter Update");

            WithConnection(
                c =>
                {


                    value.ExecuteNonQuery(c);
                }
            );

            return Task.FromResult(default(object));
        }
    }
}
