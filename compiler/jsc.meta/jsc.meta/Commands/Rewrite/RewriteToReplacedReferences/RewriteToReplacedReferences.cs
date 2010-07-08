using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

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
         */

        public override void Invoke()
        {
            var il = this.Assembly.FullName + ".il";

            Process.Start(
                this.ildasm.FullName,
                this.Assembly.FullName +
                @" /OUT=""" + il + @".il"""
            );

            // update the IL manually for now
            Debugger.Break();

            Process.Start(
                this.ilasm.FullName,
                @"""" + il + @"""" +
                @" /DLL /OUTPUT=""" + il + @".dll"""
            );
        }

        /*
         
         */
    }
}
