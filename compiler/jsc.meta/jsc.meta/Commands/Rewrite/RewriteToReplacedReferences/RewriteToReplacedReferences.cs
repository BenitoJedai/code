using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.IDL;
using ScriptCoreLib.Ultra.IL;

namespace jsc.meta.Commands.Rewrite.RewriteToReplacedReferences
{
    public partial class RewriteToReplacedReferences : CommandBase
    {
        /*
         * JSC is using Reflection.Emit at the moment to rewrite assemblies. Yet when it comes
         * to actually retargeting to different CLR versions we get into trouble.
         * 
         * What we want to do:
         * 
         * CLR 4.0 to CLR 2.0
         * CLR 4.0 to CLR 2.0.5 (silverlight)
         * 
         * Once implemented this tool here will allow us to emit silverlight assemblies.
         * 
         * For now we can try using idasm and ilasm.
         * 
         * We could actually do more and export native methods...
         * Those native exported methods could be used by java runtime.
         * 
         * Note that some types move between assemblies between
         * CLR versions like System.Func.
         */

        public override void Invoke()
        {
            if (AttachDebugger)
                Debugger.Launch();

            if (this.Output == null)
                this.Output = this.Assembly;

            if (this.DefaultToOrcas)
            {
                this.References = this.References.Concat(this.ReferencesForOrcas).ToArray();
                this.ilasm = this.ilasm20;
            }

            var staging = Path.ChangeExtension(this.Output.FullName, ".staging");

            Directory.CreateDirectory(staging);

            var il = Path.Combine(staging, Path.ChangeExtension(this.Output.Name, ".il"));

            Console.WriteLine(il);

            // http://msdn.microsoft.com/en-us/library/f7dy01k1(VS.80).aspx
            Process.Start(
                new ProcessStartInfo(
                    this.ildasm.FullName,
                    this.Assembly.FullName +
                    @" /utf8 /forward /QUOTEALLNAMES /OUT=""" + il + @""""
                )
                {
                    UseShellExecute = true,
                    CreateNoWindow = true
                }
            ).WaitForExit();


            IDLParserToken il_source = File.ReadAllText(il);

            var a = il_source.ToAssembly();

            foreach (var item in this.References)
            {
                a.AssemblyExternList.Where(k => k.Name.Text == item.Name).WithEach(
                     k =>
                     {
                         k.Version.Text = item.Version;
                         k.PublicKeyToken.Text = item.PublicKeyToken;
                     }
                );
            }


            File.WriteAllText(il, il_source.GetString());

            Process.Start(
                this.ilasm.FullName,
                @"""" + il + @"""" +
                @" /DLL /OUTPUT=""" + this.Output.FullName + @""""
            ).WaitForExit();

            Process.Start(
                new ProcessStartInfo(
                    this.PEVerify.FullName,
                    @" /NOLOGO """ + this.Output.FullName + @""""
                )
                {
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            ).WaitForExit();

        }

        /*
         
         */
    }
}
