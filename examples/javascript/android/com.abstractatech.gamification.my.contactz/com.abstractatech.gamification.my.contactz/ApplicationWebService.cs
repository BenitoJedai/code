using android.content;
using android.database;
using android.provider;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Xml.Linq;

namespace com.abstractatech.gamification.my.contactz
{
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
        public void WebMethod2(string e, Action<string> y, Action done)
        {
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


                    y(new { id, name }.ToString());
                }
            }

            done();
        }

    }
}
