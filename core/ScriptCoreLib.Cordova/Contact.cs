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
    /// Contains properties that describe a contact, such as a user's personal or business contact
    /// http://docs.phonegap.com/en/1.7.0/cordova_contacts_contacts.md.html#Contact
    /// </summary>
    [Script(IsNative = true)]
    public class Contact
    {
        #region Constructor

        public Contact(string id = null, string displayName = null, string name = null, string nickname=null, ContactField[] phoneNumbers = null, ContactField[] emails = null, ContactAddress[] addresses = null,
    ContactField[] ims = null, ContactOrganization[] organizations = null, JavaScript.DOM.IDate birthday = null, string note=null, ContactField[] photos = null, ContactField[] categories = null, ContactField[] urls = null)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        ///  A globally unique identifier. (DOMString)
        /// </summary>
        public string id;

        /// <summary>
        ///  The name of this Contact, suitable for display to end-users. (DOMString)
        /// </summary>
        public string displayName;

        /// <summary>
        ///  An object containing all components of a persons name. (ContactName)
        /// </summary>
        public string name;

        /// <summary>
        ///  A casual name to address the contact by. (DOMString)
        /// </summary>
        public string nickname;

        /// <summary>
        ///  An array of all the contact's phone numbers. (ContactField[])
        /// </summary>
        public ContactField[] phoneNumbers;

        /// <summary>
        ///  An array of all the contact's email addresses. (ContactField[])
        /// </summary>
        public ContactField[] emails;

        /// <summary>
        ///  An array of all the contact's addresses. (ContactAddresses[])
        /// </summary>
        public ContactAddress[] addresses;

        /// <summary>
        ///  An array of all the contact's IM addresses. (ContactField[])
        /// </summary>
        public ContactField[] ims;

        /// <summary>
        ///  An array of all the contact's organizations. (ContactOrganization[])
        /// </summary>
        public ContactOrganization[] organizations;

        /// <summary>
        ///  The birthday of the contact. (Date)
        /// </summary>
        public DateTime birthday;

        /// <summary>
        ///  A note about the contact. (DOMString)
        /// </summary>
        public string note;

        /// <summary>
        ///  An array of the contact's photos. (ContactField[])
        /// </summary>
        public ContactField[] photos;

        /// <summary>
        ///  An array of all the contacts user defined categories. (ContactField[])
        /// </summary>
        public ContactField[] categories;

        /// <summary>
        ///  An array of web pages associated to the contact. (ContactField[])
        /// </summary>
        public ContactField[] urls;

        #endregion

        #region Methods

        /// <summary>
        ///  Returns a new Contact object that is a deep copy of the calling object, with the id property set to null.
        /// </summary>
        /// <returns></returns>
        public Contact clone()
        {
            return default(Contact);
        }

        /// <summary>
        ///  Removes the contact from the device contacts database. 
        ///  An error callback is called with a ContactError object if the removal is unsuccessful.
        /// </summary>
        public void remove(Action onSuccess, Action<ContactError> onError) { }

        /// <summary>
        /// Saves a new contact to the device contacts database, or updates an existing contact if a contact with the same id already exists.
        /// </summary>
        public void save()
        {}

        #endregion
    }
}
