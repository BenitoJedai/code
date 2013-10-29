using AndroidFlashTreasureHunt.Design;
using AndroidFlashTreasureHunt.HTML.Pages;
using AndroidFlashTreasureHunt.IsometricBuilder.HTML.Audio.FromAssets;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using com.abstractatech.gamification.fth;


namespace AndroidFlashTreasureHunt
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            FormStyler.AtFormCreated =
             s =>
             {
                 s.Context.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                 //var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDrag().AttachTo(s.Context.GetHTMLTarget());
                 var x = new ChromeTCPServerWithFrameNone.HTML.Pages.AppWindowDragWithShadow().AttachTo(s.Context.GetHTMLTarget());
             };


            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "AvalonUgh";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;



                ChromeTCPServer.TheServerWithStyledForm.Invoke(AppSource.Text,
                    DefaultWidth: ApplicationSprite.DefaultWidth,
                    DefaultHeight: ApplicationSprite.DefaultHeight,
                    AtFormCreated: FormStyler.AtFormCreated

                );

                return;
            }
            #endregion


            //new gong().AttachToDocument().play();
            new ThreeDStuff.js.Tycoon4();

            //AndroidFlashTreasureHunt.IsometricView.ApplicationContent.InitializeContent(
            //    delegate
            //    {
            InitializeContent();
            //    }
            //);



        }

        private void InitializeContent()
        {
            Action<string> sprite_keydown = delegate { };
            Action<string> sprite_keyup = delegate { };

            // Initialize ApplicationSprite
            var ee = sprite.AttachSpriteToDocument();

            ee.style.width = "1px";
            ee.style.height = "1px;";
            ee.style.position = IStyle.PositionEnum.absolute;
            ee.style.right = "1em";
            ee.style.top = "1em";

            ee.style.With(
                 (dynamic s) => s.webkitTransition = "all 0.3s linear"
             );


            sprite.WhenReady(
                delegate
                {
                    ee.style.SetSize(
                        FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlWidth,
                        FlashTreasureHunt.ActionScript.FlashTreasureHunt.DefaultControlHeight
                    );
                }
            );


        }

    }
}
