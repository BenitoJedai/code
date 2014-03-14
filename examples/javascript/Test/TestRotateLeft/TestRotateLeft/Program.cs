using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using System;

namespace TestRotateLeft
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Error	2	Operator '>>' cannot be applied to operands of type 'string[]' and 'int'	X:\jsc.svn\examples\javascript\test\TestRotateLeft\TestRotateLeft\Program.cs	13	21	TestRotateLeft
            //var x = args >> 7;

            Application.RotateLeft(3614090487, 7);

            // 
            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
        }

    }
}
