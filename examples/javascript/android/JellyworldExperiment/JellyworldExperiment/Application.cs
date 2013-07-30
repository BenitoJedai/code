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
            var borders = Native.Document.body;

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


            dynamic ss = borders.style;

            ss.webkitTransition = "all 0.3s linear";


            #region FlashGreen
            Action<Action> FlashGreen =
                yield =>
                {
                    page.init.Hide();


                    borders.style.borderRightColor = "rgba(0, 255, 0, 0.5)";
                    borders.style.borderLeftColor = "rgba(0, 255, 0, 0.5)";
                    borders.style.borderBottomColor = "rgba(0, 255, 0, 0.5)";
                    borders.style.borderTopColor = "rgba(0, 255, 0, 0.5)";

                    new ScriptCoreLib.JavaScript.Runtime.Timer(
                        delegate
                        {
                            borders.style.borderRightColor = "rgba(255, 255, 0, 0)";
                            borders.style.borderLeftColor = "rgba(255, 255, 0, 0)";
                            borders.style.borderBottomColor = "rgba(255, 255, 0, 0)";
                            borders.style.borderTopColor = "rgba(255, 255, 0, 0)";

                            yield();
                        }
                    ).StartTimeout(500);
                };
            #endregion



            var disable_ondeviceorientation = false;

            var errortimer = new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    disable_ondeviceorientation = true;
                    borders.style.borderRightColor = "rgba(255, 0, 0, 0.2)";
                    borders.style.borderLeftColor = "rgba(255, 0, 0, 0.2)";
                    borders.style.borderBottomColor = "rgba(255, 0, 0, 0.2)";
                    borders.style.borderTopColor = "rgba(255, 0, 0, 0.2)";
                }
            );

            new IHTMLIFrame { src = "/HardwareDetection" }.With(
                HardwareDetection =>
                {
                    //HardwareDetection.style.visibility = IStyle.VisibilityEnum.hidden;

                    // android browser does not allow this:
                    HardwareDetection.style.display = IStyle.DisplayEnum.none;

                    Native.Window.onmessage +=
                      ee =>
                      {
                          // stop listening
                          if (HardwareDetection == null)
                              return;

                          // am i talking to you?
                          if (ee.source != HardwareDetection.contentWindow)
                              return;

                          var data = "" + ee.data;

                          var pre = new IHTMLPre
                          {
                              innerText = data + "..."
                          }.AttachToDocument();

                          // android 2.3 wont find this.
                          #region Found orientation sensor
                          if (data == "Found orientation sensor")
                          {
                              Native.Document.title = "Sensor mode";

                              FlashGreen(
                                  delegate
                                  {
                                      var goup = false;
                                      var goright = false;
                                      var godown = false;
                                      var goleft = false;


                                      var dx = 0;
                                      var dy = 0;

                                      var dxdy = new IHTMLPre { innerText = "" }.AttachToDocument();

                                      var disable_ondeviceorientation_bytouch = false;

                                      #region ontouchmove
                                      var touchx = 0;
                                      var touchy = 0;


                                      Native.Document.body.ontouchend +=
                                          e =>
                                          {

                                              //Native.Document.body.style.backgroundColor = JSColor.None;


                                          };

                                      Native.Document.body.ontouchstart +=
                                       e =>
                                       {
                                           //Native.Document.body.style.backgroundColor = JSColor.Yellow;
                                           touchx = e.touches[0].pageX;
                                           touchy = e.touches[0].pageY;

                                           e.preventDefault();

                                           // special rule:
                                           //goupspecial = true;
                                       };

                                      Native.Document.body.ontouchmove +=
                                        e =>
                                        {

                                            e.preventDefault();

                                            var ztouchx = e.touches[0].pageX;
                                            var ztouchy = e.touches[0].pageY;

                                            dy += (ztouchy - touchy);
                                            dx += (ztouchx - touchx);

                                            if (dx > 0)

                                                borders.style.borderRightColor = "rgba(0, 255, 0, 0.5)";
                                            if (dx < 0)
                                                borders.style.borderLeftColor = "rgba(0, 255, 0, 0.5)";
                                            if (dy > 0)
                                                borders.style.borderBottomColor = "rgba(0, 255, 0, 0.5)";
                                            if (dy < 0)
                                                borders.style.borderTopColor = "rgba(0, 255, 0, 0.5)";


                                            //shadow_table_dx += (ztouchx - touchx);
                                            //shadow_table_dy += (ztouchy - touchy);
                                            //shadow_table.style.SetLocation(shadow_table_dx, shadow_table_dy);

                                            dxdy.innerText = new { dx, dy }.ToString();



                                            touchx = ztouchx;
                                            touchy = ztouchy;
                                        };
                                      #endregion


                                      var zyx = new IHTMLPre { innerText = "" }.AttachToDocument();



                                      #region ondeviceorientation
                                      Native.Window.ondeviceorientation +=
                                          e =>
                                          {
                                              if (disable_ondeviceorientation_bytouch)
                                              {
                                                  return;
                                              }

                                              if (disable_ondeviceorientation)
                                              {
                                                  return;
                                              }


                                              #region desktop chrome misreports?
                                              // Uncaught ReferenceError: alpha is not defined 
                                              if ("this.alpha == null".js<bool>(e))
                                              {
                                                  return;
                                              }
                                              #endregion


                                              // http://code.google.com/p/chromium/issues/detail?id=135317
                                              var orientation = Native.Window.orientation;

                                              e.preventDefault();
                                              e.stopPropagation();

                                              zyx.innerText = new
                                              {
                                                  alpha = (int)e.alpha,
                                                  beta = (int)e.beta,
                                                  gamma = (int)e.gamma,
                                                  orientation
                                              }.ToString();


                                              borders.style.borderRightColor = "rgba(255, 255, 255, 0.0)";
                                              borders.style.borderLeftColor = "rgba(255, 255, 255, 0.0)";
                                              borders.style.borderBottomColor = "rgba(255, 255, 255, 0.0)";
                                              borders.style.borderTopColor = "rgba(255, 255, 255, 0.0)";

                                              goup = false;
                                              goright = false;
                                              godown = false;
                                              goleft = false;

                                              #region decide
                                              Action<double, double> decide =
                                                  (movementY, movementZ) =>
                                                  {


                                                      if (movementY < 35)
                                                      {
                                                          // device is on the table. discard the sensor.
                                                          if (movementY > 5)
                                                          {
                                                              borders.style.borderTopColor = "rgba(0, 255, 0, 1.0)";
                                                              goup = true;
                                                          }
                                                      }
                                                      //else if (movementY > 33)
                                                      else if (movementY > 60)
                                                      {
                                                          borders.style.borderBottomColor = "rgba(255, 0, 0, 1)";
                                                          godown = true;
                                                      }

                                                      if (movementZ < -10)
                                                      {
                                                          goleft = true;
                                                          borders.style.borderLeftColor = "rgba(255, 255, 0, 0.5)";
                                                      }
                                                      else if (movementZ > 10)
                                                      {
                                                          goright = true;
                                                          borders.style.borderRightColor = "rgba(255, 255, 0, 0.5)";

                                                      }
                                                  };
                                              #endregion

                                              //if (movementY < -2)
                                              if (orientation == 0)
                                              {
                                                  //  which is it 0 or 180

                                                  var movementY = e.beta;
                                                  var movementZ = e.gamma;

                                                  if (e.beta < 0)
                                                  {
                                                      // up side down!
                                                      movementY = -e.beta;
                                                      movementZ = -e.gamma;
                                                  }

                                                  decide(movementY, movementZ);
                                              }
                                              else if (orientation == 180)
                                              {
                                                  // when is 180 reported?

                                                  var movementY = -e.beta;
                                                  var movementZ = -e.gamma;


                                                  decide(movementY, movementZ);

                                              }
                                              #region landscape
                                              else if (orientation == -90)
                                              {
                                                  var movementY = e.gamma;
                                                  var movementZ = -e.beta;

                                                  decide(movementY, movementZ);

                                              }
                                              else if (orientation == 90)
                                              {
                                                  var movementY = -e.gamma;
                                                  var movementZ = e.beta;

                                                  decide(movementY, movementZ);

                                              }
                                              #endregion

                                          };
                                      #endregion


                                      var id = new Random().Next();
                                      var frame = 0;

                                      #region loop
                                      Action loop = null;

                                      var zdx = 0;
                                      var zdy = 0;
                                      var zgoup = false;
                                      var zgoright = false;
                                      var zgodown = false;
                                      var zgoleft = false;

                                      loop = delegate
                                      {
                                          frame++;



                                          if (frame > 1)

                                              if (dx == zdx)
                                                  if (dy == zdy)

                                                      if (zgoup == goup)
                                                          if (zgoright == goright)
                                                              if (zgodown == godown)
                                                                  if (zgoleft == goleft)
                                                                  {
                                                                      // check again
                                                                      Native.Window.requestAnimationFrame += loop;

                                                                      return;
                                                                  }

                                          zdx = dx;
                                          zdy = dy;

                                          zgoup = goup;
                                          zgoright = goright;
                                          zgodown = godown;
                                          zgoleft = goleft;

                                          dx = 0;
                                          dy = 0;

                                          errortimer.Stop();
                                          errortimer.StartTimeout(2000);


                                          service.AtFrame(
                                              "" + id,
                                              "" + frame,


                                              dx: "" + zdx,
                                              dy: "" + zdy,

                                              goleft: "" + System.Convert.ToInt32(zgoleft),
                                              goup: "" + System.Convert.ToInt32(zgoup),
                                              goright: "" + System.Convert.ToInt32(zgoright),
                                              godown: "" + System.Convert.ToInt32(zgodown),

                                              yield:
                                                  delegate
                                                  {
                                                      disable_ondeviceorientation = false;
                                                      errortimer.Stop();

                                                      Native.Window.requestAnimationFrame += loop;
                                                  }
                                          );


                                      };

                                      Native.Window.requestAnimationFrame += loop;
                                      #endregion
                                  }
                              );
                          }
                          #endregion



                          #region Found flash camera
                          if (data == "Found flash camera")
                          {
                              Native.Document.title = "Camera mode";

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
                                            DualViewWithCamera.style.top = "-100%";
                                            DualViewWithCamera.style.width = "100%";
                                            DualViewWithCamera.style.height = "100%";

                                            DualViewWithCamera.onload +=
                                                delegate
                                                {


                                                    // that iframe will have its own borders now.
                                                    Native.Document.body.style.borderWidth = "0px";

                                                    DualViewWithCamera.style.top = "0px";

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


                                            FlashGreen(
                                                delegate
                                                {
                                                    ss.webkitTransition = "";
                                                    DualViewWithCamera.AttachToDocument();
                                                }
                                            );
                                        }
                                      );
                                  };

                              open();
                          }
                          #endregion


                          // if we found a camera
                          //// thanks for info. you are done!
                          //HardwareDetection.Orphanize();

                          //// stop listening
                          //HardwareDetection = null;
                      };
                }
            ).AttachToDocument();
            #endregion



        }

    }

    [Obsolete("Temporary workaround to enable multiple apps.")]
    public sealed class __WithCamera
    {
        [Obsolete("Temporary workaround to enable multiple apps.")]
        public sealed class __Sprite : Sprite, JellyworldExperiment.DualViewWithCamera.IApplicationSprite
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

        public __WithCamera(global::JellyworldExperiment.DualViewWithCamera.HTML.Pages.IApp page)
        {

            IHTMLElement borders = null;



            __Sprite sprite = null;

            if (Native.Document.location.hash == "")
            {
                sprite = new __Sprite();
                sprite.ToTransparentSprite();
                sprite.AutoSizeSpriteTo(page.ContentSize);
                page.Content.AttachToDocument();
                sprite.AttachSpriteTo(page.Content);
                sprite.InitializeContent();


                borders = Native.Document.body;

                borders.style.margin = "0em";
                borders.style.borderWidth = "3em";
                borders.style.borderStyle = "solid";

                dynamic ss = borders.style;

                ss.webkitTransition = "all 0.3s linear";
            }

            var app = new JellyworldExperiment.DualView.HTML.Pages.App();

            app.Container.AttachToDocument();
            app.Container.style.position = IStyle.PositionEnum.absolute;
            app.Container.style.left = "0px";
            app.Container.style.right = "0px";
            app.Container.style.bottom = "0px";

            app.range_s.value = "70";

            var xapp = new JellyworldExperiment.DualView.Application(app);

            if (Native.Document.location.hash == "")
            {
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
            }

            app.SimulateFace.Hide();
            app.Automatisation.Hide();
            app.StyleMe.style.color = JSColor.White;

            if (Native.Document.location.hash == "")
            {


                #region EventSource
                try
                {
                    // this is like a ComponentModel timer where handler can raise events

                    var status = new IHTMLPre { innerText = "" }.AttachToDocument();
                    //var onmessage = new IHTMLPre { innerText = "" }.AttachToDocument();
                    status.style.color = JSColor.Gray;
                    status.style.Float = IStyle.FloatEnum.right;

                    new EventSource().With(
                      s =>
                      {
                          //s.status =
                          s["status"] =
                              e =>
                              {
                                  //Native.Document.title = "" + e.data;
                                  status.innerText = "" + e.data;
                              };



                          s.onerror +=
                              e =>
                              {
                                  //Native.Document.title = "error";
                                  //errortimer.StartTimeout(500);
                              };

                          #region onmessage
                          s.onmessage +=
                              e =>
                              {
                                  //errortimer.Stop();
                                  status.innerText = "..";

                                  var xml = "" + e.data;
                                  var data = XElement.Parse(xml);

                                  long
                                   last_ms = long.Parse(data.Attribute("last_ms").Value),

                                   x = long.Parse(data.Attribute("x").Value),
                                   y = long.Parse(data.Attribute("y").Value);

                                  if (x != 0)
                                      if (y != 0)
                                      {

                                          // less sensitivity, due to higher dpi?
                                          var report_dx = (int)(x * 0.3);
                                          var report_dy = (int)(y * 0.3);

                                          xapp.ChangeRotationBy(report_dx, report_dy);
                                      }

                                  bool
                                      goleft = 0 < long.Parse(data.Attribute("goleft").Value),
                                      goup = 0 < long.Parse(data.Attribute("goup").Value),
                                      goright = 0 < long.Parse(data.Attribute("goright").Value),
                                      godown = 0 < long.Parse(data.Attribute("godown").Value);

                                  status.innerText = new { goleft, goup, goright, godown }.ToString();


                                  borders.style.borderRightColor = "rgba(255, 255, 0, 0)";
                                  borders.style.borderLeftColor = "rgba(255, 255, 0, 0)";
                                  borders.style.borderBottomColor = "rgba(255, 255, 0, 0)";
                                  borders.style.borderTopColor = "rgba(255, 255, 0, 0)";

                                  xapp.forward = false;
                                  xapp.backward = false;
                                  xapp.strafeleft = false;
                                  xapp.straferight = false;



                                  if (goup)
                                  {
                                      borders.style.borderTopColor = "rgba(0, 255, 0, 1.0)";
                                      xapp.forward = true;
                                  }
                                  else if (godown)
                                  {
                                      borders.style.borderBottomColor = "rgba(255, 0, 0, 1.0)";
                                      xapp.backward = true;
                                  }
                                  if (goleft)
                                  {
                                      xapp.strafeleft = true;
                                      borders.style.borderLeftColor = "rgba(255, 255, 0, 0.2)";
                                  }
                                  else if (goright)
                                  {
                                      xapp.straferight = true;
                                      borders.style.borderRightColor = "rgba(255, 255, 0, 0.2)";
                                  }

                                  xapp.AfterKeystateChange();
                              };
                          #endregion

                      }
                    );


                }
                catch
                {
                    // not available on built in web browser for android
                }
                #endregion

            }


        }
    }

    [Obsolete("Temporary workaround to enable multiple apps.")]
    public sealed class __Templates
    {
        public __Templates(global::My.Solutions.Pages.Templates.HTML.Pages.IDefaultPage page)
        {
            new global::My.Solutions.Pages.Templates.Application(page);
        }
    }

    [Obsolete("Temporary workaround to enable multiple apps.")]
    public sealed class __HardwareDetection
    {
        #region Application_HardwareDetection_Sprite
        [Obsolete("Temporary workaround to enable multiple apps.")]
        public sealed class __Sprite : Sprite, IApplicationSprite
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

        public readonly __Sprite sprite = new __Sprite();
        #endregion

        public __HardwareDetection(global::JellyworldExperiment.HardwareDetection.HTML.Pages.IApp page)
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
                                 //Console.WriteLine("ondeviceorientation without alpha? " + eventData);
                                 //Console.WriteLine("ondeviceorientation without alpha? " + eventData.alpha);
                                 //Console.WriteLine("ondeviceorientation without alpha? ");
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
