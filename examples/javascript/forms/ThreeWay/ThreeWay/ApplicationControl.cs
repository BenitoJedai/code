using ThreeWay;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThreeWay.Data;

namespace ThreeWay
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, System.EventArgs e)
        {

            var l =
                from i in this.book1LeftSheetBindingSourceBindingSource
                select (Book1LeftSheetRow)i;


            var r =
                from i in this.book1RightSheetBindingSourceBindingSource

                // Additional information: Input string was not in a correct format.
                select (Book1RightSheetRow)i;

            var m =
               from i in this.book1MiddleSheetBindingSourceBindingSource
               select (Book1MiddleSheetRow)i;

            // https://code.google.com/p/morelinq/source/browse/#hg%2FMoreLinq
            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationControl.cs
            // http://stackoverflow.com/questions/161404/using-distinct-inner-join-in-sql
            // http://stackoverflow.com/questions/14321013/distinct-in-linq-based-on-only-one-field-of-the-table

            var j = from im in m
                    join il in l on im.Key equals il.MiddleSheet
                    join ir in r on im.Key equals ir.MiddleSheet


                    let v = new Book1MiddleViewRow
                    {
                        //Key = im.Key,


                        Content = im.Content,
                        LatestLeftContent = il.LeftContent,
                        LatestRightContent = ir.RightContent,
                    }

                    select v;


            // [0x00000000] = {0, Q, A, R, , 5/5/2014 4:56:15 PM}
            // can we do this in sql?
            //var z = j.GroupBy(x => x.Key).Select(x => x.First()).ToArray();

            // http://social.msdn.microsoft.com/Profile/anders%20hejlsberg/activity

            var zz =
                from v in j
                group v by v.Key;


            var z = j.GroupBy(x => x.Key).Select(x => x.Last()).ToArray();

            // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.GroupBy(System.Collections.Generic.IEnumerable`1[[ThreeWay.Data.Book1MiddleViewRow, ThreeWay.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], System.Func`2[[ThreeWay.Data.Book1MiddleViewRow, ThreeWay.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[ThreeWay.Data.Book1MiddleViewKey, ThreeWay.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]])]

            this.book1MiddleViewBindingSourceBindingSource.DataSource =
               z.AsDataTable();

        }

    }
}
