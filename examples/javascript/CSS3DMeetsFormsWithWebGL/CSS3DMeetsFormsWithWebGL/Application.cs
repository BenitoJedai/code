using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CSS3DMeetsFormsWithWebGL.HTML.Pages;
using CSS3DMeetsFormsWithWebGL.Design;
using CSS3DMeetsFormsWithWebGL.Library;

namespace CSS3DMeetsFormsWithWebGL
{
    static class XX
    {
        public static INode[] ExceptTextNodes(this INode[] e)
        {
            return e.Where(k => k.nodeType != INode.NodeTypeEnum.TextNode).ToArray();
        }
    }
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    public sealed class Application
    {
 //       { trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = Void PopupInsteadOfClosing(System.Windows.Forms.Form), DeclaringType = Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions, Location =
 //assembly: X:\jsc.svn\examples\javascript\CSS3DMeetsFormsWithWebGL\CSS3DMeetsFormsWithWebGL\bin\Debug\CSS3DMeetsFormsWithWebGL.exe
 //type: CSS3DMeetsFormsWithWebGL.ApplicationContent+<>c__DisplayClass23, CSS3DMeetsFormsWithWebGL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 //offset: 0x0010
 // method:Void <InitializeContent>b__18(System.Windows.Forms.Form), ex = System.InvalidOperationException: Renew any references. TargetField can not be null. { Location =
 //assembly: X:\jsc.svn\examples\javascript\CSS3DMeetsFormsWithWebGL\CSS3DMeetsFormsWithWebGL\bin\Debug\Abstractatech.JavaScript.FormAsPopup.dll
 //type: Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions+<>c__DisplayClass6, Abstractatech.JavaScript.FormAsPopup, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 //offset: 0x0021
 // method:Void <PopupInsteadOfClosing>b__0() }
 //  at jsc.Languages.IL.ILTranslationExtensions.EmitToArguments.<.ctor>b__47(ILInstruction )

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefault page)
        {
            // http://www.addyosmani.com/resources/googlebox/


            new ApplicationContent().Initialize(page);


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }
    }

    public sealed class ApplicationContent
    {

        #region trans3d
        [Script(HasNoPrototype = true, ExternalTarget = "M44")]
        public class M44
        {
            public float _22;
            public float _42;
            internal void rotX(double tilt)
            {
                throw new NotImplementedException();
            }

            internal M44 translate(float p1, float p2, float p3)
            {
                throw new NotImplementedException();
            }

            internal void rotY(float rot_cur)
            {
                throw new NotImplementedException();
            }

            internal void mul(M44 lidTrans1, M44 lidTrans2)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "TransformNode")]
        public class TransformNode
        {
            public M44 localTrans;

            public TransformNode(M44 rootTransMatrix)
            {
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "CSSCube")]
        public class CSSCube
        {
            public M44 localTrans;
            public float height;

            public CSSCube(int p1, int p2, int p3, TransformNode rootTransNode)
            {

            }


            internal CSSFace getSide(int i)
            {
                throw new NotImplementedException();
            }

            internal CSSFace getTop()
            {
                throw new NotImplementedException();
            }

            internal CSSFace getBottom()
            {
                throw new NotImplementedException();
            }

            internal void applyTransform()
            {
                throw new NotImplementedException();
            }

            internal void changeHeight(float h)
            {
                throw new NotImplementedException();
            }
        }

        [Script(HasNoPrototype = true, ExternalTarget = "Vec3")]
        public class Vec3
        {
            public float z;
        }

        [Script(HasNoPrototype = true, ExternalTarget = "CSSFace")]
        public class CSSFace
        {
            public Vec3 N;

            public INode element;
            public INode backElement;

            public M44 preTrans;
            public M44 postTrans;
            public TransformNode tNode;
            private TransformNode transformNode;
            private int p1;
            private int p2;

            public CSSFace(TransformNode transformNode, int p1, int p2)
            {
                // TODO: Complete member initialization
                this.transformNode = transformNode;
                this.p1 = p1;
                this.p2 = p2;
            }



            internal void applyTransform()
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        public void Initialize(IDefault page, Action yield = null)
        {
            global::DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();

            new trans3d().Content.AttachToDocument().onload +=
                delegate
                {
                    InitializeContent(page);

                    if (yield != null)
                        yield();
                };

        }

        public FrontPanel frontcontrol;

        public void InitializeContent(IDefault page)
        {
            var control = new UserControl1();

            this.frontcontrol = new FrontPanel();

            this.frontcontrol.RelativeToAbsolute =
                href =>
                {
                    if (href.StartsWith("/"))
                        return Native.Document.location.href.TakeUntilLastIfAny("/") + href;

                    return Native.Document.location.href + href;
                };


            Native.Document.body.ondragover +=
                evt =>
                {
                    evt.stopPropagation();
                    evt.preventDefault();

                    evt.dataTransfer.dropEffect = "copy"; // Explicitly show this is a copy.
                };

            Native.Document.body.ondrop +=
                evt =>
                {
                    #region dataTransfer
                    evt.dataTransfer.types.WithEach(
                        x =>
                        {
                            Console.WriteLine(x);

                            //SystemSounds.Beep.Play();
                            //Console.Beep();

                            #region text/uri-list
                            if (x == "text/uri-list")
                            {
                                var src = evt.dataTransfer.getData(x);

                                if (src != "about:blank")
                                {
                                    if (src.StartsWith("http://www.youtube.com/watch?v="))
                                        src = "http://www.youtube.com/embed/" + src.SkipUntilIfAny("http://www.youtube.com/watch?v=").TakeUntilIfAny("&");

                                    Console.WriteLine(new { src });


                                    frontcontrol.CreateWindowAndNavigate(src);
                                }
                            }
                            #endregion



                        }
                    );
                    #endregion
                };

            Console.Write("frontcontrol set");

            // http://www.addyosmani.com/resources/googlebox/test.js
            var TILT_BASE = 2.0f;

            var qtext = page.search_text;

            var tilt_cur = TILT_BASE;
            var tilt = TILT_BASE;
            var rot_cur = 1f;
            var rot = 1f;

            var lidAngle = 0f;
            var lidAngleV = 0f;
            var close_ok = false;
            var playing = false;

            var iframe = page.bottom_iframe;

            iframe.Hide();

            var rootTransMatrix = new M44();
            var rootTransNode = new TransformNode(rootTransMatrix);

            rootTransMatrix.rotX(tilt);

            M44 lidTrans1 = (new M44()).translate(0f, 100f, 0f);
            M44 lidTrans2 = new M44();

            var box = new CSSCube(200, 200, 80, rootTransNode);

            var gbox = page.gift_box;
            var gbox_childnodes = gbox.childNodes.ExceptTextNodes();

            for (var i = 0; i < 4; i++)
            {
                CSSFace f = box.getSide(i);
                f.element = gbox_childnodes[7 + i];
                f.backElement = gbox_childnodes[1 + i];
            }


            ((CSSFace)box.getTop()).element = gbox_childnodes[11];
            var frontElement = (IHTMLElement)gbox_childnodes[7];
            //var frontElement_firstChild = (IHTMLElement)frontElement.childNodes.ExceptTextNodes()[0];

            CSSFace bt = box.getBottom();
            bt.element = gbox_childnodes[5];
            bt.preTrans.rotX(Math.PI);
            box.getTop().backElement = gbox_childnodes[6];


            var floorFace = new CSSFace(bt.tNode, 256, 256);
            floorFace.element = gbox_childnodes[0];
            floorFace.N.z = 1f;

            #region updateTransform
            Action updateTransform =
                delegate
                {
                    box.localTrans.rotY(rot_cur);
                    box.localTrans._42 = (80f - box.height) / 2f;

                    rootTransMatrix.rotX(tilt_cur);
                    rootTransMatrix._42 = (80f - box.height) / 2f;

                    floorFace.postTrans.translate(-28, -88 - box.localTrans._42, 0);

                    box.applyTransform();
                    floorFace.applyTransform();
                };


            updateTransform();
            #endregion


            #region resizing
            var resizing = default(Action);

            var resize_handle = page.resize_handle;
            resize_handle.style.cursor = IStyle.CursorEnum.move;

            var prevHandleY = 0f;
            var prevBoxH = 0f;

            resize_handle.onmousedown +=
                e =>
                {
                    e.PreventDefault();
                    e.StopPropagation();

                    resizing = resize_handle.CaptureMouse();
                    //prevHandleY = e.screenY;
                    prevHandleY = e.OffsetY;
                    prevBoxH = box.height;


                };

            resize_handle.onmousemove +=
                e =>
                {
                    if (resizing != null)
                    {
                        e.PreventDefault();
                        e.StopPropagation();

                        var yy = rootTransMatrix._22;
                        if (yy < -0.01 || yy > 0.01)
                        {
                            //var dy = e.screenY - prevHandleY;
                            var dy = e.OffsetY - prevHandleY;

                            var h = prevBoxH - dy / yy;
                            if (h < 20) h = 20;
                            if (h > 200) h = 200;
                            box.changeHeight(h);

                            frontcontrol.Size = new System.Drawing.Size(
                                frontElement.Bounds.Width,
                                frontElement.Bounds.Height
                            );

                            //frontElement_firstChild.style.height = frontElement.style.height;
                            //frontElement_firstChild.style.width = frontElement.style.width;
                            updateTransform();
                        }
                        else
                        {

                            resizing();
                            resizing = null;
                        }
                    }
                };

            resize_handle.onmouseup +=
                e =>
                {
                    if (resizing != null)
                    {
                        resizing();
                        resizing = null;
                    }
                };
            #endregion



            #region intpMotion
            Func<bool> intpMotion =
                delegate
                {
                    var dR = rot_cur - rot;
                    var dT = tilt_cur - tilt;

                    if (dR < 0) dR = -dR;
                    if (dT < 0) dT = -dT;

                    var not_finished = false;
                    if (dR < 0.002)
                        rot_cur = rot;
                    else
                    {
                        not_finished = true;
                        rot_cur = rot_cur * 0.8f + rot * 0.2f;
                    }

                    if (dT < 0.002)
                        tilt_cur = tilt;
                    else
                    {
                        not_finished = true;
                        tilt_cur = tilt_cur * 0.8f + tilt * 0.2f;
                    }

                    return not_finished;

                };
            #endregion

            #region setLidRotate
            Action<float> setLidRotate =
                  a =>
                  {
                      lidTrans2.rotX(-a);
                      lidTrans2._42 = -100;
                      box.getTop().preTrans.mul(lidTrans1, lidTrans2);
                  };
            #endregion


            #region doAnimation
            Action doAnimation = null;

            doAnimation =
                delegate
                {
                    playing = false;
                    if (lidAngle > 0)
                    {
                        playing = true;
                        if (close_ok || lidAngleV > 0)
                            lidAngleV -= 0.01f;

                        lidAngle += lidAngleV;

                        if (lidAngle < 0)
                        {
                            lidAngle = 0;
                            iframe.src = "about:blank";
                            iframe.Hide();
                        }
                        else if (lidAngle >= 1.3f)
                        {
                            lidAngle = 1.3f;
                            playing = false;
                        }
                    }
                    else
                    {
                        lidAngle = 0f;
                        lidAngleV = 0f;
                    }

                    setLidRotate(lidAngle);

                    if ((resizing == null) && intpMotion())
                        playing = true;

                    updateTransform();

                    if (playing)
                    {
                        var _this = this;
                        Native.window.requestAnimationFrame +=
                            delegate
                            {
                                doAnimation();
                            };
                    }

                };
            #endregion


            #region onmousemove
            Native.Document.body.onmousemove +=
              e =>
              {
                  if (control.checkBox1.Checked)
                      return;

                  if (Native.Document.pointerLockElement == Native.Document.body)
                  {
                      rot += e.movementX * 0.01f;
                      tilt += e.movementY * 0.01f;
                  }
                  else
                  {
                      rot = e.CursorX * 0.006f - 0.9f;

                      tilt = (TILT_BASE - e.CursorY * 0.004f);
                  }
                  if (tilt < 0.5f) tilt = 0.5f;

                  //Console.WriteLine(new { rot, tilt }.ToString());

                  if (!playing)
                      doAnimation();
              };
            #endregion






            #region close_button
            page.close_button.onclick +=
              e =>
              {
                  e.StopPropagation();
                  close_ok = true;
                  if (lidAngle > 1.2)
                  {
                      lidAngle -= 0.1f;
                      lidAngleV = 0.01f;
                  }

                  if (!playing)
                      doAnimation();
              };
            #endregion

            #region onSearchClick
            Action onSearchClick =
                delegate
                {
                    lidAngle += 0.01f;
                    lidAngleV = 0.17f;
                    close_ok = false;

                    iframe.Show();

                    iframe.allowFullScreen = true;
                    // Refused to display document because display forbidden by X-Frame-Options.
                    iframe.src = frontcontrol.comboBox1.Text;

                    if (!playing)
                        doAnimation();
                };

            page.search_text.onkeydown +=
             e =>
             {
                 if (e.KeyCode == 13)
                 {
                     onSearchClick();
                 }
             };

            page.search_button.onclick +=
                e =>
                {
                    onSearchClick();
                };
            #endregion


            page.search_fullscreen.onmousedown +=
                e =>
                {
                    e.preventDefault();

                    if (e.MouseButton != IEvent.MouseButtonEnum.Left)
                        Native.Document.body.requestPointerLock();
                };

            Native.Document.body.onmouseup +=
              delegate
              {
                  Native.Document.exitPointerLock();
              };

            page.search_fullscreen.onclick +=
                delegate
                {
                    page.box_label.requestFullscreen();
                };


            {

                var ContainerShadow = new IHTMLDiv().AttachTo(page.box_label_container);

                ContainerShadow.style.position = IStyle.PositionEnum.absolute;
                ContainerShadow.style.left = "0px";
                ContainerShadow.style.top = "0px";
                ContainerShadow.style.right = "0px";
                ContainerShadow.style.bottom = "0px";

                var Container = new IHTMLDiv().AttachTo(page.box_label_container);

                Container.style.position = IStyle.PositionEnum.absolute;
                Container.style.left = "0px";
                Container.style.top = "0px";
                Container.style.right = "0px";
                Container.style.bottom = "0px";

                control.AttachControlTo(Container);
                control.AutoSizeControlTo(ContainerShadow);
            }


            {

                var ContainerShadow = new IHTMLDiv().AttachTo(page.front_panel);

                ContainerShadow.style.position = IStyle.PositionEnum.absolute;
                ContainerShadow.style.left = "0px";
                ContainerShadow.style.top = "0px";
                ContainerShadow.style.right = "0px";
                ContainerShadow.style.bottom = "0px";

                var Container = new IHTMLDiv().AttachTo(page.front_panel);

                Container.style.position = IStyle.PositionEnum.absolute;
                Container.style.left = "0px";
                Container.style.top = "0px";
                Container.style.right = "0px";
                Container.style.bottom = "0px";

                frontcontrol.AttachControlTo(Container);
                //frontcontrol.AutoSizeControlTo(ContainerShadow);
                frontcontrol.Width = 200;
                frontcontrol.Height = 80;

                frontcontrol.button1.Click +=
                    delegate
                    {
                        onSearchClick();
                    };

                resize_handle.Orphanize().AttachTo(page.front_panel);


                var once = false;

                frontcontrol.NewForm +=
                    f =>
                    {
                        if (once)
                        {
#if DOESITWORK
                            Abstractatech.JavaScript.FormAsPopup.FormAsPopupExtensions.PopupInsteadOfClosing(f);
#endif

                            return;
                        }

                        once = true;

                        //f.DisableFormClosingHandler = true;

                        global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
                            f
                        );
                    };
            }

        }

    }


    sealed class __WebGLLesson10Application
    {
        public __WebGLLesson10Application(global::WebGLLesson10.HTML.Pages.IDefault page)
        {
            new global::WebGLLesson10.Application(page);
        }
    }

    sealed class __WebGLSpiral
    {
        public __WebGLSpiral(global::WebGLSpiral.HTML.Pages.IDefault page)
        {
            new global::WebGLSpiral.Application(page);
        }
    }

    sealed class __ImpAdventures
    {
        public __ImpAdventures(global::ImpAdventures.HTML.Pages.IDefault page)
        {
            // did you know we will be binding to keyboard?
            new global::ImpAdventures.Application(page);
        }
    }

    sealed class __IsometricTycoonViewWithToolbar
    {
        public __IsometricTycoonViewWithToolbar(global::IsometricTycoonViewWithToolbar.HTML.Pages.IDefault page)
        {
            new global::IsometricTycoonViewWithToolbar.Application(page);
        }
    }

    sealed class __McKrackenFirstRoom
    {
        public __McKrackenFirstRoom(global::McKrackenFirstRoom.HTML.Pages.IDefault page)
        {
            new global::McKrackenFirstRoom.Application(page);
        }
    }

    //sealed class __AvalonUgh
    //{
    //    public __AvalonUgh(global::AvalonUgh.LabsActivity.HTML.Pages.IDefault page)
    //    {
    //        new global::AvalonUgh.LabsActivity.Application(page);
    //    }
    //}

    //sealed class __AvalonTycoonMansion
    //{
    //    public __AvalonTycoonMansion(global::AvalonTycoonMansion.iPad.HTML.Pages.IDefault page)
    //    {
    //        new global::AvalonTycoonMansion.iPad.ApplicationContent();
    //    }
    //}

    //sealed class __JavaDosBoxQuakeBeta
    //{
    //    public __JavaDosBoxQuakeBeta(global::JavaDosBoxQuakeBeta.HTML.Pages.IDefault page)
    //    {
    //        new global::JavaDosBoxQuakeBeta.Application(page);
    //    }
    //}

    //sealed class __Boing4KTemplate
    //{
    //    public __Boing4KTemplate(global::Boing4KTemplate.HTML.Pages.IDefault page)
    //    {
    //        new global::Boing4KTemplate.Application(page);
    //    }
    //}

    //sealed class __RayCasterApplet
    //{
    //    public __RayCasterApplet(global::RayCasterApplet.HTML.Pages.IDefault page)
    //    {
    //        new global::RayCasterApplet.Application(page);
    //    }
    //}

    //sealed class __FlashCamera
    //{
    //    public __FlashCamera(global::FlashCamera.HTML.Pages.IDefault page)
    //    {
    //        new global::FlashCamera.Application(page);
    //    }
    //}
}
