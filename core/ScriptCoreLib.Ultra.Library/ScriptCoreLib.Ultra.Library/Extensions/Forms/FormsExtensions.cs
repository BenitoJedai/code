using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    [Obsolete("where shall this extension be at?")]
    public static class BindingSourceExtensions
    {
        // X:\jsc.svn\examples\javascript\forms\Test\TestSQLJoin\TestSQLJoin\ApplicationControl.cs

        //[Obsolete("we want to be able to write from x in source")]
        public static IEnumerable<R> Select<R>(this BindingSource x, Func<DataRowView, R> f)
        {
            return x.AsEnumerable().Select(f);
        }


        [Obsolete("what else would bindingsource have if not rows?")]
        public static IEnumerable<DataRowView> AsEnumerable(this BindingSource x)
        {
            return
                from i in Enumerable.Range(0, x.Count)
                select (DataRowView)x[i];

        }
    }
}

namespace ScriptCoreLib.Extensions
{
    public static class FormsExtensions
    {
        // move to .Forms namespace?

        public static T AttachTo<T>(this T source, Control c) where T : Control
        {
            c.Controls.Add(source);

            return source;
        }
    }
}
