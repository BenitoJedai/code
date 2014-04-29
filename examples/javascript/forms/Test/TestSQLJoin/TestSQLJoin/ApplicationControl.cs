using TestSQLJoin;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestSQLJoin.Data;
using System.Data;
using ScriptCoreLib.Extensions;

namespace TestSQLJoin
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void theView1_AtRefresh()
        {
            // http://social.msdn.microsoft.com/Forums/en-US/2535837a-9839-4825-a277-a851484d7826/how-i-can-make-bindingsource-readonly
            // join!
            // X:\jsc.svn\examples\javascript\Test\TestLINQJoin\TestLINQJoin\Application.cs

            // client side join
            // trust and data is here

            var DealerContact =
                from i in Enumerable.Range(0, this.theDealerContact1.book1DealerBindingSourceBindingSource.Count)
                let r = (DataRowView)this.theDealerContact1.book1DealerBindingSourceBindingSource[i]

                // what about implicit cast from DataRowView?
                let z = (Book1DealerContactRow)r.Row
                select z;

            var Dealer =
                from i in Enumerable.Range(0, this.theDealer1.book1DealerBindingSourceBindingSource.Count)
                let r = (DataRowView)this.theDealer1.book1DealerBindingSourceBindingSource[i]
                let z = (Book1DealerRow)r.Row
                select z;


            var DealerOther =
                from i in Enumerable.Range(0, this.theDealerOtherText1.book1DealerOtherTextBindingSourceBindingSource.Count)
                let r = (DataRowView)this.theDealerOtherText1.book1DealerOtherTextBindingSourceBindingSource[i]
                let z = (Book1DealerOtherTextRow)r.Row
                select z;



            var dealercontacts = from contact in DealerContact
                                 join dealer in Dealer on contact.DealerId equals dealer.ID

                                 // add one more
                                 join other in DealerOther on contact.DealerId equals other.ID

                                 // new viewrow { x = ?, }

                                 //select new { contact, dealer, other };

                                 let v = new Book1TheViewRow
                                 {
                                     DealerContactText = contact.DealerContactText,
                                     DealerText = dealer.DealerText,
                                     DealerOtherText = other.DealerOtherText
                                 }

                                 select v;

            this.theView1.book1TheViewBindingSourceBindingSource.Clear();

            dealercontacts.WithEach(
                k =>
                {
                    var r = (DataRowView)this.theView1.book1TheViewBindingSourceBindingSource.AddNew();


                }
            );


        }

    }
}
