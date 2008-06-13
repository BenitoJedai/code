using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Nonoba.api
{
    [Script(IsNative = true)]
    public class MessageEvent : Event
    {
        public Message message;

        #region Constants
        /// <summary>
        /// 
        /// </summary>
        public static readonly string MESSAGE = "onMessage";

        #endregion

        public MessageEvent(string type, Message message)
        {
        }

        [Script(NotImplementedHere = true)]
        public static MessageEvent CloneFrom(object e)
        {
            return default(MessageEvent);
        }
    }
}
