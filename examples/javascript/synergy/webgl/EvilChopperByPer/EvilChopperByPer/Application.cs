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
using EvilChopperByPer.Design;
using EvilChopperByPer.HTML.Pages;

namespace EvilChopperByPer
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // http://www.ludumdare.com/compo/ludum-dare-25/?action=preview&uid=6781
        // http://dl.dropbox.com/u/104012548/ld25/index.html

        public readonly ApplicationWebService service = new ApplicationWebService();

        // tell jsc that clientside wants to access this
        Levels ref0;

        // jsc, can tou see you are repeating yourself?:P
        SoundsSounds ref2;

        // what if jsc would allow to reference directly from AssetsLibrary?
        // currently we have to have the sprite in the same assembly
        EvilChopperByPer.Library.SoundManager2 ref1;

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // incompatible!
            //Uncaught TypeError: Object [object Object] has no method 'addSelf' level.js:575

            //THREE.Object3D ref0;

            //THREE.opensource.gihtub.three.js.build.three ref0;

            IFunction.Of("loadResources").apply(null);
            IFunction.Of("setup").apply(null);
            //            new IFunction(@"
            //     loadResources(); // Will start loading when sound manager 2 is ready
            //        $(document).ready(function() {
            //            setup();
            //        });
            //            ").apply(null);

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
