using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.mx.controls;
using System.Text;
using System;
using ScriptCoreLib.ActionScript.flash.ui;


namespace FullscreenStage.ActionScript
{
    [Script]
    class GenericType<T>
    {
        public T Value;
    }

    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FullscreenStage : Sprite
    {
        
        TextField Output;

        void WriteLine(string e)
        {
            Write(e + "\n");
        }

        void Write(string e)
        {
            Output.text += e;
        }
        
        public FullscreenStage()
        {
            
            // debug player http://www.adobe.com/support/flashplayer/downloads.html

            // http://blog.fatal-exception.co.uk/?p=7

            stage.SetFullscreen(true);
     

            this.graphics.beginFill(0x0, 1);
            this.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);
            this.graphics.endFill();

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
                   }
               );
            }


            Output = (TextField)
                addChild(
                    new TextField
                    {
                        
                        x = 20,
                        y = 40,
                        selectable = false,
                        sharpness = -400,
                        textColor = 0xffffff,
                        multiline = true
                    }
                );

            

            var w = new StringBuilder();

            var g_int = new GenericType<int> { Value = 7 };
            var g_string = new GenericType<string> { Value = "hey" };

            Action<string> Append = Write;

            w.AppendLine("hello world 1: " + g_int.Value);
            w.AppendLine("hello world 2: " + g_string.Value);

            Write(w.ToString());
            Append(">>> " + w);
            
            var tag = "length: ";
            var val = tag + tag.Length;

            WriteLine(val);

            var mnu = new ContextMenu();

            mnu.hideBuiltInItems();

            this.contextMenu = mnu;
        }
    }

}
