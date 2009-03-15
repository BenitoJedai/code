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
		// change: C:\util\xampplite\apache\conf\httpd.conf

		// http://localhost/jsc/FlashExternalInterface

		//Alias /jsc/FlashExternalInterface "C:\work\jsc.svn\actionscript\Examples\FlashExternalInterface\FlashExternalInterface\bin\Debug\web"
		//<Directory "C:\work\jsc.svn\actionscript\Examples\FlashExternalInterface\FlashExternalInterface\bin\Debug\web">
		//       Options Indexes FollowSymLinks ExecCGI
		//       AllowOverride All
		//       Order allow,deny
		//       Allow from all
		//</Directory>

		// http://curtismorley.com/2008/11/01/actionscript-security-error-2060-security-sandbox-violation/
		// http://blog.deconcept.com/code/externalinterface.html
		// http://blog.warptube.com/2008/12/2/oddities-with-externalinterface-and-ie

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

					try
					{

						ExternalInterface.call("setTimeout", "document.title = 'flashed';", 0);
						status.text = ExternalInterface.call("function1", "hello world").ToString();
					}
					catch (Exception ex)
					{
						status.text = ex.Message;
					}
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

			//ExternalInterface.call("setTimeout", "document.title = 'ready';", 0);

        }
    }

}
