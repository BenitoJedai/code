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

namespace SharedBrowserSessionExperiment
{
    public partial class PositionsWatchdog : Form
    {
        public PositionsWatchdog()
        {
            InitializeComponent();
        }

        private async void PositionsWatchdog_Load(object sender, EventArgs e)
        {
            var b = new TheBrowserTab { Owner = this };

            var old = -1;

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

                           var key = await this.applicationWebService1.InsertPosition(
                               new DataLayer.Data.NavigationOrdersPositionsRow
                               {
                                   NavigateBindingSourcePosition = p,
                                   Left = b.Left,
                                   Top = b.Top,
                                   Width = b.Width,
                                   Height = b.Height
                               }
                           );

                           rr["Key"] = "" + key;
                           this.Text = new { key, p }.ToString();
                       }
                   );


                };

            await b.ShowAsync();

            this.Close();
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
