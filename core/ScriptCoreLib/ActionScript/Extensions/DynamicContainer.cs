using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.Extensions
{
    [Script]
    public class DynamicContainer
    {
        public object Subject;

        [Script(OptimizedCode = "return subject[key];")]
        public static object GetValue(object subject, object key)
        {
            return default(object);
        }

        [Script(OptimizedCode = "subject[key] = value;")]
        public static void SetValue(object subject, object key, object value)
        {
        }

        public object this[object Key]
        {
            get
            {
                return GetValue(Subject, Key);
            }
            set
            {
                SetValue(Subject, Key, value);
            }
        }
    }
}
