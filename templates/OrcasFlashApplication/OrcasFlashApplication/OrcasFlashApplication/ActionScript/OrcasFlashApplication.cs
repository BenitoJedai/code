using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;

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

        static Func<Data> Test1(Func<string> GetText, Func<Data, Data> Handler)
        {
            return () => Handler(new Data { Text = GetText() });
        }

        static Func<Data> Test2(Func<string> GetText, Func<Data, Data> Handler)
        {
            return () => { return Handler(new Data { Text = GetText() }); };
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public OrcasFlashApplication()
        {
            if (Test1(() => "", i => i) == null)
                System.Console.WriteLine("Compiler Error 1");

            if (Test2(() => "", i => i) == null)
                System.Console.WriteLine("Compiler Error 2");

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
    }
}