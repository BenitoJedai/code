using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/EventDispatcher.html
    [Script(IsNative = true)]
    public class EventDispatcher : IEventDispatcher
    {

        #region Methods
        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public void addEventListener(string type, Function listener, bool useCapture, int priority, bool useWeakReference)
        {
        }

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public void addEventListener(string type, Function listener, bool useCapture, int priority)
        {
        }

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public void addEventListener(string type, Function listener, bool useCapture)
        {
        }

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        public void addEventListener(string type, Function listener)
        {
        }

        /// <summary>
        /// Dispatches an event into the event flow.
        /// </summary>
        public bool dispatchEvent(Event @event)
        {
            return default(bool);
        }

        /// <summary>
        /// Checks whether the EventDispatcher object has any listeners registered for a specific type of event.
        /// </summary>
        public bool hasEventListener(string type)
        {
            return default(bool);
        }

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        public void removeEventListener(string type, Function listener, bool useCapture)
        {
        }

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        public void removeEventListener(string type, Function listener)
        {
        }

        /// <summary>
        /// Checks whether an event listener is registered with this EventDispatcher object or any of its ancestors for the specified event type.
        /// </summary>
        public bool willTrigger(string type)
        {
            return default(bool);
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Aggregates an instance of the EventDispatcher class.
        /// </summary>
        public EventDispatcher(IEventDispatcher target)
        {
        }

        /// <summary>
        /// Aggregates an instance of the EventDispatcher class.
        /// </summary>
        public EventDispatcher()
        {
        }

        #endregion


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
