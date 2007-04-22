using ScriptCoreLib;

namespace ScriptCoreLib.Shared
{
    [Script]
    public interface IAssemblyInfo
    {
        string BuildDateTimeString { get; }
        string ModuleName { get; }
    }
}
