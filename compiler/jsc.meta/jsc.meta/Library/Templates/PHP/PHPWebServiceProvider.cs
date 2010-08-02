using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.PHP;
using ScriptCoreLib;

namespace jsc.meta.Library.Templates.PHP
{
    internal static class PHPWebServiceProvider
    {
        // this class is a template
        // this class cannot be used in .net
        // this could be defined in ScriptCoreLib.Ultra

        /// <summary>
        /// This method is to be called in the index.php
        /// </summary>
        [Script(NoDecoration = true)]
        internal static void PHPWebServiceProvider_Serve()
        {
            Native.echo("powered by jsc");
        }
    }
}
