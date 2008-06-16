using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.events;

namespace ScriptCoreLib.ActionScript.Extensions.Nonoba.api
{
    [Script(Implements = typeof(Connection))]
    internal static class __Connection
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region Disconnect
        public static void add_Disconnect(Connection that, Action<object> value)
        {
            CommonExtensions.CombineDelegate(that, value, ConnectionEvent.DISCONNECT);
        }

        public static void remove_Disconnect(Connection that, Action<object> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ConnectionEvent.DISCONNECT);
        }
        #endregion

        #region Init
        public static void add_Init(Connection that, Action<object> value)
        {
            CommonExtensions.CombineDelegate(that, value, ConnectionEvent.INIT);
        }

        public static void remove_Init(Connection that, Action<object> value)
        {
            CommonExtensions.RemoveDelegate(that, value, ConnectionEvent.INIT);
        }
        #endregion

        #endregion

        #region Implementation for methods marked with [Script(NotImplementedHere = true)]
        #region Message
        public static void add_Message(Connection that, Action<MessageEvent> value)
        {
            if (!MessageFixup.ContainsKey(value))
                MessageFixup[value] = i => value(MessageEvent.CloneFrom(i));

            CommonExtensions.CombineDelegate(that, MessageFixup[value], MessageEvent.MESSAGE);
        }

        public static void remove_Message(Connection that, Action<MessageEvent> value)
        {
            if (MessageFixup.ContainsKey(value))
            {
                CommonExtensions.RemoveDelegate(that, MessageFixup[value], MessageEvent.MESSAGE);
                MessageFixup.Remove(value);
            }
        }

        internal static readonly Dictionary<Action<MessageEvent>, Action<object>> MessageFixup = new Dictionary<Action<MessageEvent>, Action<object>>();

        #endregion

        #endregion


    }
}
