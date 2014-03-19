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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NfcUnlockPrototype;
using NfcUnlockPrototype.Design;
using NfcUnlockPrototype.HTML.Pages;
using System.Threading;

namespace NfcUnlockPrototype
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
            var fName = new IHTMLInput().AttachToDocument();
            var lName = new IHTMLInput().AttachToDocument();
            var submit = new IHTMLButton { innerHTML = "Submit"}.AttachToDocument();
            var a = new IHTMLAnchor().AttachToDocument();
            a.style.visibility = IStyle.VisibilityEnum.visible;


            Action<string> startSession = async user => 
            {
                while (true)
                {
                    var res = await IsNfcApproved(user);
                    if (res != null)
                    {
                        if (!res.IsCard)
                        {
                            lName.style.visibility = IStyle.VisibilityEnum.visible;
                            fName.style.visibility = IStyle.VisibilityEnum.visible;
                            submit.style.visibility = IStyle.VisibilityEnum.visible;
                            a.style.visibility = IStyle.VisibilityEnum.hidden;

                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Result is NULL");
                    }
                    Thread.Sleep(1000);
                }
            };

            submit.onclick += async delegate
            {

                var user = fName.value + lName.value;
                Console.WriteLine(user);

                //await InsertUserAuth(user,false);


                var res = await IsNfcApproved(user);
                if (res != null)
                {
                    if (res.IsCard)
                    {
                        lName.style.visibility = IStyle.VisibilityEnum.hidden;
                        fName.style.visibility = IStyle.VisibilityEnum.hidden;
                        submit.style.visibility = IStyle.VisibilityEnum.hidden;
                        a.innerHTML = "Welcome! " + fName + " " + lName;
                        a.style.visibility = IStyle.VisibilityEnum.visible;
                        startSession(user);
                    }
                }
                else
                {
                    Console.WriteLine("Result is NULL");
                }
            };


        }

    }
}
