using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.net;
using System;
using System.Xml.Linq;


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
            var x = XDocument.Parse(Asset_MySettings.ToStringAsset());

            new TextField
            {
                // correct encoding without BOM
                //width = stage.width,
                text = x.Root.Name + " = " + x.Root.ToString()
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

            RuntimeTypeReflection();
        }

        private void RuntimeTypeReflection()
        {
            // http://nwebb.co.uk/blog/?p=186
            // http://livedocs.adobe.com/flex/2/langref/flash/utils/package.html#describeType()
            // http://onrails.org/articles/2007/02/24/flex-introspection-api-describetype-value-xml
            // http://paranoicnotes.wordpress.com/2007/09/19/flashutilsdescribetype-loosely-coupled-components/

            Action<string> Write = null;
            Action<string> WriteLine = i => Write(i + "\n");

            Action<MyDataClassCommon> Do =
                g =>
                {

                    var t = g.GetType();

                    //WriteLine("type: " + t.ToString());

                    WriteLine("name: " + t.Name);



                    foreach (var f in t.GetFields())
                    {
                        if (f.FieldType.Equals(typeof(string)))
                        {
                            f.SetValue(g, "hello world");
                        }

                        WriteLine("field: " + f.Name + " as " + f.FieldType.Name);

                    }


                    WriteLine(" ~ = " + g.Text);
                    WriteLine("");
                };


            var z = new TextField
            {
                // correct encoding without BOM
                text = "",
                x = 100,
                autoSize = TextFieldAutoSize.LEFT

            }.AttachTo(this);

            Write = z.appendText;

            Do(new Serialized.MyDataClass());
            Do(new Serialized2.MyDataClass());
        }
    }

    [Script]
    abstract class MyDataClassCommon
    {
        public string Text;
    }

    namespace Serialized
    {
        [Script]
        sealed class MyDataClass : MyDataClassCommon
        {

            public int Value;
            public Serialized2.MyDataClass Data;
        }
    }

    namespace Serialized2
    {
        [Script]
        sealed class MyDataClass : MyDataClassCommon
        {
            public int Value;
            public Serialized.MyDataClass Data;
        }
    }
}
