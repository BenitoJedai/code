using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Reflection;

[assembly: Obfuscation(Feature = "script")]
namespace TestImplementsViaAQN.BCL
{
    [Script(ImplementsViaAssemblyQualifiedName = "System.Windows.Input.TouchEventArgs, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
    internal class __TouchEventArgs 
    {

    }

    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T>
    {

    }

    public interface AssemblyReferenceToken
    {

    }
}
