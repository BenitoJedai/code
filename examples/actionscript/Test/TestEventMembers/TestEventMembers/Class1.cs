using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;

namespace ScriptCoreLib.ActionScript.Extensions.flash.display
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(InteractiveObject))]
    internal static class __InteractiveObject
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]

        public static void add_click(InteractiveObject that, Action<object> value)
        {
        }

        public static void remove_click(InteractiveObject that, Action<object> value)
        {
        }
        #endregion

    }
}
namespace ScriptCoreLib.ActionScript.flash.display
{
    [Script(IsNative = true)]
    public class InteractiveObject
    {

        /// <summary>
        /// Dispatched when a user presses and releases the main button of the user's pointing device over the same InteractiveObject.
        /// </summary>
        [method: Script(NotImplementedHere = true)]
        public event Action<object> click;
    }
}

namespace TestEventMembers
{
    public class Class1
    {
    }
}
