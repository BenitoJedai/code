using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using ScriptCoreLib;
using System.Xml.Linq;
using System.Xml;

namespace jsc.Languages.ActionScript
{
    static class EntryPointProvider
    {
        static public void WriteEntryPoints(this Assembly a, DirectoryInfo dir)
        {
            var Entries = from _Type in a.GetTypes()
                          let _Script = _Type.GetCustomAttributes<ScriptAttribute>().SingleOrDefault()
                          where _Script != null
                          let _ScriptApplicationEntryPoint = _Type.GetCustomAttributes<ScriptApplicationEntryPointAttribute>().SingleOrDefault()
                          where _ScriptApplicationEntryPoint != null
                          let _GoogleGadget = _Type.GetCustomAttributes<GoogleGadgetAttribute>().SingleOrDefault()
                          select new { _Type, _Script, _ScriptApplicationEntryPoint, _GoogleGadget };

            /*
<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
width="160" height="160"
codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab">
<param name="movie" value="http://hosting.gmodules.com/ig/gadgets/file/112091410969506928037/FlashMinesweeper.swf" />
<param name="quality" value="high" />
<param name="bgcolor" value="#869ca7" />

<embed src="http://hosting.gmodules.com/ig/gadgets/file/112091410969506928037/FlashMinesweeper.swf" quality="high" bgcolor="#869ca7"
width="160" height="160" align="middle"
play="true" loop="false"
type="application/x-shockwave-flash"
pluginspage="http://www.macromedia.com/go/getflashplayer">
</embed>
</object>              
             */

            // http://kb.adobe.com/selfservice/viewContent.do?externalId=tn_4150
            // http://www.joostdevalk.nl/code/valid-flash-embedding.html

            /*
<object type="application/x-shockwave-flash" 
	data="music/sound.swf" 
	width="0" height="0">
	<param name="movie" value="music/sound.swf" />
	<param name="quality" value="high"/>
</object>             
             */

            Func<string, int, int, XElement> FlashTag =
                (href, width, height) =>
                    new XElement("object",
                        new
                        {
                            type = "application/x-shockwave-flash",
                            data = href,
                            width,
                            height
                        }.GetPropertiesAsXAttributes(),
                        new XElement("param", 
                            new {
                                name = "movie",
                                value = href
                            }.GetPropertiesAsXAttributes()
                        )
                    );


            foreach (var v in Entries)
                using (var w = dir.CreateFile(v._Type.Name + ".htm"))
                {
                    w.Write(
                        FlashTag(v._Type.Name + ".swf", 320, 200)
                    );
                }

            foreach (var v in from i in Entries where i._GoogleGadget != null select i)
                using (var w = dir.CreateFile(v._Type.Name + ".GoogleGadget.xml"))
                {
                    var xw = XmlWriter.Create(w);

                    new XDocument(
                        new XElement("Module",
                            new XElement("ModulePrefs", v._GoogleGadget.GetPropertiesAsXAttributes()),
                            new XElement("Content",
                                new XAttribute("type", "html"),
                                new XCData(
                                    FlashTag(v._Type.Name + ".swf", v._GoogleGadget.width, v._GoogleGadget.height).ToString()
                                )
                            )
                        )
                    ).WriteTo(xw);

                    xw.Flush();
                }

        }
    }
}
