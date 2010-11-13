using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestInterfaceSatisfaction
{

    interface ParentInterface
    {
        bool enabled { get; set; }
    }

    interface ChildInterface : ParentInterface
    {
        // inherits enabled
    }

    class ParentClass
    {
        public bool enabled { get; set; }
    }

    class ChildClass : ParentClass, ChildInterface
    {

        // forced to implement "enabled" due to ParentInterface
        // BUT inherits "enabled" from ParentClass so don't need to do anything
        // Compiler likely links ParentClass.enabled{get;set;}  with ParentInterface.enabled{get;set;}
        // BUT reflection does not let do this with DefineOverride, throwing reason:
        //     that enabled{get/set} must come from ChildClass not from ParentClass

        // so the only solution I see here is to take the ParentClass and duplicate such method in ChildClass
        // perhaps compiler already does this automatic duplication??
        // Unless you've seen this before and have a way around it?

    }
}
