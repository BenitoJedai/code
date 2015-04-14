using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.StarlingSetup.ActionScript.Images;
using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using starling.core; // !!
using starling.display;
using starling.text;
using starling.textures;
using starling.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.StarlingSetup.Library
{

    public delegate Func<Texture> TextureFromImage(ScriptCoreLib.ActionScript.flash.display.IBitmapDrawable img, int innersize = 96);

    public delegate Func<Texture> Texture64Constructor(
        string asset,
        double alpha = 1.0,
        bool flipx = false,
        int innersize = 64,
        ColorTransform adjustAlpha = null,
        ScriptCoreLib.ActionScript.flash.filters.ColorMatrixFilter filter = null
    );

    public delegate Func<Texture> SpriteToTexture64Constructor(
        global::ScriptCoreLib.ActionScript.flash.display.Sprite asset,
        double alpha = 1.0,
        bool flipx = false,
        int innersize = 64,
        ColorTransform adjustAlpha = null,
        ScriptCoreLib.ActionScript.flash.filters.ColorMatrixFilter filter = null
    );

    public delegate void FrameHandler(ScriptCoreLib.ActionScript.flash.display.Stage stage, Starling starling);



    public class StarlingGameSpriteBase : Sprite
    {
        public int frameid = 0;


        public FrameHandler onsyncframe = delegate { };
        public FrameHandler onbeforefirstframe = delegate { };


        public static FrameHandler onframe = delegate { };
        public static Action<Action<int, int>> onresize;



        public Random random = new Random();


        public double internalscale = 0.3;


        public SpriteToTexture64Constructor new_texsprite_crop;
        public Texture64Constructor new_tex_crop;
        public TextureFromImage new_tex96;


        public double stagescale;

        public Stopwatch gametime = new Stopwatch();



        public Sprite
            Content,
            Content_layer0_ground,
            Content_layer0_tracks,
            Content_layer2_shadows,
            Content_layer3_buildings,
            Content_layer10_hiddenforgoggles;



        public static int DefaultLogoCount = 2;

        TextField info;

        int Source0TextureMaxBottom = 0;

        double stagex = 200.0;
        double stagey = 200.0;


        public static StarlingGameSpriteBase instance;

        public StarlingGameSpriteBase()
        {
            instance = this;

            gametime.Start();

            this.Content = new Sprite().AttachTo(this);

            this.Content_layer0_ground = new Sprite().AttachTo(this.Content);
            this.Content_layer0_tracks = new Sprite().AttachTo(this.Content);
            this.Content_layer2_shadows = new Sprite().AttachTo(this.Content);
            this.Content_layer3_buildings = new Sprite().AttachTo(this.Content);

            this.Content_layer10_hiddenforgoggles = new Sprite().AttachTo(this.Content);
            this.Content_layer10_hiddenforgoggles.visible = false;

            this.info = new TextField(
                800,
                400,
                "Welcome to Starling!"
                ) { hAlign = HAlign.LEFT, vAlign = VAlign.TOP };

            //info.AttachTo(this);
            //.MoveTo(72, 8);





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
            var Source0TextureTop = 0;
            var Source0TextureLeft = 0;

            // fighting mipmapping
            var Source0Padding = 4;

			// ?
            var Source0 = new XLazy<TextureAtlas>(
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
               (asset, alpha, flipx, innersize, adjustAlpha, filter) =>
               {

                   var shape = ScriptCoreLib.ActionScript.Extensions.KnownEmbeddedResources.Default[asset].ToSprite();

                   return new_texsprite_crop(shape, alpha, flipx, innersize, adjustAlpha, filter);
               };


            this.new_texsprite_crop =
               (shape, alpha, flipx, innersize, adjustAlpha, filter) =>
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

                       if (filter != null)
                           shape.filters = new[] { filter };

                       // this does not work!
                       //shape.alpha = 0.3;



                       // next image will appear right to us + padding to prevent bleed
                       Source0TextureLeft += innersize + Source0Padding;
                       Source0TextureMaxBottom = Source0TextureMaxBottom.Max(Source0TextureTop + innersize + Source0Padding);

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

                       if (adjustAlpha == null)
                           adjustAlpha = new ColorTransform();

                       adjustAlpha.alphaMultiplier = alpha;


                       // public void draw(IBitmapDrawable source, Matrix matrix, ColorTransform colorTransform, string blendMode, Rectangle clipRect);
                       //SourceBitmapData0.draw(shape, m, adjustAlpha, clipRect: rect);
                       SourceBitmapData0.draw(shape, m, adjustAlpha, null, rect);
                   }

                   var TextureIndex = Source0TextureCount;

                   Source0TextureCount++;

                   // Error	5	Cannot convert anonymous method to type 'FlashHeatZeeker.UnitPed.Library.Lazy<starling.textures.TextureAtlas>' because it is not a delegate type	X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.UnitPed\Library\StarlingGameSpriteWithPed.cs	178	43	FlashHeatZeeker.UnitPed
                   var y = new XLazy<TextureAtlas>(
                       delegate
                       {
                           Source0.Content.addRegion(TextureIndex.ToString(), rect);

                           return Source0.Content;
                       }
                    );


                   // ???
                   return () => y.Content.getTexture(TextureIndex.ToString());
               };
            #endregion


            #region new_tex96
            this.new_tex96 =
               (shape, innersize) =>
               {

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
                       Source0TextureLeft += innersize + Source0Padding;
                       Source0TextureMaxBottom = Source0TextureMaxBottom.Max(Source0TextureTop + innersize + Source0Padding);

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
                   var y = new XLazy<TextureAtlas>(
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
                //var count = 64;
                var count = DefaultLogoCount;

                for (int i = 0; i < count; i++)
                    for (int yi = 0; yi < count; yi++)
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


            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/display3D/Context3D.html#driverInfo
            //Text = Starling.current.context.driverInfo;
            Text = "driverInfo missing";


            // how expensive is delegate call in a frame?
            onframe +=
                (stage, starling) =>
                {
                    if (this.frameid == 0)
                    {
                        if (this.onbeforefirstframe != null)
                            this.onbeforefirstframe(stage, starling);
                    }

                    this.frameid++;


                    if (!this.DisableDefaultContentDransformation)
                    {
                        var cm = new Matrix();
                        cm.scale(stagescale, stagescale);

                        if (autorotate)
                            cm.rotate(this.gametime.ElapsedMilliseconds * 0.001);

                        cm.translate(stagex, stagey);
                        this.Content.transformationMatrix = cm;
                    }


                    // does this cost us 40FPS??
                    //var texmem = (this.Source0TextureMaxBottom * 100 / 2048) + "%";

                    //this.info.text = new { this.frameid, texmem, this.Text }.ToString();

                };
        }

        ////public void __onframe(ScriptCoreLib.ActionScript.flash.display.Stage stage, Starling starling)
        ////{
        ////    //if (this.frameid == 0)
        ////    //{
        ////    //    if (this.onbeforefirstframe != null)
        ////    //        this.onbeforefirstframe(stage, starling);
        ////    //}

        ////    this.frameid++;


        ////    if (!this.DisableDefaultContentDransformation)
        ////    {
        ////        var cm = new Matrix();
        ////        cm.scale(stagescale, stagescale);

        ////        if (autorotate)
        ////            cm.rotate(this.gametime.ElapsedMilliseconds * 0.001);

        ////        cm.translate(stagex, stagey);
        ////        this.Content.transformationMatrix = cm;
        ////    }


        ////    //var texmem = (this.Source0TextureMaxBottom * 100 / 2048) + "%";

        ////    //this.info.text = new { this.frameid, texmem, this.Text }.ToString();
        ////}


        public string Text;

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
