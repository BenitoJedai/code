using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;

[assembly: Script]
[assembly: ScriptTypeFilter(ScriptType.ActionScript)]



namespace ScriptCoreLib.ActionScript.Extensions.flash.display
{
    // if a type implements a type that is set to be native, then only implementation
    // which is marked with NotImplementedHere applies

    [Script(Implements = typeof(InteractiveObject))]
    internal static class __InteractiveObject
    {
        #region Implementation for methods marked with [Script(NotImplementedHere = true)]

        public static void add_click(InteractiveObject that, Action value)
        {
        }

        public static void remove_click(InteractiveObject that, Action  value)
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
        public event Action click;
    }
}

namespace TestEventMembers
{
    [Script]
    public class Class1
    {
        static void Main()
        {
            var i = new InteractiveObject();

            i.click += delegate
            {

            };
        }
    }
}



