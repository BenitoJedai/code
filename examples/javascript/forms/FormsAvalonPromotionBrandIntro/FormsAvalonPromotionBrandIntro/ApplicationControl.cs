using FormsAvalonPromotionBrandIntro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FormsAvalonPromotionBrandIntro
{
    public partial class ApplicationControl : UserControl
    {
        ScriptCoreLib.Shared.Avalon.Integration.IAssemblyReferenceToken __Avalon_Integration = null;
        //ScriptCoreLib.Shared.Avalon.IAssemblyReferenceToken __Avalon = null;

        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new FormsAvalonAnimation.Form1().Show();
        }

    }
}
