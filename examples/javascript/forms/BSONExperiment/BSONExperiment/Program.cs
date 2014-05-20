using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ScriptCoreLib.Desktop.Forms.Extensions;
using System;
using System.Security;

//[assembly: SecurityRules(SecurityRuleSet.Level1)]
//[assembly: SecurityRules(SecurityRuleSet.Level2, SkipVerificationInFullTrust = true)]

namespace BSONExperiment
{
    /// <summary>
    /// You can debug your application by hitting F5.
    /// </summary>
    internal static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            // http://stackoverflow.com/questions/20252500/attempt-by-security-transparent-method-to-access-security-critical-method-failed

            //Additional information: Attempt by security transparent method 'BSONExperiment.Program.Main(System.String[])' to access security critical method 'ScriptCoreLib.Desktop.Forms.Extensions.DesktopFormsExtensions.Launch<BSONExperiment.ApplicationControl>(System.Func`1<BSONExperiment.ApplicationControl>, System.Action`1<WindowInfo`1<BSONExperiment.ApplicationControl>>)' failed.

            // PORTABLE
            // X:\opensource\github\Newtonsoft.Json\Src\Newtonsoft.Json\Properties\AssemblyInfo.cs


            //Assembly 'BSONExperiment, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null' 
            // is marked with the AllowPartiallyTrustedCallersAttribute, 
            // and uses the level 2 security transparency model.  
            // Level 2 transparency causes all methods in AllowPartiallyTrustedCallers 
            // assemblies to become security transparent by default, 
            // which may be the cause of this exception.

            DesktopFormsExtensions.Launch(
                () => new ApplicationControl()
            );
#else
            // at jsc.meta.Commands.Rewrite.RewriteToAssembly.<>c__DisplayClass11c.<>c__DisplayClass12b.<WriteSwitchRewrite>b__cf(ILGenerator flow_il) in x:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs:line 800

            RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
#endif
        }

    }
}
