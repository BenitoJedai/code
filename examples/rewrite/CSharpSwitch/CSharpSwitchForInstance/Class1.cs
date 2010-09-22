using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpSwitchForInstance
{
    public class Class1
    {
        public string Bar(int caseSwitch2)
        {
            switch (caseSwitch2)
            {
                case 1:
                    return ("Case 1");
                case 2:
                    return ("Case 2");
                default:
                    return ("Default case");
            }
        }
    }
}
