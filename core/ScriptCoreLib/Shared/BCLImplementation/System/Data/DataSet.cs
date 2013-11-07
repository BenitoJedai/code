using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Data
{
    [Script(Implements = typeof(global::System.Data.DataSet))]
    internal class __DataSet : __MarshalByValueComponent
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201311/20131107/assetslibrary

        public __DataSet()
        {
            // Error	2	'System.Data.DataTableCollection' does not contain a constructor that takes 1 arguments	X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Data\DataSet.cs	17	27	ScriptCoreLib

            this.Tables = new __DataTableCollection();
        }
        public DataTableCollection Tables { get; set; }
    }
}
