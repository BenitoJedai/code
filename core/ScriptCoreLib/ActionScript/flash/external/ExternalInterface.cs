using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.external
{
    //http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/flash/external/ExternalInterface.html
    [Script(IsNative = true)]
    public static class ExternalInterface
    {
        /// <summary>
        /// Indicates whether this player is in a container that offers an external interface.
        /// </summary>
        static public bool available { get; private set; }

        /// <summary>
        /// Indicates whether the external interface should attempt to pass ActionScript exceptions to the current browser and JavaScript exceptions to Flash Player.
        /// </summary>
        static public bool marshallExceptions { get; set; }

        /// <summary>
        /// Returns the id attribute of the object tag in Internet Explorer, or the name attribute of the embed tag in Netscape.
        /// </summary>
        static public string objectID { get; private set; }


        /// <summary>
        /// Registers an ActionScript method as callable from the container.
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="closure"></param>
        public static void addCallback(string functionName, Function closure)
        {
        }
 	 	
        /// <summary>
        /// Calls a function exposed by the Flash Player container, passing zero or more arguments.
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public static object call(string functionName, object arguments)
        {
            return default(object);
        }

    }

}
