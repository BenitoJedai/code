using ScriptCoreLib;

namespace ScriptCoreLib.Shared.Serialized
{
    [Script]
    public interface IObjectStreamHelper<T>
    {
        string Stream { get; set; }
        T Item { get; set; }
    }

    [Script]
    public class DualNotation<TType>
    {
        public string Stream;
        public bool IsBase64;
        public TType Target;
    }
}
