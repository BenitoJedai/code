using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System.Linq;
using System;
using ScriptCoreLib.Shared.Query;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    
    
    internal static partial class __Enumerable
    {
//        static void NoOptimization() { }



        
        //[Script(NotImplementedHere = true)]
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            // !! AsEnumerable is defined in another language selection in another assembly
            // the runtime should create a SZArray for arrays but it does not for the moment

            // NoOptimization();

            return InternalSequence.AsEnumerable(source);
        }



 


 


    }
}
