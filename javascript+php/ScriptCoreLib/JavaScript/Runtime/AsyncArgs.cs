using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.JavaScript.Runtime
{
    [Script]
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
