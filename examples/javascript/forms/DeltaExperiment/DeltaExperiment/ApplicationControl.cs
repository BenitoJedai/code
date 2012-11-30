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

#if DEBUG
            // cannot_do_this_just_yet
            // using a field component from the serverside shall cause
            // this function to run on serverside.
            // implicit rewrite
            // any incoming parameter read shall be performed ahead of time
            var now = DateTime.Now;

            applicationWebService1.delta.Add(
                ticks: now.Ticks,
                x: int.Parse(x)
            );

            // what if there is a continuation
            // what if we want to consult the client side
            // and then continue on the server side?
            // the simple approach could be to allow this if stack is empty.
#else

            applicationWebService1.__button2_Click(x);
#endif
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

            Action3<string> yield =
                 (x, y, z) =>
                 {
                     var r = new DataGridViewRow();

                     r.Cells.AddTextRange(
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
               "x",
               "y",
               "z"
           );
        }

        private void button4_Click(object sender, EventArgs e)
        {
            applicationWebService1.delta.Last(
              ticks =>
              {
                  label4.Text = "" + ticks;
              }
          );
        }

    }
}
