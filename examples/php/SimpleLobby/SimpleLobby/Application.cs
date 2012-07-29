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
using SimpleLobby.Design;
using SimpleLobby.HTML.Pages;
using System.Collections.Generic;

namespace SimpleLobby
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* log:
         * 01. save project
         * 02. compile project for assets
         * 03. commit to svn.
         * 04. add a button at random location
         * 05. run to see the button
         * 06. add a button to clear lobby
         * 07. test with multiple devices
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {

            var r = new Random();
            var id = "" + r.Next();
            var x = r.Next(400);
            var y = r.Next(400);
            var btn = new IHTMLButton { innerText = "this is id " + id + " at " + x + ", " + y };

            btn.AttachToDocument();
            btn.style.SetLocation(x, y);

            btn.onclick +=
                delegate
                {
                    btn.disabled = true;
                    service.UpdateLobby(id, x + "", y + "",
                        delegate
                        {
                            btn.disabled = false;

                        }
                    );
                };

            page.ClearLobby.onclick +=
                delegate
                {
                    page.ClearLobby.disabled = true;
                    service.ClearLobby(
                        delegate
                        {
                            page.ClearLobby.disabled = false;

                        }
                    );
                };


            var a = new List<IHTMLButton>();

            page.RefreshLobby.onclick +=
                delegate
                {
                    a.WithEach(
                        k =>
                        {
                            k.Orphanize();
                        }
                    );
                    a.Clear();

                    page.RefreshLobby.disabled = true;
                    service.EnumerateLobby(
                        (_id, _x, _y) =>
                        {
                            if (_id == "")
                            {
                                page.RefreshLobby.disabled = false;
                                return;
                            }

                            if (_id == id)
                                return;

                            var shadow = new IHTMLButton
                            {
                                innerText = "id " + _id + " at " + _x + ", " + _y
                            };

                            shadow.AttachToDocument();
                            shadow.style.SetLocation(Convert.ToInt32(_x), Convert.ToInt32(_y));

                            a.Add(shadow);
                        }
                    );

                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
