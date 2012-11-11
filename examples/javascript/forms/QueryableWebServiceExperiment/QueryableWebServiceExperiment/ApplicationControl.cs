using QueryableWebServiceExperiment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace QueryableWebServiceExperiment
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            listBox1.Items.Clear();

            var now = DateTime.Now;

            this.applicationAsyncWebService1.Add(new Foo { Text = "foo button1_Click " + now });

            Expression<Func<Foo, bool>> filter =
                x => x.Text.Contains("foo");

            // Error	1	Cannot implicitly convert type 'System.Linq.Expressions.Expression<System.Func<QueryableWebServiceExperiment.Foo,bool>>' to 'System.Xml.Linq.XElement'	X:\jsc.svn\examples\javascript\forms\QueryableWebServiceExperiment\QueryableWebServiceExperiment\ApplicationControl.cs	32	32	QueryableWebServiceExperiment
            //XElement xfilter = filter;

            XElement xfilter = filter.ToXElement();

            this.applicationAsyncWebService1.AsyncEnumerate(xfilter,
                   item =>
                   {

                       listBox1.Items.Add(new { item.Text }.ToString());
                   }
               );

            this.applicationAsyncWebService1.AsyncEnumerate(filter,
                item =>
                {

                    listBox1.Items.Add(new { item.Text }.ToString());
                }
            );

        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            var now = DateTime.Now;

            this.applicationAsyncWebService1.Add(new Foo { Text = "foo " + now });
        }

    }
}
