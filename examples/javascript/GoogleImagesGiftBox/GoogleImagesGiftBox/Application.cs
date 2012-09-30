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

            Action intpMotion =
                delegate
                { 
                
                
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

            Native.Document.onmousemove +=
                e =>
                {
                };

            page.close_button.onclick +=
              e =>
              {
              };

            page.search_text.onkeydown +=
             e =>
             {

             };

            page.search_button.onclick +=
                e =>
                {

                };

        }

    }
}
