using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript
{
    static partial class chrome
    {
        static partial class webstore
        {
            public static void install(string url, Action<string> successCallback, Action<string> failureCallback)
            {
            }
        }

        static partial class bookmarks
        {
            public static BookmarkTreeNode[] getTree(Action<BookmarkTreeNode[]> callback)
            {
                return default(BookmarkTreeNode[]);
            }
        }
    }
}
