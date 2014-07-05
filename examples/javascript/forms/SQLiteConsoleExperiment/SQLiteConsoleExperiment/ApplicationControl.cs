using ShellWithPing.Library;
using SQLiteConsoleExperiment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.SQLite;

namespace SQLiteConsoleExperiment
{
    public partial class ApplicationControl : UserControl

    // can do that yet. the referenced webservice is not correctly linked yet
    //: ShellWithPing.ApplicationControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var DefaultText =
@"example:
  cls
  ls

  create table if not exists Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null)
  create table if not exists Table1Meta ( MetaKey INTEGER PRIMARY KEY AUTOINCREMENT , DeclaringType INTEGER  , MemberName text not null , MemberValue text not null, FOREIGN KEY(DeclaringType) REFERENCES Table1(ContentKey))

  select * from sqlite_master
  select * from INFORMATION_SCHEMA.TABLES

  insert into Table1 (ContentValue) values ('AddItem')
  insert into Table1 (ContentValue) values (cast( 'AddItem' as longtext))

  insert into Table1Meta (MemberName, MemberValue, DeclaringType)  values ('Left', '1', 0)

  select ContentKey, ContentValue from Table1
  select ContentKey, ContentValue, coalesce((select MemberValue from Table1Meta where DeclaringType = ContentKey order by MetaKey desc), '0') as Left from Table1


";

            var c = new ConsoleWindow { Text = "SQLite " + new { ApplicationWebService.DataSource } }
                .AppendLine(DefaultText);


            c.Show();

            c.AtCommand +=
                async (x, y) =>
                {
                    if (x == "cls")
                    {
                        c.Clear(DefaultText);
                        return;
                    }

                    if (x == "ls")
                    {
                        x = "select * from sqlite_master";
                    }

                    Action<XElement> AtDataGridContent =
                        xml =>
                        {
                            //var ff = new Form
                            //{
                            //    Text = x
                            //};

                            //var fw = new WebBrowser { Dock = DockStyle.Fill };

                            //fw.DocumentText = xml.ToString();

                            //ff.Controls.Add(fw);

                            //ff.StartPosition = FormStartPosition.Manual;
                            //ff.Left = c.Right - ff.Width;
                            //ff.Top = c.Top;
                            //ff.Height = c.Height;

                            //ff.Show();

                            //MessageBox.Show(xml.ToString());
                        };

                    var data = await this.applicationWebService1.ExecuteReaderAsync(x, y, AtDataGridContent);

                    {
                        var ff = new Form
                        {
                            Text = new { data.TableName }.ToString()
                        };

                        var g = new DataGridView
                        {
                            Dock = DockStyle.Fill,
                            DataSource = data
                        };

                        ff.Controls.Add(g);

                        ff.Show();

                    }
                };

            c.FormClosing +=
                (s, a) =>
                {
                    if (c.DisableFormClosingHandler)
                        return;

                    //if (MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    a.Cancel = true;


                    c.WindowState = FormWindowState.Minimized;
                };

            if (NewForm != null)
                NewForm(c);
        }


        public event Action<ConsoleWindow> NewForm;


        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // X:\jsc.svn\examples\javascript\Test\TestSQLiteConnection\TestSQLiteConnection\Application.cs
            // x:\jsc.svn\examples\javascript\linq\ggearalpha\ggearalpha\library\googlegearsadvanced.cs
            // lets have sql in client..

            var cc = new SQLiteConnection(

                    new SQLiteConnectionStringBuilder
            {
                // wont matter on client for now
                DataSource = "file:PerformanceResourceTimingData2.xlsx.sqlite"
            }.ToString()

                );
            cc.Open();

            var c = new ConsoleWindow { Text = "SQLite in client" };


            c.AppendLine("CREATE TABLE IF NOT EXISTS Employee_Table (xid, Name, Location)");
            c.AppendLine("insert into Employee_Table(xid, Name, Location) values(0, 'foo', 'bar')");
            c.AppendLine("SELECT xid, Name, Location FROM Employee_Table");

            c.Show();


            //            ---------------------------
            //Microsoft Visual Studio Express 2013 for Web
            //-------------------------- -
            //Error while trying to run project: Unable to start program 'X:\jsc.svn\examples\javascript\forms\SQLiteConsoleExperiment\SQLiteConsoleExperiment\bin\Debug\SQLiteConsoleExperiment.exe'.



            //The 32 - bit version of the Visual Studio Remote Debugging Monitor(MSVSMON.EXE) cannot be used to debug 64 - bit processes or 64 - bit dumps.Please use the 64 - bit version instead.
            //      -------------------------- -
            //      OK
            //      -------------------------- -


            c.AtCommand +=
          async (sql, y) =>
                {
                    if (sql == "cls")
                    {
                        c.Clear();
                        return;
                    }

                    {
                        var c1 = cc.CreateCommand();
                        c1.CommandText = sql;
                        // allow the callback
                        var r = await c1.ExecuteReaderAsync();

                        Console.WriteLine(new { r });

                        var index = 0;

                        while (r.Read())
                        {
                            if (index == 0)
                            {
                                y(
                                    string.Join(
                                        " | ",
                                        Enumerable.Range(
                                            0, r.FieldCount
                                        ).Select(ordinal => r.GetName(ordinal).PadRight(20, '_'))
                                    )
                                );
                            }

                            // first step becoming access
                            //y("yield row " + new { r.FieldCount });
                            //y("yield row ");



                            //xid_________________ | Name________________ | Location____________
                            //null________________ | null________________ | null________________


                            // X:\jsc.svn\examples\javascript\test\TestPadRight\TestPadRight\Application.cs


                            y(
                                string.Join(
                                    " | ",
                                    Enumerable.Range(
                                        0, r.FieldCount
                                    ).Select(ordinal => r[ordinal].ToString().PadRight(20, '·'))
                                )
                            );

                            index++;

                        }
                    }

                };

            #region FormClosing
            c.FormClosing +=
                (s, a) =>
                {
                    if (c.DisableFormClosingHandler)
                        return;

                    //if (MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    a.Cancel = true;


                    c.WindowState = FormWindowState.Minimized;
                };
            #endregion




            if (NewForm != null)
                NewForm(c);
        }
    }
}
