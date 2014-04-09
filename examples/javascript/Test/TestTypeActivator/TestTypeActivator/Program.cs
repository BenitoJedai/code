using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestTypeActivator
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\javascript\forms\test\TestTypeActivatorRef\TestTypeActivatorRef\Class1.cs

            // No parameterless constructor defined for this object.

            Activator.CreateInstance(
             typeof(Foo)
         );


            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
