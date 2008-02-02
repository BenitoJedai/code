﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/EventDispatcher.html
    [Script(IsNative = true)]
    public class EventDispatcher
    {

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public void addEventListener(string type, Function listener, bool useCapture, int priority, bool useWeakReference)
        {

        }

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        public void removeEventListener(string type, Function listener, bool useCapture)
        {

        }

        [method: Script(NotImplementedHere = true)]
        public event Action<Event> activate;

        [method: Script(NotImplementedHere = true)]
        public event Action<Event> deactivate;






    }

}
