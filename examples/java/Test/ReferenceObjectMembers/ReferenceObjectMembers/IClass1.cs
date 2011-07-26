using System;
using ScriptCoreLib;
namespace ReferenceObjectMembers
{
    [Script]
    interface IClass1
    {
        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}
