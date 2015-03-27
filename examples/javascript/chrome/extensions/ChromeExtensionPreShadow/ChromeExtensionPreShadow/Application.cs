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
using ChromeExtensionPreShadow;
using ChromeExtensionPreShadow.Design;
using ChromeExtensionPreShadow.HTML.Pages;
using chrome;
using System.Net;

namespace ChromeExtensionPreShadow
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
            // can we inject ourselves into a chrome tab
            // before the page loads?

            // http://stackoverflow.com/questions/19191679/chrome-extension-inject-js-before-page-load
            // If you want to dynamically run a script as soon as possible, then call chrome.tabs.executeScript when the chrome.webNavigation.onCommitted event is triggered.

            // when does that happen?

            // if we are able to preload, would we be able to act as adblock?


            // as per X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionShadowExperiment\ChromeExtensionShadowExperiment\Application.cs



            // first lets get this test running in chrome


            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_tabs = self_chrome.tabs;

            if (self_chrome_tabs != null)
            {
                #region Installed

                chrome.runtime.Installed += delegate
                {
                    // our API does not have a Show
                    new chrome.Notification
                    {
                        Message = "ChromeExtensionPreShadow Installed!"
                    };
                };
                #endregion

                // what about
                // Error code: ERR_INTERNET_DISCONNECTED

                var once = new { tabId = default(TabIdInteger), url = default(string) }.ToEmptyList();


                new { }.With(
                    async delegate
                {
                    // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionWithWorker\ChromeExtensionWithWorker\Application.cs

                    var code = await new WebClient().DownloadStringTaskAsync(
                       new Uri(Worker.ScriptApplicationSource, UriKind.Relative)
                  );

                    new chrome.Notification
                    {
                        Message = "code " + new { code.Length }
                    };

                    chrome.webNavigation.Committed +=
                          async z =>
                        {
                            // 0:5212ms at Delay {{ _title = , _message = webNavigation! Committed {{ url = http://example.com/, tabId = 99, transitionType = typed, transitionQualifiers = from_address_bar }} }} 

                            if (z.url.StartsWith("https://www.google.ee/_/chrome/newtab?"))
                            {
                                //Unchecked runtime.lastError while running tabs.executeScript: Cannot access a chrome:// URL
                                return;
                            }

                            //                            0:487298ms at Delay {
                            //                                {
                            //                                    _title = , _message = webNavigation!Committed {
                            //                                        {
                            //                                            url = chrome://chrome/extensions/, tabId = 158, transitionType = auto_bookmark, transitionQualifiers =  }} }} view-source:41478
                            //Unchecked runtime.lastError while running tabs.executeScript: Cannot access a chrome:// URL

                            if (z.transitionType == "auto_subframe")
                            {
                                // this seems to be an ad?
                                // https://developer.chrome.com/extensions/history

                                return;
                            }

                            if (z.url.StartsWith("chrome-devtools://"))
                                return;

                            if (z.url.StartsWith("chrome://"))
                                return;


                            // now would be nice to check if this tab was already injected.
                            once.Add(new { z.tabId, z.url });


                            var n = new chrome.Notification
                            {
                                Message = "webNavigation! Committed " + new { z.url, z.tabId, z.transitionType, z.transitionQualifiers }
                            };

                            //                        0:3388ms at Delay {
                            //                            {
                            //                                _title = , _message = webNavigation!Committed {
                            //                                    {
                            //                                        url = https://www.google.ee/_/chrome/newtab?espv=2&ie=UTF-8, tabId = 125, transitionType = typed, transitionQualifiers =  }} }} view-source:41478
                            //Unchecked runtime.lastError while running tabs.executeScript: Cannot access a chrome:// URL


                            // https://developer.chrome.com/extensions/tabs#method-executeScript
                            //z.tabId.executeScript(
                            //    new
                            //    {
                            //        code = "document.body.style.borderLeft='1em solid yellow';",
                            //        runAt = "document_start"
                            //    }
                            //);

                            // we should now be able to take command of that web page.
                            // yet. we might not want to change its DOM yet?
                            // maybe wait for context menu, keyboard or action icon click?


                            // Content scripts execute in a special environment called an isolated world. 
                            // They have access to the DOM of the page they are injected into, but not to any JavaScript variables or 
                            // functions created by the page. It looks to each content script as if there is no other JavaScript executing
                            // on the page it is running on. The same is true in reverse: JavaScript running on the page cannot call any 
                            // functions or access any variables defined by content scripts.

                            var result = await z.tabId.executeScript(
                            //new { file = url }
                            new
                            {
                                code,

                                runAt = "document_start"
                            }
                            );
                        };
                }
                );



                //chrome.tabs.Created +=
                //     (z) =>
                //    {
                //        var n = new Notification
                //        {
                //            Message = "Created! " + new { z.id }
                //        };
                //    };

                //chrome.tabs.Activated +=
                //     (z) =>
                //    {
                //        var n = new Notification
                //        {
                //            Message = "Activated! " + new { z }
                //        };

                //    };


                return;
            }


            // inside executeScript

            Native.document.documentElement.style.borderTop = "1em solid yellow";
            //Native.body.style.borderTop = "1em solid yellow";
            Console.WriteLine("injected!");

            // save view-source to B:
            // reload extension

            // 0:6311ms injected!
            // lets test agiainst file://

            // it works.
            // either do the workers now or lets test register element?

            // Uncaught NotSupportedError: Failed to execute 'registerElement' on 'Document': Registration failed for type 'x-foo'. Elements cannot be registered from extensions.
            //Native.document.registerElement("x-foo",
            //    (IHTMLElement e) =>
            //    {
            //        e.shadow.appendChild("x-foo element provided by ChromeExtensionPreShadow");
            //    }
            //);



            // !! Elements cannot be registered from extensions.
            // X:\jsc.svn\examples\javascript\chrome\extensions\ChromeExtensionPreShadow\ChromeExtensionPreShadow\Application.cs
            // https://code.google.com/p/chromium/issues/detail?id=390807

            Native.document.querySelectorAll("x-foo").WithEach(
                e =>
                {
                    // what about elements added later?

                    e.shadow.appendChild("x-foo element provided by ChromeExtensionPreShadow without registerElement");

                    //MutationCallback

                }
            );

            // ILMutationObserver
            new MutationObserver(
                new MutationCallback(
                    (MutationRecord[] mutations, MutationObserver observer) =>
                            {
                                // MutationCallback: {{ Length = 3 }}
                                // MutationCallback: {{ type = childList }}
                                // MutationCallback: {{ type = characterData }}

                                mutations.WithEach(
                                    m =>
                                    {
                                        // not a good idea. recursive
                                        //new IHTMLPre { "MutationCallback: " + new { m.type } }.AttachToDocument();

                                        if (m.type == "childList")
                                        {
                                            m.addedNodes.WithEach(
                                                addedNode =>
                                                {
                                                    //new IHTMLPre { "MutationCallback: " + new { addedNode } }.AttachToDocument();

                                                    //MutationCallback: { { addedNode = [object HTMLElement] } }
                                                    if (addedNode.nodeType == INode.NodeTypeEnum.ElementNode)
                                                    {
                                                        var addedElement = (IHTMLElement)addedNode;

                                                        //MutationCallback: addedElement { { localName = x - foo } }
                                                        //new IHTMLPre { "MutationCallback: addedElement " + new { addedElement.localName } }.AttachToDocument();

                                                        if (addedElement.localName == "x-foo")
                                                        {
                                                            addedElement.shadow.appendChild("x-foo element provided by ChromeExtensionPreShadow without registerElement");
                                                        }
                                                    }
                                                }
                                            );

                                        }
                                    }
                                );


                            }
                )
            ).observe(Native.document.documentElement,
                new
            {
                // Set to true if mutations to target's children are to be observed.
                childList = true,
                // Set to true if mutations to target's attributes are to be observed. Can be omitted if attributeOldValue and/or attributeFilter is specified.
                //attributes = true,
                // Set to true if mutations to target's data are to be observed. Can be omitted if characterDataOldValue is specified.
                //characterData = true,
                // Set to true if mutations to not just target, but also target's descendants are to be observed.
                subtree = true,
            }
            );



        }

    }
}
