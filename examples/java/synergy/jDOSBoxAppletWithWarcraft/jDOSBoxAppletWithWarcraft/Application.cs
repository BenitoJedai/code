using jDOSBoxAppletWithWarcraft.HTML.Pages;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;

using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Xml.Linq;

namespace jDOSBoxAppletWithWarcraft
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp xpage = null)
        {
            // subst a: X:\jsc.svn\examples\java\synergy\jDOSBoxAppletWithWarcraft\jDOSBoxAppletWithWarcraft\bin\Debug\staging\jDOSBoxAppletWithWarcraft.Application\web



            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                //Console.WriteLine("FlashHeatZeeker shall run as a chrome app as server");

                //chrome.Notification.DefaultTitle = "Operation «Heat Zeeker»";
                //chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;

                //ChromeTCPServer.TheServerWithStyledForm.Invoke(

                ChromeTCPServer.TheServer.Invoke(
                      AppSource.Text,
                      u => Native.window.open(u)
                  );

                return;
            }
            #endregion



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140221-war


            // 20 MB disk. How to create one ourselves?
            // view-source:http://www.classicdosgames.com/online/doom19s.html

            //            0011 javassist create javassist.ClassMap
            //System.TypeLoadException: Declaration referenced in a method implementation cannot be a final method.  Type: 'javassist.ClassMap'.  Assembly: 'javassist, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null'.

            // http://www.dosbox.com/wiki/IMGMOUNT#The_.22-size.22_parameter_.28required_to_to_create_bootable_hard_disk_images.29

            var borders = Native.Document.body;

            //dynamic ss = borders.style;

            //ss.webkitTransition = "all 0.3s linear";
            borders.style.transition = "all 0.3s linear";


            borders.style.borderWidth = "3em";
            borders.style.borderStyle = "solid";

            borders.style.borderRightColor = "rgba(0, 0, 255, 0.5)";
            borders.style.borderLeftColor = "rgba(0, 0, 255, 0.5)";
            borders.style.borderBottomColor = "rgba(0, 0, 255, 0.5)";
            borders.style.borderTopColor = "rgba(0, 0, 255, 0.5)";

            var disable_ondeviceorientation = false;

            var errortimer = new Timer(
                delegate
                {
                    disable_ondeviceorientation = true;
                    borders.style.borderRightColor = "rgba(255, 0, 0, 0.2)";
                    borders.style.borderLeftColor = "rgba(255, 0, 0, 0.2)";
                    borders.style.borderBottomColor = "rgba(255, 0, 0, 0.2)";
                    borders.style.borderTopColor = "rgba(255, 0, 0, 0.2)";
                }
            );

            xpage._IWantToUseTheApplet.onclick +=
                delegate
                {
                    // https://code.google.com/p/chromium/issues/detail?id=288935
                    // http://stackoverflow.com/questions/21318087/chrome-packaged-app-using-java-plugin
                    // java applets wont work in chrome webview?
                    // Initialize ApplicationApplet
                    var applet = new ApplicationApplet();

                    var container = applet.ToHTMLElement();

                    //ApplicationContent.InitializeContent(container);

                    var ref4 = "assets/jDOSBoxAppletWithWarcraft/war1.img";

                    var location = "" + Native.Document.location;

                    //Native.document.baseURI = "assets/jDOSBoxAppletWithWarcraft/";

                    // <param name="param1" value="imgmount e http://127.0.0.1:20169/assets/jDOSBoxAppletWithWarcraft/war1.img">

                    var cmd = "imgmount e " + location.TakeUntilLastIfAny("/") + "/" + ref4;

                    new IHTMLParam { name = "param1", value = cmd }.AttachTo(container);
                    new IHTMLParam { name = "param2", value = "e:" }.AttachTo(container);
                    new IHTMLParam { name = "param3", value = "dir" }.AttachTo(container);
                    //new IHTMLParam { name = "param4", value = "cd war1" }.AttachTo(e);
                    //new IHTMLParam { name = "param5", value = "setup" }.AttachTo(e);
                    //new IHTMLParam { name = "param6", value = "war" }.AttachTo(e);

                    //e.AttachToDocument();

                    // java.security.AccessControlException: access denied ("java.net.SocketPermission" "192.168.43.252:24369" "connect,resolve")

                    //Native.window.open("about:blank", "_blank", 800, 600, false).With(
                    //    w =>
                    //    {
                    //        #region for non default paths we need explicit location!

                    //        var archive = container.AsXElement().Attribute("archive");

                    //        archive.Value = location + archive.Value;

                    //        #endregion

                    //        //<applet archive="assets/jDOSBoxAppletWithWarcraft.Application/jDOSBoxAppletWithWarcraft.ApplicationApplet.jar" scriptable="true" code="jDOSBoxAppletWithWarcraft.ApplicationApplet" width="800" height="600"><param name="codebase_lookup" value="false"><param name="separate_jvm" value="true"><param name="param1" value="imgmount e http://192.168.1.100:13331/assets/jDOSBoxAppletWithWarcraft/war1.img"><param name="param2" value="e:"><param name="param3" value="dir"></applet>


                    //        w.onload +=
                    //            delegate
                    //            {
                    // java.security.AccessControlException: access denied
                    //new IHTMLBase { href = location }.AttachTo(
                    //    w.document.body.parentNode.firstChild
                    //    );

                    var GrayWindow = new GrayWindow();

                    xpage._IWantToUseTheApplet.parentNode.replaceChild(
                        GrayWindow.body,
                       xpage._IWantToUseTheApplet
                    );

                    //w.document.title = "dosbox applet for secondary monitor";



                    container.AttachTo(GrayWindow.center);
                    //            };
                    //    }
                    //);





                    var page = new { Keyboard = Native.Document.body };

                    #region onkeydown
                    page.Keyboard.onkeydown +=
                     e =>
                     {
                         e.preventDefault();
                         e.stopPropagation();


                         int KeyCode = e.KeyCode;
                         if (KeyCode == 13)
                             KeyCode = 10;
                         int KeyChar = KeyCode;



                         applet.__MainApplet_keyPressed("" + KeyCode, "" + KeyChar,
                             message => Native.window.alert(message)
                         );
                     };

                    page.Keyboard.onkeyup +=
                        e =>
                        {
                            e.preventDefault();
                            e.stopPropagation();

                            int KeyCode = e.KeyCode;

                            if (KeyCode == 13)
                                KeyCode = 10;



                            int KeyChar = KeyCode;



                            applet.__MainApplet_keyReleased("" + KeyCode, "" + KeyChar,
                                message => Native.window.alert(message)
                            );
                        };
                    #endregion


                    #region onmousemove
                    page.Keyboard.onmouseup +=
                       e =>
                       {
                           if (Native.Document.pointerLockElement == page.Keyboard)
                           {
                               e.preventDefault();
                               e.stopPropagation();

                               applet.__MainApplet_mouseReleased();

                               return;
                           }


                       };


                    page.Keyboard.onmousedown +=
                        e =>
                        {
                            if (Native.Document.pointerLockElement == page.Keyboard)
                            {
                                e.preventDefault();
                                e.stopPropagation();


                                applet.__MainApplet_mousePressed();

                                return;
                            }

                            page.Keyboard.requestPointerLock();

                        };

                    page.Keyboard.onmousemove +=
                     e =>
                     {
                         if (Native.Document.pointerLockElement == page.Keyboard)
                         {
                             e.preventDefault();
                             e.stopPropagation();


                             var dx = e.movementX;
                             var dy = e.movementY;

                             applet.__MainApplet_mousemove("" + dx, "" + dy);

                         }
                     };
                    #endregion

                    var status = new IHTMLPre { innerText = "" }.AttachToDocument();
                    var onmessage = new IHTMLPre { innerText = "" }.AttachToDocument();

                    #region EventSource
                    new EventSource().With(
                        s =>
                        {
                            //s.status =
                            s["status"] =
                                e =>
                                {
                                    status.innerText = "" + e.data;
                                };



                            s.onerror +=
                                e =>
                                {
                                    errortimer.StartTimeout(300);
                                };


                            s.onmessage +=
                                e =>
                                {
                                    errortimer.Stop();

                                    var xml = "" + e.data;
                                    var data = XElement.Parse(xml);

                                    //long
                                    //    last_ms = long.Parse(data.Attribute("last_ms").Value),

                                    //    x = long.Parse(data.Attribute("x").Value),
                                    //    y = long.Parse(data.Attribute("y").Value);

                                    bool
                                        goleft = 0 < long.Parse(data.Attribute("goleft").Value),
                                        goup = 0 < long.Parse(data.Attribute("goup").Value),
                                        goright = 0 < long.Parse(data.Attribute("goright").Value),
                                        godown = 0 < long.Parse(data.Attribute("godown").Value);


                                    onmessage.innerText = "" +
                                        new
                                        {
                                            //last_ms,
                                            //x,
                                            //y,
                                            goleft,
                                            goup,
                                            goright,
                                            godown
                                        };

                                    borders.style.borderRightColor = "rgba(255, 255, 255, 0.0)";
                                    borders.style.borderLeftColor = "rgba(255, 255, 255, 0.0)";
                                    borders.style.borderBottomColor = "rgba(255, 255, 255, 0.0)";
                                    borders.style.borderTopColor = "rgba(255, 255, 255, 0.0)";

                                    //Native.Document.body.style.backgroundColor = "rgba(255, 255, 0, 0.4)";

                                    //if (x == 0)
                                    //    if (y == 0)
                                    //        Native.Document.body.style.backgroundColor = JSColor.None;

                                    //shadow_table_dx += (int)x;
                                    //shadow_table_dy += (int)y;
                                    //shadow_table.style.SetLocation(shadow_table_dx, shadow_table_dy);

                                    Action<string> sprite_keyup =
                                        x =>
                                        {
                                            applet.__MainApplet_keyReleased(
                                                x, x, Console.WriteLine
                                            );
                                        };

                                    Action<string> sprite_keydown =
                                      x =>
                                      {
                                          applet.__MainApplet_keyPressed(
                                              x, x, Console.WriteLine
                                          );
                                      };

                                    sprite_keyup("" + (int)System.Windows.Forms.Keys.Up);
                                    sprite_keyup("" + (int)System.Windows.Forms.Keys.Down);
                                    sprite_keyup("" + (int)System.Windows.Forms.Keys.Left);
                                    sprite_keyup("" + (int)System.Windows.Forms.Keys.Right);

                                    if (goup)
                                    {
                                        borders.style.borderTopColor = "rgba(0, 255, 0, 0.5)";
                                        sprite_keydown("" + (int)System.Windows.Forms.Keys.Up);
                                    }
                                    else if (godown)
                                    {
                                        borders.style.borderBottomColor = "rgba(0, 255, 0, 0.5)";

                                        sprite_keydown("" + (int)System.Windows.Forms.Keys.Down);
                                    }

                                    if (goleft)
                                    {
                                        sprite_keydown("" + (int)System.Windows.Forms.Keys.Left);
                                        borders.style.borderLeftColor = "rgba(0, 255, 0, 0.5)";
                                    }
                                    else if (goright)
                                    {
                                        sprite_keydown("" + (int)System.Windows.Forms.Keys.Right);
                                        borders.style.borderRightColor = "rgba(0, 255, 0, 0.5)";

                                    }
                                };
                        }
                    );
                    #endregion

                };



