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
using JellyworldExperiment.Design;
using JellyworldExperiment.HTML.Pages;
using ScriptCoreLib.ActionScript.flash.display;
using JellyworldExperiment.HardwareDetection;
using ScriptCoreLib.JavaScript.Runtime;

namespace JellyworldExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        /* 
         * for http://blog.sc5.fi/2012/11/announcing-finhtml5-a-day-jam-packed-with-inspiration-for-the-future/#comment-142
         * Lets create a new demo.
         * 01. First let's tell use the screen and window size.
         * 02. If the client is flash capable tell that we have a cam (FoundCamera)
         * 03. If the client is orientation capable tell that (ondeviceorientation)
         * 04. Commit to svn
         * 05. Wait anwsers from JellyworldExperiment.HardwareDetection
         * 06. Add iframe to get messages
         */

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            #region /HardwareDetection

            new IHTMLPre
            {
                innerText = "/HardwareDetection... "

            }.AttachToDocument();

            new IHTMLIFrame { src = "/HardwareDetection" }.With(
                HardwareDetection =>
                {
                    //HardwareDetection.style.visibility = IStyle.VisibilityEnum.hidden;

                    // android browser does not allow this:
                    HardwareDetection.style.display = IStyle.DisplayEnum.none;

                    Native.Window.onmessage +=
                      e =>
                      {
                          // stop listening
                          if (HardwareDetection == null)
                              return;

                          // am i talking to you?
                          if (e.source != HardwareDetection.contentWindow)
                              return;

                          new IHTMLPre
                          {
                              innerText = "/HardwareDetection onmessage: " + new { e.data }.ToString()
                          }.AttachToDocument();

                          //// thanks for info. you are done!
                          //HardwareDetection.Orphanize();

                          //// stop listening
                          //HardwareDetection = null;
                      };
                }
            ).AttachToDocument();
            #endregion


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }



    [Obsolete("Temporary workaround to enable multiple apps.")]
    public sealed class Application_HardwareDetection
    {
        #region Application_HardwareDetection_Sprite
        [Obsolete("Temporary workaround to enable multiple apps.")]
        public sealed class Application_HardwareDetection_Sprite : Sprite, IApplicationSprite
        {
            public event Action FoundMutedCamera;
            public event Action FoundUnmutedCamera;

            public event Action FoundCamera;
            public event Action LookingForCamera;

            public void RaiseLookingForCamera()
            {
                if (LookingForCamera != null)
                    LookingForCamera();
            }


            public void RaiseFoundCamera()
            {
                if (FoundCamera != null)
                    FoundCamera();
            }

            public void RaiseFoundMutedCamera()
            {
                if (FoundMutedCamera != null)
                    FoundMutedCamera();
            }

            public void RaiseFoundUnmutedCamera()
            {
                if (FoundUnmutedCamera != null)
                    FoundUnmutedCamera();
            }

            public void InitializeContent()
            {
                this.InternalInitializeContent();
            }
        }

        public readonly Application_HardwareDetection_Sprite sprite = new Application_HardwareDetection_Sprite();
        #endregion

        public Application_HardwareDetection(global::JellyworldExperiment.HardwareDetection.HTML.Pages.IApp page)
        {
            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);

            sprite.FoundCamera +=
                delegate
                {
                    Native.Document.body.style.backgroundColor = JSColor.Green;

                    Native.Window.parent.With(
                          parent =>
                          {
                              // not talking to self
                              if (parent == Native.Window)
                                  return;



                              parent.postMessage("FoundCamera");
                          }
                      );
                };

            sprite.InitializeContent();


            Native.Window.parent.With(
                   parent =>
                   {
                       // not talking to self
                       if (parent == Native.Window)
                           return;


                       Native.Window.ondeviceorientation +=
                         eventData =>
                         {
                             // done talking
                             if (parent == null)
                                 return;


                             #region desktop chrome misreports?
                             // Uncaught ReferenceError: alpha is not defined 
                             if ("this.alpha == null".js<bool>(eventData))
                             {
                                 Console.WriteLine("ondeviceorientation without alpha? " + eventData);
                                 Console.WriteLine("ondeviceorientation without alpha? " + eventData.alpha);
                                 Console.WriteLine("ondeviceorientation without alpha? ");
                                 return;
                             }
                             #endregion

                             parent.postMessage("ondeviceorientation " + new { eventData.alpha, eventData.beta, eventData.gamma });

                             // stop talking
                             parent = null;
                         };
                   }
               );
        }
    }

    public static class X
    {
        public static T js<T>(this string body, object that = null)
        {
            return (T)new IFunction("return " + body + ";").apply(that);
        }
    }
}
