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
                (x, y) =>
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
                            var ff = new Form
                            {
                                Text = x
                            };

                            var fw = new WebBrowser { Dock = DockStyle.Fill };

                            fw.DocumentText = xml.ToString();

                            ff.Controls.Add(fw);

                            ff.StartPosition = FormStartPosition.Manual;
                            ff.Left = c.Right - ff.Width;
                            ff.Top = c.Top;
                            ff.Height = c.Height;

                            ff.Show();

                            //MessageBox.Show(xml.ToString());
                        };

                    this.applicationWebService1.ExecuteReaderAsync(x, y, AtDataGridContent);
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

    }
}
