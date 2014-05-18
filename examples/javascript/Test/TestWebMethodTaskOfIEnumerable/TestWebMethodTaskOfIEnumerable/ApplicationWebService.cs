using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestWebMethodTaskOfIEnumerable
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // x:\jsc.svn\examples\javascript\test\testienumerableforservice\testienumerableforservice\applicationwebservice.cs
        // X:\jsc.svn\examples\javascript\forms\FormsNICWithDataSource\FormsNICWithDataSource\ApplicationWebService.cs
        // X:\jsc.svn\core\ScriptCoreLib.Ultra\ScriptCoreLib.Ultra\Ultra\Library\StringConversions.cs

        [Obsolete("async is to please the client, server runs this sync and buffers all returned items. for now.")]
        public async Task<IEnumerable<string>> GetStrings()
        {
            return
                from i in Enumerable.Range(0, 33)
                let goo = "foo"
                select new { i, goo }.ToString();
        }
    }
}
