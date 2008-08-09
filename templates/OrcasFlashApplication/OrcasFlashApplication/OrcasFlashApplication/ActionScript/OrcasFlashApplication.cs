using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;
using System.IO;

namespace OrcasFlashApplication.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class OrcasFlashApplication : Sprite
    {
        [Script]
        public class Data
        {
            public string Text;
        }


        /// <summary>
        /// Default constructor
        /// </summary>
        public OrcasFlashApplication()
        {
			TestBinaryReader();

 

            var dict = new Dictionary<string, string>
            {
                {"hello", "world2"}
            };

            for (var j = 0.0; j < 1; j += 0.1)
            {
                this.graphics.beginFill(0xff0000, j);
                this.graphics.drawCircle(40, 40, 40 * (1.0 - j));
                this.graphics.endFill();
            }

            var step = 100;
            for (int i = 0; i < 4; i++)
            {
                addChild(
                   new TextField
                   {
                       text = "hello world",
                       x = step * i,
                       y = 20,
                       textColor = 0x00ff00,
                       sharpness = 400
                   });
            }

            addChild(
                new TextField
                {
                    text = "powered by jsc",
                    x = 20,
                    y = 40,
                    selectable = false,
                    sharpness = -400,
                    textColor = 0xffffff
                });
        }

		private void TestBinaryReader()
		{
			var wm = new MemoryStream();
			var w = new BinaryWriter(wm);

			w.Write((short)0x1234);
			w.Write((short)0x09FF);
			w.Write((byte)0xff);


			var wmText = new TextField { text = wm.ToArray().ToString(), autoSize = TextFieldAutoSize.LEFT }.AttachTo(this);

			wm.Position = 0;

			if (wm.Length != 5)
				throw new Exception("not 3 in length");

			var r = new BinaryReader(wm);

			var _x1234 = r.ReadInt16();
			if (_x1234 != 0x1234)
				throw new Exception("not 0x1234");

			var _x0909 = r.ReadInt16();
			if (_x0909 != 0x09FF)
				throw new Exception("not 0x09FF: " + _x0909);

			var _xff = r.ReadByte();
			if (_xff != 0xff)
				throw new Exception("not xff");
		}
    }
}