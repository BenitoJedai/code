using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.system
{
    // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/system/MessageChannel.html
    [Script(IsNative = true)]
    public class MessageChannel : EventDispatcher
    {
        /// <summary>
        ///  Dispatched each time the sending worker calls this MessageChannel object's send() 
        ///  method, indicating that a new message object is available in the MessageChannel 
        ///  instance's queue.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> channelMessage;

        public void send(object arg)
        {

        }

        public object receive()
        {
            return default(object);

        }
    }
}

namespace ScriptCoreLib.ActionScript.Extensions.flash.system
{

    [Script(Implements = typeof(ScriptCoreLib.ActionScript.flash.system.MessageChannel))]
    internal static class __MessageChannel
    {


        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region timer
        public static void add_channelMessage(ScriptCoreLib.ActionScript.flash.system.MessageChannel that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.CHANNEL_MESSAGE);
        }

        public static void remove_channelMessage(ScriptCoreLib.ActionScript.flash.system.MessageChannel that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.CHANNEL_MESSAGE);
        }
        #endregion


        #endregion
    }
}
