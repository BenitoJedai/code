using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using Sudoku.Transform;

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

            for (int y = 1; y < 10; y++)
                for (int x = 1; x < 10; x++)
                {
                    new TextField
                    {
                        text = t[x, y].ToString(),
                        x = 28 * x,
                        y = 28 * y
                    }.AttachTo(this);
                }
        }
    }
}