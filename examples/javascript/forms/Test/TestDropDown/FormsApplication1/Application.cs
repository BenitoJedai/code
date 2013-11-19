using FormsApplication1;
using FormsApplication1.Design;
using FormsApplication1.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
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
using TestDropDown;

namespace FormsApplication1
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            content.AttachControlToDocument();


          

            var ss = new DataGridView
                {
                    DataSource = Book1.GetDataSet(),
                    DataMember = "Assets"
                };
            content.Controls.Add(ss);

            var x = new IHTMLDiv();

            x.style.backgroundColor = "rgba(255,255,255,1.0)";
            x.style.position = IStyle.PositionEnum.absolute;
            x.style.height = "auto";
            x.style.width = "100%";
            x.style.top = "100%";
            x.style.zIndex = 999;
            var temp = ss.Rows[1];
            var sss = temp.Cells[0].AsHTMLElementContainer();
            sss.style.backgroundColor = "red";
            new TheOtherOption { }.With(
               o =>
               {
                   o.BackColor = Color.White;

                   o.GetHTMLTarget().With(
                       div =>
                       {
                           div.AttachTo(x);


                           div.style.position = IStyle.PositionEnum.absolute;
                       }
                   );

               }
           );


            x.AttachTo(sss.parentNode);
            x.Hide();

            sss.onmouseover += delegate
            {
                x.Show();
            };

            sss.onmouseout += delegate
            {
                x.Hide();
            };



            //that.GotFocus += delegate
            //{
            //    x.Show();
            //};

            //that.Leave += delegate
            //{
            //    x.Hide();
            //};

           
            
        }

    }
}
