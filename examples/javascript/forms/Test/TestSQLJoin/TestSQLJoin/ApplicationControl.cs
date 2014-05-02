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
using System;

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

            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Extensions\Forms\FormsExtensions.cs

            // Error	1	Instance argument: cannot convert from 'System.Windows.Forms.BindingSource' to 'System.Data.DataRowCollection'	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationControl.cs	33	27	TestSQLJoin
            // Error	2	'System.Windows.Forms.BindingSource' does not contain a definition for 'AsEnumerable' and the best extension method overload 'ScriptCoreLib.Extensions.DataExtensions.AsEnumerable(System.Data.DataRowCollection)' has some invalid arguments	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationControl.cs	36	27	TestSQLJoin

            // Error	25	Could not find an implementation of the query pattern for source type 'System.Windows.Forms.BindingSource'.  'Select' not found.  Consider explicitly specifying the type of the range variable 'r'.	X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationControl.cs	38	27	TestSQLJoin



            var DealerContact =
                from r in this.theDealerContact1.book1DealerContactBindingSourceBindingSource
                select (Book1DealerContactRow)r;


            var Dealer =
                from i in this.theDealer1.book1DealerBindingSourceBindingSource
                select (Book1DealerRow)i;


            //at System.Number.StringToNumber(String str, NumberStyles options, NumberBuffer& number, NumberFormatInfo info, Boolean parseDecimal)
            //at System.Number.ParseInt64(String value, NumberStyles options, NumberFormatInfo numfmt)
            //at System.String.System.IConvertible.ToInt64(IFormatProvider provider)
            //at System.Convert.ToInt64(Object value)
            //at TestSQLJoin.Data.Book1DealerOtherTextRow.op_Implicit(DataRow )

            //ApplicationForm.Load
            //{ ColumnName = ID }
            //{ ColumnName = DealerOtherText }
            //UnhandledException:
            //System.UnhandledExceptionEventArgs

            var DealerOther =
                from i in this.theDealerOtherText1.book1DealerOtherBindingSourceBindingSource

                // Additional information: Input string was not in a correct format.
                // what input string?
                // ah. we made it a key?
                // enter { ColumnName = DealerOtherText, FieldType = TestSQLJoin.Data.Book1DealerOtherTextKey }

                select (Book1DealerOtherRow)i;




            var dealercontacts0 =


            #region from contact in DealerContact
 DealerContact
            #endregion

            #region join dealer in Dealer on contact.DealerId equals dealer.ID
.Join(
                    Dealer,
                    contact => contact.DealerId, dealer => dealer.ID,

            #endregion

            #region select new Book1TheViewRow;
 (contact, dealer) =>
                        new Book1TheViewRow
                        {
                            DealerContactText = contact.DealerContactText,
                            DealerText = dealer.DealerText,
                            //DealerOtherText = other.DealerOtherText
                        }
                );
            #endregion



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

            #region .ReplaceWith( Book1TheViewRow[])
            // should jsc enerate such extension method?
            // should bindingsource have typed datarowviews?

            // Additional information: Cannot clear this list.
            // why?
            // is our bindingsource missing something?
            //this.theView1.book1TheViewBindingSourceBindingSource.Clear();
            while (this.theView1.book1TheViewBindingSourceBindingSource.Count > 0)
                theView1.book1TheViewBindingSourceBindingSource.RemoveAt(0);


            dealercontacts.WithEach(
                k =>
                {
                    var r = (DataRowView)this.theView1.book1TheViewBindingSourceBindingSource.AddNew();

                    r["DealerContactText"] = k.DealerContactText;
                    r["DealerOtherText"] = k.DealerOtherText;
                    r["DealerText"] = k.DealerText;
                }
            );
            #endregion


        }

        private async void toolStripButton5_Click(object sender, System.EventArgs e)
        {
            // A first chance exception of type 'System.NullReferenceException' occurred in TestSQLJoin.exe


            var dealercontacts = await applicationWebService1.GetTheViewData();

            //148:26559ms UploadValuesAsync { status = 200, responseType = arraybuffer }
            //148:26578ms GetString  { Length = 0 }

            // script: error JSC1000: No implementation found for this native method, please implement [System.Data.SQLite.SQLiteConnection.get_LastInsertRowId()]
            var s = dealercontacts.AsDataTable();


            Console.WriteLine(" switch from demo data to new local data " + new { dealercontacts = dealercontacts.Count() }
                );
            // 148:91651ms  switch from demo data to new local data { dealercontacts = 27 } 

            this.theView1.book1TheViewBindingSourceBindingSource.DataSource
                = s;

            //#region .ReplaceWith( Book1TheViewRow[])
            //// should jsc enerate such extension method?
            //// should bindingsource have typed datarowviews?

            //// Additional information: Cannot clear this list.
            //// why?
            //// is our bindingsource missing something?
            ////this.theView1.book1TheViewBindingSourceBindingSource.Clear();
            //while (this.theView1.book1TheViewBindingSourceBindingSource.Count > 0)
            //    theView1.book1TheViewBindingSourceBindingSource.RemoveAt(0);


            //dealercontacts.WithEach(
            //    k =>
            //    {
            //        var r = (DataRowView)this.theView1.book1TheViewBindingSourceBindingSource.AddNew();

            //        r["DealerContactText"] = k.DealerContactText;
            //        r["DealerOtherText"] = k.DealerOtherText;
            //        r["DealerText"] = k.DealerText;
            //        r["Dealer"] = k.Dealer;
            //        r["DealerContact"] = k.DealerContact;
            //        r["Tag"] = k.Tag;

            //    }
            //);
            //#endregion

        }

    }
}
