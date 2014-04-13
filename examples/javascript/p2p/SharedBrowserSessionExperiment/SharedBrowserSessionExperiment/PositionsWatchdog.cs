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
            Console.WriteLine("will send to server " + new { this.applicationWebService1.LastKnownPositionKey });
            var b0 = await this.applicationWebService1.GetLastPosition();

            //I/chromium( 6636): [INFO:CONSOLE(37861)] "%c92:97445ms UploadValuesAsync { status = 200, responseType = arraybuffer }", source: http://192.168.43.7:14633/view-source (37861)
            //I/chromium( 6636): [INFO:CONSOLE(37861)] "%c92:97460ms GetString  { Length = 0 }", source: http://192.168.43.7:14633/view-source (37861)
            //I/chromium( 6636): [INFO:CONSOLE(37861)] "%c92:97496ms server sent us { LastKnownPositionKey = 0 }", source: http://192.168.43.7:14633/view-source (37861)

            // 92:12282ms server sent us { LastKnownPositionKey = 0 } 
            Console.WriteLine("server sent us, are fields shared? " + new
            {
                b0.Key,
                this.applicationWebService1.LastKnownPositionKey
            });



            var b = b0;

            if (b.Key != default(NavigationOrdersPositionsKey))
            {
                if (this.applicationWebService1.LastKnownPositionKey == 0)
                {
                    Console.WriteLine("wtf? key did not make it over the wire?");
                    button1.Enabled = true;
                    return;
                }

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
                checkBox1.Text = "enter RefreshAsync";
                await RefreshAsync();
                checkBox1.Text = "exit RefreshAsync";

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
