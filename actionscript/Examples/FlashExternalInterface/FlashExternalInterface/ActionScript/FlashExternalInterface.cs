using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.ActionScript.flash.system;
using System;


namespace FlashExternalInterface.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashExternalInterface : Sprite
    {
        public FlashExternalInterface()
        {
            addChild(
                    new TextField
                    {
                        text = ExternalInterface.available ?
                        "ExternalInterface available" : "ExternalInterface not available",
                        x = 20,
                        y = 20,
                        selectable = false,
                        textColor = 0xffffff,
                        autoSize = TextFieldAutoSize.LEFT,
                        mouseEnabled = false
                    }
                );

            addChild(
                    new TextField
                    {
                        text = Security.sandboxType,
                        x = 20,
                        y = 40,
                        selectable = false,
                        textColor = 0xffffff,
                        autoSize = TextFieldAutoSize.LEFT,
                        mouseEnabled = false
                    }
                );

            Security.allowDomain("*");


            var status =
                new TextField
                {
                    text = "ready",
                    x = 20,
                    y = 80,
                    selectable = false,
                    textColor = 0xffffff,
                    autoSize = TextFieldAutoSize.LEFT,
                    mouseEnabled = false
                }.AttachTo(this);

            Func<string, Sprite> CreateButton =
                text =>
                {
                    var z = new Sprite();

                    z.graphics.beginFill(0xff0000);
                    z.graphics.drawRect(0, 0, 100, 20);
                    z.graphics.endFill();
                    z.width = 100;
                    z.height = 20;
                    
                    new TextField { text = text, mouseEnabled = false, width = 100, height = 20 }.AttachTo(z);

                    return z;
                };

            Func<string, Action, Sprite> AddButton =
                (text, click) =>
                {
                    var z = CreateButton(text);

                    z.click += delegate { click(); };

                    z.AttachTo(this);

                    return z;

                };

            var s = AddButton("call function1!",
                delegate
                {
                    status.text = "click!";
                    status.text = ExternalInterface.call("function1", "hello world").ToString();
                }
            );

            var a = AddButton("show settings!",
              delegate
              {
                  status.text = "settings!";

                  Security.showSettings();
              }
            );

            a.x = 102;

            var b = AddButton("add callback!",
                delegate
                {
                    status.text = "new callback!";

                    Action function2 =
                       delegate
                       {
                           status.text = "function2 called";
                       };

                    ExternalInterface.addCallback("function2", function2.ToFunction());
                }
              );


            b.x = 204;
        }
    }

}
