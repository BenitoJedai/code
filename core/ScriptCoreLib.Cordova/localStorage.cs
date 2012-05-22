using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;

using ScriptCoreLib.Shared.Drawing;
using System;


namespace ScriptCoreLib.Cordova
{
    /// <summary>
    /// Provides access to a W3C Storage interface (http://dev.w3.org/html5/webstorage/#the-localstorage-attribute)
    /// http://docs.phonegap.com/en/1.7.0/cordova_storage_storage.md.html#localStorage
    /// </summary>
    [Script(IsNative = true)]
    public class LocalStorage
    {
        #region Constructor

        public LocalStorage()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        ///  Returns the name of the key at the position specified.
        /// </summary>
        /// <param name="index"></param>
        public string key(int index)
        {
            return default(string);
        }

        /// <summary>
        ///  Returns the item identified by it's key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getItem(string key)
        {
            return default(string);
        }
        
        /// <summary>
        /// Saves and item at the key provided.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void setItem(string key, string value)
        {
        }

        /// <summary>
        /// Removes the item identified by it's key.
        /// </summary>
        /// <param name="key"></param>
        public void removeItem(string key){}
        
        /// <summary>
        ///  Removes all of the key value pairs.
        /// </summary>
        public void clear(){}

        #endregion


    }
}
