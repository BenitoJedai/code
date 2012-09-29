using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestIEquatable
{
    public struct ReadOnlyArray<T> : IEquatable<ReadOnlyArray<T>>
    {
        public bool Equals(ReadOnlyArray<T> other)
        {
            throw null;
        }
    }
}