#if FTILT
            xpage._IAmTheTiltSensorUseThisDeviceToControlTheApplet.onclick +=
                delegate
                {


                    borders.style.borderRightColor = "rgba(255, 0, 0, 0.5)";
                    borders.style.borderLeftColor = "rgba(255, 0, 0, 0.5)";
                    borders.style.borderBottomColor = "rgba(255, 0, 0, 0.5)";
                    borders.style.borderTopColor = "rgba(255, 0, 0, 0.5)";

                    var goup = false;
                    var goright = false;
                    var godown = false;
                    var goleft = false;

                    var zyx = new IHTMLPre { innerText = "" }.AttachToDocument();

            #region ondeviceorientation
                    Native.window.ondeviceorientation +=
                        e =>
                        {
                            if (disable_ondeviceorientation)
                                return;

            #region desktop chrome misreports?
                            // Uncaught ReferenceError: alpha is not defined 
                            if ("this.alpha == null".js<bool>(e))
                            {
                                return;
                            }
            #endregion

                            e.preventDefault();
                            e.stopPropagation();

                            zyx.innerText = new { e.alpha, e.beta, e.gamma }.ToString();

                            var movementX = e.alpha;
                            var movementY = e.beta;
                            var movementZ = e.gamma;

                            borders.style.borderRightColor = "rgba(255, 255, 255, 0.0)";
                            borders.style.borderLeftColor = "rgba(255, 255, 255, 0.0)";
                            borders.style.borderBottomColor = "rgba(255, 255, 255, 0.0)";
                            borders.style.borderTopColor = "rgba(255, 255, 255, 0.0)";

                            goup = false;
                            goright = false;
                            godown = false;
                            goleft = false;

                            if (movementY < -2)
                            {
                                borders.style.borderTopColor = "rgba(0, 255, 0, 0.5)";
                                goup = true;
                            }
                            else if (movementY > 33)
                            {
                                borders.style.borderBottomColor = "rgba(0, 255, 0, 0.5)";
                                godown = true;
                            }

                            if (movementZ < -15)
                            {
                                goleft = true;
                                borders.style.borderLeftColor = "rgba(0, 255, 0, 0.5)";
                            }
                            else if (movementZ > 15)
                            {
                                goright = true;
                                borders.style.borderRightColor = "rgba(0, 255, 0, 0.5)";

                            }
                        };
            #endregion

                    var id = new Random().Next();
                    var frame = 0;

            #region loop
                    Action loop = null;

                    //var zdx = 0;
                    //var zdy = 0;
                    var zgoup = false;
                    var zgoright = false;
                    var zgodown = false;
                    var zgoleft = false;

                    loop = delegate
                    {
                        frame++;



                        if (frame > 1)
                            //if (dx == zdx)
                            //    if (dy == zdy)
                            if (zgoup == goup)
                                if (zgoright == goright)
                                    if (zgodown == godown)
                                        if (zgoleft == goleft)
                                        {
                                            // check again
                                            Native.window.requestAnimationFrame += loop;

                                            return;
                                        }

                        //zdx = dx;
                        //zdy = dy;

                        zgoup = goup;
                        zgoright = goright;
                        zgodown = godown;
                        zgoleft = goleft;

                        //dx = 0;
                        //dy = 0;

                        errortimer.Stop();
                        errortimer.StartTimeout(300);

                        service.AtFrame(
                            "" + id,
                            "" + frame,


                            goleft: "" + System.Convert.ToInt32(zgoleft),
                            goup: "" + System.Convert.ToInt32(zgoup),
                            goright: "" + System.Convert.ToInt32(zgoright),
                            godown: "" + System.Convert.ToInt32(zgodown),

                            yield:
                                delegate
                                {
                                    disable_ondeviceorientation = false;
                                    errortimer.Stop();

                                    Native.window.requestAnimationFrame += loop;
                                }
                        );


                    };

                    Native.window.requestAnimationFrame += loop;
            #endregion
                };
#endif

        }



    }

    public static class ApplicationContent
    {

        public static void InitializeContent(IHTMLElement e)
        {
            var ref4 = "assets/jDOSBoxAppletWithWarcraft/war1.img";

            var location = "" + Native.Document.location;

            // <param name="param1" value="imgmount e http://127.0.0.1:20169/assets/jDOSBoxAppletWithWarcraft/war1.img">

            var cmd = "imgmount e " + location.TakeUntilLastIfAny("/") + "/" + ref4;

            new IHTMLParam { name = "param1", value = cmd }.AttachTo(e);
            new IHTMLParam { name = "param2", value = "e:" }.AttachTo(e);
            new IHTMLParam { name = "param3", value = "dir" }.AttachTo(e);
            //new IHTMLParam { name = "param4", value = "cd war1" }.AttachTo(e);
            //new IHTMLParam { name = "param5", value = "setup" }.AttachTo(e);
            //new IHTMLParam { name = "param6", value = "war" }.AttachTo(e);

            //e.AttachToDocument();

            e.With(
                ee =>
                {
                    Native.Document.body.insertBefore(
                        ee,
                        Native.Document.body.firstChild
                    );

                    ee.style.Float = IStyle.FloatEnum.right;
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
