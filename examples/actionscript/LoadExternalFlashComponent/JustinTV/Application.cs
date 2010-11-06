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
using JustinTV.Components;
using JustinTV.HTML.Pages;

namespace JustinTV
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // Initialize MySprite1
            var s = new MySprite1();

            s.AttachSpriteTo(page.Content);


            // Send data from JavaScript to the server tier
            service.WebMethod2(
                "",
                list =>
                {
                    var ChannelContainer = page.Content;

                    Action<string> ChannelSelected =
                        channel =>
                        {
                            page.Header.innerHTML = "JustinTV - " + channel;
                            s.api_play_live(channel);
                        };

                    Foo.ListElements(list, ChannelContainer, ChannelSelected);
                }
            );
        }

     

    }

    public class Foo
    {
        public static void ListElements(XElement list, IHTMLElement ChannelContainer, Action<string> ChannelSelected)
        {
            list.Elements("stream").WithEach(
                stream =>
                {
                    var channel = stream.Element("channel");
                    var channel_url = channel.Element("channel_url").Value;
                    var embed_enabled = channel.Element("embed_enabled").Value == "true";

                    if (!embed_enabled)
                        return;

                    var screen_cap_url = channel.Element("screen_cap_url_medium").Value;

                    var name = channel_url.SkipUntilLastOrEmpty("/");

                    new IHTMLImage(screen_cap_url)
                    {
                        title = name,
                    }.AttachTo(
                        new IHTMLAnchor
                        {
                            href = channel_url
                        }.AttachTo(ChannelContainer).With(
                            a =>
                            {
                                a.onclick +=
                                    e =>
                                    {
                                        e.PreventDefault();

                                        ChannelSelected(name);
                                    };
                            }

                        )
                    );
                }
            );
        }
    }

}
