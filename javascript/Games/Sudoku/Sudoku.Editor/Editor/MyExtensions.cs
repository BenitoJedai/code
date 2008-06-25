using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript.DOM.XML;
using ScriptCoreLib.JavaScript.Net;
using ScriptCoreLib.Shared;


namespace Sudoku.Editor
{
    [Script]
    static class MyExtensions
    {
        public static void DownloadToString(this string url, Action<string> handler)
        {
            new IXMLHttpRequest(HTTPMethodEnum.GET, url,
                r =>
                {
                    handler(r.responseText);
                }
            );
                    
        }
    }
}
