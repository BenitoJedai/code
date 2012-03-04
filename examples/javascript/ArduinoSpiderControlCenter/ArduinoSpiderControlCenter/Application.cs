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
using ArduinoSpiderControlCenter.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace ArduinoSpiderControlCenter
{
    /// <summary>
    /// This type will run as JavaScript.
    /// </summary>
    internal sealed class Application
    {
        // could we make use of a tab/ipad later?

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            var SpiderModelContent = new SpiderModel.ApplicationContent();




            @"Hello world".ToDocumentTitle();

            var LeftLR = new IHTMLDiv();

            LeftLR.style.position = IStyle.PositionEnum.absolute;
            LeftLR.style.left = "0";
            LeftLR.style.top = "0";
            LeftLR.style.bottom = "0";
            LeftLR.style.width = "4em";
            LeftLR.style.Opacity = 0.5;
            LeftLR.AttachToDocument();

            var LeftIR = new IHTMLDiv();

            LeftIR.style.position = IStyle.PositionEnum.absolute;
            LeftIR.style.left = "0";
            LeftIR.style.top = "0";
            LeftIR.style.height = "1em";
            LeftIR.style.width = "4em";
            LeftIR.style.Opacity = 0.8;
            LeftIR.AttachToDocument();
            LeftIR.style.backgroundColor = JSColor.FromRGB(0xB0, 0, 0);



            var RightLR = new IHTMLDiv();

            RightLR.style.position = IStyle.PositionEnum.absolute;
            RightLR.style.right = "0";
            RightLR.style.top = "0";
            RightLR.style.bottom = "0";
            RightLR.style.Opacity = 0.5;
            RightLR.style.width = "4em";
            RightLR.AttachToDocument();


            var RightIR = new IHTMLDiv();

            RightIR.style.position = IStyle.PositionEnum.absolute;
            RightIR.style.right = "0";
            RightIR.style.top = "0";
            RightIR.style.height = "1em";
            RightIR.style.width = "4em";
            RightIR.style.Opacity = 0.8;
            RightIR.AttachToDocument();
            RightIR.style.backgroundColor = JSColor.FromRGB(0xB0, 0, 0);



            LeftLR.style.backgroundColor = JSColor.FromRGB(0x80, 0, 0);
            RightLR.style.backgroundColor = JSColor.FromRGB(0x80, 0, 0);

            page.PageContainer.AttachToDocument();
            page.PageContainer.style.color = JSColor.White;
            page.PageContainer.style.textShadow = "#000 0px 0px 3px";
            page.ElShadow.style.textShadow = "#000 0px 0px 3px";

            #region AtResize
            Action AtResize = delegate
            {
                page.PageContainer.style.SetLocation(0, 0, Native.Window.Width, Native.Window.Height);


            };

            AtResize();

            Native.Window.onresize += delegate { AtResize(); };
            #endregion

            var t = new Timer(
                delegate
                {
                    Native.Document.body.style.cursor = IStyle.CursorEnum.wait;
                }
            );

            Action poll = null;

            poll = delegate
                {
                    t.StartTimeout(400);

                    // Send data from JavaScript to the server tier
                    service.WebMethod2(
                        @"A string from JavaScript.",
                        COM46_Line =>
                        {
                            Native.Document.body.style.cursor = IStyle.CursorEnum.@default;
                            t.Stop();

                            page.Content.innerText = COM46_Line;

                            // jsc: why string.split with each not working??

                            var a = COM46_Line.Split(';');

                            byte RightLR_value = 0;
                            byte LeftLR_value = 0;

                            #region parse RightLR, LeftLS, LeftIR, RightIR
                            for (int i = 0; i < a.Length; i++)
                            {
                                var u = a[i];

                                u.TakeUntilOrEmpty(":").Trim().With(
                                    key =>
                                    {
                                        var _value = u.SkipUntilOrEmpty(":").Trim();

                                        // 1024 is dark


                                        #region RightLR
                                        if (key == "RightLR")
                                        {

                                            var value_int32 = int.Parse(_value);
                                            var value_1024 = (1024 - Math.Min(int.Parse(_value), 1024));

                                            // jsc: please do the masking when casting to byte yyourself, thanks :)
                                            RightLR_value = (byte)((255 * value_1024 / 1024) & 0xff);
                                            RightLR_value = (byte)Math.Min(255, RightLR_value * 2);

                                            if (RightLR_value == 255)
                                                RightLR.style.backgroundColor = JSColor.Cyan;
                                            else
                                                RightLR.style.backgroundColor = JSColor.FromGray(RightLR_value);

                                        }
                                        #endregion

                                        #region LeftLS
                                        if (key == "LeftLS")
                                        {

                                            var value_int32 = int.Parse(_value);
                                            var value_1024 = (1024 - Math.Min(int.Parse(_value), 1024));

                                            // jsc: please do the masking when casting to byte yyourself, thanks :)
                                            LeftLR_value = (byte)((255 * value_1024 / 1024) & 0xff);
                                            LeftLR_value = (byte)Math.Min(255, LeftLR_value * 2);

                                            //LeftLR.innerText = "" + ivalue;

                                            if (LeftLR_value == 255)
                                                LeftLR.style.backgroundColor = JSColor.Cyan;
                                            else
                                                LeftLR.style.backgroundColor = JSColor.FromGray(LeftLR_value);

                                        }
                                        #endregion

                                        #region LeftIR
                                        if (key == "LeftIR")
                                        {

                                            var value_int32 = int.Parse(_value);

                                            if (value_int32 > 400)
                                                LeftIR.style.backgroundColor = JSColor.Red;
                                            else
                                                if (value_int32 > 200)
                                                    LeftIR.style.backgroundColor = JSColor.Yellow;
                                                else
                                                    LeftIR.style.backgroundColor = JSColor.Green;

                                            LeftIR.style.height = value_int32 + "px";

                                            SpiderModelContent.tween_red_obstacle_L_y((1 - value_int32 / 600) * 24);
                                        }
                                        #endregion

                                        #region RightIR
                                        if (key == "RightIR")
                                        {

                                            var value_int32 = int.Parse(_value);

                                            if (value_int32 > 400)
                                                RightIR.style.backgroundColor = JSColor.Red;
                                            else
                                                if (value_int32 > 200)
                                                    RightIR.style.backgroundColor = JSColor.Yellow;
                                                else
                                                    RightIR.style.backgroundColor = JSColor.Green;



                                            RightIR.style.height = value_int32 + "px";
                                            SpiderModelContent.tween_red_obstacle_R_y((1 - value_int32 / 600) * 24);

                                        }
                                        #endregion

                                    }
                                );
                            }
                            #endregion

                            #region next
                            new Timer(
                               delegate
                               {
                                   Native.Window.requestAnimationFrame += poll;
                               }
                           ).StartTimeout(200);
                            #endregion


                            // dark 70 .. 255 bright

                            SpiderModelContent.tween_white_arrow_y(
                                50 * (1 - ((Math.Max(LeftLR_value, RightLR_value) - 70) / (255 - 70)))
                            );

                            SpiderModelContent.tween_white_arrow_x(
                                (LeftLR_value - 60) * -20f / (255 - 60)
                            + (RightLR_value - 60) * 20f / (255 - 60)
                            );

                            Native.Document.title = LeftLR_value + " " + RightLR_value;
                        }
                    );
                };

            Native.Window.requestAnimationFrame += poll;

        }

    }
}
