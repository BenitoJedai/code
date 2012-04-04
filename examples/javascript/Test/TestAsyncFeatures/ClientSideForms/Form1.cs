using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyncResearch;

namespace ClientSideForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var a = new ApplicationWebService();

            a.WebMethod2(button1.Text, x => button1.Text = x);

            MessageBox.Show("All done, Sir!");
        }

        int c = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            this.button1.ForeColor = (c % 2) == 0 ? Color.Red : SystemColors.WindowText;
            this.button2.ForeColor = (c % 2) != 0 ? Color.Red : SystemColors.WindowText;
            this.button3.ForeColor = (c % 2) == 0 ? Color.Red : SystemColors.WindowText;
            this.button4.ForeColor = (c % 2) != 0 ? Color.Red : SystemColors.WindowText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var a = new ApplicationWebService();

            a.WebMethod4(button2.Text, x => button2.Text = x);

            MessageBox.Show("All done, Sir!");
        }

        async private void button3_Click(object sender, EventArgs e)
        {
            var a = new ApplicationWebService();


//2>C:\Windows\Microsoft.NET\Framework\v4.0.30319\Microsoft.Common.targets(1546,5): 
            //warning MSB3270: There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "Z:\jsc.svn\examples\javascript\Test\TestAsyncFeatures\TestAsyncFeatures\bin\Debug\TestAsyncFeatures.exe", "x86". This mismatch may cause runtime failures. Please consider changing the targeted processor architecture of your project through the Configuration Manager so as to align the processor architectures between your project and references, or take a dependency on references with a processor architecture that matches the targeted processor architecture of your project.
//2>Z:\jsc.svn\examples\javascript\Test\TestAsyncFeatures\ClientSideForms\Form1.cs(51,13,51,68): 
            //error CS4001: Cannot await 'void'
//2>Z:\jsc.svn\examples\javascript\Test\TestAsyncFeatures\ClientSideForms\Form1.cs(58,13,58,68): 
            //error CS4033: The 'await' operator can only be used within an async method. 
            //Consider marking this method with the 'async' modifier and changing its return type to 'Task'.


            await a.WebMethod8(button3.Text, x => button3.Text = x);

            MessageBox.Show("All done, Sir!");
        }

        async private void button4_Click(object sender, EventArgs e)
        {
            var a = new ApplicationWebService();

            var i =  await a.WebMethod16(button3.Text, x => button3.Text = x);

            // see! i is int!
            MessageBox.Show(i + "\n\n All done, Sir!");
        }
    }
}
