//using System.Linq;

using ScriptCoreLib;


using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM.XML;

using global::System.Collections.Generic;
using global::System.Linq;
using global::ScriptCoreLib.Shared.Lambda;




namespace ClickOnce.js
{
    using ScriptCoreLib.Shared;
    using ScriptCoreLib.Shared.Lambda;

    using ScriptCoreLib.Shared.Drawing;

    using System;

    [Script, Serializable]
    public sealed class Tip
    {
        public string Color;
        public string Content;
    }

    [Script, Serializable]
    public sealed class TipDocument
    {
        public Tip[] Tips;
        public string Topic;
        public string Location;
    }

    [Script, ScriptApplicationEntryPoint(IsClickOnce = true, ScriptedLoading=true)]
    public class TipRotator
    {
        public static readonly TipDocument DefaultData =
            new TipDocument
                {
                    Topic = "Useful tips",
                    Tips = new[]
                    {
                        new Tip { Color = "blue", Content = "Javascript can run anywhere!" },
                        new Tip { Color = "red", Content = "Use jsc to compile c# to javascript!" },
                        new Tip { Color = "black", Content = "ScriptApplication can be launched just by clicking a link! It's that good!" },
                    },
                    Location = ""
                };

        public readonly TipDocument Data;

        
        public TipRotator(TipDocument _Data)
        {
            this.Data = _Data;

            if (this.Data == null)
                this.Data = DefaultData;

            var div = new IHTMLDiv();

            var header = new IHTMLElement(IHTMLElement.HTMLElementEnum.h3, this.Data.Topic).AttachTo(div);

            var randomized = this.Data.Tips.Randomize();

            var current = randomized.First();

            var content = new IHTMLElement(IHTMLElement.HTMLElementEnum.p).AttachTo(div);

            var next = new IHTMLButton("Next Tip").AttachTo(div);

            var src = "assets/ClickOnce/Button_19.gif";

            
            var img = new IHTMLImage(this.Data.Location + src).AttachTo(next);

            img.style.verticalAlign = "middle";

            Action GoNext =
                delegate
                {
                    current = randomized.Next(i => i == current);

                    content.innerText = current.Content;
                    content.style.color = current.Color;
                };

            GoNext();

            next.onclick +=
                delegate
                {
                    GoNext();
                };

            div.AttachTo(Native.Document.body);
        }


        static TipRotator()
        {
            var KnownTypes = new object[] { 
                    new Tip(), 
                    new TipDocument() 
                };

            typeof(TipRotator).SpawnTo<TipDocument>(KnownTypes, i => new TipRotator(i));


        }


    }

}
