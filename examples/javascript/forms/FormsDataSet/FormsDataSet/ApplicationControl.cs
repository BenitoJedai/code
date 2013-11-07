using FormsDataSet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsDataSet
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131107/assetslibrary

            Action<DataSet> apply =
                x =>
                {
                    this.dataGridView1.DataSource = x;

                    //                    arg[0] is typeof System.EventHandler
                    //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.ListControl.add_SelectedValueChanged(System.EventHandler)]
                    // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.DataGridView.set_DataMember(System.String)]

                    this.comboBox1.SelectedIndexChanged +=
                        delegate
                        {
                            Console.WriteLine("SelectedIndexChanged " + new { this.comboBox1.SelectedIndex, this.comboBox1.Text });

                            this.dataGridView1.DataMember = this.comboBox1.Text;
                        };

                    Console.WriteLine("ApplicationControl_Load Tables enter");

                    foreach (DataTable item in x.Tables)
                    {
                        Console.WriteLine("ApplicationControl_Load Tables " + new { item.TableName });
                        this.comboBox1.Items.Add(item.TableName);
                        //this.dataGridView1.DataMember = item.TableName;
                    }

                    Console.WriteLine("ApplicationControl_Load Tables exit");



                    this.comboBox1.SelectedIndex = 0;
                };

            var xx = await this.applicationWebService1.Book1();
            apply(xx);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
