using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;

namespace TestSkip
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var a = new[] { 5, 6, 7, 8, 9 };


            foreach (var item in __Enumerable.Skip(a, 2))
            {
                System.Console.WriteLine(new { item });
            }

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
