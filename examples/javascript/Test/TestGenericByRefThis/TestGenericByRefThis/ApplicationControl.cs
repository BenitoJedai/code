using TestGenericByRefThis;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestGenericByRefThis
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        //struct Foo
        //class Foo<G>
        struct Foo<G>
        {
            public string Text;

            public void Method1()
            {
                // Error	1	Cannot pass 'this' as a ref or out argument because it is read-only	X:\jsc.svn\examples\javascript\Test\TestGenericByRefThis\TestGenericByRefThis\ApplicationControl.cs	23	29	TestGenericByRefThis
                Method2(ref this);
            }

            void Method2(ref Foo<G> f)
            {
                //Text = "Method2";

                f.Text = "Method2";
            }
        }

        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRGenericByRefThis\TestJVMCLRGenericByRefThis\Program.cs

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // Error	1	Use of unassigned local variable 's'	X:\jsc.svn\examples\javascript\Test\TestGenericByRefThis\TestGenericByRefThis\ApplicationControl.cs	40	13	TestGenericByRefThis
            // Error	3	Cannot convert null to 'TestGenericByRefThis.ApplicationControl.Foo<string>' because it is a non-nullable value type	X:\jsc.svn\examples\javascript\Test\TestGenericByRefThis\TestGenericByRefThis\ApplicationControl.cs	39	29	TestGenericByRefThis
            var s = default(Foo<string>);

            s.Method1();

            this.ParentForm.Text = new { s.Text }.ToString();
        }
    }
}
