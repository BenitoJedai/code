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
        internal Texture256[] textures;

        //readonly List<Array2DUInt32> textures = new List<Array2DUInt32>();

        private void LoadTextures()
        {
            // 24fps 10715136bytes

            textureFiles = new[] { "wall.jpg", "tech2.jpg", "roof.jpg"
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
                , "114.png" 
            };
            textureLoadNum = 0;

            //textures = new uint[textureFiles.Length][][];
            textures = new Texture256[textureFiles.Length];
            //textures = new TextureSelector { Target = this };

            bitmapLoader = new Loader();
            bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));

            bitmapLoader.contentLoaderInfo.complete += onBitmapLoaded;
        }




        private void onBitmapLoaded(Event e)
        {
            var bd = (Bitmap)(bitmapLoader.getChildAt(0));
            var bdata = bd.bitmapData;

            var t = new Texture256(256, 256); ;

            textures[textureLoadNum] = t;

            if (bdata.width == 256)
            {
                for (var j = 0; j < 256; j++)
                {
                    for (var k = 0; k < 256; k++)
                    {
                        t[j, k] = bdata.getPixel(j, k);
                    }
                }
            }
            else if (bdata.width == 64)
            {
                // 64 x 4 = 256
                // make it bigger then

                for (var j = 0; j < 64; j++)
                {
                    for (var k = 0; k < 64; k++)
                    {
                        var c = bdata.getPixel(j, k); ;
                        var j4 = j * 4;
                        var k4 = k * 4;

                        for (int zj = 0; zj < 4; zj++)
                            for (int zk = 0; zk < 4; zk++)
                            {
                                t[j4 + zj, k4 + zk] = c;

                            }
                
                    }
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
