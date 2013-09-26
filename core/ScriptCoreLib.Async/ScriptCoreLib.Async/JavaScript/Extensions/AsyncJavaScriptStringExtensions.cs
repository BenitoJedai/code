using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCoreLib.JavaScript.Extensions
{
    public static class AsyncJavaScriptStringExtensions
    {
        public static async Task<string> ToDocumentTitle(this Task<string> title)
        {
            var value = await title;

            value.ToDocumentTitle();

            return value;
        }
    }
}
