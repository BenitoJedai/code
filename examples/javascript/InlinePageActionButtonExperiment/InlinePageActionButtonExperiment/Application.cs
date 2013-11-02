using InlinePageActionButtonExperiment;
using InlinePageActionButtonExperiment.Design;
using InlinePageActionButtonExperiment.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InlinePageActionButtonExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {


        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            //            An unhandled exception of type 'System.IO.FileLoadException' occurred in ScriptCoreLibA.dll

            //Additional information: Could not load file or assembly 'System.Data.SQLite, Version=1.0.86.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)

            var content = default(ApplicationControl);
            Action onclick =
           delegate
           {

               if (content == null)
               {
                   content = new ApplicationControl();


                   page.editcontext.style.SetSize(
                       content.Width,
                       content.Height
                   );

                   content.AttachControlTo(page.editcontent);

                   // blend with control
                   var bc = content.BackColor;
                   page.edit.style.backgroundColor = bc.ToString();

               }
               else
               {

                   content.ParentForm.Close();
                   content = null;

                   // blend with DOM
                   page.edit.style.backgroundColor = JSColor.Transparent;
               }
           };

            page.edit.onclick += e => { onclick(); e.preventDefault(); };
            page.edit.oncontextmenu += e => { onclick(); e.preventDefault(); };



            page.editcontent.Clear();
            page.editcontent.style.border = "none";

        }

    }
}
