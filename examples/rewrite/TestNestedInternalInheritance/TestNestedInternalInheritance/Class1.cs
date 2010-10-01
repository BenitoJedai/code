using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestNestedInternalInheritance
{
    internal class InternalToType_
    {
        public interface InternalToTypeContext
        {
        }
    }

    internal class InternalToType_Consumer
    {
        [Obfuscation(Feature = "invalidmerge")]
        public class InternalToTypeContext : InternalToType_.InternalToTypeContext
        {
        }
    }
}
