using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.events
{
    // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/events/Event.html
    [Script(IsNative = true)]
    public class Event
    {
        #region Constants
        /// <summary>
        /// [static] The Event.ACTIVATE constant defines the value of the type property of an activate event object.
        /// </summary>
        public static readonly string ACTIVATE = "activate";

        /// <summary>
        /// [static] The Event.ADDED constant defines the value of the type property of an added event object.
        /// </summary>
        public static readonly string ADDED = "added";

        /// <summary>
        /// [static] The Event.ADDED_TO_STAGE constant defines the value of the type property of an addedToStage event object.
        /// </summary>
        public static readonly string ADDED_TO_STAGE = "addedToStage";

        /// <summary>
        /// [static] The Event.CANCEL constant defines the value of the type property of a cancel event object.
        /// </summary>
        public static readonly string CANCEL = "cancel";

        /// <summary>
        /// [static] The Event.CHANGE constant defines the value of the type property of a change event object.
        /// </summary>
        public static readonly string CHANGE = "change";

        /// <summary>
        /// [static] The Event.CLOSE constant defines the value of the type property of a close event object.
        /// </summary>
        public static readonly string CLOSE = "close";

        /// <summary>
        /// [static] The Event.COMPLETE constant defines the value of the type property of a complete event object.
        /// </summary>
        public static readonly string COMPLETE = "complete";

        /// <summary>
        /// [static] The Event.CONNECT constant defines the value of the type property of a connect event object.
        /// </summary>
        public static readonly string CONNECT = "connect";

        /// <summary>
        /// [static] The Event.DEACTIVATE constant defines the value of the type property of a deactivate event object.
        /// </summary>
        public static readonly string DEACTIVATE = "deactivate";

        /// <summary>
        /// [static] The Event.ENTER_FRAME constant defines the value of the type property of an enterFrame event object.
        /// </summary>
        public static readonly string ENTER_FRAME = "enterFrame";

        /// <summary>
        /// [static] The Event.FULL_SCREEN constant defines the value of the type property of a fullScreen event object.
        /// </summary>
        public static readonly string FULLSCREEN = "fullScreen";

        /// <summary>
        /// [static] The Event.ID3 constant defines the value of the type property of an id3 event object.
        /// </summary>
        public static readonly string ID3 = "id3";

        /// <summary>
        /// [static] The Event.INIT constant defines the value of the type property of an init event object.
        /// </summary>
        public static readonly string INIT = "init";

        /// <summary>
        /// [static] The Event.MOUSE_LEAVE constant defines the value of the type property of a mouseLeave event object.
        /// </summary>
        public static readonly string MOUSE_LEAVE = "mouseLeave";

        /// <summary>
        /// [static] The Event.OPEN constant defines the value of the type property of an open event object.
        /// </summary>
        public static readonly string OPEN = "open";

        /// <summary>
        /// [static] The Event.REMOVED constant defines the value of the type property of a removed event object.
        /// </summary>
        public static readonly string REMOVED = "removed";

        /// <summary>
        /// [static] The Event.REMOVED_FROM_STAGE constant defines the value of the type property of a removedFromStage event object.
        /// </summary>
        public static readonly string REMOVED_FROM_STAGE = "removedFromStage";

        /// <summary>
        /// [static] The Event.RENDER constant defines the value of the type property of a render event object.
        /// </summary>
        public static readonly string RENDER = "render";

        /// <summary>
        /// [static] The Event.RESIZE constant defines the value of the type property of a resize event object.
        /// </summary>
        public static readonly string RESIZE = "resize";

        /// <summary>
        /// [static] The Event.SCROLL constant defines the value of the type property of a scroll event object.
        /// </summary>
        public static readonly string SCROLL = "scroll";

        /// <summary>
        /// [static] The Event.SELECT constant defines the value of the type property of a select event object.
        /// </summary>
        public static readonly string SELECT = "select";

        /// <summary>
        /// [static] The Event.SOUND_COMPLETE constant defines the value of the type property of a soundComplete event object.
        /// </summary>
        public static readonly string SOUND_COMPLETE = "soundComplete";

        /// <summary>
        /// [static] The Event.TAB_CHILDREN_CHANGE constant defines the value of the type property of a tabChildrenChange event object.
        /// </summary>
        public static readonly string TAB_CHILDREN_CHANGE = "tabChildrenChange";

        /// <summary>
        /// [static] The Event.TAB_ENABLED_CHANGE constant defines the value of the type property of a tabEnabledChange event object.
        /// </summary>
        public static readonly string TAB_ENABLED_CHANGE = "tabEnabledChange";

        /// <summary>
        /// [static] The Event.TAB_INDEX_CHANGE constant defines the value of the type property of a tabIndexChange event object.
        /// </summary>
        public static readonly string TAB_INDEX_CHANGE = "tabIndexChange";

        /// <summary>
        /// [static] The Event.UNLOAD constant defines the value of the type property of an unload event object.
        /// </summary>
        public static readonly string UNLOAD = "unload";

        #endregion

        #region Properties
        /// <summary>
        /// [read-only] Indicates whether an event is a bubbling event.
        /// </summary>
        public bool bubbles { get; private set; }

        /// <summary>
        /// [read-only] Indicates whether the behavior associated with the event can be prevented.
        /// </summary>
        public bool cancelable { get; private set; }

        /// <summary>
        /// [read-only] The object that is actively processing the Event object with an event listener.
        /// </summary>
        public object currentTarget { get; private set; }

        /// <summary>
        /// [read-only] The current phase in the event flow.
        /// </summary>
        public uint eventPhase { get; private set; }

        /// <summary>
        /// [read-only] The event target.
        /// </summary>
        public object target { get; private set; }

        /// <summary>
        /// [read-only] The type of event.
        /// </summary>
        public string type { get; private set; }

        #endregion

    }
}
