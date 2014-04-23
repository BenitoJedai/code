using FormsContactsViaDataSource;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ScriptCoreLib.Extensions;
using System;


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

            var count = await this.applicationWebService1.GetContactsCount();

            var perpage = 11;
            var pagecount = count / perpage;

            Console.WriteLine(new
            {
                count,
                pagecount,
                this.contactDataGetContactsByPageBindingSourceBindingSource
            });

            // "Cannot read property 'jAEABrc_alj_abCA79ng3_b3w' of undefined"

            // return ( function () { var c$11 = this.contactDataGetContactsByPageBindingSourceBindingSource.jAEABrc_alj_abCA79ng3_b3w(); return (c$11 instanceof _3tIZYSq_akTK30oTh9r6C0w ? c$11 : null); } )();


            var pages = Enumerable.Range(0, pagecount).Select(
                //x => this.contactDataGetContactsByPageBindingSourceBindingSource.AddNew() as DataRowView
                x => this.contactDataGetContactsByPageBindingSourceBindingSource.AddNew()
            ).ToArray();


            //this.bindingNavigator1.add


            this.contactDataGetContactsByPageBindingSourceBindingSource.CurrentChanged +=
                async delegate
                {
                    var a = await this.applicationWebService1.GetContacts(
                        this.contactDataGetContactsByPageBindingSourceBindingSource.Position * perpage
                        , perpage);

                    Replace(a);
                };

            Console.WriteLine("before Position");
            //this.contactDataGetContactsByPageBindingSourceBindingSource.ResetCurrentItem();
            this.contactDataGetContactsByPageBindingSourceBindingSource.Position = 0;

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
