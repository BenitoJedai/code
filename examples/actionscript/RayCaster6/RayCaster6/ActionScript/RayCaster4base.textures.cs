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
using ScriptCoreLib.ActionScript.RayCaster;


namespace RayCaster6.ActionScript
{


    partial class RayCaster4base
    {
      

        private void LoadTextures()
        {
            // 24fps 10715136bytes

            var textureFiles = new[] { "bwall.png", "tech2.jpg", "roof.jpg"
                , "114.png" 
              
            };
            var textureLoadNum = 0;

            //textures = new uint[textureFiles.Length][][];
            textures = new Texture64[textureFiles.Length];
            //textures = new TextureSelector { Target = this };
     


            var bitmapLoader = new Loader();
            bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));

            bitmapLoader.contentLoaderInfo.complete +=
                e =>
                {
                    var bmp = (Bitmap)(bitmapLoader.content);

                    textures[textureLoadNum] = (Bitmap)(bitmapLoader.content);



                    if (textureLoadNum < textureFiles.Length - 1)
                    {
                        textureLoadNum++;
                        bitmapLoader.unload();
                        bitmapLoader.load(new URLRequest("flashsrc/textures/" + textureFiles[textureLoadNum]));
                    }
                    else
                    {
                        time = getTimer();


                        IsReady = true;
                    }

                };

        }





        // fps:
        // 22
        // 21
    }

}
