using TestSQLiteParameter;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;

namespace TestSQLiteParameter
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var now = DateTime.Now;

#if xDEBUG
            this.table1Component1.Add(
                // new Tables.Table1.AddQueryParameters
                       new Tables.Table1.AddQuery
                       {
                           // implicit?
                           ContentValue = "Load: " + now
                       }
                   );

            this.table1Component1.Enumerate(
                // dynamic until we can actually infer what
                // fields we are getting
                reader =>
                {
                    var data = new { reader.ContentKey, reader.ContentValue };

                    // Send it back to the caller.
                    this.listBox1.Items.Add(data.ToString());
                }
            );
#else

            this.applicationWebService1.WebMethod2("Load: " + now,
                y =>
                {
                    this.listBox1.Items.Add(y);
                }
            );
#endif
        }

    }
}
