using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.filters;
using System;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.geom;


namespace RayCaster6.ActionScript
{
    

    partial class RayCaster4base
    {
        protected string[] textureFiles;
        protected int textureLoadNum;

        //protected uint[][][] textures = new uint[4][][];
        //internal uint[][][] textures;
        internal Array2DUInt32[] textures;

        //readonly List<Array2DUInt32> textures = new List<Array2DUInt32>();

        private void LoadTextures()
        {
            textureFiles = new[] { "wall.jpg", "tech2.jpg", "roof.jpg" };
            textureLoadNum = 0;

            //textures = new uint[textureFiles.Length][][];
            textures = new Array2DUInt32[textureFiles.Length];
            //textures = new TextureSelector { Target = this };

            bitmapLoader = new Loader();
            bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));

            bitmapLoader.contentLoaderInfo.complete += onBitmapLoaded;
        }




        private void onBitmapLoaded(Event e)
        {
            var bd = (Bitmap)(bitmapLoader.getChildAt(0));
            var bdata = bd.bitmapData;

            //var t = new uint[256][]; 
            var t = new Array2DUInt32(256, 256);;

            textures[textureLoadNum] = t;

            for (var j = 0; j < 256; j++)
            {
                //textures[textureLoadNum][j] = new uint[256];
                for (var k = 0; k < 256; k++)
                {
                    t[j, k] = bdata.getPixel(j, k);
                }
            }

            if (textureLoadNum < textureFiles.Length - 1)
            {
                textureLoadNum++;
                bitmapLoader.unload();
                bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));
            }
            else
            {
                prepare();
            }

        }

        // fps:
        // 22
        // 21
    }

}
