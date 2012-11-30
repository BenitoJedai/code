using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestPropertyAttributes
{
    public class Class1
    {
        [Browsable(false)]
        public object foo { get; set; }
    }
}
