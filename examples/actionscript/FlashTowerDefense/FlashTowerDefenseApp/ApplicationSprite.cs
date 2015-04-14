using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;

namespace FlashTowerDefenseApp.Components
{
    internal sealed class ApplicationSprite : Sprite
    {
        public const int DefaultWidth = 560;

        public const int DefaultHeight = 480;

        public ApplicationSprite()
        {
            // http://stackoverflow.com/questions/4003286/error-1030-stack-depth-is-unbalanced

            var g = new FlashTowerDefense.ActionScript.FlashTowerDefense();

            g.AttachTo(this);
        }

    }
}

//{ trace = X:\jsc.internal.svn\compiler\jsc\Languages\IL\ILTranslationExtensions.EmitToArguments.cs, TargetMethod = Void InvokeAsync(System.Str
// assembly: X:\jsc.svn\examples\actionscript\FlashTowerDefense\FlashTowerDefenseApp\bin\Debug\Chrome Web Server Styled Form.dll
// type: ChromeTCPServer.TheServerWithStyledForm, Chrome Web Server Styled Form, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// offset: 0x00bb
//  method:Void Invoke(System.String, Int32, Int32, System.Action`1[ScriptCoreLib.JavaScript.Extensions.FormStyler]), ex = System.IO.FileNotFoun
//File name: 'ScriptCoreLib.Extensions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'