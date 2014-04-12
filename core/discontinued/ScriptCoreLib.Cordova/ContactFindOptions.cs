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
    /// Contains properties that can be used to filter the results of a contacts.find operation
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#ContactFindOptions
    /// </summary>
    [Script(IsNative = true)]
    public class ContactFindOptions
    {
        #region Constructor

        public ContactFindOptions()
        {

        }
        
        #endregion

        #region Properties 

        /// <summary>
        /// filter: The search string used to find contacts. (DOMString) (Default: "")
        /// </summary>
        public string filter;

        /// <summary>
        /// multiple: Determines if the find operation should return multiple contacts. (Boolean) (Default: false)
        /// </summary>
        public bool multiple=false;


        #endregion

    }
}
