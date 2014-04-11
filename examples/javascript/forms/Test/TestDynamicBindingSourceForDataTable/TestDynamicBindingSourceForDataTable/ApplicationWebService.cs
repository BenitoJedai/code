using FormsAutoSumGridSelection.Data;
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

namespace TestDynamicBindingSourceForDataTable
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public partial class ApplicationWebService : Component
    {
        public async void AsyncDataSourceImport(Action<ZooBookSheet1Row> yield)
        {
            var r = new ZooBookSheet1Row { FooColumn = "foo from server", GooColumn = 400 };

            yield(r);
        }
    }
}
