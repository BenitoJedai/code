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
    /// Contains name properties of a Contact object
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#ContactName
    /// </summary>
    [Script(IsNative = true)]
    public class ContactName
    {
        #region Constructor

        public ContactName(string formatted, string familyName, string givenName, string middle, string prefix, string suffix)
        {

        }
        
        #endregion

        #region Properties 

        /// <summary>
        /// formatted: The complete name of the contact. (DOMString)
        /// </summary>
        public string formatted;
        
        /// <summary>
        /// familyName: The contacts family name. (DOMString) 
        /// </summary>
        public string familyName;

        /// <summary>
        /// The contacts given name. (DOMString)
        /// </summary>
        public string givenName;

        /// <summary>
        /// : The contacts middle name. (DOMString)
        /// </summary>
        public string middleName;

        /// <summary>
        ///  The contacts prefix (example Mr. or Dr.) (DOMString)
        /// </summary>
        public string honorificPrefix;

        /// <summary>
        /// The contacts suffix (example Esq.). (DOMString)
        /// </summary>
        public string honorificSuffix;

        #endregion

    }
}
