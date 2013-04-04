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
using My.Solutions.Pages.Templates.Design;
using My.Solutions.Pages.Templates.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Avalon.Tween;
using ScriptCoreLib.Shared.Lambda;

namespace My.Solutions.Pages.Templates
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
        public Application(IDefaultPage page = null)
        {
            var h = Native.Document.location.hash;


            // are we running as a clone?
            // if so our location hash should be stored as html.

            Native.Document.getElementsByTagName("link").Select(k => (IHTMLLink)k).Where(k => k.rel == "location").ToList().ForEach(
                location =>
                {
                    //  href = file:///X:/temp/Spiral.htm#WebGLSpiral 

                    location.href.SkipUntilLastOrEmpty("#").With(
                        href =>
                        {
                            if (h == "")
                            {
                                h = "#" + href;
                            }

                        }
                    );
                }
            );

            Console.WriteLine("Templates loaded... " + new { Native.Document.location, h });

            //{
            //    var html = new global::wolfenstein4kTemplate.HTML.Pages.DefaultPage();
            //    html.Container.AttachToDocument();
            //    new global::wolfenstein4kTemplate.Application(html);
            //}

            Action LoadEmAll = delegate { };

            var cc = new IHTMLCenter().AttachToDocument();
            cc.style.lineHeight = "0px";



            var cutoff = 480;

            #region iframe
            Action<int, int, double, IHTMLImage, string> iframe = (iwidth, iheight, maxextra, preview, alias) =>
            {
                // media selector?
                if (alias == "")
                {

                }

                var c = new IHTMLDiv().AttachTo(cc);

                //c.style.backgroundColor = JSColor.Black;
                c.style.display = IStyle.DisplayEnum.inline_block;
                c.style.position = IStyle.PositionEnum.relative;
                c.style.SetSize(iwidth, iheight);

                IHTMLElement content = preview;

                preview.style.SetLocation(0, 0);
                preview.AttachTo(c);
                preview.style.cursor = IStyle.CursorEnum.pointer;
                preview.title = "Click to activate, doubleclick to enlarge";

                var HasFocus = false;
                var HasMouse = false;


                Action content_SetExtraSize = delegate
                {

                };
                #region SetExtraSize
                var SetExtraSize = NumericEmitter.OfDouble(
                  (extrasizef, yy) =>
                  {

                      var extrasize = System.Convert.ToInt32(iwidth * extrasizef * 0.5);


                      content_SetExtraSize = delegate
                      {
                          content.style.zIndex = extrasize;


                          if (HasFocus)
                              new IFunction("this.style.boxShadow = '0px 0px " + (extrasize) + "px 0px rgba(255, 255, 0, 1)';").apply(content);
                          else
                              new IFunction("this.style.boxShadow = '0px 0px " + (extrasize) + "px 0px rgba(0, 0, 255, 1)';").apply(content);
                          //box-shadow: 0px 0px 70px 0px rgba(0, 0, 0, 1);

                          content.style.SetLocation(
                                System.Convert.ToInt32(iwidth * extrasizef * -0.5),
                              System.Convert.ToInt32(iheight * extrasizef * -0.5),
                              System.Convert.ToInt32(iwidth * (1 + extrasizef)),
                              System.Convert.ToInt32(iheight * (1 + extrasizef))
                          );
                      };

                      content_SetExtraSize();
                  }
              );
                #endregion


                #region HasMouse
                c.onmouseover +=
                 delegate
                 {
                     HasMouse = true;

                     if (HasFocus)
                         return;

                     SetExtraSize(maxextra, 0);
                 };




                c.onmouseout +=
                    delegate
                    {
                        HasMouse = false;

                        if (HasFocus)
                            return;

                        SetExtraSize(0, 0);
                    };
                #endregion


                #region Activate
                Action Activate =
                    delegate
                    {
                        // can we also do groups?

                        //preview.Orphanize();

                        var a = new IHTMLIFrame
                        {
                            width = iwidth,
                            height = iheight,
                            frameBorder = "0",
                            scrolling = "no",
                            allowFullScreen = true
                        }.AttachTo(c);

                        preview.style.Opacity = 0.7;


                        a.style.SetLocation(0, 0);



                        #region HasFocus
                        a.onload +=
                            delegate
                            {
                                //Native.Window.alert(
                                //    new {

                                //        a.contentWindow.performance.timing.connectStart,
                                //        a.contentWindow.performance.timing.loadEventStart,
                                //    }
                                //);

                                a.contentWindow.onblur +=
                                    delegate
                                    {
                                        HasFocus = false;
                                        if (HasMouse)
                                            SetExtraSize(maxextra * 1, 0);
                                        else
                                            SetExtraSize(0, 0);
                                    };

                                a.contentWindow.onfocus +=
                                    delegate
                                    {
                                        HasFocus = true;
                                        SetExtraSize(maxextra * 1.1, 0);
                                    };

                                if (HasMouse)
                                    a.contentWindow.focus();

                                new Timer(
                                    delegate
                                    {
                                        preview.FadeOut();

                                        new Timer(
                                          delegate
                                          {

                                              content = a;
                                              content_SetExtraSize();
                                          }
                                      ).StartTimeout(300);
                                    }
                                ).StartTimeout(1000);


                            };
                        #endregion



                        a.contentWindow.document.location.replace(alias);

                        //a.tabIndex = 1;





                    };

                LoadEmAll +=
                    delegate
                    {
                        if (Activate == null)
                            return;

                        Activate();
                        Activate = null;
                    };

                c.onclick +=
                    delegate
                    {
                        if (Activate == null)
                            return;

                        Activate();
                        Activate = null;

                        // unsubscribe event;
                    };
                #endregion






                SetExtraSize(0, 0);

            };
            #endregion



            #region y
            Action<string, Action, IHTMLImage> y =
                (alias, yield, preview) =>
                {
                    if (h == "")
                        if (Native.Window.Width > cutoff)
                            iframe(96, 96, 3, preview, alias);
                        else
                            iframe(32, 32, 3, preview, alias);
                    else if (h == alias)
                        if (yield != null)
                            yield();
                };
            #endregion

            // jsc cannot handle multiple delegates on the same statement yet.

            y("#WebGLDopamineMolecule", () => new WebGLDopamineMolecule.Application(), new WebGLDopamineMolecule.HTML.Images.FromAssets.Preview());
            y("#WebGLEthanolMolecule", () => new WebGLEthanolMolecule.Application(), new WebGLEthanolMolecule.HTML.Images.FromAssets.Preview());
            y("#WebGLYomotsuTPS", () => new WebGLYomotsuTPS.Application(), new WebGLYomotsuTPS.HTML.Images.FromAssets.Preview());
            y("#WebGLYomotsuMD2Model", () => new WebGLYomotsuMD2Model.Application(), new WebGLYomotsuMD2Model.HTML.Images.FromAssets.Preview());
            y("#WebGLSphereRayTrace", () => new WebGLSphereRayTrace.Application(), new WebGLSphereRayTrace.HTML.Images.FromAssets.Preview());
            y("#WebGLFireballExplosion", () => new WebGLFireballExplosion.Application(), new WebGLFireballExplosion.HTML.Images.FromAssets.Preview());
            y("#WebGLChocolux", () => new WebGLChocolux.Application(), new WebGLChocolux.HTML.Images.FromAssets.Preview());
            y("#WebGLPuls", () => new WebGLPuls.Application(), new WebGLPuls.HTML.Images.FromAssets.Preview());
            y("#WebGLCelShader", () => new WebGLCelShader.Application(), new WebGLCelShader.HTML.Images.FromAssets.Preview());
            y("#WebGLClouds", () => new WebGLClouds.Application(), new WebGLClouds.HTML.Images.FromAssets.Preview());
            y("#WebGLCone", () => new WebGLCone.Application(), new WebGLCone.HTML.Images.FromAssets.Preview());
            y("#WebGLShaderDisturb", () => new WebGLShaderDisturb.Application(), new WebGLShaderDisturb.HTML.Images.FromAssets.Preview());
            y("#WebGLDynamicTerrainTemplate", () => new WebGLDynamicTerrainTemplate.Application(), new WebGLDynamicTerrainTemplate.HTML.Images.FromAssets.Preview());
            y("#WebGLEscherDrosteEffect", () => new WebGLEscherDrosteEffect.Application(), new WebGLEscherDrosteEffect.HTML.Images.FromAssets.Preview());
            y("#WebGLHand", () => new WebGLHand.Application(), new WebGLHand.HTML.Images.FromAssets.Preview());
            y("#WebGLInvade", () => new WebGLInvade.Application(), new WebGLInvade.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson01", () => new WebGLLesson01.Application(), new WebGLLesson01.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson02", () => new WebGLLesson02.Application(), new WebGLLesson02.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson03", () => new WebGLLesson03.Application(), new WebGLLesson03.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson04", () => new WebGLLesson04.Application(), new WebGLLesson04.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson05", () => new WebGLLesson05.Application(), new WebGLLesson05.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson06", () => new WebGLLesson06.Application(), new WebGLLesson06.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson07", () => new WebGLLesson07.Application(), new WebGLLesson07.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson08", () => new WebGLLesson08.Application(), new WebGLLesson08.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson09", () => new WebGLLesson09.Application(), new WebGLLesson09.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson10", () => new WebGLLesson10.Application(), new WebGLLesson10.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson11", () => new WebGLLesson11.Application(), new WebGLLesson11.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson12", () => new WebGLLesson12.Application(), new WebGLLesson12.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson13", () => new WebGLLesson13.Application(), new WebGLLesson13.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson14", () => new WebGLLesson14.Application(), new WebGLLesson14.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson15", () => new WebGLLesson15.Application(), new WebGLLesson15.HTML.Images.FromAssets.Preview());
            y("#WebGLLesson16", () => new WebGLLesson16.Application(), new WebGLLesson16.HTML.Images.FromAssets.Preview());
            y("#WebGLNyanCat", () => new WebGLNyanCat.Application(), new WebGLNyanCat.HTML.Images.FromAssets.Preview());
            y("#WebGLPlanetGenerator", () => new WebGLPlanetGenerator.Application(), new WebGLPlanetGenerator.HTML.Images.FromAssets.Preview());
            y("#WebGLSimpleCubic", () => new WebGLSimpleCubic.Application(), new WebGLSimpleCubic.HTML.Images.FromAssets.Preview());
            y("#WebGLSpadeWarrior", () => new WebGLSpadeWarrior.Application(), new WebGLSpadeWarrior.HTML.Images.FromAssets.Preview());
            y("#SpiderModel", () => new SpiderModel.Application(), new SpiderModel.HTML.Images.FromAssets.Preview());
            y("#WebGLWindWheel", () => new WebGLWindWheel.Application(), new WebGLWindWheel.HTML.Images.FromAssets.Preview());
            y("#WebGLTunnel", () => new WebGLTunnel.Application(), new WebGLTunnel.HTML.Images.FromAssets.Preview());
            y("#WebGLSpiral", () => new WebGLSpiral.Application(), new WebGLSpiral.HTML.Images.FromAssets.Preview());

            y("#WebGLCannonPhysicsEngine", () => new WebGLCannonPhysicsEngine.Application(), new WebGLCannonPhysicsEngine.HTML.Images.FromAssets.Preview());
            y("#WebGLBossHarvesterByOutsideOfSociety", () => new WebGLBossHarvesterByOutsideOfSociety.Application(), new WebGLBossHarvesterByOutsideOfSociety.HTML.Images.FromAssets.Preview());
            y("#WoodsXmasByRobert", () => new WoodsXmasByRobert.Application(), new WoodsXmasByRobert.HTML.Images.FromAssets.Preview());



            if (h == "")
            {
                var maxextra = 3;
                var iwidth = 96;
                var iheight = 96;


                if (Native.Window.Width > cutoff)
                {
                    iframe(iwidth, iheight, maxextra, new HTML.Images.FromAssets.Preview(), "");
                    //iframe(iwidth, iheight, maxextra, new HTML.Images.FromAssets.Preview(), "");

                    new IHTMLButton { innerText = "Fullscreen" }.With(
                        btn =>
                        {
                            btn.style.position = IStyle.PositionEnum.absolute;
                            btn.style.left = "1em";
                            btn.style.bottom = "1em";

                            btn.onclick +=
                                  delegate
                                  {
                                      Native.Document.body.requestFullscreen();
                                  };


                        }
                    ).AttachToDocument();

                    new IHTMLButton { innerText = "Load Em All" }.With(
                        btn =>
                        {
                            btn.style.position = IStyle.PositionEnum.absolute;
                            btn.style.right = "1em";
                            btn.style.bottom = "1em";

                            btn.onclick +=
                                delegate
                                {
                                    btn.Orphanize();

                                    LoadEmAll();
                                };
                        }
                    ).AttachToDocument();
                }
                else
                {
                    iwidth = 32;
                    iheight = 32;
                }

                #region ApplyMargins
                Action ApplyMargins = delegate
                {
                    if (Native.Window.Width < cutoff)
                    {
                        cc.style.margin = "0px";
                    }
                    else
                    {
                        cc.style.margin = "3%";
                        cc.style.marginTop = "10%";
                    }
                };

                ApplyMargins();

                Native.Window.onresize +=
                    delegate
                    {
                        ApplyMargins();
                    };
                #endregion

                #region requestFullscreen
                Native.Document.body.ondblclick +=
                    delegate
                    {
                        //if (IsDisposed)
                        //    return;

                        // http://tutorialzine.com/2012/02/enhance-your-website-fullscreen-api/

                        Native.Document.body.requestFullscreen();

                        //AtResize();
                    };
                #endregion

            }

        }

    }
}
