using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AndroidListApplications.Design;
using AndroidListApplications.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using System.Collections.Generic;

namespace AndroidListApplications
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            page.clear.WhenClicked(
                delegate
                {
                    //page.search.Clear();
                    page.search.value = "";
                }
            );

            new IHTMLButton { innerText = "Install" }.AttachToDocument().With(
                   btn =>
                   {
                       // http://help.adobe.com/en_US/air/build/WSfffb011ac560372f-5d0f4f25128cc9cd0cb-7ffd.html

                       btn.onclick +=
                           delegate
                           {
                               service.Install("assets/AndroidListApplications/foo.apk");
                           };
                   }
               );

            var items = new
            {
                div = default(IHTMLDiv),
                packageName = "",
                name = "",
                Remove = default(IHTMLButton),
                Launch = default(IHTMLButton)
            }.ToEmptyList();

            Action queryIntentActivities =
                delegate
                {
                    var a = new List<string>();

                    // Send data from JavaScript to the server tier
                    service.queryIntentActivities(
                        yield_done:
                            delegate
                            {
                                items.WithEach(
                                    item =>
                                    {
                                        if (a.Contains(item.packageName))
                                            return;

                                        item.div.style.color = "red";

                                        item.Launch.disabled = true;
                                        item.Remove.disabled = true;
                                    }
                                );

                                // remove others!
                            },
                        yield: (packageName, name) =>
                        {
                            a.Add(packageName);

                            // already have it
                            if (items.Any(k => k.packageName == packageName))
                                return;

                            var div = new IHTMLDiv();
                            div.style.margin = "1em";

                            if (Native.Document.body.firstChild == null)
                                div.AttachToDocument();
                            else
                                Native.Document.body.insertBefore(div, Native.Document.body.firstChild);

                            var Remove = new IHTMLButton { innerText = "Remove" }.AttachTo(div).WhenClicked(
                                    btn =>
                                    {
                                        // http://help.adobe.com/en_US/air/build/WSfffb011ac560372f-5d0f4f25128cc9cd0cb-7ffd.html


                                        if (!Native.Window.confirm("Remove " + name + "?"))
                                            return;

                                        service.Remove(packageName, name);
                                    }
                               );

                            var Launch = new IHTMLButton { innerText = "Launch" }.AttachTo(div).WhenClicked(
                                    btn =>
                                    {
                                        // http://help.adobe.com/en_US/air/build/WSfffb011ac560372f-5d0f4f25128cc9cd0cb-7ffd.html

                                        service.Launch(packageName, name);
                                    }
                                );

                            var item = new
                            {
                                div,
                                packageName,
                                name,
                                Remove,
                                Launch
                            };


                            items.Add(item);

                            //https://play.google.com/store/apps/details?id=com.abstractatech.battery

                            new IHTMLAnchor { href = "https://play.google.com/store/apps/details?id=" + packageName, innerText = name }.AttachTo(div);
                        }
                    );
                };

            queryIntentActivities();

            new Timer(
                delegate
                {
                    items.WithEach(
                        item =>
                        {
                            if (string.IsNullOrEmpty(page.search.value))
                            {
                                item.div.Show();
                            }
                            else
                            {
                                if (item.packageName.Contains(page.search.value))
                                {
                                    item.div.Show();
                                }
                                else
                                {
                                    item.div.Hide();
                                }
                            }

                        }
                    );

                    queryIntentActivities();
                }
            ).StartInterval(2000);


        }

    }
}
