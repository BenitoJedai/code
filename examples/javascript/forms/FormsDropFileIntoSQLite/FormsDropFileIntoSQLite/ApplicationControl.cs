using FormsDropFileIntoSQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System.IO;
using System;
using System.Media;
using System.Data.SQLite;

namespace FormsDropFileIntoSQLite
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        public const string DataSource = "SQLiteWithDataGridView51.sqlite";


        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            var f = e.Data.GetFormats();
            // FileDrop
            var x = f.Select(k => e.Data.GetData(k)).ToArray();

            var FileDrop = (string[])e.Data.GetData(DataFormats.FileDrop);

            FileDrop.WithEach(
                k =>
                {
                    var fi = new FileInfo(k);

                    dataGridView1.Rows.Add(
                        fi.Extension,
                        fi.Name,
                        fi.Length
                    );


                    #region add to db
                    // http://code.activestate.com/recipes/252531-storing-binary-data-in-sqlite/


                    var csb = new SQLiteConnectionStringBuilder
                    {
                        DataSource = DataSource,
                        Version = 3
                    };

                    using (var c = new SQLiteConnection(csb.ConnectionString))
                    {
                        c.Open();

                        {
                            var sql = "create table if not exists Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null, ContentBytes blob)";
                            using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                            {
                            }
                        }

                        {
                            var sql = "insert into Table1 (ContentValue, ContentBytes) values (?, ?)";
                            var cmd = new SQLiteCommand(sql, c);
                            cmd.Parameters.AddWithValue("", fi.Name);
                            cmd.Parameters.AddWithValue("", File.ReadAllBytes(fi.FullName));

                            using (var reader = cmd.ExecuteReader())
                            {
                            }
                        }
                    }
                    #endregion
                }
            );

            SystemSounds.Beep.Play();
            //Console.Beep();
        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            #region read
            var csb = new SQLiteConnectionStringBuilder
          {
              DataSource = DataSource,
              Version = 3
          };

            using (var c = new SQLiteConnection(csb.ConnectionString))
            {
                c.Open();

                {
                    var sql = "create table if not exists Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null, ContentBytes blob)";
                    using (var reader = new SQLiteCommand(sql, c).ExecuteReader())
                    {
                    }
                }

                {
                    var sql = "select ContentValue, ContentBytes from Table1";
                    var cmd = new SQLiteCommand(sql, c);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ContentValue = reader.GetString(reader.GetOrdinal("ContentValue"));

                            var chunkSize = 4096;

                            // Get size of image data–pass null as the byte array parameter
                            long bytesize = reader.GetBytes(reader.GetOrdinal("ContentBytes"), 0, null, 0, 0);
                            // Allocate byte array to hold image data
                            byte[] imageData = new byte[bytesize];
                            long bytesread = 0;
                            int curpos = 0;
                            while (bytesread < bytesize)
                            {
                                // chunkSize is an arbitrary application defined value 
                                bytesread += reader.GetBytes(reader.GetOrdinal("ContentBytes"), curpos, imageData, curpos, chunkSize);
                                curpos += chunkSize;
                            }

                            dataGridView1.Rows.Add(
                                "",
                                ContentValue,
                                "" + bytesize
                            );
                        }
                    }
                }
            }
            #endregion

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
