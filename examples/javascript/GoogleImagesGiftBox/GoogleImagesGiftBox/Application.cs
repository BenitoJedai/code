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
using GoogleImagesGiftBox.HTML.Pages;
using GoogleImagesGiftBox.Design;

namespace GoogleImagesGiftBox
{
    static class X
    {
        public static INode[] ExceptTextNodes(this INode[] e)
        {
            return e.Where(k => k.nodeType != INode.NodeTypeEnum.TextNode).ToArray();
        }
    }
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // http://www.addyosmani.com/resources/googlebox/

            new trans3d().Content.AttachToDocument().onload +=
                delegate
                {
                    InitializeContent(page);
                };


            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

        #region trans3d
        [Script(HasNoPrototype = true, ExternalTarget = "M44")]
        public class M44
        {
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

        void InitializeContent(IDefaultPage page)
        {
            // http://www.addyosmani.com/resources/googlebox/test.js
            var TILT_BASE = 2.0f;

            #region resizing
            var resizing = false;

            var resize_handle = page.resize_handle;

            resize_handle.onmousedown +=
                e =>
                {
                    resizing = true;
                    //this.prevHandleY = e.screenY;
                    //this.prevBoxH = this.box.height;
                };

            Native.Document.onmousemove +=
                e =>
                {
                    if (resizing)
                    {
                        //    var yy = this.rootTransMatrix._22;
                        //    if (yy < -0.01 || yy > 0.01)
                        //    {
                        //        var dy = e.screenY - this.prevHandleY;

                        //        var h = this.prevBoxH - dy / yy;
                        //        if (h < 20) h = 20;
                        //        if (h > 200) h = 200;
                        //        this.box.changeHeight(h);

                        //        this.frontElement.firstChild.style.height = this.frontElement.style.height;
                        //        this.frontElement.firstChild.style.width = this.frontElement.style.width;
                        //        this.updateTransform();
                        //    }
                        //    else
                        //        this.resizing = false;
                    }
                };

            Native.Document.onmouseup +=
                e =>
                {
                    resizing = false;
                };
            #endregion


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
            var frontElement = gbox_childnodes[7];

            CSSFace bt = box.getBottom();
            bt.element = gbox_childnodes[5];
            bt.preTrans.rotX(Math.PI);
            box.getTop().backElement = gbox_childnodes[6];


            var floorFace = new CSSFace(bt.tNode, 256, 256);
            floorFace.element = gbox_childnodes[0];
            floorFace.N.z = 1f;

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
                            //iframe[0].setAttribute('src', "about:blank");
                            //iframe.hide();
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

                    //setLidRotate(lidAngle);

                    if (!resizing && intpMotion())
                        playing = true;

                    updateTransform();

                    if (playing)
                    {
                        var _this = this;
                        Native.Window.requestAnimationFrame +=
                            delegate
                            {
                                doAnimation();
                            };
                    }

                };
            #endregion


            Native.Document.body.onmousemove +=
              e =>
              {

                  rot = e.CursorX * 0.006f - 0.9f;

                  tilt = (TILT_BASE - e.CursorY * 0.004f);
                  if (tilt < 0.5f) tilt = 0.5f;

                  //Console.WriteLine(new { rot, tilt }.ToString());

                  if (!playing)
                      doAnimation();
              };





            Action<float> setLidRotate =
                a =>
                {
                    //this.lidTrans2.rotX(-a);
                    //this.lidTrans2._42 = -100;
                    //this.box.getTop().preTrans.mul(this.lidTrans1, this.lidTrans2);
                };

            #region buttons
            page.close_button.onclick +=
              e =>
              {
                  //e.stopPropagation();
                  //this.close_ok = true;
                  //if (this.lidAngle > 1.2)
                  //{
                  //    this.lidAngle -= 0.1;
                  //    this.lidAngleV = 0.01;
                  //}

                  //if (!this.playing)
                  //    this.doAnimation();
              };

            page.search_text.onkeydown +=
             e =>
             {
                 //if (e.keyCode == 13)
                 //{
                 //    this.onSearchClick();
                 //}
             };

            page.search_button.onclick +=
                e =>
                {
                    lidAngle += 0.01f;
                    lidAngleV = 0.17f;
                    close_ok = false;

                    //this.iframe.show();
                    //this.iframe[0].setAttribute('src', "http://images.google.com/m/search?q="+this.qtext.val());

                    if (!playing)
                        doAnimation();
                };
            #endregion


        }

    }
}
