using System;

using ScriptCoreLib;
//using ScriptCoreLib.LanguageProviders;




//[assembly:
//  ScriptLanguageProvider(typeof(PHP5Provider), "*.source.php", "*.source.shared"),
//  ScriptLanguageProvider(typeof(PHP4Provider), "*.source.php", "*.source.shared"),
//  ScriptLanguageProvider(typeof(ActionScript2Provider), "*.source.as", "*.source.shared"),
//  ScriptLanguageProvider(typeof(ActionScript3Provider), "*.source.as", "*.source.shared"),
//  ScriptLanguageProvider(typeof(Java4Provider), "java"),
//  ScriptLanguageProvider(typeof(object), "java")
//]



namespace ScriptCoreLib
{

    public abstract class LanguageProvider 
    {

    }



    /// <summary>
    /// this attribut tells the compiler in what namespaces the classes marked with ScriptAttribute are eligible for cross compilation
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
    public sealed class ScriptLanguageProviderAttribute : Attribute
    {
        public readonly Type Provider;
        public readonly string[] NamespaceFilter;

        public ScriptLanguageProviderAttribute(Type Provider, params string[] NamespaceFilter)
        {
            this.Provider = Provider;
            this.NamespaceFilter = NamespaceFilter;
        }


    }

    
}
