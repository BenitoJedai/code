using FormsNICWithDataSource;
using FormsNICWithDataSource.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;

namespace FormsNICWithDataSource
{
    public partial class ApplicationControl : UserControl
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140423
        // script: error JSC1000: No implementation found for this native method, please implement [System.Windows.Forms.BindingSource.set_AllowNew(System.Boolean)]

        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void nICDataGetInterfacesBindingSourceDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // now what?

            var a = await this.applicationWebService1.GetInterfaces();


            // no update?
            if (a == null)
            {
                this.ParentForm.Text = "no data";
                return;
            }

            // add it to the bindingsource

            this.ParentForm.Text = "" + a.Count();

            Replace(a.ToArray());


        }

        private void Replace(NICDataGetInterfacesRow[] a)
        {
            #region data to bindingsource
            // Additional information: Cannot clear this list.
            //this.nICDataGetInterfacesBindingSourceBindingSource.Clear();

            while (this.nICDataGetInterfacesBindingSourceBindingSource.Count > 0)
                this.nICDataGetInterfacesBindingSourceBindingSource.RemoveAt(0);

            foreach (NICDataGetInterfacesRow item in a)
            {
                // Additional information: Item cannot be added to a read-only or fixed-size list.
                (this.nICDataGetInterfacesBindingSourceBindingSource.AddNew() as DataRowView).With(
                    r =>
                    {
                        // is there a more elegant way?
                        r["Name"] = item.Name;

                        r["SupportsMulticast"] = item.SupportsMulticast;

                        r["GatewayAddresses"] = item.GatewayAddresses;
                        r["Timestamp"] = item.Timestamp;
                    }
                );
            }
            #endregion
        }

    }
}
