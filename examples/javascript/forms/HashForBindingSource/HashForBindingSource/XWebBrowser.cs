using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HashForBindingSource
{
    [Obsolete("we want to databind to DocumentText")]
    public class XWebBrowser : WebBrowser
    {
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string DocumentText { get { return base.DocumentText; } set { base.DocumentText = value; } }
    }
}
