using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace com.abstractatech.multimouse
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //................................................................09dc:01:01 RewriteToAssembly error: System.ArgumentNullException: Value cannot be null.
            //Parameter name: key
            //   at System.Collections.Generic.Dictionary`2.FindEntry(TKey key)
            //   at System.Collections.Generic.Dictionary`2.ContainsKey(TKey key)
            //   at jsc.Library.VirtualDictionary`2.get_Item(TKey k) in x:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Library\VirtualDictionary.cs:line 91
            //   at jsc.meta.Library.ILStringConversions.<>c__DisplayClassc0.<>c__DisplayClassd4.<Prepare>b__6e()
            //   at jsc.meta.Library.ILGeneratorExtensions.EmitWithParameter(ILGenerator il, Int32 ParameterPosition, Action handler)
            //   at jsc.meta.Library.ILStringConversions.<>c__DisplayClassc0.<Prepare>b__6d(Type CacheType, ILStringConversionArguments e)
            //   at jsc.meta.Library.ILStringConversions.ILStringConversion.<.ctor>b__de(ILStringConversionArguments e)

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
