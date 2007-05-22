using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.Runtime
{
    [Script, System.Obsolete("Should not be used.", true)]
    public class AsyncArgs<TItem, TSource>
        where TItem : class
        where TSource : class
    {
        public TItem Item;
        public TSource Source;

        public AsyncArgs(TSource s)
        {
            Source = s;
        }
    }
}
