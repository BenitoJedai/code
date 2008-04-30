using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.net;
using System;


namespace FlashXMLExample.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashXMLExample : Sprite
    {
        public const string Path_MySettings = "assets/FlashXMLExample/MySettings.xml";

        [Embed("/" + Path_MySettings)]
        static Class Asset_MySettings;

        // http://www.adobe.com/cfusion/communityengine/index.cfm?event=showdetails&productId=2&postId=8046
        // http://theflashblog.com/?p=242
        // http://livedocs.adobe.com/flex/3/html/help.html?content=13_Working_with_XML_11.html

        public FlashXMLExample()
        {
            new TextField
            {
                // correct encoding without BOM
                //width = stage.width,
                text = Asset_MySettings.ToXMLAsset().toXMLString()
            }.AttachTo(this);

            var loader = new URLLoader();

            loader.complete +=
                delegate
                {
                    new TextField
                    {
                        // correct encoding without BOM
                        //width = stage.width,
                        text = new XML(loader.data).toXMLString(),
                        y = 200
                    }.AttachTo(this);
                };


    
            loader.ioError +=
                e =>
                {
                    new TextField
                    {
                        // correct encoding without BOM
                        text = e.text,
                        y = 100,
                        width = 400,
                        textColor = 0xff0000

                    }.AttachTo(this);                    
                };
            
            try
            {
                loader.load(new URLRequest(Path_MySettings));
            }
            catch (Exception exc)
            {
                new TextField
                {
                     //correct encoding without BOM
                    text = exc.Message,
                    y = 100,
                    width = 400,
                    textColor = 0xff0000

                }.AttachTo(this);   
            }
 

        }
    }

}
