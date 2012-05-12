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
    /// Supports generic fields in a Contact object. 
    /// Some properties that are stored as ContactField objects include email addresses, phone numbers, and urls
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#ContactField
    /// </summary>
    [Script(IsNative = true)]
    public class ContactField
    {
        #region Constructor

        public ContactField(string type, string value, bool pref)
        {

        }
        
        #endregion

        #region Properties 

        /// <summary>
        ///  A string that tells you what type of field this is (example: 'home'). (DOMString)
        /// </summary>
        public string type;

        /// <summary>
        ///  The value of the field (such as a phone number or email address). (DOMString)
        /// </summary>
        public string value;

        /// <summary>
        ///  Set to true if this ContactField contains the user's preferred value. (boolean)
        /// </summary>
        public bool pref;

        #endregion

    }
}
