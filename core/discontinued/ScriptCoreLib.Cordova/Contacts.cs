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
    /// The contacts object provides access to the device contacts database
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#Contacts
    /// </summary>
    [Script(IsNative = true)]
    public class Contacts
    {
        #region Constructor

        public Contacts()
        {

        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a new Contact object
        /// </summary>
        /// <param name="?"></param>
        public Contact create(object properties)
        {
            return default(Contact);
        }


        public void find(string[] contactFields, Action<Contact[]> contactSuccess, Action<ContactError>contactError, ContactFindOptions contactFindOptions)
        {

        }

        #endregion

    }
}
