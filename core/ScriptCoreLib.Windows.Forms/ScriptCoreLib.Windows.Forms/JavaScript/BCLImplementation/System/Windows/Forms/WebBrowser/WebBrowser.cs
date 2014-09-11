using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System.Windows.Forms;
using ScriptCoreLib.Shared.BCLImplementation.System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    // http://referencesource.microsoft.com/#System.Windows.Forms/ndp/fx/src/winforms/Managed/System/WinForms/WebBrowser.cs
    // https://github.com/mono/mono/blob/master/mcs/class/Managed.Windows.Forms/System.Windows.Forms/WebBrowser.cs
    // https://github.com/mono/mono/blob/master/mcs/class/Mono.WebBrowser/Mono.Mozilla/WebBrowser.cs


    // tested by X:\jsc.svn\examples\javascript\chrome\ChromeFormsWebBrowserExperiment\ChromeFormsWebBrowserExperiment\Application.cs
    [Script(Implements = typeof(global::System.Windows.Forms.WebBrowser))]
    public class __WebBrowser : __WebBrowserBase
    {
        // X:\jsc.svn\examples\javascript\Test\TestShadowIFrame\TestShadowIFrame\Application.cs
        // X:\jsc.svn\examples\javascript\chrome\apps\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs

        public bool ScriptErrorsSuppressed { get; set; }

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

        public override void Refresh()
        {
            Navigate(InternalUrl.ToString());
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

            // about:blank
            if (!urlString.Contains(":"))
            {
                /// baseURI
                var loc = Native.document.location.href;
                if (urlString.StartsWith("/"))
                    if (loc.EndsWith("/"))
                    {
                        urlString = urlString.Substring(1);
                    }

                urlString = loc + urlString;
            }

            // onload: { Url = ://out:NaN/, src = about:blank, href = about:blank }
            this.InternalUrl = new Uri(urlString);

            Console.WriteLine("__WebBrowser.Navigate " + new { InternalUrl });

            this.InternalElement.src = urlString;
        }


        #region ScrollBarsEnabled
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
        #endregion

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


        // which document are we talking about?
        public static Action<__WebBrowser> InitializeInternalElement =
            that =>
            {
                // tested by?
                // X:\jsc.svn\examples\javascript\chrome\ChromeTCPServerWithFrameNone\ChromeTCPServerWithFrameNone\Application.cs
                // X:\jsc.svn\examples\javascript\chrome\ChromeFormsWebBrowserExperiment\ChromeFormsWebBrowserExperiment\Application.cs

                // Refused to frame 'http://example.com/' because it violates the following Content Security Policy directive: "frame-src 'self' data: chrome-extension-resource:".

                that.InternalElement = new IHTMLIFrame
                {
                    frameBorder = "0",
                    allowTransparency = true,
                    allowFullScreen = true
                };
            };

        public __WebBrowser()
        {
            // we should not create any <webview> elements before we know which document will be using this WebBrowser.


            InitializeInternalElement(this);

            this.InternalElement.onload +=
                delegate
                {
                    try
                    {
                        // DOM Exception?
                        var onload_href = this.InternalElement.contentWindow.document.location.href;

                        // 1:93559ms __WebBrowser { href = about:blank } 
                        // 2:27059ms __WebBrowser { onload_href = http://192.168.43.252:5645/, src =  }

                        if (string.IsNullOrEmpty(this.InternalElement.src))
                        {
                            // set by DocumentText ?
                        }
                        else
                        {
                            Console.WriteLine("__WebBrowser " + new { onload_href, this.InternalElement.src });

                            InternalUrl = new Uri(onload_href);

                            // enable reloading
                            if (this.InternalElement.src != onload_href)
                            {
                                // X:\jsc.svn\examples\javascript\io\ApplicationSnapshotStorage\ApplicationSnapshotStorage\Application.cs

                                // fixup, loaded from cashe?
                                // when do we need to do this?

                                this.InternalElement.src = onload_href;
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

                this.HTMLTargetRef.requestAnimationFrame +=
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
