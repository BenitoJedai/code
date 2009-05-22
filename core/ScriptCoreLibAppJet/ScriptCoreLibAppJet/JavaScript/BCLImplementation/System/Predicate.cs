using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibAppJet.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Predicate<>))]
    internal delegate bool __Predicate<T>(T e);
}
