using System;
using System.Collections.Generic;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.media;

namespace ZIPExample1.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class ZIPExample1 : Sprite
    {


        [Embed("/assets/ZIPExample1/dude5.zip")]
        Class MyZipFile;





        /// <summary>
        /// Default constructor
        /// </summary>
        public ZIPExample1()
        {
            var BitmapsLoaded = 0;

            var Bitmaps = Enumerable.ToArray(
                from File in
                    from f in MyZipFile.ToFiles()
                    // you can filter your images here
                    where f.FileName.EndsWith(".png")
                    select f
                select new { File, GetBitmap = File.Bytes.LoadBytes<Bitmap>(i => BitmapsLoaded++) }
            );

    
            


            (200).AtInterval(
                (t, add) =>
                {
                    if (BitmapsLoaded != Bitmaps.Length)
                        return;

           
                    var Entry = Bitmaps[t.currentCount % Bitmaps.Length];
                    var Bitmap = Entry.GetBitmap();

                    Bitmap.scaleX = 4;
                    Bitmap.scaleY = 4;
                    Bitmap.AttachTo(this);

                    var text = new TextField
                    {
                        selectable = false,
                        text = Entry.File.FileName
                    }.AttachTo(this);


                    add(
                        delegate
                        {
                            text.Orphanize();
                            Bitmap.Orphanize();
                        }
                    );



                }
            );


        }
    }
}