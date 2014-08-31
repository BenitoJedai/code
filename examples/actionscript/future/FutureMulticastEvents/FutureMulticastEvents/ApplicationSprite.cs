using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;
using System.Xml.Linq;

namespace FutureMulticastEvents
{
    public sealed class ApplicationSprite : Sprite
    {
        // encrypted UDP, WebRTC ?
        [multicast]
        public event Action<XElement> LANMulticastEvent;

        public ApplicationSprite()
        {


            LANMulticastEvent +=
                incoming =>
                {

                };



            // are we connected?
            LANMulticastEvent(
                new XElement("x", "y")
            );
        }

    }


    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class multicast : Attribute
    {

    }
}
