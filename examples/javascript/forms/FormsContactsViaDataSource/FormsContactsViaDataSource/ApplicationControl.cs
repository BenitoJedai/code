using FormsContactsViaDataSource;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;


namespace FormsContactsViaDataSource
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // X:\jsc.svn\examples\javascript\forms\FormsNICWithDataSource\FormsNICWithDataSource\ApplicationWebService.cs

            var a = await this.applicationWebService1.GetContacts();

            Replace(a);
        }

        private void Replace(IEnumerable<Data.ContactDataGetContactsRow> a)
        {
            #region data to bindingsource
            // Additional information: Cannot clear this list.
            //this.nICDataGetInterfacesBindingSourceBindingSource.Clear();

            while (this.contactDataGetContactsBindingSourceBindingSource.Count > 0)
                this.contactDataGetContactsBindingSourceBindingSource.RemoveAt(0);

            foreach (var item in a)
            {
                // Additional information: Item cannot be added to a read-only or fixed-size list.
                (this.contactDataGetContactsBindingSourceBindingSource.AddNew() as DataRowView).With(
                    r =>
                    {
                        // is there a more elegant way?
                        r["Key"] = item.Key;

                        r["name"] = item.name;
                        r["email"] = item.email;

                        r["Timestamp"] = item.Timestamp;
                    }
                );
            }
            #endregion
        }

    }
}
