using ScriptCoreLib;

namespace ScriptCoreLib.Shared.Serialized
{
    [Script]
    [System.Obsolete]
    public interface IObjectStreamHelper<T>
    {
        string Stream { get; set; }
        T Item { get; set; }
    }

    [Script]
    [System.Obsolete]
    public class DualNotation<TType>
    {
        public string Stream;
        public bool IsBase64;
        public TType Target;
    }
}
