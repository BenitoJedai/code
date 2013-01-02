using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript
{
    static partial class chrome
    {
        static partial class omnibox
        {
            // http://developer.chrome.com/extensions/omnibox.html
            public static void setDefaultSuggestion(object suggestion)
            {
            }
        }

        static partial class webstore
        {
            // https://developers.google.com/chrome/apps/docs/no_crx
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

        public partial class debugger
        {
            // http://developer.chrome.com/extensions/debugger.html#method-attach
            public static void attach(object target, string requiredVersion)
            {
            }
        }
    }
}
