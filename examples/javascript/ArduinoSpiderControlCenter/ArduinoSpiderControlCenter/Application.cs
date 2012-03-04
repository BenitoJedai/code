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

            page.PageContainer.AttachToDocument();
            page.PageContainer.style.color = JSColor.White;

            @"Hello world".ToDocumentTitle();

            var LeftLR = new IHTMLDiv();

            LeftLR.style.position = IStyle.PositionEnum.absolute;
            LeftLR.style.left = "0";
            LeftLR.style.top = "0";
            LeftLR.style.bottom = "0";
            LeftLR.style.width = "4em";
            LeftLR.AttachToDocument();

            var LeftIR = new IHTMLDiv();

            LeftIR.style.position = IStyle.PositionEnum.absolute;
            LeftIR.style.left = "0";
            LeftIR.style.top = "0";
            LeftIR.style.height = "1em";
            LeftIR.style.width = "4em";
            LeftIR.AttachToDocument();
            LeftIR.style.backgroundColor = JSColor.FromRGB(0xB0, 0, 0);



            var RightLR = new IHTMLDiv();

            RightLR.style.position = IStyle.PositionEnum.absolute;
            RightLR.style.right = "0";
            RightLR.style.top = "0";
            RightLR.style.bottom = "0";
            RightLR.style.width = "4em";
            RightLR.AttachToDocument();


            var RightIR = new IHTMLDiv();

            RightIR.style.position = IStyle.PositionEnum.absolute;
            RightIR.style.right = "0";
            RightIR.style.top = "0";
            RightIR.style.height = "1em";
            RightIR.style.width = "4em";
            RightIR.AttachToDocument();
            RightIR.style.backgroundColor = JSColor.FromRGB(0xB0, 0, 0);



            LeftLR.style.backgroundColor = JSColor.FromRGB(0x80, 0, 0);
            RightLR.style.backgroundColor = JSColor.FromRGB(0x80, 0, 0);

            new Timer(
                t =>
                {
                    Native.Document.title = "#" + t.Counter;

                    // Send data from JavaScript to the server tier
                    service.WebMethod2(
                        @"A string from JavaScript.",
                        COM46_Line =>
                        {
                            page.Content.innerText = COM46_Line;

                            // jsc: why string.split with each not working??

                            var a = COM46_Line.Split(';');

                            for (int i = 0; i < a.Length; i++)
                            {
                                var u = a[i];

                                u.TakeUntilOrEmpty(":").Trim().With(
                                    key =>
                                    {
                                        var _value = u.SkipUntilOrEmpty(":").Trim();

                                        // 1024 is dark



                                        if (key == "RightLR")
                                        {

                                            var value_int32 = int.Parse(_value);
                                            var value_1024 = (1024 - Math.Min(int.Parse(_value), 1024));

                                            // jsc: please do the masking when casting to byte yyourself, thanks :)
                                            var ivalue = (byte)((255 * value_1024 / 1024) & 0xff);

                                            ivalue = (byte)Math.Min(255, ivalue * 2);

                                            if (ivalue == 255)
                                                RightLR.style.backgroundColor = JSColor.Cyan;
                                            else
                                                RightLR.style.backgroundColor = JSColor.FromGray(ivalue);

                                        }

                                        if (key == "LeftLS")
                                        {

                                            var value_int32 = int.Parse(_value);
                                            var value_1024 = (1024 - Math.Min(int.Parse(_value), 1024));

                                            // jsc: please do the masking when casting to byte yyourself, thanks :)
                                            var ivalue = (byte)((255 * value_1024 / 1024) & 0xff);

                                            ivalue = (byte)Math.Min(255, ivalue * 2);

                                            //LeftLR.innerText = "" + ivalue;

                                            if (ivalue == 255)
                                                LeftLR.style.backgroundColor = JSColor.Cyan;
                                            else
                                                LeftLR.style.backgroundColor = JSColor.FromGray(ivalue);

                                        }

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

                                        }


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

                                        }

                                    }
                                );
                            }

                        }
                    );
                }
            ).StartInterval();

        }

    }
}
