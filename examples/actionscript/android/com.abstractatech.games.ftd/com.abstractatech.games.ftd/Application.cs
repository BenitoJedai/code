using com.abstractatech.games.ftd.Design;
using com.abstractatech.games.ftd.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace com.abstractatech.games.ftd
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
        public Application(IDefault page)
        {
            // Initialize ApplicationSprite
            sprite.ToTransparentSprite();

            var c = new IHTMLDiv().AttachToDocument();

            c.style.position = IStyle.PositionEnum.absolute;
            c.style.right = "0";
            c.style.top = "0";

            sprite.AttachSpriteTo(c);

            new IHTMLButton { innerText = "Green" }.AttachToDocument().onclick +=
                                      delegate
                                      {
                                          Native.Document.body.style.backgroundColor = JSColor.Green;
                                      };

            new IHTMLButton { innerText = "White" }.AttachToDocument().onclick +=
                                     delegate
                                     {
                                         Native.Document.body.style.backgroundColor = JSColor.White;
                                     };
            new IHTMLButton { innerText = "Yellow" }.AttachToDocument().onclick +=
                                delegate
                                {
                                    Native.Document.body.style.backgroundColor = JSColor.Yellow;
                                };
            Native.Document.body.style.backgroundColor = JSColor.Yellow;

            new IHTMLDiv { innerText = "loading flash... or use another device with " + Native.Document.location }.AttachToDocument();

            // some systems may never be ready as they do not support flash!
            sprite.WhenReady(
                delegate
                {
                    #region Connect To Session
                    new IHTMLButton { innerText = "Connect To Session" }.AttachToDocument().With(
                        btn =>
                        {
                            btn.onclick += delegate
                            {
                                sprite.ConnectToSession();

                                btn.Orphanize();



                            };

                        }
                    );
                    #endregion


                    #region Create Game
                    new IHTMLButton { innerText = "Create Game" }.AttachToDocument().With(
                          b2 =>
                          {
                              b2.onclick +=
                                  delegate
                                  {
                                      sprite.CreateGame();

                                      b2.Orphanize();
                                  };
                          }
                      );
                    #endregion

                    sprite.AtMessageFromFlash +=
                        message =>
                        {
                            new IHTMLDiv { innerText = message }.AttachToDocument();
                        };

                    sprite.AtConnect +=
                        delegate
                        {
                            new IHTMLDiv { innerText = "Send message: " }.AttachToDocument();

                            new IHTMLInput().AttachToDocument().With(
                                textarea =>
                                {
                                    new IHTMLButton { innerText = "Send" }.AttachToDocument().onclick +=
                                        delegate
                                        {
                                            sprite.RaiseMessageToFlashNetGroup(textarea.value);
                                            textarea.value = "";
                                        };
                                }
                            );

                            new IHTMLBreak().AttachToDocument();

                            new IHTMLDiv { innerText = "Change name: " }.AttachToDocument();

                            new IHTMLInput().AttachToDocument().With(
                                MyName =>
                                {
                                    new IHTMLButton { innerText = "Change" }.AttachToDocument().onclick +=
                                     delegate
                                     {
                                         sprite.SetMyName(MyName.value);
                                     };
                                }
                            );

                            new IHTMLDiv { innerText = "finding neighbors may take 7 seconds..." }.AttachToDocument();
                            new IHTMLDiv { innerText = "then chat, start game, set name, move around" }.AttachToDocument();

                        };
                }
            );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
