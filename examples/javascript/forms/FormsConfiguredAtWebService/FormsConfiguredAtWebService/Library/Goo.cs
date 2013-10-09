using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsConfiguredAtWebService.Library
{
    public partial class Goo : Form
    {
        public Goo()
        {
            InitializeComponent();
        }

        public string GooTitle = "GooTitle";
        public string GooButtonText = "click me";
        public string GooButtonMessage = "GooButtonMessage";
        public DataTable GooDataSource;

        private void Goo_Load(object sender, EventArgs e)
        {
            this.Text = GooTitle;
            this.button1.Text = GooButtonText;

            this.dataGridView1.DataSource = GooDataSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GooButtonMessage);
        }


        public ApplicationWebService service;

        private
            //async 
            void button2_Click(object sender, EventArgs e)
        {
            //var x = await service.SpecialMessage();

            service.SpecialMessage().ContinueWith(
                task =>
                {
                    var x = task.Result;


                    MessageBox.Show(new { x }.ToString());
                }
            );

        }
    }
}
