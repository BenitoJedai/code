using org.si.sion;
using org.si.sion.midi;
using org.si.sion.utils.soundloader;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace SiON
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            //import org.si.sion.SiONDriver;
            //import org.si.sion.midi.SMFData;

            //[Embed(source="test.mid", mimeType="application/octet-stream")]
            //var Test:Class;

      
            var smfData = new SMFData();
            var driver = new SiONDriver();

            // http://opengameart.org/forumtopic/midi-in-as3-adobe-flash

            smfData.loadBytes(
                // implicit conversion?

                // this will sound like 8bit game console.
                KnownEmbeddedResources.Default["assets/SiON/test.mid"].ToByteArrayAsset()
            );

            driver.play(smfData);
        }

    }
}
