using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FormsWebServiceViaInheritance.Library
{
    public partial class TimerDisplay : UserControl
    {
        public TimerDisplay()
        {
            Console.WriteLine("TimerDisplay InitializeComponent");
            InitializeComponent();
        }

        private Stopwatch w = new Stopwatch();

        private void TimerDisplay_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //            script: error JSC1000:
            //error:
            //  statement cannot be a load instruction (or is it a bug?)
            //  [0x0007] ldarg.0    +1 -0

            // assembly: U:\FormsWebServiceViaInheritance.Application.exe
            // type: FormsWebServiceViaInheritance.Library.TimerDisplay, FormsWebServiceViaInheritance.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x0007
            //  method:Void timer1_Tick(System.Object, System.EventArgs)

            var Elapsed = w.Elapsed;

            this.label1.Text = new { Foo, Elapsed }.ToString();
        }

        public string Foo = "default";

        public void Go()
        {
            w.Start();
            var Elapsed = w.Elapsed;
            this.label1.Text = new { Foo, Elapsed }.ToString();
            timer1.Enabled = true;
        }
    }
}
