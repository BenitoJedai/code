﻿using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataTable))]
    public class __DataTable : __MarshalByValueComponent
    {
        // X:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\JavaScript\BCLImplementation\System\Windows\Forms\DataGridView.cs

        public string TableName { get; set; }

        public DataColumnCollection Columns { get; internal set; }

        public __DataTable()
        {
            this.Columns = new __DataColumnCollection();
            this.Rows = new __DataRowCollection();
        }

        public DataRowCollection Rows { get; internal set; }

        public DataRow NewRow()
        {
            return new __DataRow();
        }
    }
}
