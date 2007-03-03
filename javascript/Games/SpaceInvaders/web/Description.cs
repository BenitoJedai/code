using ScriptCoreLib;
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
        public readonly IHTMLImage aenemy = "gfx/aenemy.gif";
        public readonly IHTMLImage benemy = "gfx/benemy.gif";
        public readonly IHTMLImage cenemy = "gfx/cenemy.gif";
        public readonly IHTMLImage biggun = "gfx/biggun.gif";
        public readonly IHTMLImage ufo = "gfx/ufo.gif";

        public IHTMLImage[] Images
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

        static ImageResources _Default;

        public static ImageResources Default
        {
            get
            {
                if (_Default == null)
                    _Default = new ImageResources();

                return _Default;
            }
        }
    }
}