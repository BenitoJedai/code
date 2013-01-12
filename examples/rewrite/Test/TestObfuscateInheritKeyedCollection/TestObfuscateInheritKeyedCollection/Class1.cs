using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TestObfuscateInheritKeyedCollection
{
    public class Class1 : KeyedCollection<string, string>
    {
        public string ForgetMyName(string item)
        {
            return GetKeyForItem(item);
        }

        protected override string GetKeyForItem(string item)
        {
            throw new NotImplementedException();
        }
    }
}
