﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Text
{
    [Script(Implements = typeof(global::System.Text.Encoding))]
    internal abstract class __Encoding
    {
        public virtual string GetString(byte[] bytes)
        {
            return default(string);
        }

        public virtual byte[] GetBytes(string s)
        {
            return default(byte[]);
        }

        [Obsolete("Not Supported By Portable Class Library")]
        public static Encoding ASCII
        {
            get
            {
                // cache?
                return new ASCIIEncoding();
            }
        }
    }
}
