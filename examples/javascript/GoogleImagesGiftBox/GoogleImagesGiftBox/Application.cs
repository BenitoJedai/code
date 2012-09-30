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


        void InitializeContent(IDefaultPage page)
        {
            // http://www.addyosmani.com/resources/googlebox/test.js
            var TILT_BASE = 2.0;

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
            var rot_cur = 0;
            var rot = 0;

            var lidAngle = 0;
            var lidAngleV = 0;
            var close_ok = false;
            var playing = false;

            var iframe = page.bottom_iframe;
            
            iframe.Hide();

            //this.rootTransMatrix = new M44();
            //this.rootTransNode = new TransformNode(this.rootTransMatrix);

            //this.rootTransMatrix.rotX(this.tilt);

            //this.lidTrans1 = (new M44()).translate(0, 100, 0);
            //this.lidTrans2 = new M44();

            //this.box = new CSSCube(200, 200, 80, this.rootTransNode);

            var gbox = page.gift_box;
            //var i;

            //for (i = 0;i < 4;i++) {
            //    var f = this.box.getSide(i);
            //    f.element = gbox.childNodes[7+i];
            //    f.backElement = gbox.childNodes[1+i];
            //}
            //this.box.getTop().element = gbox.childNodes[11];
            //this.frontElement = gbox.childNodes[7];

            //var bt = this.box.getBottom();
            //bt.element = gbox.childNodes[5];
            //bt.preTrans.rotX(Math.PI);
            //this.box.getTop().backElement = gbox.childNodes[6];


            //this.floorFace = new CSSFace(bt.tNode, 256, 256);
            //this.floorFace.element = gbox.childNodes[0];
            //this.floorFace.N.z = 1;

            Action updateTransform =
     delegate
     {
         //this.box.localTrans.rotY(this.rot_cur);
         //this.box.localTrans._42 = (80 - this.box.height) / 2;
         //this.rootTransMatrix.rotX(this.tilt_cur);
         //this.rootTransMatrix._42 = (80 - this.box.height) / 2;
         //this.floorFace.postTrans.translate(-28, -88 - this.box.localTrans._42, 0);

         //this.box.applyTransform();
         //this.floorFace.applyTransform();
     };


            //this.updateTransform();

            Action intpMotion =
                delegate
                {
                    //var dR = this.rot_cur - this.rot;
                    //var dT = this.tilt_cur - this.tilt;

                    //if (dR < 0) dR = -dR;
                    //if (dT < 0) dT = -dT;

                    //var not_finished = false;
                    //if (dR < 0.002)
                    //    this.rot_cur = this.rot;
                    //else
                    //{
                    //    not_finished = true;
                    //    this.rot_cur = this.rot_cur * 0.8 + this.rot * 0.2;
                    //}

                    //if (dT < 0.002)
                    //    this.tilt_cur = this.tilt;
                    //else
                    //{
                    //    not_finished = true;
                    //    this.tilt_cur = this.tilt_cur * 0.8 + this.tilt * 0.2;
                    //}

                    //return not_finished;

                };

            Action doAnimation =
                delegate
                {
                    //        this.playing = false;
                    //if (this.lidAngle > 0) {
                    //    this.playing = true;
                    //    if (this.close_ok || this.lidAngleV > 0)
                    //        this.lidAngleV -= 0.01;

                    //    this.lidAngle += this.lidAngleV;

                    //    if (this.lidAngle < 0) {
                    //        this.lidAngle = 0;
                    //        this.iframe[0].setAttribute('src', "about:blank");
                    //        this.iframe.hide();
                    //    }
                    //    else if (this.lidAngle >= 1.3) {
                    //        this.lidAngle = 1.3;
                    //        this.playing = false;
                    //    }
                    //}
                    //else {
                    //    this.lidAngle  = 0;
                    //    this.lidAngleV = 0;
                    //}

                    //this.setLidRotate(this.lidAngle);

                    //if (!this.resizing && this.intpMotion())
                    //    this.playing = true;

                    //this.updateTransform();

                    //if (this.playing) {
                    //    var _this = this;
                    //    setTimeout(function(){_this.doAnimation()}, 10);
                    //}

                };

 




            Action<float> setLidRotate =
                a =>
                {
                    //this.lidTrans2.rotX(-a);
                    //this.lidTrans2._42 = -100;
                    //this.box.getTop().preTrans.mul(this.lidTrans1, this.lidTrans2);
                };

            Native.Document.onmousemove +=
                e =>
                {
                    //if (e.pageX >= 0 && e.pageY)
                    //{
                    //    this.rot = e.pageX * 0.006 - 0.9;

                    //    this.tilt = (TILT_BASE - e.pageY * 0.004);
                    //    if (this.tilt < 0.5) this.tilt = 0.5;

                    //    if (!this.playing)
                    //        this.doAnimation();
                    //}
                };

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
                    //                    this.lidAngle += 0.01;
                    //this.lidAngleV = 0.17;
                    //this.close_ok = false;

                    //this.iframe.show();
                    //this.iframe[0].setAttribute('src', "http://images.google.com/m/search?q="+this.qtext.val());

                    //if (!this.playing)
                    //    this.doAnimation();
                };

        }

    }
}
