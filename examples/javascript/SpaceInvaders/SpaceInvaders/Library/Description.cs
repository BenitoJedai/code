using System;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Drawing;
using SpaceInvaders.HTML.Images.FromAssets;



namespace SpaceInvaders.Library
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

            public static implicit operator Item(IHTMLImage URL)
            {
                return new Item { _Image = URL };
            }

            public Func<string> GetBaseURL;

            private IHTMLImage _Image;
            public IHTMLImage Image
            {
                get
                {
                    //if (_Image == null)
                    //{
                    //    if (GetBaseURL == null)
                    //        _Image = new IHTMLImage(URL);
                    //    else
                    //        _Image = new IHTMLImage(GetBaseURL() + URL);
                    //}

                    return _Image;
                }
            }

        }

        public readonly Item aenemy = new aenemy();
        public readonly Item benemy = new benemy();
        public readonly Item cenemy = new cenemy();
        public readonly Item biggun = new biggun();
        public readonly Item ufo = new ufo();

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