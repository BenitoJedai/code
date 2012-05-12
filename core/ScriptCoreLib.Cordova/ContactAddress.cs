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
    /// Contains address properties for a Contact object
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#ContactAddress
    /// </summary>
    [Script(IsNative = true)]
    public class ContactAddress
    {
        #region Constructor

        public ContactAddress(bool pref, string type, string formatted, string streetAddress, string locality, string region, string postalCode, string country)
        {

        }
        
        #endregion

        #region Properties 

        /// <summary>
        ///  Set to true if this ContactAddress contains the user's preferred value. (boolean)
        /// </summary>
        public bool pref;

        /// <summary>
        ///  A string that tells you what type of field this is (example: 'home'). _(DOMString)
        /// </summary>
        public string type;

        /// <summary>
        ///  The full address formatted for display. (DOMString)
        /// </summary>
        public string formatted;

        /// <summary>
        ///  The full street address. (DOMString)
        /// </summary>
        public string streetAddress;

        /// <summary>
        ///  The city or locality. (DOMString)
        /// </summary>
        public string locality;

        /// <summary>
        ///  The state or region. (DOMString)
        /// </summary>
        public string region;

        /// <summary>
        ///  The zip code or postal code. (DOMString)
        /// </summary>
        public string postalCode;

        /// <summary>
        ///  The country name. (DOMString)
        /// </summary>
        public string country;

        #endregion

    }
}
