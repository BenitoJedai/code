using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestVirtualPropertyMerge
{
    public class Class1
    {
        public event Action Handler;


        public int MyProperty { get; set; }
        public virtual int MyVirtualProperty { get; set; }

        public void Extension1()
        { }

    }


    public class Class2 : Class1
    {
        public override int MyVirtualProperty { get; set; }

        public void Extension2()
        { }

    }
}
