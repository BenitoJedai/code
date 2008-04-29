using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/IEventDispatcher.html
    [Script(IsNative = true)]
    public interface IEventDispatcher
    {
        #region Methods
        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        void addEventListener(string type, Function listener, bool useCapture, int priority, bool useWeakReference);

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        void addEventListener(string type, Function listener, bool useCapture, int priority);

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        void addEventListener(string type, Function listener, bool useCapture);

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener receives notification of an event.
        /// </summary>
        void addEventListener(string type, Function listener);

        /// <summary>
        /// Dispatches an event into the event flow.
        /// </summary>
        bool dispatchEvent(Event @event);

        /// <summary>
        /// Checks whether the EventDispatcher object has any listeners registered for a specific type of event.
        /// </summary>
        bool hasEventListener(string type);

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        void removeEventListener(string type, Function listener, bool useCapture);

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        void removeEventListener(string type, Function listener);

        /// <summary>
        /// Checks whether an event listener is registered with this EventDispatcher object or any of its ancestors for the specified event type.
        /// </summary>
        bool willTrigger(string type);

        #endregion

        #region Constructors
        #endregion

    }
}
