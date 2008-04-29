using System;
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

        #region Events
        /// <summary>
        /// Dispatched when Flash Player or an AIR application gains operating system focus and becomes active.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> activate;

        /// <summary>
        /// Dispatched when Flash Player or an AIR application loses operating system focus and is becoming inactive.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<Event> deactivate;

        #endregion

    






    }

}
