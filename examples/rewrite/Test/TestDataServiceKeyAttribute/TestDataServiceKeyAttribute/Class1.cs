using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;

namespace TestDataServiceKeyAttribute
{
    [DataServiceKeyAttribute("foo", "bar")]
    public class Class1
    {
    }
}
