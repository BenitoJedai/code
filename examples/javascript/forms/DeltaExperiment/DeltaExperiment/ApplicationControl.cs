using DeltaExperiment;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.GLSL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DeltaExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        int dx = 0;

        private void button1_Click(object sender, System.EventArgs e)
        {
            dx++;

            this.label2.Text = "" + dx;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            var x = "" + dx;

            dx = 0;
            this.label2.Text = "" + dx;


            applicationWebService1.__button2_Click(x);
        }

        private void button3_Click(object sender, EventArgs e)
        {
#if DataGridView_Implements_DataSource
            var data = new List<ivec3>();

            applicationWebService1.delta.Enumerate(
                reader =>
                {
                    long id = reader.id;
                    long ticks = reader.ticks;

                    ivec3 xyz = reader.xyz;

                    data.Add(xyz);

                    //Debugger.Break();
                }
            );

            this.dataGridView1.DataSource = data;


     
#else

            this.dataGridView1.Rows.Clear();

            Action<string, string, string, string> yield =
                 (ticks, x, y, z) =>
                 {
                     var r = new DataGridViewRow();

                     r.Cells.AddTextRange(
                         ticks,
                         x,
                         y,
                         z
                     );

                     this.dataGridView1.Rows.Add(r);
                 };

#if false // ImplementsImplicitWebServiceCallSite
            applicationWebService1.delta.Enumerate(
                reader =>
                {
                    long id = reader.id;
                    long ticks = reader.ticks;

                    ivec3 xyz = reader.xyz;

                    yield(
                        "" + xyz.x,
                        "" + xyz.y,
                        "" + xyz.z
                    );
                }
            );
#else

            applicationWebService1.__button3_Click(yield);
#endif


#endif
        }

        private void ApplicationControl_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.AddTextRange(
               "ticks",
               "x",
               "y",
               "z"
           );
        }

        long ticks_int64;

        private void button4_Click(object sender, EventArgs e)
        {
#if false // ImplementsImplicitWebServiceCallSite

            applicationWebService1.delta.Last(
              ticks =>
              {
                  this.ticks = ticks;

                  label4.Text = "" + ticks;
              }
            );
#else
            applicationWebService1.__button4_Click(
                ticks =>
                {
                    this.ticks_int64 = long.Parse(ticks);

                    Console.WriteLine(new { ticks, ticks_int64 });
                    label4.Text = ticks;
                }
            );
#endif
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Action<string, string, string, string> yield =
                 (ticks, x, y, z) =>
                 {
                     var r = new DataGridViewRow();

                     r.Cells.AddTextRange(
                         ticks,
                         x,
                         y,
                         z
                     );

                     this.dataGridView1.Rows.Add(r);
                 };

            this.dataGridView1.Rows.Clear();

#if false // ImplementsImplicitWebServiceCallSite
            applicationWebService1.delta.Sum(
                new Schema.DeltaTable.SumQuery { ticks = ticks },
                reader =>
                {

                    ivec3 xyz = reader.xyz;

                    yield(
                        "" + xyz.x,
                        "" + xyz.y,
                        "" + xyz.z
                    );
                }
            );
#else
            applicationWebService1.__button5_Click(
                "" + this.ticks_int64, yield
            );
#endif
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.ticks_int64 = 0;

            label4.Text = "" + ticks_int64;
        }

    }
}
