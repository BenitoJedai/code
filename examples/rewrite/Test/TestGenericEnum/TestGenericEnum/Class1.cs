using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGenericEnum
{
    public class CommonAttributeData
    {

    }

    public class CustomAttributesBag<T> where T : CommonAttributeData
    {
        internal enum CustomAttributeBagCompletionPart : byte  //<T> 
        {
            All,
            None
        }

        class foo
        { 
        
        }

        class foo<F>
        {
            public T t;
            public F f;

        }
    }
}
