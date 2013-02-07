using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;

namespace FlashHeatZeekerWithStarlingT10.Library
{



    public partial class FrameDiagnostics : Form
    {
        public FrameDiagnostics()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        public void Initialize(IFrameDiagnostics e)
        {
            Console.WriteLine("FrameDiagnostics.Initialize");
            this.checkBox1.CheckedChanged +=
                delegate
                {

                    e.user_pause.value = Convert.ToString(this.checkBox1.Checked);
                };

            this.checkBox2.CheckedChanged +=
                 delegate
                 {
                     e.hidetrees.value = Convert.ToString(this.checkBox2.Checked);
                 };

            this.checkBox3.CheckedChanged +=
                 delegate
                 {
                     e.hidegroundunits.value = Convert.ToString(this.checkBox3.Checked);
                 };

            this.checkBox4.CheckedChanged +=
                 delegate
                 {
                     e.hideairunits.value = Convert.ToString(this.checkBox4.Checked);
                 };

            var traceperformance = new { task = "", set_time = default(Action<string>) }.ToEmptyList();

            e.traceperformance(
                (task, time) =>
                {
                    var x = traceperformance.FirstOrDefault(k => k.task == task);

                    if (x == null)
                    {
                        var r = new DataGridViewRow();
                        var c_task = new DataGridViewTextBoxCell { Value = task };
                        var c_time = new DataGridViewTextBoxCell { };

                        r.Cells.Add(c_task);
                        r.Cells.Add(c_time);
                        this.dataGridView1.Rows.Add(r);

                        Action<string> set_time =
                            value =>
                            {
                                c_time.Value = value;
                            };

                        x = new { task, set_time }.AddTo(traceperformance);
                    }

                    x.set_time(time);
                }
            );

            this.checkBox5.CheckedChanged +=
                delegate
                {
                    e.traceperformance_enabled.value = Convert.ToString(this.checkBox5.Checked);

                };

            this.checkBox6.CheckedChanged +=
               delegate
               {
                   e.hidelayers.value = Convert.ToString(this.checkBox6.Checked);
               };

            this.button1.Click +=
               delegate
               {
                   e.F1();
               };

            this.button2.Click +=
             delegate
             {
                 e.F2();
             };
        }

        private void FrameDiagnostics_Load(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
