using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCachingIdentityFactory
{
    public class CachingFactoryBase<T>
    {
    }

    public class CachingIdentityFactory<TKey, TValue> : CachingFactoryBase<CachingIdentityFactory<TKey, TValue>.Entry> where TKey : class
    {
        public struct Entry
        {
            public TKey key;
            public TValue value;
        }
    }
}
