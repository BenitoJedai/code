// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using TextboxAutoSize.HTML.Pages;
using TextboxAutoSize;

namespace TextboxAutoSize
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            Func<string> GetText1 = () => page.Text1.value;

            page.Text1.onfocus += delegate
            {
                GetText1 = () => page.Text1.value;
            };
            page.Text1AutoSize.onfocus += delegate
            {
                GetText1 = () => page.Text1AutoSize.value;
            };

            Func<string> GetText2 = () => page.Text2.value;


            page.Text2.onfocus += delegate
            {
                GetText2 = () => page.Text2.value;
            };
            page.Text2AutoSize.onfocus += delegate
            {
                GetText2 = () => page.Text2AutoSize.value;
            };


            Action Update =
                delegate
                {
                    var FontFamily = (IStyle.FontFamilyEnum)(object)page.FontFamily.GetSelectionText();

                    var u = new IHTMLElement[]
                    {
                        page.Text1,
                        page.Text1Shadow,
                        page.Text1AutoSize,
                        page.Text2,
                        page.Text2Shadow,
                        page.Text2AutoSize,
                    };

                    foreach (var item in u)
                    {
                        item.style.padding = "0px";
                        item.style.margin = "0px";
                        item.style.borderWidth = "0px";
                        item.style.fontFamily = FontFamily;
                        item.style.fontSize = page.FontSize.GetSelectionText() + page.FontSizeUnit.GetSelectionText();
                        item.style.color = page.Foreground.GetSelectionText();
                        item.style.backgroundColor = page.Background.GetSelectionText();
                        item.style.lineHeight = page.LineHeight.GetSelectionText();
                        item.style.whiteSpace = IStyle.WhiteSpaceEnum.pre;
                        item.style.display = IStyle.DisplayEnum.inline_block;
                        item.style.overflow = IStyle.OverflowEnum.hidden;
                        item.style.resize = "none";



                    }

                    page.Text2.wrap = "off";
                    page.Text2AutoSize.wrap = "off";

                    var Shadows = new IHTMLElement[]
                    {
                         page.Text2ShadowContainer,
                          page.Text1ShadowContainer
                    };

                    Shadows.WithEach(
                        k =>
                        {
                            if (page.HideShadow.@checked)
                            {
                                //page.Text2ShadowContainer.style.display = IStyle.DisplayEnum.none;

                                k.style.position = IStyle.PositionEnum.absolute;
                                k.style.overflow = IStyle.OverflowEnum.hidden;
                                k.style.width = "0px";
                                k.style.height = "0px";
                            }   
                            else
                            {   
                                k.style.display = IStyle.DisplayEnum.empty;
                                k.style.position = IStyle.PositionEnum.relative;
                                k.style.overflow = IStyle.OverflowEnum.auto;
                                k.style.width = "";
                                k.style.height = "";
                            }
                        }
                    );

                    page.Text1Shadow.innerText = GetText1();

                    GetText2().Replace("\r", "").With(
                        value =>
                        {
                            if (value.EndsWith("\n"))
                            {
                                // span seems to loose the last \n
                                page.Text2Shadow.innerText = value + "\n";
                            }
                            else
                            {
                                page.Text2Shadow.innerText = value;
                            }

                        }
                    );

                    page.Text1AutoSize.style.SetSize(page.Text1Shadow.scrollWidth, page.Text1Shadow.scrollHeight);
                    page.Text2AutoSize.style.SetSize(page.Text2Shadow.scrollWidth, page.Text2Shadow.scrollHeight);

                    page.Text1AutoSize.value = GetText1();
                    page.Text2AutoSize.value = GetText2();

                    page.Text1.value = GetText1();
                    page.Text2.value = GetText2();
                };

            page.FontFamily.onchange += delegate { Update(); };
            page.FontSize.onchange += delegate { Update(); };
            page.FontSizeUnit.onchange += delegate { Update(); };
            page.LineHeight.onchange += delegate { Update(); };


            page.FontSizeUnit.onchange += delegate { Update(); };
            page.Foreground.onchange += delegate { Update(); };
            page.Background.onchange += delegate { Update(); };
            page.HideShadow.onchange += delegate { Update(); };

            page.Text1.onkeyup += delegate { Update(); };
            page.Text1.onchange += delegate { Update(); };
            page.Text2.onkeyup += delegate { Update(); };
            page.Text2.onchange += delegate { Update(); };

            page.Text1AutoSize.onkeyup += delegate { Update(); };
            page.Text1AutoSize.onchange += delegate { Update(); };
            page.Text2AutoSize.onkeyup += delegate { Update(); };
            page.Text2AutoSize.onchange += delegate { Update(); };


            Update();
            @"Hello world".ToDocumentTitle();
            new ApplicationWebService().WebMethod2(
                new XElement(@"Document",
                    new object[] {
						new XElement(@"Data", 
							new object[] {
								@"Hello world"
							}
						),
						new XElement(@"Client", 
							new object[] {
								@"Unchanged text"
							}
						)
					}
                ),
                delegate(XElement doc)
                {
                    doc.Element(@"Data").Value.ToDocumentTitle();
                }
            );
        }

    }
}
