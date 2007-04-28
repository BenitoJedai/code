using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

[assembly:
ScriptResources("gfx")
]

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

        public readonly Item aenemy = "gfx/aenemy.gif";
        public readonly Item benemy = "gfx/benemy.gif";
        public readonly Item cenemy = "gfx/cenemy.gif";
        public readonly Item biggun = "gfx/biggun.gif";
        public readonly Item ufo = "gfx/ufo.gif";

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
                return 
                    Sequence.Count(
                        Sequence.Where(Items, i => !i.Image.complete)
                    ) == 0;
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