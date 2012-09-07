using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestBooleanLogic
{
    public static class Class1
    {
        static bool foo(bool moveState_front, bool moveState_Backwards, bool moveState_left, bool moveState_right)
        {
            var direction = 0;

            if (moveState_front && !moveState_left && !moveState_Backwards && !moveState_right) { direction += 0; }

            return moveState_front || moveState_Backwards || moveState_left || moveState_right;
        }
    }
}
