using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using System;
using System.Linq;



namespace SpaceInvaders.source.js.gfx
{
    [Script]
    public class ImageResources
    {
        [Script]
        public class Item
        {
            public string URL;

            public IHTMLImage Clone()
            {
                return (IHTMLImage)Image.cloneNode(false);
            }

            public static implicit operator Item(string URL)
            {
                return new Item { URL = URL };
            }

            public Func<string> GetBaseURL;

            private IHTMLImage _Image;
            public IHTMLImage Image
            {
                get
                {
                    if (_Image == null)
                    {
                        if (GetBaseURL == null)
                            _Image = new IHTMLImage(URL);
                        else
                            _Image = new IHTMLImage(GetBaseURL() + URL);
                    }

                    return _Image;
                }
            }

        }

        public readonly Item aenemy = "assets/SpaceInvaders/aenemy.gif";
        public readonly Item benemy = "assets/SpaceInvaders/benemy.gif";
        public readonly Item cenemy = "assets/SpaceInvaders/cenemy.gif";
        public readonly Item biggun = "assets/SpaceInvaders/biggun.gif";
        public readonly Item ufo = "assets/SpaceInvaders/ufo.gif";

        public Item[] Items
        {
            get
            {
                return new [] {
                    this.aenemy,
                    this.benemy,
                    this.cenemy,
                    this.biggun,
                    this.ufo,
                };
            }
        }

        public bool IsComplete
        {
            get 
            {
                return Items.Where(i => !i.Image.complete).Count() == 0;
            }
        }
      

            public ImageResources()
            {
                foreach (Item v in Items)
                {
                    v.GetBaseURL = () => BaseURL;
                }
            }

            public string BaseURL = "";

            public static implicit operator ImageResources(string BaseURL)
            {
                return new ImageResources { BaseURL = BaseURL };
            }

        }
    }