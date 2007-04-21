using ScriptCoreLib;



namespace ScriptCoreLib.JavaScript.Silverlight
{

    [Script(HasNoPrototype = true)]
    public abstract class DependencyObject
    {
        public T GetValue<T>(string propertyName)
        {
            return default(T);
        }

        public T SetValue<T>(string propertyName, T value)
        {
            return default(T);
        }
    }
}
