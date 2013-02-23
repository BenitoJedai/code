using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using starling.utils;
using System.Diagnostics;
using starling.text;
using FlashHeatZeeker.StarlingSetup.ActionScript.Images;
using System.ComponentModel;
using starling.core;

namespace FlashHeatZeeker.StarlingSetup.Library
{

    class Lazy<T>
    {
        public Func<T> InternalGetContent;
        public T InternalContent;

        public T Content
        {
            get
            {
                if (this.InternalGetContent != null)
                {
                    this.InternalContent = this.InternalGetContent();
                    this.InternalGetContent = null;

                }


                return this.InternalContent;
            }
        }

        public Lazy(Func<T> y)
        {
            InternalGetContent = y;
        }
    }

    public delegate Func<Texture> Texture64Constructor(string asset, double alpha = 1.0, bool flipx = false, int innersize = 64);

    public delegate void FrameHandler(ScriptCoreLib.ActionScript.flash.display.Stage stage, Starling starling);

    public class StarlingGameSpriteBase : Sprite
    {
        public int frameid = 0;


        public FrameHandler onbeforefirstframe = delegate { };

        public static FrameHandler onframe = delegate { };
        public static Action<Action<int, int>> onresize;

        public Sprite Content;
        public Random random = new Random();


        public double internalscale = 0.3;

        public Texture64Constructor new_tex_crop;
        public double stagescale;

        public Stopwatch gametime = new Stopwatch();

