using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.mx.core;


namespace FlashMinesweeper.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(backgroundColor = 0xffffff)]
    public class FlashMinesweeper : Sprite
    {
        // todo:
        // http://www.kirupa.com/forum/showthread.php?t=261577
        // http://groups.google.com/group/youtube-api-basics/browse_thread/thread/89ff378fc44985f0/6b63c2e46159640f?lnk=gst&q=flash+loadClip&rnum=1

        [Script]
        static public class Assets
        {
            public const string Path = "/assets/FlashMinesweeper";

            [Embed(source = Path + "/flag.png")]
            static public readonly Class flag;

            [Embed(source = Path + "/button.png")]
            static public readonly Class button;

        }

        [Script]
        public class MineButton : Sprite
        {
            private readonly BitmapAsset img_flag = Assets.flag.ToBitmapAsset();
            private readonly BitmapAsset img_button = Assets.button.ToBitmapAsset();

            public MineButton()
            {
                img = img_button;
            }


            private Bitmap _img;

            public Bitmap img
            {
                set
                {
                    if (_img != null)
                    {
                        this.removeChild(_img);
                    }

                    _img = value;

                    if (_img != null)
                    {
                        this.addChild(_img);
                    }
                }
            }
        }

        public FlashMinesweeper()
        {
            for (int x = 0; x < 10; x++)
                for (int y = 0; y < 10; y++)
                {
                    var b1 = new MineButton
                        {
                            x = x * 16,
                            y = y * 16
                        }.AttachTo(this);

                }



        }
    }

}
