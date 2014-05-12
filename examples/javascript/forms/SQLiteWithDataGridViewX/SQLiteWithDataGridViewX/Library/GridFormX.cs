using SQLiteWithDataGridViewX.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteWithDataGridViewX.Library
{
    public partial class GridFormX : Form
    {
        public GridFormX()
        {
            InitializeComponent();
        }

        private async void GridFormX_Load(object sender, EventArgs e)
        {
            var u = await this.applicationWebService1.SelectContent();

            this.schemaTheGridTableViewBindingSourceBindingSource.DataSource = u.AsDataTable();
        }

        private void schemaTheGridTableViewBindingSourceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (schemaTheGridTableViewBindingSourceDataGridView.Rows[e.RowIndex].IsNewRow)
                return;

            var u = 
                from x in  this.schemaTheGridTableViewBindingSourceBindingSource
                select (SchemaTheGridTableViewRow)x;

            var ContentKey = u.ElementAt(e.RowIndex).ContentKey;

            var f = new GridFormX
            {
                Owner = this,
                StartPosition = FormStartPosition.Manual
            };

            f.applicationWebService1.ParentContentKey = ContentKey;

            f.Location = new Point(this.Left, this.Top + 32);

            f.Show();

        }
    }
}
