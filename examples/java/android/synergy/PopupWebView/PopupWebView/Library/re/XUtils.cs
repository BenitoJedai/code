using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopupWebView.Library.re
{
    class XUtils
    {
        public static bool isSet(int flags, int flag)
        {
            return (flags & flag) == flag;
        }
    }
}
