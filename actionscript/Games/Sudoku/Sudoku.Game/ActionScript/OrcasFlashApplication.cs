using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using Sudoku.Transform;
using ScriptCoreLib.ActionScript.flash.filters;

namespace Sudoku.ActionScript
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class Sudoku : Sprite
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Sudoku()
        {
            var t = new SudokuFile(Assets.Level1.ToStringAsset());

            t.Mappings.Randomize();

            const int padding = 2;
            const int w = 28;
            const int h = 28;

            for (int y = 1; y < 10; y++)
                for (int x = 1; x < 10; x++)
                {
                    var s = new Sprite();


                    var text = new TextField
                    {
                        text = t[x, y].ToString(),
                        mouseEnabled = false
                    }.AttachTo(s);

                    text.autoSize = TextFieldAutoSize.LEFT;
                    text.x = -text.width / 2;
                    text.y = -text.height / 2;

                    s.graphics.lineStyle(3, 0xff5300, 1);
                    s.graphics.drawRect(-w / 2, -h / 2, w, h);
                    
                    // s.filters = new[] { new BevelFilter() };

                    s.x = (w + padding) * x;
                    s.y = (h + padding) * y;

                    s.AttachTo(this);

                }
        }
    }
}