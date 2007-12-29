using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;


namespace YouTubeExample.js
{
    [Script, ScriptApplicationEntryPoint(IsClickOnce = true)]
    public class Class1
    {

        public Class1()
        {


            // http://annevankesteren.nl/test/flash/embed/embed-element.php

            // <object width="425" height="355"><param name="movie" value="http://www.youtube.com/v/kCgCSMpRN40&rel=1"></param><param name="wmode" value="transparent"></param><embed src="http://www.youtube.com/v/kCgCSMpRN40&rel=1" type="application/x-shockwave-flash" wmode="transparent" width="425" height="355"></embed></object>

            new IHTMLButton("video - string").Aggregate(
                btn =>
                {
                    btn.onclick +=
                        ev =>
                        {
                            new IHTMLDiv("<object width='425' height='355'><param name='movie' value='http://www.youtube.com/v/kCgCSMpRN40&rel=1'></param><param name='wmode' value='transparent'></param><embed src='http://www.youtube.com/v/kCgCSMpRN40&rel=1' type='application/x-shockwave-flash' wmode='transparent' width='425' height='355'></embed></object>").AttachToDocument();
                        };
                }

            ).AttachToDocument();

            var FlashMovie = "http://www.youtube.com/v/kCgCSMpRN40";
            var src = FlashMovie + "&rel=1";

            new IHTMLButton("video - dom").Aggregate(
                btn =>
                {
                    btn.onclick +=
                        ev =>
                        {
                            var _object = new IHTMLObject
                            {
                                width = 425,
                                height = 355
                            };


                            new IHTMLParam { name = "movie", value = src }.AttachTo(_object);
                            new IHTMLParam { name = "wmode", value = "transparent" }.AttachTo(_object);
                            new IHTMLEmbed
                            {

                            }.Aggregate(
                                embed =>
                                {
                                    embed.setAttribute("src", src);
                                    embed.setAttribute("type", "application/x-shockwave-flash");
                                    embed.setAttribute("wmode", "transparent");
                                    embed.setAttribute("width", "425");
                                    embed.setAttribute("height", "355");
                                }
                            ).AttachTo(_object);
                            _object.AttachToDocument();
                        };

                }
            ).AttachToDocument();

            new IHTMLButton("video - object").Aggregate(
                btn =>
                {
                    btn.onclick +=
                        ev =>
                        {
                            var _object = new IHTMLObject
                            {
                                width = 425,
                                height = 355
                            };


                            new IHTMLParam { name = "movie", value = src }.AttachTo(_object);
                            new IHTMLParam { name = "wmode", value = "transparent" }.AttachTo(_object);


                            _object.AttachToDocument();
                        };
                }
            ).AttachToDocument();

            new IHTMLButton("video - embed").Aggregate(
                btn =>
                {
                    btn.onclick +=
                        ev =>
                    new IHTMLEmbed
                    {

                    }.Aggregate(
                        embed =>
                        {
                            embed.setAttribute("src", src);
                            embed.setAttribute("type", "application/x-shockwave-flash");
                            embed.setAttribute("wmode", "transparent");
                            embed.setAttribute("width", "425");
                            embed.setAttribute("height", "355");
                        }
                    ).AttachToDocument();
                }
            ).AttachToDocument();


        }

        static Class1()
        {
            typeof(Class1).SpawnTo(i => new Class1());
        }


    }

}
