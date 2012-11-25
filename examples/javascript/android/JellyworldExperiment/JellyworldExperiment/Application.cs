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
         * 07. Test dual view.
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
                innerText = "Looking at your hardware... "
            }.AttachToDocument();


            new IHTMLPre
            {
                innerText = "We need a laptop with flash, camera, CSS 3D,..."
            }.AttachToDocument();

            new IHTMLPre
            {
                innerText = "From your laptop open " + Native.Document.location
            }.AttachToDocument();

            new IHTMLPre
            {
                innerText = "Your android device becomes the tilt sensor!"
            }.AttachToDocument();

            new IHTMLPre
            {
                innerText = "Screen is " + Native.Screen.width + "x" + Native.Screen.height + "..."
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

                          var data = "" + e.data;

                          var pre = new IHTMLPre
                          {
                              innerText = e.data + "..."
                          }.AttachToDocument();

                          // android 2.3 wont find this.
                          if (data == "Found orientation sensor")
                          {

                           

                          }


                          if (data == "Found flash camera")
                          {
                              Action open =
                                  delegate
                                  {
                                      new IHTMLPre
                                      {
                                          innerText = "Opening..."
                                      }.AttachToDocument();

                                      new IHTMLIFrame
                                      {
                                          src = "/DualViewWithCamera",
                                          frameBorder = "0"
                                      }.With(
                                        DualViewWithCamera =>
                                        {
                                            DualViewWithCamera.style.position = IStyle.PositionEnum.absolute;
                                            DualViewWithCamera.style.left = "0px";
                                            DualViewWithCamera.style.top = "0px";
                                            DualViewWithCamera.style.width = "100%";
                                            DualViewWithCamera.style.height = "100%";

                                            DualViewWithCamera.onload +=
                                                delegate
                                                {
                                                    new IHTMLPre
                                                    {
                                                        innerText = "Then, open left and/or right screen..."
                                                    }.AttachToDocument();

                                                    new IHTMLPre
                                                    {
                                                        innerText = "Then, use shared perspective..."
                                                    }.AttachToDocument();

                                                    new IHTMLPre
                                                    {
                                                        innerText = "Then, allow flash camera..."
                                                    }.AttachToDocument();

                                                    new IHTMLPre
                                                    {
                                                        innerText = "Then, go fullscreen!"
                                                    }.AttachToDocument();
                                                };

                                            DualViewWithCamera.AttachToDocument();
                                        }
                                      );
                                  };

                              open();
                          }

                          // if we found a camera
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
    public sealed class Application_DualViewWithCamera
    {
        [Obsolete("Temporary workaround to enable multiple apps.")]
        public sealed class ApplicationSprite : Sprite, JellyworldExperiment.DualViewWithCamera.IApplicationSprite
        {
            public void InitializeContent()
            {
                JellyworldExperiment.DualViewWithCamera.ApplicationSpriteContent.InternalInitializeContent(
                    this,
                    (Left, Top, Width, Height) =>
                    {
                        if (AverageChanged != null)
                            AverageChanged("" + Left, "" + Top, "" + Width, "" + Height);
                    }
                );
            }

            // does not work?
            //public event Action<double, double, double, double> AverageChanged;

            //  Error: Implicit coercion of a value of type int to an unrelated type String.
            public event Action<string, string, string, string> AverageChanged;

        }

        public Application_DualViewWithCamera(global::JellyworldExperiment.DualViewWithCamera.HTML.Pages.IApp page)
        {
            var borders = new IHTMLDiv();

            borders.style.position = IStyle.PositionEnum.absolute;
            borders.style.left = "0px";
            borders.style.top = "0px";
            borders.style.right = "0px";
            borders.style.bottom = "0px";

            borders.style.borderLeftColor = "rgba(255, 255, 255, 0.3)";
            borders.style.borderWidth = "3em";
            borders.style.borderStyle = "solid";

            borders.AttachToDocument();

            var sprite = new ApplicationSprite();

            sprite.ToTransparentSprite();

            sprite.AutoSizeSpriteTo(page.ContentSize);

            page.Content.AttachToDocument();

            sprite.AttachSpriteTo(page.Content);

            sprite.InitializeContent();


            var app = new JellyworldExperiment.DualView.HTML.Pages.App();

            app.Container.AttachToDocument();
            app.Container.style.position = IStyle.PositionEnum.absolute;
            app.Container.style.left = "0px";
            app.Container.style.right = "0px";
            app.Container.style.bottom = "0px";

            app.range_s.value = "70";

            var xapp = new JellyworldExperiment.DualView.Application(app);

            sprite.AverageChanged +=
                (Left, Top, Width, Height) =>
                {
                    xapp.FaceDetectedAt(
                        int.Parse(Left),
                        int.Parse(Top),
                        int.Parse(Width),
                        int.Parse(Height)
                    );
                };

            app.SimulateFace.Hide();
            app.Automatisation.Hide();
            app.StyleMe.style.color = JSColor.White;



            var movementX = 0.0;
            var movementY = 0.0;
            var movementZ = 0.0;

            try
            {
                // this is like a ComponentModel timer where handler can raise events

                dynamic ss = borders.style;

                ss.webkitTransition = "all 0.3s linear";

                var events = new EventSource();

                events.onerror +=
                    delegate
                    {
                        borders.style.borderRightColor = "rgba(255, 0, 0, 0.5)";
                        borders.style.borderLeftColor = "rgba(255, 0, 0, 0.5)";
                        borders.style.borderBottomColor = "rgba(255, 0, 0, 0.5)";
                        borders.style.borderTopColor = "rgba(255, 0, 0, 0.5)";
                    };

                events.onmessage +=
                     e =>
                     {

                         var xml = XElement.Parse((string)e.data);

                         movementX = double.Parse(xml.Attribute("x").Value);
                         movementY = -double.Parse(xml.Attribute("y").Value);
                         movementZ = -double.Parse(xml.Attribute("z").Value);

                         Console.WriteLine(
                             new { movementX, movementY, movementZ }
                             );

                         borders.style.borderRightColor = "rgba(255, 255, 255, 0.0)";
                         borders.style.borderLeftColor = "rgba(255, 255, 255, 0.0)";
                         borders.style.borderBottomColor = "rgba(255, 255, 255, 0.0)";
                         borders.style.borderTopColor = "rgba(255, 255, 255, 0.0)";

                         xapp.forward = false;
                         xapp.backward = false;
                         xapp.strafeleft = false;
                         xapp.straferight = false;

                         if (movementY < -2)
                         {
                             borders.style.borderTopColor = "rgba(255, 255, 255, 0.3)";
                             xapp.forward = true;
                         }
                         else if (movementY > 33)
                         {
                             borders.style.borderBottomColor = "rgba(255, 255, 255, 0.3)";
                             xapp.backward = true;
                         }

                         if (movementZ < -15)
                         {
                             xapp.strafeleft = true;
                             borders.style.borderLeftColor = "rgba(255, 255, 255, 0.3)";
                         }
                         else if (movementZ > 15)
                         {
                             borders.style.borderRightColor = "rgba(255, 255, 255, 0.3)";
                             xapp.straferight = true;

                         }

                         xapp.AfterKeystateChange();

                         //page.Content.Clear();

                         //new IHTMLPre { innerText = xml.ToString() }.AttachTo(page.Content);
                     };
            }
            catch
            {
                // not available on built in web browser for android
            }



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



                              parent.postMessage("Found flash camera");
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

                             parent.postMessage("Found orientation sensor");

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
