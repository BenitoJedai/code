using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.flash.text;

namespace ScriptCoreLib.ActionScript.Extensions.flash.text
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(TextField))]
    public static class __TextField
    {

        #region change
        public static void add_change(TextField that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.CHANGE);
        }

        public static void remove_change(TextField that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.CHANGE);
        }
        #endregion

        #region link
        public static void add_link(TextField that, Action<TextEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TextEvent.LINK);
        }

        public static void remove_link(TextField that, Action<TextEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TextEvent.LINK);
        }
        #endregion

        #region scroll
        public static void add_scroll(TextField that, Action<Event> value)
        {
            CommonExtensions.CombineDelegate(that, value, Event.SCROLL);
        }

        public static void remove_scroll(TextField that, Action<Event> value)
        {
            CommonExtensions.RemoveDelegate(that, value, Event.SCROLL);
        }
        #endregion

        #region textInput
        public static void add_textInput(TextField that, Action<TextEvent> value)
        {
            CommonExtensions.CombineDelegate(that, value, TextEvent.TEXT_INPUT);
        }

        public static void remove_textInput(TextField that, Action<TextEvent> value)
        {
            CommonExtensions.RemoveDelegate(that, value, TextEvent.TEXT_INPUT);
        }
        #endregion



    }
}
