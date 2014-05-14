using AsyncOrderByExpression;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AsyncOrderByExpression
{
    using System;
    using System.Linq.Expressions;
    //using SchemaContext;


    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private async void button1_Click(object sender, System.EventArgs e)
        {
            // when can jsc do this?

            //var z = await applicationWebService1.WebMethod2(
            //    // this is only a simle order by expression
            //    // does jsc support <T> web methods?
            //    // what about expressions?

            //    x => x.zoo
            //);


            // Error	6	Cannot implicitly convert type 'System.Linq.IOrderedQueryable<FooRow>' to 'System.Linq.Expressions.Expression<System.Linq.IOrderedQueryable<FooRow>>'	X:\jsc.svn\examples\javascript\future\AsyncOrderByExpression\AsyncOrderByExpression\ApplicationControl.cs	36	17	AsyncOrderByExpression
            // Error	6	Cannot implicitly convert type 'System.Linq.IOrderedQueryable<FooRow>' to 'System.Func<object, object>'	X:\jsc.svn\examples\javascript\future\AsyncOrderByExpression\AsyncOrderByExpression\ApplicationControl.cs	39	17	AsyncOrderByExpression
            //Expression<Func<IQueryable<FooRow>, IQueryable<FooRow>>> q = foo =>
            //    from u in foo
            //    orderby u.zoo descending
            //    select u;


            // message: "type$dh_bJ4gDA1jK1oRu5qhtWYA is not defined"
            // what type is missing for us?

            var zz = await applicationWebService1.WithSelector(
                foo =>
                    from u in foo
                        // what would it take to enable this for js?
                        //orderby u.zoo descending
                    orderby u.goo descending
                    select u
            );

            Debugger.Break();
        }
    }
}
