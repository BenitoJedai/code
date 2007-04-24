using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using global::System.Collections.Generic;

namespace SimpleFilmstrip.js
{
    [Script]
    public class Class1
    {
        public const string Alias = "Class1";
        public const string DefaultData = "Class1Data";

        /// <summary>
        /// Creates a new control
        /// </summary>
        /// <param name="DataElement">The hidden data element</param>
        public Class1(IHTMLElement DataElement)
        {
            IHTMLDiv Control = new IHTMLDiv();

            Control.style.position = IStyle.PositionEnum.absolute;

            Control.style.background = "url(assets/filmstrip.png) no-repeat";
            Control.style.height = "600px";
            Control.style.width = "326px";

            var index = 0;

            var t_icount = default(int);
            var t_interval = default(int);
            var t_iwidth = default(int);
            var t_iheight = default(int);
            var t_feed = default(string);

            var Restart = default(Action);

            var feed = new IHTMLInput(HTMLInputTypeEnum.text, "assets/veh_cy.png");
            var iwidth = new IHTMLInput(HTMLInputTypeEnum.text, "48");
            var iheight = new IHTMLInput(HTMLInputTypeEnum.text, "48");
            var icount = new IHTMLInput(HTMLInputTypeEnum.text, "32");
            var interval = new IHTMLInput(HTMLInputTypeEnum.text, "50");
            var fps = new IHTMLInput(HTMLInputTypeEnum.text, "24");


            feed.onchange += delegate { Restart(); };
            iwidth.onchange += delegate { Restart(); };
            iheight.onchange += delegate { Restart(); };

            interval.onchange += delegate
            {
                int v = int.Parse(interval.value);

                if (v == 0)
                    return;

                fps.value = "" + (1000 / v);


                Restart();
            };

            icount.onchange += delegate { Restart(); };

            fps.onchange += delegate
            {
                int v = int.Parse(fps.value);

                if (v == 0)
                    return;

                interval.value = "" + (1000 / v);

                Restart();
            };


            var fieldset = new IHTMLElement(IHTMLElement.HTMLElementEnum.fieldset);

            fieldset.appendChild(new IHTMLElement(IHTMLElement.HTMLElementEnum.legend, "Properties"));

            Func<string, IHTMLElement, IHTMLDiv> AsLabel =
                (string text, IHTMLElement control) =>
                {
                    var label = new IHTMLLabel(text, control);

                    control.style.position = IStyle.PositionEnum.absolute;
                    control.style.left = "8em";

                    return new IHTMLDiv(label, control);
                };

            fieldset.appendChild(AsLabel("feed:", feed));
            fieldset.appendChild(AsLabel("width:", iwidth));
            fieldset.appendChild(AsLabel("height:", iheight));
            fieldset.appendChild(AsLabel("count:", icount));
            fieldset.appendChild(AsLabel("interval:", interval));
            fieldset.appendChild(AsLabel("fps:", fps));


            fieldset.style.position = IStyle.PositionEnum.absolute;
            fieldset.style.top = "320px";

            var image = new IHTMLDiv();




            image.style.position = IStyle.PositionEnum.absolute;
            image.style.top = "52px";
            image.style.left = "32px";


            var t = new Timer();

            t.Tick += delegate
            {

                image.style.backgroundPosition = "-" + (index * t_iwidth) + "px 0px";


                index = (index + 1) % t_icount;
            };

            Restart =
                delegate
                {
                    t_icount = int.Parse(icount.value);
                    t_interval = int.Parse(interval.value);
                    t_iwidth = int.Parse(iwidth.value);
                    t_iheight = int.Parse(iheight.value);
                    t_feed = feed.value;

                    image.style.background = "url(" + t_feed + ") no-repeat";

                    image.style.width = t_iwidth + "px";
                    image.style.height = t_iheight + "px";

                    t.StartInterval(t_interval);
                };

            Restart();

            Control.appendChild(image, fieldset);


            DataElement.insertNextSibling(
                Control

            );
        }

        static Class1()
        {
            // spawn this class when document is loaded 
            Native.Spawn(
                new Pair<string, EventHandler<IHTMLElement>>(Alias, e => new Class1(e))
                );
        }
    }
}
