using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Configuration
{
    [Script(Implements = typeof(global::System.Configuration.SettingsBase))]
    internal class __SettingsBase
    {

        public static __SettingsBase Synchronized(__SettingsBase settingsBase)
        {
            // javascript is single threaded...
            // we do have worker threads!

            return settingsBase;
        }


        public virtual object this[string propertyName]
        {
            get
            {
                // Error	205	'ScriptCoreLib.JavaScript.DOM.IWindow' does not contain a definition for 'localStorage' and no extension method 'localStorage' accepting a first argument of type 'ScriptCoreLib.JavaScript.DOM.IWindow' could be found (are you missing a using directive or an assembly reference?)	X:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\JavaScript\BCLImplementation\System\Configuration\SettingsBase.cs	16	50	ScriptCoreLib.Redux
                // Error	207	Cannot implicitly convert type 'ScriptCoreLib.JavaScript.DOM.IWindow [x:\jsc.svn\core\ScriptCoreLib\bin\Debug\ScriptCoreLib.dll]' to 'ScriptCoreLib.JavaScript.DOM.IWindow [x:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\JavaScript\DOM\IWindow.cs(8)]'	X:\jsc.svn\core\ScriptCoreLib.Redux\ScriptCoreLib.Redux\JavaScript\BCLImplementation\System\Configuration\SettingsBase.cs	19	29	ScriptCoreLib.Redux


                var w = Native.window;
                object r = null;

                var localStorage = w.localStorage;
                // not available for android webview
                if (localStorage != null)
                {
                    var value = localStorage[propertyName];

                    r = value;

                    try
                    {
                        var int32 = int.Parse(value);

                        var int32string = "" + int32;
                        if (value == int32string)
                            r = int32;
                    }
                    catch
                    {

                    }
                }

                return r;
            }
            set
            {
                var w = Native.window;
                var localStorage = w.localStorage;
                if (localStorage != null)
                    localStorage[propertyName] = "" + value;
            }
        }

    }
}
