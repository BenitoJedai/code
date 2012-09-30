using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;

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
                InternalUrl = value;

                this.InternalElement.src = value.ToString();
            }
        }

        public void Navigate(string urlString)
        {
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

        public __WebBrowser()
        {
            this.InternalElement = new IHTMLIFrame
            {
                //frameBorder = "0"
            };

        }
    }
}
