using ScriptCoreLib.JavaScript.Controls;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.ComponentModel;
using ScriptCoreLib.Shared.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms
{
    partial class __DataGridView
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140420

        public bool MultiSelect { get; set; }
        public __DataGridViewSelectedCellCollection InternalSelectedCells { get; set; }
        public DataGridViewSelectedCellCollection SelectedCells { get; set; }

        [Obsolete("what about hidden selection, without focus per BindingSource Position?")]
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                var x = new __DataGridViewSelectedRowCollection();

                // script: error JSC1000: No implementation found for this native method, please implement [static System.Linq.Enumerable.Distinct(System.Collections.Generic.IEnumerable`1[[System.Windows.Forms.DataGridViewRow, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]])]

                //JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView.cs(355,42): error CS1061: 'ScriptCoreLib.Shared.Lambda.BindingListWithEvents<ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__DataGridViewRow>' does not contain a definition for 'InternalList' and no extension method 'InternalList' accepting a first argument of type 'ScriptCoreLib.Shared.Lambda.BindingListWithEvents<ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__DataGridViewRow>' could be found (are you missing a using directive or an assembly reference?) [Z:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.csproj]
                //JavaScript\BCLImplementation\System\Windows\Forms\DataGridView\DataGridView.cs(356,41): error CS1061: 'ScriptCoreLib.Shared.Lambda.BindingListWithEvents<ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__DataGridViewRow>' does not contain a definition for 'InternalList' and no extension method 'InternalList' accepting a first argument of type 'ScriptCoreLib.Shared.Lambda.BindingListWithEvents<ScriptCoreLib.JavaScript.BCLImplementation.System.Windows.Forms.__DataGridViewRow>' could be found (are you missing a using directive or an assembly reference?) [Z:\jsc.svn\core\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms\ScriptCoreLib.Windows.Forms.csproj]

                // X:\jsc.svn\core\ScriptCoreLib\Shared\Lambda\BindingListWithEvents.cs

                foreach (var item in InternalSelectedCells.InternalItems)
                {
                    if (!x.InternalItems.Source.Contains(item.OwningRow))
                        x.InternalItems.Source.Add(item.OwningRow);

                }


                return (DataGridViewSelectedRowCollection)(object)x;
            }
        }

    }
}
