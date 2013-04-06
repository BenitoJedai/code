using android.content;
using android.database;
using android.provider;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace AndroidContacts
{
    // in a way this is a datatype
    public delegate void AtContact(
        string id,
        string name,
        string email,
        string gravatar
    );

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
        public void GetContacts(string e,
            AtContact y)
        {
#if Android
            // http://www.higherpass.com/Android/Tutorials/Working-With-Android-Contacts/
            ContentResolver cr = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getContentResolver();

            // F/System.Console( 4600): #5 java.lang.SecurityException: Permission Denial: reading com.android.providers.contacts.ContactsProvider2 uri content://com.android.contacts/contacts from pid=4600, uid=10002 requires android.permission.READ_CONTACTS, or grantUriPermission()
            Cursor cur = cr.query(
                ContactsContract.Contacts.CONTENT_URI,
                    null, null, null, null);


            if (cur.getCount() > 0)
            {
                while (cur.moveToNext())
                {
                    var id = cur.getString(
                                cur.getColumnIndex(ContactsContract.Contacts._ID));
                    var name = cur.getString(
                                    cur.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME));

                    var email = "";
                    var gravater = "";

                    // ContactsContract.Contacts

                    try
                    {
                        // http://developer.android.com/reference/android/provider/ContactsContract.CommonDataKinds.Email.html
                        // http://developer.android.com/reference/android/provider/ContactsContract.Data.html
                        // http://developer.android.com/reference/android/provider/ContactsContract.RawContactsColumns.html#CONTACT_ID

                        Cursor emails = cr.query(
                            ContactsContract.CommonDataKinds.Email.CONTENT_URI,
                              null,

                              // how are we supposed to access these constants?
                            // jsc does not inherit interface constants?
                           "contact_id = ?",

                            new[] { id }, null);
                        if (emails.moveToNext())
                        {
                            // This would allow you get several email addresses
                            email = emails.getString(emails.getColumnIndex(ContactsContract.CommonDataKinds.Email.DATA));

                            gravater = "http://www.gravatar.com/avatar/" + Encoding.UTF8.GetBytes(email.ToLower()).ToMD5Bytes().ToHexString();


                        }
                        emails.close();

                        //email = cur.getString(
                        //              cur.getColumnIndex(ContactsContract.CommonDataKinds.Email.ADDRESS));


                    }
                    catch
                    {

                    }

                    y(id, name, email, gravater);
                }
            }
#else
            // emulate the data


            var gravater = "http://www.gravatar.com/avatar/" + Encoding.UTF8.GetBytes("arvo.sulakatko@jsc-solutions.net".ToLower()).ToMD5Bytes().ToHexString();

            y(id: "1", name: "jack sparrow", email: "jack.sparrow@jsc-solutions.net", gravatar: gravater);
#endif

            // Send it back to the caller.
            //y(e);
        }

    }
}
