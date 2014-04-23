using android.content;
using android.database;
using android.provider;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FormsContactsViaDataSource
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        public async Task<IEnumerable<Data.ContactDataGetContactsRow>> GetContacts()
        {
#if DEBUG

            return
                from i in Enumerable.Range(0, 4)
                select new Data.ContactDataGetContactsRow
                {
                    Key = (Data.ContactDataGetContactsKey)i,

                    name = "name" + i,
                    email = "name" + i + "@example.com"
                };
#else

            // X:\jsc.svn\examples\javascript\android\forms\AndroidFormsContactsDataTable\AndroidFormsContactsDataTable\ApplicationWebService.cs
            // http://www.higherpass.com/Android/Tutorials/Working-With-Android-Contacts/
            ContentResolver cr = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getContentResolver();

            Cursor cur = cr.query(
                ContactsContract.Contacts.CONTENT_URI,
                    null, null, null, null);

            // http://developer.android.com/reference/android/provider/ContactsContract.Contacts.html

            return
                from index in Enumerable.Range(0, cur.getCount())

                let id = cur.getLong(
                    cur.getColumnIndex(ContactsContract.Contacts._ID))

                let name = cur.getString(
                    cur.getColumnIndex(ContactsContract.Contacts.DISPLAY_NAME))



                select new Data.ContactDataGetContactsRow
                {
                    Key = (Data.ContactDataGetContactsKey)id,
                    name = name
                };

#endif
        }

    }
}
