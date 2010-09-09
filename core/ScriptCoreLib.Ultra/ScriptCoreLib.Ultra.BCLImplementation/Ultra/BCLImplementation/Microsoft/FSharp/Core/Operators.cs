using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.BCLImplementation.System;

namespace ScriptCoreLib.Ultra.BCLImplementation.Microsoft.FSharp.Core
{
    [Script(ImplementsViaAssemblyQualifiedName = @"Microsoft.FSharp.Core.Operators, FSharp.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    internal class __Operators
    {
        public static T1 Fst<T1, T2>(__Tuple<T1, T2> tuple)
        {
            return tuple.Item1;
        }


        public static T2 Snd<T1, T2>(__Tuple<T1, T2> tuple)
        {
            return tuple.Item2;
        }

        public static T Raise<T>(Exception exn)
        {
            throw exn;
        }

        public static T FailWith<T>(string message)
        {
            throw new Exception(message);
        }

 

 


 

    }
}
