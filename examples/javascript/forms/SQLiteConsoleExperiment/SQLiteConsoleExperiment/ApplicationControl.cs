using ShellWithPing.Library;
using SQLiteConsoleExperiment;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
  create table if not exists Table1 (ContentKey INTEGER PRIMARY KEY AUTOINCREMENT, ContentValue text not null)
  insert into Table1 (ContentValue) values ('AddItem')
  select ContentKey, ContentValue from Table1

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

                    this.applicationWebService1.ExecuteReaderAsync(x, y);
                };

            c.FormClosing +=
                (s, a) =>
                {
                    //if (MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    a.Cancel = true;


                    c.WindowState = FormWindowState.Minimized;
                };
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {

        }

    }
}
