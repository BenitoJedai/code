using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Common;
using System.Linq;
using System.Text;

namespace TestDataServiceKeyAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class XDataServiceKeyAttribute : Attribute
    {
        public XDataServiceKeyAttribute(params string[] keyNames)
        {
            this.KeyNames = new ReadOnlyCollection<string>(keyNames);
        }

        public ReadOnlyCollection<string> KeyNames { get; private set; }
    }

    [XDataServiceKeyAttribute("foo", "bar")]
    [DataServiceKeyAttribute("foo", "bar")]
    public class Class1
    {
    }
}
