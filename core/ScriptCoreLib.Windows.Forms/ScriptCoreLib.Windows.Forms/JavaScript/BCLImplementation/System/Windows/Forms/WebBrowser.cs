﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Windows.Forms;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    [Script(Implements = typeof(global::System.Windows.Forms.WebBrowser))]
    internal class __WebBrowser : __WebBrowserBase
    {
        public Uri InternalUrl;

        public Uri Url
        {
            get
            {
                return InternalUrl;
            }

            set
            {

                Navigate(value.ToString());
            }
        }


        public string DocumentTitle
        {
            get
            {
                var x = "";
                try
                {
                    x = InternalElement.title;
                }
                catch
                { }

                return x;
            }
        }

        public void Navigate(string urlString)
        {
            var loc = Native.Document.location.href;

            // about:blank
            if (!urlString.Contains(":"))
            {
                if (urlString.StartsWith("/"))
                    if (loc.EndsWith("/"))
                    {
                        urlString = urlString.Substring(1);
                    }

                urlString = loc + urlString;
            }

            // onload: { Url = ://out:NaN/, src = about:blank, href = about:blank }
            InternalUrl = new Uri(urlString);
            this.InternalElement.src = urlString;
        }

        public bool InternalScrollBarsEnabled;
        public bool ScrollBarsEnabled
        {
            get
            {
                return InternalScrollBarsEnabled;
            }

            set
            {
                InternalScrollBarsEnabled = value;
                if (value)
                    this.InternalElement.scrolling = "yes";
                else
                    this.InternalElement.scrolling = "no";
            }
        }

        public IHTMLIFrame InternalElement;

        public override IHTMLElement HTMLTargetContainerRef
        {
            get
            {
                return InternalElement;

            }
        }

        public override IHTMLElement HTMLTargetRef
        {
            get
            {
                return InternalElement;
            }
        }

        public event WebBrowserNavigatingEventHandler Navigating;

        // or should this be document completed?
        public event WebBrowserNavigatedEventHandler Navigated;

        public __WebBrowser()
        {

            this.InternalElement = new IHTMLIFrame
            {
                frameBorder = "0",
                allowTransparency = true,
                allowFullScreen = true
            };

            this.InternalElement.onload +=
                delegate
                {
                    try
                    {
                        var href = this.InternalElement.contentWindow.document.location.href;

                        InternalUrl = new Uri(href);

                        // enable reloading
                        if (this.InternalElement.src != href)
                        {
                            // fixup, loaded from cashe?
                            this.InternalElement.src = href;
                        }
                        else
                        {
                            if (Navigated != null)
                                Navigated(
                                    this,
                                    (WebBrowserNavigatedEventArgs)(object)new __WebBrowserNavigatedEventArgs { Url = InternalUrl }
                                );


                            this.InternalElement.contentWindow.onunload +=
                                delegate
                                {
                                    if (Navigating != null)
                                        Navigating(
                                            this,
                                            (WebBrowserNavigatingEventArgs)(object)new __WebBrowserNavigatingEventArgs
                                            {
                                                //Url = InternalUrl 
                                            }
                                        );
                                };
                        }


                    }
                    catch
                    { }
                };

            // http://www.w3schools.com/html5/att_iframe_sandbox.asp
            //dynamic iframe = this.InternalElement;

            //iframe.sandbox = "allow-scripts allow-forms";

        }

        public string DocumentText
        {
            get
            {
                return "";
            }
            set
            {
                // Uncaught TypeError: Cannot read property 'document' of null 

                //this.InternalElement.onload +=
                //    delegate
                //    {
                //        this.InternalElement.contentWindow.document.body.innerHTML = value;
                //    };

                Native.Window.requestAnimationFrame +=
                    delegate
                    {

                        this.InternalElement.contentWindow.document.open("about:blank", "replace");
                        this.InternalElement.contentWindow.document.write(
                            value
                        );
                        this.InternalElement.contentWindow.document.close();

                    };

            }
        }
    }
}
