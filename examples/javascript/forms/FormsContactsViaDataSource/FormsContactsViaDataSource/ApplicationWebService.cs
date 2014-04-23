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
            // http://stackoverflow.com/questions/11275650/how-to-increase-heap-size-of-an-android-application


            //Implementation not found for type import :
            //type: System.Type
            //method: System.Type GetType(System.String)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            //assembly: V:\staging\clr\FormsContactsViaDataSource.ApplicationWebService

            var VMRuntime = Type.GetType("dalvik.system.VMRuntime");

            Console.WriteLine(
                new { VMRuntime }
                );

            //var MinimumHeapSize = dalvik.system.VMRuntime.getRuntime().getMinimumHeapSize();


            //Console.WriteLine(
            //    new { MinimumHeapSize }
            //    );

            // http://www.cs.cmu.edu/~srini/15-446/android/android-sdk-linux_x86-1.0_r2/docs/reference/dalvik/system/VMRuntime.html

            //java.lang.Runtime.getRuntime().

            // X:\jsc.svn\examples\javascript\android\forms\AndroidFormsContactsDataTable\AndroidFormsContactsDataTable\ApplicationWebService.cs
            // http://www.higherpass.com/Android/Tutorials/Working-With-Android-Contacts/
            ContentResolver cr = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getContentResolver();

            Cursor cur = cr.query(
                ContactsContract.Contacts.CONTENT_URI,
                    null, null, null, null);

            // http://developer.android.com/reference/android/provider/ContactsContract.Contacts.html


            return
                from index in Enumerable.Range(
                    0,

                    // take?
                    cur.getCount().Min(10)

                    )

                where cur.moveToNext()

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
