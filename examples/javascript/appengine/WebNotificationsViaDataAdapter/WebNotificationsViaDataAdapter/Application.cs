using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebNotificationsViaDataAdapter;
using WebNotificationsViaDataAdapter.Design;
using WebNotificationsViaDataAdapter.HTML.Pages;
using WebNotificationsViaDataAdapter.Schema;

namespace WebNotificationsViaDataAdapter
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
            // http://www.paulund.co.uk/playground/demo/html5-notification-api/

            #region FooTableDesigner
            var zpage = new InlinePageActionButtonExperiment();
            var zedit = page.EditElement;


            page.EditElement.parentNode.replaceChild(
                zpage.Container,
                page.EditElement
            );

            //page.EditElement.ReplaceWith(zpage);

            zpage.edit = zedit;


            var content = default(FooTableDesigner);
            Action onclick =
           async delegate
           {

               if (content == null)
               {
                   content = new FooTableDesigner();


                   zpage.editcontext.style.SetSize(
                       content.Width,
                       content.Height
                   );

                   content.AttachControlTo(zpage.editcontent);

                   // blend with control
                   var bc = content.BackColor;
                   zpage.edit.style.backgroundColor = bc.ToString();


                   var data = await this.__FooTable_Select();

                   await Native.window.requestAnimationFrameAsync;


                   content.dataGridView1.DataSource = data;


                   return;
               }

               content.ParentForm.Close();
               content = null;

               // blend with DOM
               zpage.edit.style.backgroundColor = JSColor.Transparent;
           };

            zpage.edit.onclick += e => { onclick(); e.preventDefault(); };
            zpage.edit.oncontextmenu += e => { onclick(); e.preventDefault(); };



            zpage.editcontent.Clear();
            zpage.editcontent.style.border = "none";
            #endregion





            new Stopwatch().With(
                async s =>
                {
                    s.Start();

                    // http://www.soundjay.com/phone-sounds-1.html

                    Action flash = async delegate
                    {
                        page.YellowNotificationElement.style.backgroundColor = "yellow";
                        new HTML.Audio.FromAssets.cell_phone_vibrate_1().play();

                        var v = new Stopwatch();
                        v.Start();

                        while (v.ElapsedMilliseconds < 1500)
                        {

                            page.YellowNotificationElement.style.marginLeft = ((v.ElapsedMilliseconds % 3) - 1) + "px";

                            await Native.window.requestAnimationFrameAsync;
                        }

                        page.YellowNotificationElement.style.backgroundColor = "";

                    };

                    page.YellowNotificationElement.style.backgroundColor = "";

                    page.YellowNotificationElement.onclick += delegate { flash(); };

                    var delayfrom = s.ElapsedMilliseconds;

                    while (s.IsRunning)
                    {
                        Native.document.title = s.ToString();

                        var delayto = s.ElapsedMilliseconds;
                        var n = await this[delayfrom, delayto];
                        delayfrom = delayto;

                        foreach (var item in n)
                        {
                            flash();


                            new IHTMLDiv { innerText = new { item }.ToString() }.AttachToDocument();
                        }



                        await Task.Delay(500);


                    }
                }
            );
        }

    }
}
