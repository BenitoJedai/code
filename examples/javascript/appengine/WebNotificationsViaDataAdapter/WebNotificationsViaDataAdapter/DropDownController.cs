using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System.Windows.Forms;

namespace ScriptCoreLib.Extensions
{
    class DropDownController
    {
        public void AddDropDown(IHTMLDiv app, UserControl uc)
        {
            app.style.SetSize(
                uc.Width,
                uc.Height
            );
            uc.AttachControlTo(app);
            uc.ParentForm.GetHTMLTarget().style.boxShadow = "black 3px 3px 6px -3px";
        }
        public void RemoveDropDown(IHTMLDiv app, UserControl uc)
        {
            uc.ParentForm.Close();
            uc = null;
        }
    }
}
