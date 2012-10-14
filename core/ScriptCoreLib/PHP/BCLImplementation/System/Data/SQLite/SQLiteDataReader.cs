using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.SQLite
{
    [Script(Implements = typeof(global::System.Data.SQLite.SQLiteDataReader))]
    internal class __SQLiteDataReader : __DbDataReader
    {
        public IArray cursor; // Cursor cursor;
        public object queryResult;

        int __state;
        int __index = 0;

        public override void Close()
        {
            // ?
        }

        public override bool Read()
        {
            if (queryResult == null)
                return false;

            var x = MySQL.API.mysql_fetch_array(queryResult, MySQL.API.FetchArrayResult.MYSQL_BOTH);

            var e = Expando.Of(x);

            //Console.WriteLine(e.TypeString);

            if (e.IsArray)
            {
                cursor = x;

                if (cursor != null)
                    if (cursor.Length > 0)
                    {

                        return true;
                    }
            }

            return false;

            /*
            if (__state == 0)
            {
                __state = 1;
    
                //cursor.moveToFirst();

                __index = 0; // move to first                
            }
            else
            {
                //cursor.moveToNext();

                __index++; // move to next
            }

            return __index < cursor.Length; //!(cursor.isAfterLast());
             * */
        }

        public override int GetOrdinal(string name)
        {
            var Keys = cursor.Keys;

            var i = -1;

            for (int j = 0; j < Keys.Length; j++)
            {
                if (Keys[j] == name)
                {
                    i = j;
                    break;
                }
            }

            return i;
        }

        public override object this[string name]
        {
            get
            {
                // int i = cursor.getColumnIndex(name);

                // return cursor.getString(i);

                var Keys = cursor.Keys;
                if (Keys.Contains(name))
                {
                    var value = cursor[name];
                    return value;
                }

                return null;
            }
        }

        public override string GetString(int i)
        {
            // int i = cursor.getColumnIndex(name);

            // return cursor.getString(i);

            var keys = (object[])cursor.Keys;
            var name = keys[i];

            return (string)cursor[name];
        }

        public override int GetInt32(int i)
        {
            // int i = cursor.getColumnIndex(name);

            // return cursor.getString(i);

            var keys = (object[])cursor.Keys;
            var name =  (string)keys[i];
            var value = (string)cursor[name];
            var ivalue = int.Parse(value);

            return ivalue;
        }

    }


}
