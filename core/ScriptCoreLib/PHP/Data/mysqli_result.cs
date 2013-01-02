using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.Data
{
    // http://php.net/manual/en/class.mysqli-result.php
    [Script(IsNative = true)]
    public class mysqli_result
    {
        public int field_count;
        public int num_rows;


        // http://php.net/manual/en/mysqli-result.fetch-field-direct.php

        /// <summary>
        /// Fetch meta-data for a single field
        /// </summary>
        /// <param name="fieldnr">The field number. This value must be in the range from 0 to number of fields - 1.</param>
        /// <returns>Returns an object which contains field definition information from the specified result set.</returns>
        public mysqli_result_field fetch_field_direct(int fieldnr)
        {
            return default(mysqli_result_field);
        }

        public bool data_seek(int offset)
        {
            return default(bool);
        }

        public object[] fetch_row()
        {
            return default(object[]);
        }

        public void close()
        {

        }
    }

    [Script(IsNative = true)]
    public abstract class mysqli_result_field
    {
        public string name;
        public int type;


    }


}
