using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/nullable.cs

    [Script(Implements = typeof(global::System.Nullable<>))]
    internal class __Nullable<T>
    {
        public __Nullable()
            : this(false)
        {
        }

        public __Nullable(T value)
            : this(true, value)
        {

        }

        public __Nullable(bool HasValue, T value = default(T))
        {
            this.HasValue = HasValue;

            if (HasValue)
                this.InternalValue = value;
        }

        public T InternalValue;
        public T Value
        {
            get
            {
                return InternalValue;
            }
        }

        public bool HasValue { get; set; }
    }
}
