using TestMinSelector;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestMinSelector
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // x:\jsc.svn\examples\javascript\linq\test\auto\testselect\testselectmin\program.cs

            var data = new[] {
                    new { x = 7, Tag = "big" },
                    new {x = 5, Tag = "small" }
            };


            var min = data.Min(x => x.x);

            this.ParentForm.Text = new { min }.ToString();

        }
    }
}
