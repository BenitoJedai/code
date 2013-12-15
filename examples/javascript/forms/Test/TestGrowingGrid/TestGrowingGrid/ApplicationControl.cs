using TestGrowingGrid;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using ScriptCoreLib.Extensions;

namespace TestGrowingGrid
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var data = Book1.GetDataSet().Tables[0];

            this.dataGridView1.DataSource = data;

            this.AutoScroll = true;

            //this.dataGridView1.pre

            Action y = delegate
            {
                var bottom = this.dataGridView1.Bottom;

                var ControlsBelowThisPoint = Enumerable.ToArray(
                    from x in this.Controls.AsEnumerable()
                    where x.Top > bottom
                    select x
                 );


                var p = this.dataGridView1.PreferredSize;

                // ? grow only?
                //p.Width = Math.Max(this.Width - this.dataGridView1.Left - 3, p.Width);
                p.Width = Math.Max(300, p.Width);

                this.panel1.Size = p;

                this.dataGridView1.Size = this.panel1.Size;

                this.button1.Text = new { data.Rows.Count, this.panel1.Size }.ToString();

                this.SuspendLayout();
                ControlsBelowThisPoint.WithEach(
                    x =>
                    {
                        x.Top += this.dataGridView1.Bottom - bottom;
                    }
                );

                // by now our PreferredSize has changed.

                this.ResumeLayout(performLayout: true);
                //this.PerformLayout();
            };

            dataGridView1.ColumnWidthChanged +=
                delegate
                {
                    y();

                };

            data.TableNewRow +=
                delegate
                {
                    y();

                };

            y();

            this.button1.Click +=
                delegate
                {

                    //data.
                };
        }

        private void button1_Click(object sender, System.EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {

        }

    }
}
