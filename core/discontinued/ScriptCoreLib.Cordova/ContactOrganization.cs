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
    /// Contains organization properties of a Contact object
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#ContactOrganization
    /// </summary>
    [Script(IsNative = true)]
    public class ContactOrganization
    {
        #region Constructor

        public ContactOrganization(bool pref, string type, string name, string dept, string title)
        {

        }
        
        #endregion

        #region Properties 

        /// <summary>
        ///  Set to true if this ContactOrganization contains the user's preferred value. (boolean)
        /// </summary>
        public bool pref;

        /// <summary>
        ///  A string that tells you what type of field this is (example: 'home'). _(DOMString)
        /// </summary>
        public string type;

        /// <summary>
        ///  The name of the organization. (DOMString)
        /// </summary>
        public string name;

        /// <summary>
        ///  The department the contract works for. (DOMString)
        /// </summary>
        public string department;

        /// <summary>
        ///  The contacts title at the organization. (DOMString)
        /// </summary>
        public string title;

        #endregion

    }
}
