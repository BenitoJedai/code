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

            // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationControl.cs

            var j = from im in m
                    join il in l on im.Key equals il.MiddleSheet
                    join ir in r on im.Key equals ir.MiddleSheet


                    let v = new Book1MiddleViewRow
                    {
                        Content = im.Content,
                        LatestLeftContent = il.LeftContent,
                        LatestRightContent = ir.RightContent,
                    }

                    select v;


            // [0x00000000] = {0, Q, A, R, , 5/5/2014 4:56:15 PM}
            // can we do this in sql?
            //var z = j.GroupBy(x => x.Key).Select(x => x.First()).ToArray();
            var z = j.GroupBy(x => x.Key).Select(x => x.Last()).ToArray();

            this.book1MiddleViewBindingSourceBindingSource.DataSource =
               z.AsDataTable();

        }

    }
}