        public StarlingGameSpriteBase()
        {
            gametime.Start();

            this.Content = new Sprite().AttachTo(this);

            var info = new TextField(
                800,
                400,
                "Welcome to Starling!"
                ) { hAlign = HAlign.LEFT, vAlign = VAlign.TOP };

            info.AttachTo(this);
            //.MoveTo(72, 8);




            var stagex = 200.0;
            var stagey = 200.0;
            this.stagescale = internalscale;

            onresize(
                (w, h) =>
                {
                    stagex = w * 0.5;
                    stagey = h * 0.8;
                    stagescale = internalscale * (w) / (800.0);


                }
            );



            #region Source0
            var SourceBitmapData0 = new ScriptCoreLib.ActionScript.flash.display.BitmapData(
                // 28MB
                //64 * 2,
                //64 * 2,

                 // 50 MB
                 2048,
                 2048,
                 true, 0x00000000
            );
            var Source0TextureCount = 0;
            // where to start?
            var Source0TextureTop = 1024;
            var Source0TextureLeft = 0;
            var Source0TextureMaxBottom = 0;

            var Source0 = new Lazy<TextureAtlas>(
                 delegate
                 {
                     var SourceTexture = Texture.fromBitmapData(SourceBitmapData0);
                     var Source = new TextureAtlas(SourceTexture);


                     return Source;
                 }
              );
            #endregion



            #region new_tex_crop
            this.new_tex_crop =
               (asset, alpha, flipx, innersize) =>
               {
                   //var innersize = 64;
                   // outer 400, inner 64

                   // do we need to wrap?
                   if ((Source0TextureLeft + innersize) >= 2048)
                   {
                       // if so, goto next line
                       Source0TextureTop = Source0TextureMaxBottom;
                       Source0TextureLeft = 0;
                   }

                   var rect = new Rectangle(Source0TextureLeft, Source0TextureTop, innersize, innersize);

                   {
                       var shape = ScriptCoreLib.ActionScript.Extensions.KnownEmbeddedResources.Default[asset].ToSprite();

                       // this does not work!
                       //shape.alpha = 0.3;



                       // next image will appear right to us + padding to prevent bleed
                       Source0TextureLeft += innersize + 1;
                       Source0TextureMaxBottom = Source0TextureMaxBottom.Max(Source0TextureTop + innersize);

                       var m = new Matrix();

                       // flip vertical?
                       if (flipx)
                       {
                           m.translate(-400, 0);
                           m.scale(-1, 1);
                       }

                       m.translate(
                           // where to draw? we need packer algorithm?
                           -(400 - rect.width) / 2 + rect.left,
                           -(400 - rect.height) / 2 + rect.top
                      );



                       // http://stackoverflow.com/questions/8035717/actionscript-3-draw-with-transparency-on-a-bitmap

                       var adjustAlpha = new ColorTransform();
                       adjustAlpha.alphaMultiplier = alpha;


                       SourceBitmapData0.draw(shape, m, adjustAlpha, clipRect: rect);
                   }

                   var TextureIndex = Source0TextureCount;

                   Source0TextureCount++;

                   // Error	5	Cannot convert anonymous method to type 'FlashHeatZeeker.UnitPed.Library.Lazy<starling.textures.TextureAtlas>' because it is not a delegate type	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.UnitPed\Library\StarlingGameSpriteWithPed.cs	178	43	FlashHeatZeeker.UnitPed
                   var y = new Lazy<TextureAtlas>(
                       delegate
                       {
                           Source0.Content.addRegion(TextureIndex.ToString(), rect);

                           return Source0.Content;
                       }
                    );

                   return () => y.Content.getTexture(TextureIndex.ToString());
               };
            #endregion


            #region new_tex96
            Func<ScriptCoreLib.ActionScript.flash.display.IBitmapDrawable, Func<Texture>> new_tex96 =
               (shape) =>
               {
                   var innersize = 96;

                   // outer 400, inner 64

                   // do we need to wrap?
                   if ((Source0TextureLeft + innersize) >= 2048)
                   {
                       // if so, goto next line
                       Source0TextureTop = Source0TextureMaxBottom;
                       Source0TextureLeft = 0;
                   }

                   var rect = new Rectangle(Source0TextureLeft, Source0TextureTop, innersize, innersize);

                   {

                       // this does not work!
                       //shape.alpha = 0.3;



                       // next image will appear right to us
                       Source0TextureLeft += innersize + 1;
                       Source0TextureMaxBottom = Source0TextureMaxBottom.Max(Source0TextureTop + innersize);

                       var m = new Matrix();
                       m.translate(
                           // where to draw? we need packer algorithm?
                           rect.left,
                           rect.top
                      );

                       //m.scale(64 / 400.0, 64 / 400.0);

                       // http://stackoverflow.com/questions/8035717/actionscript-3-draw-with-transparency-on-a-bitmap

                       var adjustAlpha = new ColorTransform();
                       adjustAlpha.alphaMultiplier = alpha;


                       SourceBitmapData0.draw(shape, m, adjustAlpha);
                   }

                   var TextureIndex = Source0TextureCount;

                   Source0TextureCount++;

                   // Error	5	Cannot convert anonymous method to type 'FlashHeatZeeker.UnitPed.Library.Lazy<starling.textures.TextureAtlas>' because it is not a delegate type	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.UnitPed\Library\StarlingGameSpriteWithPed.cs	178	43	FlashHeatZeeker.UnitPed
                   var y = new Lazy<TextureAtlas>(
                       delegate
                       {
                           Source0.Content.addRegion(TextureIndex.ToString(), rect);

                           return Source0.Content;
                       }
                    );

                   return () => y.Content.getTexture(TextureIndex.ToString());
               };
            #endregion


            //var __bmd = new ScriptCoreLib.ActionScript.flash.display.BitmapData(96, 96, true, 0x00000000);
            //__bmd.draw(new white_jsc());
            var LogoTexture = new_tex96(new white_jsc());

            onbeforefirstframe += delegate
            {
                for (int i = 0; i < 64; i++)
                    for (int yi = 0; yi < 64; yi++)
                    {
                        var logo = new Image(LogoTexture()) { }.AttachTo(Content);

                        {
                            var cm = new Matrix();
                            cm.rotate(random.NextDouble() * Math.PI);
                            cm.translate(i * 96, yi * 96);

                            logo.transformationMatrix = cm;
                        }
                    }
            };



            onframe +=
                (stage, starling) =>
                {
                    if (frameid == 0)
                    {
                        if (onbeforefirstframe != null)
                            onbeforefirstframe(stage, starling);
                    }

                    frameid++;


                    if (!DisableDefaultContentDransformation)
                    {
                        var cm = new Matrix();
                        cm.scale(stagescale, stagescale);

                        if (autorotate)
                            cm.rotate(0.01 * frameid);

                        cm.translate(stagex, stagey);
                        Content.transformationMatrix = cm;
                    }


                    var texmem = (Source0TextureMaxBottom * 100 / 2048) + "%";

                    info.text = new { frameid, texmem, gametime.ElapsedMilliseconds }.ToString();
                };
        }

        public bool autorotate = false;
        public bool DisableDefaultContentDransformation = false;
    }

    [Description("demo")]
    public sealed class StarlingGameSpriteDemo : StarlingGameSpriteBase
    {


        public StarlingGameSpriteDemo()
        {
            this.autorotate = true;
        }
    }
}
