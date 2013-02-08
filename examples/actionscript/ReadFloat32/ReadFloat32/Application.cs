using ReadFloat32.Design;
using ReadFloat32.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ReadFloat32
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // Initialize ApplicationSprite
            sprite.AttachSpriteTo(page.Content);

            sprite.Invoke(
                base64 =>
                {
                    var bytes = Convert.FromBase64String(base64);

                    new IHTMLPre { innerText = bytes.ToHexString() }.AttachToDocument();

                    var r = new BinaryReader(new MemoryStream(bytes));

                    var space = r.ReadByte();

                    new IHTMLPre { innerText = new { space }.ToString() }.AttachToDocument();

                    try
                    {
                        // implemented in Redux build configuration
                        // script: error JSC1000: No implementation found for this native method, please implement [System.IO.BinaryReader.ReadSingle()]
                        var f = r.ReadSingle();

                        new IHTMLPre { innerText = new { f }.ToString() }.AttachToDocument();
                    }
                    catch (Exception ex)
                    {
                        new IHTMLPre { innerText = "error: " + new { ex.Message }.ToString() }.AttachToDocument();
                    }
                }
            );
        }

    }
}
