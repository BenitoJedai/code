using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using SharedBrowserSessionExperiment.DataLayer.Data;

namespace SharedBrowserSessionExperiment
{
    public partial class PositionsWatchdog : Form
    {
        public PositionsWatchdog()
        {
            InitializeComponent();
        }

        TheBrowserTab f;
        int old = -1;
        private async void PositionsWatchdog_Load(object sender, EventArgs e)
        {
            var b = new TheBrowserTab { Owner = this };
            f = b;

            b.navigationOrdersNavigateBindingSourceBindingSource.PositionChanged +=
                delegate
                {
                    var p = b.navigationOrdersNavigateBindingSourceBindingSource.Position;

                    if (p == old)
                        return;

                    old = p;

                    (this.navigationOrdersPositionsBindingSourceBindingSource.AddNew() as DataRowView).With(
                       async rr =>
                       {

                           this.Text = new { p }.ToString();
                           //Console.WriteLine(new { p });

                           // int as object tostring for 0 fails?
                           //rr["NavigateBindingSourcePosition"] = new { p }.ToString();
                           rr["NavigateBindingSourcePosition"] = "" + p;
                           rr["Left"] = "" + b.Left;
                           rr["Top"] = "" + b.Top;
                           rr["Width"] = "" + b.Width;
                           rr["Height"] = "" + b.Height;

                           await this.applicationWebService1.InsertPosition(
                               new DataLayer.Data.NavigationOrdersPositionsRow
                               {
                                   NavigateBindingSourcePosition = p,
                                   Left = b.Left,
                                   Top = b.Top,
                                   Width = b.Width,
                                   Height = b.Height
                               }
                           );

                           rr["Key"] = "" + this.applicationWebService1.LastKnownPositionKey;
                           this.Text = new { key = this.applicationWebService1.LastKnownPositionKey, p }.ToString();
                       }
                   );


                };

            await b.ShowAsync();

            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await RefreshAsync();
        }

        private async Task RefreshAsync()
        {
            button1.Enabled = false;
            await this.f.RefreshAsync();
            var b0 = await this.applicationWebService1.GetLastPosition();
            var b = b0;

            if (b.Key != default(NavigationOrdersPositionsKey))
            {
                //this.Text = new { key = this.applicationWebService1.LastKnownPositionKey }.ToString();

                ((DataRowView)this.navigationOrdersPositionsBindingSourceBindingSource.AddNew()).With(
                    //async
                       rr =>
                       {

                           //this.Text = new { p }.ToString();
                           //Console.WriteLine(new { p });

                           // int as object tostring for 0 fails?
                           //rr["NavigateBindingSourcePosition"] = new { p }.ToString();
                           rr["NavigateBindingSourcePosition"] = "" + b.NavigateBindingSourcePosition;
                           rr["Left"] = "" + b.Left;
                           rr["Top"] = "" + b.Top;
                           rr["Width"] = "" + b.Width;
                           rr["Height"] = "" + b.Height;

                           //await this.applicationWebService1.InsertPosition(
                           //    new DataLayer.Data.NavigationOrdersPositionsRow
                           //    {
                           //        NavigateBindingSourcePosition = p,
                           //        Left = b.Left,
                           //        Top = b.Top,
                           //        Width = b.Width,
                           //        Height = b.Height
                           //    }
                           //);

                           rr["Key"] = "" + b.Key;

                           //this.Text = new { key = this.applicationWebService1.LastKnownPositionKey, p }.ToString();

                           old = (int)b.NavigateBindingSourcePosition;

                           this.f.navigationOrdersNavigateBindingSourceBindingSource.Position = old;
                       }
               );
            }

            button1.Enabled = true;

        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            while (checkBox1.Checked)
            {
                await RefreshAsync();

                if (checkBox1.Checked) await Task.Delay(300);
                if (checkBox1.Checked) await Task.Delay(300);
                if (checkBox1.Checked) await Task.Delay(300);
            }
        }

        //private void navigationOrdersPositionsBindingSourceBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        //{
        //    //script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.add_AddingNew(System.ComponentModel.AddingNewEventHandler)]
        //    //script: warning JSC1000: Did you reference ScriptCoreLib via IAssemblyReferenceToken?

        //    // there is a new datarow but is there data?

        //    Console.WriteLine(
        //        "navigationOrdersPositionsBindingSourceBindingSource_AddingNew "
        //        + new { e.NewObject }
        //        );
        //}


    }
}
