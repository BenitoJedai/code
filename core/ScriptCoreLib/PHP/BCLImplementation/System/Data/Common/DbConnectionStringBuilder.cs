﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Data.Common
{
    [Script(Implements = typeof(global::System.Data.Common.DbConnectionStringBuilder))]
    internal class __DbConnectionStringBuilder
    {
        public string ConnectionString
        {
            get
            {
                return InternalGetConnectionString();
            }
        }

        protected virtual string InternalGetConnectionString()
        {
            return null;
        }
    }
}
