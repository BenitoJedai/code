using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiAppDatabase.Schema.Clients;

namespace MultiAppDatabaseExperiment
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ClientsTable_Insert(
                            new ClientsTable.Insert
                            {
                                //Don't trust text
                                Username = "",
                                Password = "",
                                ScreenWidth = 0,
                                ScreenHeight = 0
                            }
                    );
        }
        public Action<ClientsTable.Insert> _ClientsTable_Insert;

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
