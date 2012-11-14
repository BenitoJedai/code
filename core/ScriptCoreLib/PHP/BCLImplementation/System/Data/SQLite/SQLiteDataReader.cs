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

            var x = MySQL.API.mysql_fetch_array(
                queryResult,
                MySQL.API.FetchArrayResult.MYSQL_ASSOC
            );

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
                if ((string)Keys[j] == name)
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



        public override string GetName(int ordinal)
        {
            var keys = (object[])cursor.Keys;
            var name = keys[ordinal];

            return (string)name;
        }

        public override int GetInt32(int i)
        {
            var keys = (object[])cursor.Keys;
            var name = (string)keys[i];
            var value = (string)cursor[name];
            var ivalue = int.Parse(value);

            return ivalue;
        }


        public override long GetInt64(int ordinal)
        {
            var keys = (object[])cursor.Keys;
            var name = (string)keys[ordinal];
            var value = (string)cursor[name];

            // Int64?
            var ivalue = int.Parse(value);

            return ivalue;
        }

        public override Type GetFieldType(int ordinal)
        {
            // what are the actual types?
            // http://www.php.net/manual/en/function.mysql-fetch-field.php
            // http://www.w3schools.com/php/func_mysql_fetch_field.asp

            // http://www.php.net/manual/en/function.mysql-field-type.php

            var f = MySQL.API.mysql_field_type(this.queryResult, ordinal);
            //"int", "real", "string", "blob", 


            if (f == "int")
                return typeof(int);

            // In MySQL 4.1.x, the four TEXT types (TINYTEXT, TEXT, MEDIUMTEXT, and LONGTEXT) return 'blob" as field types, not "string".
            // how to fix that?
            if (f == "string")
                return typeof(string);
            if (f == "blob")
                return typeof(string);

            throw new Exception("GetFieldType unknown type: " + f);
            //return __Type.InternalGetTypeFromClassTokenName(f.type);
        }

        public override int FieldCount
        {
            get
            {
                var keys = (object[])cursor.Keys;

                return keys.Length;
            }
        }
    }

}
