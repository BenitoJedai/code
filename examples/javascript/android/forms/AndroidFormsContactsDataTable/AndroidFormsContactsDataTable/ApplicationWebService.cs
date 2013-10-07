using android.content;
using android.database;
using android.provider;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AndroidFormsContactsDataTable
{
    // in a way this is a datatype
    public delegate void AtContact(
        string id,
        string name,
        string email
    );

    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed partial class ApplicationWebService : Component
    {
        public Task<DataTable> GetContacts()
        {
            var table = new DataTable();

            //            script: error JSC1000:
            //error:
            //  statement cannot be a load instruction (or is it a bug?)

            // assembly: W:\staging\clr\AndroidFormsContactsDataTable.ApplicationWebService.AndroidActivity.dll
            // type: AndroidFormsContactsDataTable.ApplicationWebService, AndroidFormsContactsDataTable.ApplicationWebService.AndroidActivity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            // offset: 0x001d
            //  method:System.Threading.Tasks.Task`1[System.Data.DataTable] GetContacts()


            var columns_id = new DataColumn { ColumnName = "id" };
            var columns_name = new DataColumn { ColumnName = "name" };
            var columns_email = new DataColumn { ColumnName = "email" };



            table.Columns.AddRange(new[] { columns_id, columns_name, columns_email });

            AtContact y =
                (
                    string id,
                    string name,
                    string email) =>
                {
                    var row = table.NewRow();

                    row[columns_id] = id;
                    row[columns_name] = name;
                    row[columns_email] = email;

                    table.Rows.Add(row);
                };

#if ReleaseAndroid
            // http://www.higherpass.com/Android/Tutorials/Working-With-Android-Contacts/
            ContentResolver cr = ScriptCoreLib.Android.ThreadLocalContextReference.CurrentContext.getContentResolver();

            // F/System.Console( 4600): #5 java.lang.SecurityException: Permission Denial: reading com.android.providers.contacts.ContactsProvider2 uri content://com.android.contacts/contacts from pid=4600, uid=10002 requires android.permission.READ_CONTACTS, or grantUriPermission()
            Cursor cur = cr.query(
                ContactsContract.Contacts.CONTENT_URI,
                    null, null, null, null);


            if (cur.getCount() > 0)
            {
                var index = 0;
                while (cur.moveToNext())
                {
                    Console.WriteLine(new { index });
                    index++;

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

                            //gravater = "http://www.gravatar.com/avatar/" + Encoding.UTF8.GetBytes(email.ToLower()).ToMD5Bytes().ToHexString();


                        }
                        emails.close();

                        //email = cur.getString(
                        //              cur.getColumnIndex(ContactsContract.CommonDataKinds.Email.ADDRESS));


                    }
                    catch
                    {

                    }

                    y(id, name, email);

                    //                    D/dalvikvm(26286): GC_FOR_ALLOC freed 521K, 9% free 8249K/9056K, paused 19ms, total 19ms
                    //W/CursorWrapperInner(26286): Cursor finalized without prior close()
                }
            }
#else
            y(id: "1", name: "jack sparrow", email: "jack.sparrow@jsc-solutions.net");
#endif
            return Task.FromResult(table);
        }

    }
}
